using System.Diagnostics;
using System.Windows.Forms.VisualStyles;
using FontAwesome.Sharp;
using Jellyfin.Sdk;
using Jellyfin.Sdk.Generated.Models;
using JellyfinMPCShim.Interfaces;
using JellyfinMPCShim.Models;
using JellyfinMPCShim.Models.WebsocketData;
using JellyfinMPCShim.Tray.Properties;
using Microsoft.Extensions.Hosting;

namespace JellyfinMPCShim.Tray;
public partial class MainForm : Form, IJellyfinMessageHandler
{
    private readonly IHostApplicationLifetime _hostlLifetime;
    private readonly IJellyfinClient _jellyfinClient;
    private readonly IMpcClient _mpcClient;
    private bool _visible;
    private Guid? _currentGroup;

    public MainForm(IHostApplicationLifetime hostlLifetime, IJellyfinClient jellyfinClient, IMpcClient mpcClient)
    {
        _hostlLifetime = hostlLifetime;
        _jellyfinClient = jellyfinClient;
        _jellyfinClient.AddMessageHandler(this);
        _mpcClient = mpcClient;
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
            MessageBox.Show($"Error connection to Jellyfin\r\r{ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

    private async void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        await _jellyfinClient.Stop();    }

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

    private void logsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        path = Path.Combine(path, "JellyfinMPCShim.Tray", "logs");
        Process.Start("explorer.exe", path);
    }

    private async void trayMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
        groupsToolStripMenuItem.DropDownItems.Clear();
        if (!_jellyfinClient.IsConnected)
        {
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
        //Shutdown application when Jellfin connection is stopped
        _hostlLifetime.StopApplication();
        return Task.CompletedTask;
    }

    private void buttonSelectMpc_Click(object sender, EventArgs e)
    {
        using var ofd = new OpenFileDialog();
        ofd.CheckFileExists = true;
        ofd.FileName = textBoxMpcPath.Text;
        ofd.InitialDirectory = !string.IsNullOrWhiteSpace(textBoxMpcPath.Text) ? Path.GetDirectoryName(textBoxMpcPath.Text) : null;
        ofd.Filter = "exe|*.exe";
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            textBoxMpcPath.Text = ofd.FileName;
        }
    }
}
