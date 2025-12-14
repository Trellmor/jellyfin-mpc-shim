using System.Diagnostics;
using FontAwesome.Sharp;
using Jellyfin.Sdk.Generated.Models;
using JellyfinMPCShim.Interfaces;
using JellyfinMPCShim.Models;
using JellyfinMPCShim.Models.WebsocketData;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace JellyfinMPCShim.Tray;

public partial class MainForm : Form, IJellyfinMessageHandler
{
    private readonly IHostApplicationLifetime _hostlLifetime;
    private readonly IJellyfinClient _jellyfinClient;
    private readonly IMpcClient _mpcClient;
    private readonly ILogger<MainForm>? _logger;
    private bool _visible;
    private Guid? _currentGroup;

    public MainForm(IHostApplicationLifetime hostlLifetime, IJellyfinClient jellyfinClient, IMpcClient mpcClient,
        ILogger<MainForm>? logger = null)
    {
        _hostlLifetime = hostlLifetime;
        _jellyfinClient = jellyfinClient;
        _jellyfinClient.AddMessageHandler(this);
        _mpcClient = mpcClient;
        _logger = logger;
        InitializeComponent();
        connectionToolStripMenuItem.Image = IconChar.Cloud.ToBitmap(width: 16, height: 16, color: Color.Black);
        settingsToolStripMenuItem.Image = IconChar.Gear.ToBitmap(width: 16, height: 16, color: Color.Black);
        groupsToolStripMenuItem.Image = IconChar.UserGroup.ToBitmap(width: 16, height: 16, color: Color.Black);
        logsToolStripMenuItem.Image = IconChar.FileLines.ToBitmap(width: 16, height: 16, color: Color.Black);
        exitToolStripMenuItem.Image = IconChar.Close.ToBitmap(width: 16, height: 16, color: Color.Black);
    }

    private async void MainForm_Load(object sender, EventArgs e)
    {
        Visible = false;
        ShowInTaskbar = false;

        // Load settings from the new JSON-based settings system
        var settings = UserSettings.Instance;
        textBoxJellyfinHost.Text = settings.JellyfinHost;
        textBoxJellyfinUsername.Text = settings.JellyfinUser;
        textBoxJellyfinPassword.Text = settings.JellyfinPw;
        textBoxMpcPath.Text = settings.MpcPath;
        numericUpDownMpcPort.Value = settings.MpcPort;

        _logger?.LogInformation("Settings loaded. DeviceId: {DeviceId}", settings.JellyfinDeviceId);

        // Validate required settings
        if (!ValidateSettings(out var errorMessage))
        {
            ShowForm();
            return;
        }

        try
        {
            await ConnectJellyfin();
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Failed to connect to Jellyfin on startup");
            ShowForm();
            ShowConnectionError(ex);
            return;
        }

        try
        {
            await _mpcClient.Start(settings.MpcPath, settings.MpcPort);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Failed to start MPC client");
            // Don't block - MPC will be started when needed
        }
    }

    /// <summary>
    /// Validates all required settings
    /// </summary>
    private bool ValidateSettings(out string? errorMessage)
    {
        var settings = UserSettings.Instance;
        errorMessage = null;

        if (string.IsNullOrWhiteSpace(settings.JellyfinHost))
        {
            errorMessage = "Jellyfin server address is required";
            return false;
        }

        if (!UserSettings.ValidateJellyfinHost(settings.JellyfinHost, out _, out var urlError))
        {
            errorMessage = urlError;
            return false;
        }

        if (string.IsNullOrWhiteSpace(settings.JellyfinUser))
        {
            errorMessage = "Jellyfin username is required";
            return false;
        }

        if (string.IsNullOrWhiteSpace(settings.JellyfinPw))
        {
            errorMessage = "Jellyfin password is required";
            return false;
        }

        if (string.IsNullOrWhiteSpace(settings.MpcPath))
        {
            errorMessage = "MPC executable path is required";
            return false;
        }

        if (!File.Exists(settings.MpcPath))
        {
            errorMessage = $"MPC executable not found: {settings.MpcPath}";
            return false;
        }

        return true;
    }

    private void ShowForm()
    {
        _visible = true;
        Visible = true;
        ShowInTaskbar = true;
        Show();
        BringToFront();
    }

    private async Task ConnectJellyfin()
    {
        var settings = UserSettings.Instance;

        // Normalize the URL
        if (!UserSettings.ValidateJellyfinHost(settings.JellyfinHost, out var normalizedUrl, out var error))
        {
            throw new InvalidOperationException(error ?? "Invalid Jellyfin URL");
        }

        _logger?.LogInformation("Connecting to Jellyfin at {Host}", normalizedUrl);

        try
        {
            await _jellyfinClient.Start(normalizedUrl, settings.JellyfinUser, settings.JellyfinPw);
            connectionToolStripMenuItem.Text = $"Connected to {normalizedUrl}";
            connectionToolStripMenuItem.Visible = true;
            _logger?.LogInformation("Successfully connected to Jellyfin");

            // Update the tray icon tooltip
            trayIcon.Text = $"Jellyfin MPC Shim - Connected";
        }
        catch (Exception ex)
        {
            connectionToolStripMenuItem.Visible = false;
            trayIcon.Text = "Jellyfin MPC Shim - Disconnected";
            _logger?.LogError(ex, "Failed to connect to Jellyfin at {Host}", normalizedUrl);
            throw;
        }
    }

    private void ShowConnectionError(Exception ex)
    {
        var message = ex.Message;

        // Provide more helpful error messages
        if (ex.InnerException is HttpRequestException httpEx)
        {
            if (httpEx.Message.Contains("No such host"))
            {
                message = "Cannot reach Jellyfin server. Check the server address and ensure Jellyfin is running.";
            }
            else if (httpEx.Message.Contains("Connection refused"))
            {
                message = "Connection refused. Make sure Jellyfin is running and the port is correct.";
            }
            else if (httpEx.Message.Contains("401") || httpEx.Message.Contains("Unauthorized"))
            {
                message = "Authentication failed. Check your username and password.";
            }
        }
        else if (ex.Message.Contains("401") || ex.Message.Contains("Unauthorized"))
        {
            message = "Authentication failed. Check your username and password.";
        }

        MessageBox.Show(
            $"Error connecting to Jellyfin:\n\n{message}\n\nTips:\n- Ensure URL format is http://ip:port (e.g., http://192.168.1.100:8096)\n- Verify Jellyfin is running\n- Check firewall settings",
            "Connection Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }

    private void trayIcon_DoubleClick(object sender, EventArgs e)
    {
        if (_visible)
        {
            HideForm();
        }
        else
        {
            ShowForm();
        }
    }

    private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ShowForm();
    }

    private async void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        await _jellyfinClient.Stop();
        _hostlLifetime.StopApplication();
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (e.CloseReason == CloseReason.UserClosing)
        {
            HideForm();
            e.Cancel = true;
        }
    }

    private void HideForm()
    {
        _visible = false;
        ShowInTaskbar = false;
        Hide();
    }

    private async void buttonSave_Click(object sender, EventArgs e)
    {
        // Validate inputs before saving
        var hostInput = textBoxJellyfinHost.Text.Trim();

        if (!UserSettings.ValidateJellyfinHost(hostInput, out var normalizedUrl, out var urlError))
        {
            MessageBox.Show(urlError ?? "Invalid server URL", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBoxJellyfinHost.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(textBoxJellyfinUsername.Text))
        {
            MessageBox.Show("Username is required", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBoxJellyfinUsername.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(textBoxJellyfinPassword.Text))
        {
            MessageBox.Show("Password is required", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBoxJellyfinPassword.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(textBoxMpcPath.Text) || !File.Exists(textBoxMpcPath.Text))
        {
            MessageBox.Show("Please select a valid MPC executable", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBoxMpcPath.Focus();
            return;
        }

        // Save to the new JSON settings system
        var settings = UserSettings.Instance;
        settings.JellyfinHost = normalizedUrl; // Save normalized URL
        settings.JellyfinUser = textBoxJellyfinUsername.Text;
        settings.JellyfinPw = textBoxJellyfinPassword.Text;
        settings.MpcPath = textBoxMpcPath.Text;
        settings.MpcPort = (int)numericUpDownMpcPort.Value;

        try
        {
            settings.Save();
            _logger?.LogInformation("Settings saved successfully");
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Failed to save settings");
            MessageBox.Show($"Failed to save settings: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // Update the text box with normalized URL
        textBoxJellyfinHost.Text = normalizedUrl;

        // Reconnect with new settings
        try
        {
            buttonSave.Enabled = false;
            Cursor = Cursors.WaitCursor;

            await _jellyfinClient.Stop();
            await ConnectJellyfin();
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Failed to connect after saving settings");
            ShowConnectionError(ex);
            return;
        }
        finally
        {
            buttonSave.Enabled = true;
            Cursor = Cursors.Default;
        }

        try
        {
            await _mpcClient.Start(settings.MpcPath, settings.MpcPort);
        }
        catch (Exception ex)
        {
            _logger?.LogWarning(ex, "MPC connection failed - will retry when casting");
            // Don't show error - MPC will be started when needed
        }

        MessageBox.Show(
            "Settings saved successfully!\n\nThe device should now appear in Jellyfin as 'Jellyfin MPC Shim Tray'.\n\nYou can cast to it from Jellyfin Web or any Jellyfin client.",
            "Success",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);

        Close();
    }

    private void logsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        path = Path.Combine(path, "JellyfinMPCShim.Tray", "logs");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        Process.Start("explorer.exe", path);
    }

    private async void trayMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
        groupsToolStripMenuItem.DropDownItems.Clear();
        if (!_jellyfinClient.IsConnected)
        {
            connectionToolStripMenuItem.Text = "Not connected";
            connectionToolStripMenuItem.Visible = true;
            return;
        }

        var groups = await _jellyfinClient.SyncPlayGetGroups();
        if (groups == null)
        {
            return;
        }
        foreach (var group in groups)
        {
            var item = new ToolStripMenuItem
            {
                Text = group.GroupName,
                Tag = group,
                Checked = group.GroupId == _currentGroup
            };
            item.Click += SyncPlayGroutItemOnClick;
            groupsToolStripMenuItem.DropDownItems.Add(item);
        }
    }

    private void SyncPlayGroutItemOnClick(object? sender, EventArgs e)
    {
        if (_currentGroup != null)
        {
            _jellyfinClient.SyncPlayLeaveGroup();
        }

        if (sender is ToolStripMenuItem item && item.Tag is GroupInfoDto group)
        {
            if (!item.Checked && group.GroupId != null)
            {
                _jellyfinClient.SyncPlayJoinGroup(group.GroupId.Value);
            }
            item.Checked = !item.Checked;
        }
    }

    public Task HandlePlay(JellyfinWebsockeMessage<PlayRequest> message)
    {
        return Task.CompletedTask;
    }

    public Task HandlePlayState(JellyfinWebsockeMessage<PlaystateRequest> message)
    {
        return Task.CompletedTask;
    }

    public Task HandleSyncGroupUpdate(JellyfinWebsockeMessage<GroupUpdate> syncPlayGroupUpdateMessage)
    {
        switch (syncPlayGroupUpdateMessage.Data.Type)
        {
            case GroupUpdateType.UserJoined:
                break;
            case GroupUpdateType.UserLeft:
                break;
            case GroupUpdateType.GroupJoined:
                _currentGroup = syncPlayGroupUpdateMessage.Data.GroupId;
                foreach (ToolStripMenuItem item in groupsToolStripMenuItem.DropDownItems)
                {
                    if (item.Tag is GroupInfoDto group)
                    {
                        item.Checked = group.GroupId == _currentGroup;
                    }
                }
                break;
            case GroupUpdateType.GroupLeft:
            case GroupUpdateType.NotInGroup:
            case GroupUpdateType.GroupDoesNotExist:
                _currentGroup = null;
                foreach (ToolStripMenuItem item in groupsToolStripMenuItem.DropDownItems)
                {
                    item.Checked = false;
                }
                break;
            case GroupUpdateType.StateUpdate:
                break;
            case GroupUpdateType.PlayQueue:
                break;
            case GroupUpdateType.CreateGroupDenied:
                break;
            case GroupUpdateType.JoinGroupDenied:
                break;
            case GroupUpdateType.LibraryAccessDenied:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        return Task.CompletedTask;
    }

    public Task HandleSyncPlayCommand(JellyfinWebsockeMessage<SendCommand> syncPlayCommandMessage)
    {
        return Task.CompletedTask;
    }

    public Task HandleStop()
    {
        // Don't automatically shutdown - only cleanup
        // The Exit menu item will handle explicit shutdown
        _logger?.LogDebug("HandleStop called - connection closed");
        return Task.CompletedTask;
    }

    private void buttonSelectMpc_Click(object sender, EventArgs e)
    {
        using var ofd = new OpenFileDialog();
        ofd.Title = "Select MPC-HC or MPC-BE executable";
        ofd.CheckFileExists = true;
        ofd.FileName = textBoxMpcPath.Text;

        // Try to set initial directory
        if (!string.IsNullOrWhiteSpace(textBoxMpcPath.Text) && Directory.Exists(Path.GetDirectoryName(textBoxMpcPath.Text)))
        {
            ofd.InitialDirectory = Path.GetDirectoryName(textBoxMpcPath.Text);
        }
        else
        {
            // Try common MPC installation paths
            var commonPaths = new[]
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "MPC-HC"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "MPC-HC"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "MPC-BE"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "MPC-BE"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "K-Lite Codec Pack", "MPC-HC64"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "K-Lite Codec Pack", "MPC-HC")
            };

            foreach (var path in commonPaths)
            {
                if (Directory.Exists(path))
                {
                    ofd.InitialDirectory = path;
                    break;
                }
            }
        }

        ofd.Filter = "MPC Executable|mpc-hc.exe;mpc-hc64.exe;mpc-be.exe;mpc-be64.exe|All Executables|*.exe";

        if (ofd.ShowDialog() == DialogResult.OK)
        {
            textBoxMpcPath.Text = ofd.FileName;
        }
    }
}
