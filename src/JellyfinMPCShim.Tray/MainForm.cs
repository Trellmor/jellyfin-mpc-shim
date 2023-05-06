using JellyfinMPCShim.Interfaces;
using JellyfinMPCShim.Tray.Properties;
using Microsoft.Extensions.Hosting;

namespace JellyfinMPCShim.Tray;
public partial class MainForm : Form
{
    private readonly IHostApplicationLifetime _hostlLifetime;
    private readonly IJellyfinClient _jellyfinClient;
    private readonly IMpcClient _mpcClient;
    private bool _visible;

    public MainForm(IHostApplicationLifetime hostlLifetime, IJellyfinClient jellyfinClient, IMpcClient mpcClient)
    {
        _hostlLifetime = hostlLifetime;
        _jellyfinClient = jellyfinClient;
        _mpcClient = mpcClient;
        InitializeComponent();
    }

    private async void MainForm_Load(object sender, EventArgs e)
    {
        Visible = false;
        ShowInTaskbar = false;

        textBoxJellyfinHost.Text = Settings.Default.JellyfinHost;
        textBoxJellyfinUsername.Text = Settings.Default.JellyfinUser;
        textBoxJellyfinPassword.Text = Settings.Default.JellyfinPw;
        textBoxMpcPath.Text = Settings.Default.MpcPath;
        numericUpDownMpcPort.Value = Settings.Default.MpcPort;

        if (string.IsNullOrWhiteSpace(Settings.Default.JellyfinHost) ||
            string.IsNullOrWhiteSpace(Settings.Default.JellyfinUser) ||
            string.IsNullOrWhiteSpace(Settings.Default.JellyfinPw) ||
            string.IsNullOrWhiteSpace(Settings.Default.MpcPath) ||
            !File.Exists(Settings.Default.MpcPath))
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
            ShowForm();
            MessageBox.Show($"Error connectiong to jellyfin\r\r{ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        await _mpcClient.Start(Settings.Default.MpcPath, Settings.Default.MpcPort);
    }

    private void ShowForm()
    {
        _visible = true;
        Visible = true;
        ShowInTaskbar = true;
        Show();
    }

    private async Task ConnectJellyfin()
    {
        try
        {
            await _jellyfinClient.Start(Settings.Default.JellyfinHost, Settings.Default.JellyfinUser,
                Settings.Default.JellyfinPw);
            connectionToolStripMenuItem.Text = $"Connected to {Settings.Default.JellyfinHost}";
            connectionToolStripMenuItem.Visible = true;
        }
        catch
        {
            connectionToolStripMenuItem.Visible = false;
            throw;
        }
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

    private async Task exitToolStripMenuItem_Click(object sender, EventArgs e)
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
        Settings.Default.JellyfinHost = textBoxJellyfinHost.Text;
        Settings.Default.JellyfinUser = textBoxJellyfinUsername.Text;
        Settings.Default.JellyfinPw = textBoxJellyfinPassword.Text;
        Settings.Default.MpcPath = textBoxMpcPath.Text;
        Settings.Default.MpcPort = (int)numericUpDownMpcPort.Value;
        Settings.Default.Save();
        try
        {
            await _jellyfinClient.Stop();
            await ConnectJellyfin();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error connectiong to jellyfin\r\r{ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        await _mpcClient.Start(Settings.Default.MpcPath, Settings.Default.MpcPort);
        Close();
    }
}
