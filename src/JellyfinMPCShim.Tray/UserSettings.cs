using System.Text.Json;
using System.Text.Json.Serialization;

namespace JellyfinMPCShim.Tray;

/// <summary>
/// Robust settings manager that stores configuration in a fixed location
/// (%APPDATA%\JellyfinMPCShim\settings.json) to ensure persistence across runs.
/// This fixes the issue where .NET's ApplicationSettingsBase uses path-based hashing.
/// </summary>
public class UserSettings
{
    private static readonly string SettingsFolder = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "JellyfinMPCShim");

    private static readonly string SettingsFile = Path.Combine(SettingsFolder, "settings.json");

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    private static UserSettings? _instance;
    private static readonly object _lock = new();

    public static UserSettings Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    _instance ??= Load();
                }
            }
            return _instance;
        }
    }

    // Settings properties
    public string JellyfinHost { get; set; } = string.Empty;
    public string JellyfinUser { get; set; } = string.Empty;
    public string JellyfinPw { get; set; } = string.Empty;
    public string MpcPath { get; set; } = string.Empty;
    public int MpcPort { get; set; } = 13579;
    public Guid JellyfinDeviceId { get; set; } = Guid.Empty;

    [JsonIgnore]
    public bool IsFirstRun => JellyfinDeviceId == Guid.Empty;

    private UserSettings() { }

    /// <summary>
    /// Load settings from JSON file, or create defaults if not exists
    /// </summary>
    private static UserSettings Load()
    {
        try
        {
            if (File.Exists(SettingsFile))
            {
                var json = File.ReadAllText(SettingsFile);
                var settings = JsonSerializer.Deserialize<UserSettings>(json, JsonOptions);
                if (settings != null)
                {
                    // Ensure device ID is set
                    if (settings.JellyfinDeviceId == Guid.Empty)
                    {
                        settings.JellyfinDeviceId = Guid.NewGuid();
                        settings.Save();
                    }
                    return settings;
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading settings: {ex.Message}");
        }

        // Create new settings with fresh device ID
        var newSettings = new UserSettings
        {
            JellyfinDeviceId = Guid.NewGuid()
        };
        newSettings.Save();
        return newSettings;
    }

    /// <summary>
    /// Save settings to JSON file
    /// </summary>
    public void Save()
    {
        try
        {
            // Ensure directory exists
            Directory.CreateDirectory(SettingsFolder);

            var json = JsonSerializer.Serialize(this, JsonOptions);
            File.WriteAllText(SettingsFile, json);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error saving settings: {ex.Message}");
            throw new InvalidOperationException($"Failed to save settings to {SettingsFile}: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Reload settings from disk
    /// </summary>
    public static void Reload()
    {
        lock (_lock)
        {
            _instance = Load();
        }
    }

    /// <summary>
    /// Migrate settings from old ApplicationSettingsBase if they exist
    /// </summary>
    internal static void MigrateFromLegacy(Properties.Settings legacySettings)
    {
        if (Instance.IsFirstRun && !string.IsNullOrWhiteSpace(legacySettings.JellyfinHost))
        {
            Instance.JellyfinHost = legacySettings.JellyfinHost;
            Instance.JellyfinUser = legacySettings.JellyfinUser;
            Instance.JellyfinPw = legacySettings.JellyfinPw;
            Instance.MpcPath = legacySettings.MpcPath;
            Instance.MpcPort = legacySettings.MpcPort;

            // Keep old device ID if valid
            if (legacySettings.JellyfinDeviceId != Guid.Empty)
            {
                Instance.JellyfinDeviceId = legacySettings.JellyfinDeviceId;
            }

            Instance.Save();
        }
    }

    /// <summary>
    /// Get the settings folder path (useful for opening in explorer)
    /// </summary>
    public static string GetSettingsFolder() => SettingsFolder;

    /// <summary>
    /// Validate the Jellyfin host URL format
    /// </summary>
    public static bool ValidateJellyfinHost(string host, out string normalizedUrl, out string? error)
    {
        normalizedUrl = host;
        error = null;

        if (string.IsNullOrWhiteSpace(host))
        {
            error = "Jellyfin server address is required";
            return false;
        }

        // Add http:// if no protocol specified
        if (!host.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
            !host.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            host = "http://" + host;
        }

        if (!Uri.TryCreate(host, UriKind.Absolute, out var uri))
        {
            error = "Invalid server URL format. Use: http://ip:port or https://ip:port";
            return false;
        }

        // Ensure port is included
        if (uri.Port == -1 || (uri.Port == 80 && uri.Scheme == "http") || (uri.Port == 443 && uri.Scheme == "https"))
        {
            // Check if user specified port in original string
            if (!host.Contains(':') || host.LastIndexOf(':') == host.IndexOf(':'))
            {
                // Default Jellyfin port
                normalizedUrl = $"{uri.Scheme}://{uri.Host}:8096";
            }
            else
            {
                normalizedUrl = uri.ToString().TrimEnd('/');
            }
        }
        else
        {
            normalizedUrl = $"{uri.Scheme}://{uri.Host}:{uri.Port}";
        }

        return true;
    }
}
