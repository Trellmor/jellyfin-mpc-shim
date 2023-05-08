using FontAwesome.Sharp;
using Icon = System.Drawing.Icon;

namespace JellyfinMPCShim.Tray;

partial class MainForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        var resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        trayIcon = new NotifyIcon(components);
        trayMenu = new ContextMenuStrip(components);
        connectionToolStripMenuItem = new ToolStripMenuItem();
        settingsToolStripMenuItem = new ToolStripMenuItem();
        groupsToolStripMenuItem = new ToolStripMenuItem();
        logsToolStripMenuItem = new ToolStripMenuItem();
        exitToolStripMenuItem = new ToolStripMenuItem();
        groupBoxJellyfin = new GroupBox();
        textBoxJellyfinPassword = new TextBox();
        textBoxJellyfinUsername = new TextBox();
        textBoxJellyfinHost = new TextBox();
        label3 = new Label();
        label2 = new Label();
        label1 = new Label();
        groupBoxMpc = new GroupBox();
        buttonSelectMpc = new IconButton();
        numericUpDownMpcPort = new NumericUpDown();
        textBoxMpcPath = new TextBox();
        label5 = new Label();
        label4 = new Label();
        buttonSave = new IconButton();
        trayMenu.SuspendLayout();
        groupBoxJellyfin.SuspendLayout();
        groupBoxMpc.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numericUpDownMpcPort).BeginInit();
        SuspendLayout();
        // 
        // trayIcon
        // 
        trayIcon.ContextMenuStrip = trayMenu;
        trayIcon.Icon = (Icon)resources.GetObject("trayIcon.Icon");
        trayIcon.Text = "Jellyfin MPC Shim";
        trayIcon.Visible = true;
        trayIcon.DoubleClick += trayIcon_DoubleClick;
        // 
        // trayMenu
        // 
        trayMenu.Items.AddRange(new ToolStripItem[] { connectionToolStripMenuItem, settingsToolStripMenuItem, groupsToolStripMenuItem, logsToolStripMenuItem, exitToolStripMenuItem });
        trayMenu.Name = "trayMenu";
        trayMenu.Size = new Size(181, 137);
        trayMenu.Opening += trayMenu_Opening;
        // 
        // connectionToolStripMenuItem
        // 
        connectionToolStripMenuItem.Name = "connectionToolStripMenuItem";
        connectionToolStripMenuItem.Size = new Size(89, 20);
        connectionToolStripMenuItem.Text = "Connection";
        connectionToolStripMenuItem.Visible = false;
        // 
        // settingsToolStripMenuItem
        // 
        settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
        settingsToolStripMenuItem.Size = new Size(180, 22);
        settingsToolStripMenuItem.Text = "Settings";
        settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
        // 
        // groupsToolStripMenuItem
        // 
        groupsToolStripMenuItem.Name = "groupsToolStripMenuItem";
        groupsToolStripMenuItem.Size = new Size(180, 22);
        groupsToolStripMenuItem.Text = "Groups";
        // 
        // logsToolStripMenuItem
        // 
        logsToolStripMenuItem.Name = "logsToolStripMenuItem";
        logsToolStripMenuItem.Size = new Size(180, 22);
        logsToolStripMenuItem.Text = "Logs";
        logsToolStripMenuItem.Click += logsToolStripMenuItem_Click;
        // 
        // exitToolStripMenuItem
        // 
        exitToolStripMenuItem.Name = "exitToolStripMenuItem";
        exitToolStripMenuItem.Size = new Size(180, 22);
        exitToolStripMenuItem.Text = "Exit";
        exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
        // 
        // groupBoxJellyfin
        // 
        groupBoxJellyfin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        groupBoxJellyfin.Controls.Add(textBoxJellyfinPassword);
        groupBoxJellyfin.Controls.Add(textBoxJellyfinUsername);
        groupBoxJellyfin.Controls.Add(textBoxJellyfinHost);
        groupBoxJellyfin.Controls.Add(label3);
        groupBoxJellyfin.Controls.Add(label2);
        groupBoxJellyfin.Controls.Add(label1);
        groupBoxJellyfin.Location = new Point(12, 12);
        groupBoxJellyfin.Name = "groupBoxJellyfin";
        groupBoxJellyfin.Size = new Size(514, 128);
        groupBoxJellyfin.TabIndex = 1;
        groupBoxJellyfin.TabStop = false;
        groupBoxJellyfin.Text = "Jellyfin";
        // 
        // textBoxJellyfinPassword
        // 
        textBoxJellyfinPassword.Location = new Point(83, 80);
        textBoxJellyfinPassword.Name = "textBoxJellyfinPassword";
        textBoxJellyfinPassword.PasswordChar = '*';
        textBoxJellyfinPassword.Size = new Size(425, 23);
        textBoxJellyfinPassword.TabIndex = 5;
        // 
        // textBoxJellyfinUsername
        // 
        textBoxJellyfinUsername.Location = new Point(83, 51);
        textBoxJellyfinUsername.Name = "textBoxJellyfinUsername";
        textBoxJellyfinUsername.Size = new Size(425, 23);
        textBoxJellyfinUsername.TabIndex = 4;
        // 
        // textBoxJellyfinHost
        // 
        textBoxJellyfinHost.Location = new Point(83, 22);
        textBoxJellyfinHost.Name = "textBoxJellyfinHost";
        textBoxJellyfinHost.Size = new Size(425, 23);
        textBoxJellyfinHost.TabIndex = 3;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(6, 83);
        label3.Name = "label3";
        label3.Size = new Size(57, 15);
        label3.TabIndex = 2;
        label3.Text = "Password";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(6, 54);
        label2.Name = "label2";
        label2.Size = new Size(60, 15);
        label2.TabIndex = 1;
        label2.Text = "Username";
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(6, 25);
        label1.Name = "label1";
        label1.Size = new Size(39, 15);
        label1.TabIndex = 0;
        label1.Text = "Server";
        // 
        // groupBoxMpc
        // 
        groupBoxMpc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        groupBoxMpc.Controls.Add(buttonSelectMpc);
        groupBoxMpc.Controls.Add(numericUpDownMpcPort);
        groupBoxMpc.Controls.Add(textBoxMpcPath);
        groupBoxMpc.Controls.Add(label5);
        groupBoxMpc.Controls.Add(label4);
        groupBoxMpc.Location = new Point(12, 146);
        groupBoxMpc.Name = "groupBoxMpc";
        groupBoxMpc.Size = new Size(514, 100);
        groupBoxMpc.TabIndex = 3;
        groupBoxMpc.TabStop = false;
        groupBoxMpc.Text = "MPC";
        // 
        // buttonSelectMpc
        // 
        buttonSelectMpc.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        buttonSelectMpc.IconChar = IconChar.FolderOpen;
        buttonSelectMpc.IconColor = Color.Black;
        buttonSelectMpc.IconFont = IconFont.Auto;
        buttonSelectMpc.IconSize = 16;
        buttonSelectMpc.Location = new Point(480, 22);
        buttonSelectMpc.Name = "buttonSelectMpc";
        buttonSelectMpc.Size = new Size(28, 23);
        buttonSelectMpc.TabIndex = 3;
        buttonSelectMpc.UseVisualStyleBackColor = true;
        buttonSelectMpc.Click += buttonSelectMpc_Click;
        // 
        // numericUpDownMpcPort
        // 
        numericUpDownMpcPort.Location = new Point(83, 51);
        numericUpDownMpcPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
        numericUpDownMpcPort.Name = "numericUpDownMpcPort";
        numericUpDownMpcPort.Size = new Size(82, 23);
        numericUpDownMpcPort.TabIndex = 4;
        // 
        // textBoxMpcPath
        // 
        textBoxMpcPath.Location = new Point(83, 22);
        textBoxMpcPath.Name = "textBoxMpcPath";
        textBoxMpcPath.Size = new Size(391, 23);
        textBoxMpcPath.TabIndex = 2;
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Location = new Point(7, 53);
        label5.Name = "label5";
        label5.Size = new Size(29, 15);
        label5.TabIndex = 1;
        label5.Text = "Port";
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(6, 25);
        label4.Name = "label4";
        label4.Size = new Size(31, 15);
        label4.TabIndex = 0;
        label4.Text = "Path";
        // 
        // buttonSave
        // 
        buttonSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        buttonSave.IconChar = IconChar.FloppyDisk;
        buttonSave.IconColor = Color.Black;
        buttonSave.IconFont = IconFont.Auto;
        buttonSave.IconSize = 16;
        buttonSave.Location = new Point(498, 252);
        buttonSave.Name = "buttonSave";
        buttonSave.Size = new Size(28, 23);
        buttonSave.TabIndex = 6;
        buttonSave.UseVisualStyleBackColor = true;
        buttonSave.Click += buttonSave_Click;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(538, 287);
        Controls.Add(buttonSave);
        Controls.Add(groupBoxMpc);
        Controls.Add(groupBoxJellyfin);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "MainForm";
        Text = "Jellyfin MPC Shim";
        FormClosing += MainForm_FormClosing;
        Load += MainForm_Load;
        trayMenu.ResumeLayout(false);
        groupBoxJellyfin.ResumeLayout(false);
        groupBoxJellyfin.PerformLayout();
        groupBoxMpc.ResumeLayout(false);
        groupBoxMpc.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numericUpDownMpcPort).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private NotifyIcon trayIcon;
    private ContextMenuStrip trayMenu;
    private ToolStripMenuItem settingsToolStripMenuItem;
    private ToolStripMenuItem exitToolStripMenuItem;
    private GroupBox groupBoxJellyfin;
    private TextBox textBoxJellyfinPassword;
    private TextBox textBoxJellyfinUsername;
    private TextBox textBoxJellyfinHost;
    private Label label3;
    private Label label2;
    private Label label1;
    private GroupBox groupBoxMpc;
    private TextBox textBoxMpcPath;
    private Label label5;
    private Label label4;
    private NumericUpDown numericUpDownMpcPort;
    private ToolStripMenuItem connectionToolStripMenuItem;
    private ToolStripMenuItem logsToolStripMenuItem;
    private ToolStripMenuItem groupsToolStripMenuItem;
    private FontAwesome.Sharp.IconButton buttonSelectMpc;
    private FontAwesome.Sharp.IconButton buttonSave;
}
