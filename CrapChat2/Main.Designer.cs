namespace CrapChat
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label hostNameLabel;
            System.Windows.Forms.Label hostPortLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.NameInputBox = new System.Windows.Forms.TextBox();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.confirmNameButton = new System.Windows.Forms.Button();
            this.ServerList = new System.Windows.Forms.ListBox();
            this.serversFoundLabel = new System.Windows.Forms.Label();
            this.typePrompt = new System.Windows.Forms.Label();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.ClientNameList = new System.Windows.Forms.ListBox();
            this.MuteButton = new System.Windows.Forms.Button();
            this.KickButton = new System.Windows.Forms.Button();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.NewServerNameBox = new System.Windows.Forms.TextBox();
            this.CreateNewServerButton = new System.Windows.Forms.Button();
            this.NewServerPortBox = new System.Windows.Forms.TextBox();
            this.ShutdownServer = new System.Windows.Forms.Button();
            this.VolumeBar = new System.Windows.Forms.TrackBar();
            this.masterVolumeLabel = new System.Windows.Forms.Label();
            this.RefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.NotificationIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openGithubPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForNewVersionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            hostNameLabel = new System.Windows.Forms.Label();
            hostPortLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.VolumeBar)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // hostNameLabel
            // 
            hostNameLabel.AutoSize = true;
            hostNameLabel.Location = new System.Drawing.Point(15, 318);
            hostNameLabel.Name = "hostNameLabel";
            hostNameLabel.Size = new System.Drawing.Size(38, 13);
            hostNameLabel.TabIndex = 17;
            hostNameLabel.Text = "Name:";
            // 
            // hostPortLabel
            // 
            hostPortLabel.AutoSize = true;
            hostPortLabel.Location = new System.Drawing.Point(15, 345);
            hostPortLabel.Name = "hostPortLabel";
            hostPortLabel.Size = new System.Drawing.Size(29, 13);
            hostPortLabel.TabIndex = 20;
            hostPortLabel.Text = "Port:";
            // 
            // NameInputBox
            // 
            this.NameInputBox.Location = new System.Drawing.Point(12, 89);
            this.NameInputBox.MaxLength = 16;
            this.NameInputBox.Name = "NameInputBox";
            this.NameInputBox.Size = new System.Drawing.Size(124, 20);
            this.NameInputBox.TabIndex = 0;
            this.NameInputBox.TextChanged += new System.EventHandler(this.NameInputValueChanged);
            this.NameInputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NameInputKeyDown);
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(12, 69);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(55, 13);
            this.usernameLabel.TabIndex = 1;
            this.usernameLabel.Text = "Username";
            // 
            // confirmNameButton
            // 
            this.confirmNameButton.Location = new System.Drawing.Point(142, 89);
            this.confirmNameButton.Name = "confirmNameButton";
            this.confirmNameButton.Size = new System.Drawing.Size(92, 20);
            this.confirmNameButton.TabIndex = 2;
            this.confirmNameButton.Text = "Confirm Name";
            this.confirmNameButton.UseVisualStyleBackColor = true;
            this.confirmNameButton.Click += new System.EventHandler(this.ConfirmNameClicked);
            // 
            // ServerList
            // 
            this.ServerList.FormattingEnabled = true;
            this.ServerList.Location = new System.Drawing.Point(12, 145);
            this.ServerList.Name = "ServerList";
            this.ServerList.ScrollAlwaysVisible = true;
            this.ServerList.Size = new System.Drawing.Size(379, 108);
            this.ServerList.TabIndex = 3;
            this.ServerList.SelectedValueChanged += new System.EventHandler(this.SelectedServerChanged);
            // 
            // serversFoundLabel
            // 
            this.serversFoundLabel.AutoSize = true;
            this.serversFoundLabel.Location = new System.Drawing.Point(12, 125);
            this.serversFoundLabel.Name = "serversFoundLabel";
            this.serversFoundLabel.Size = new System.Drawing.Size(79, 13);
            this.serversFoundLabel.TabIndex = 4;
            this.serversFoundLabel.Text = "Servers Found:";
            // 
            // typePrompt
            // 
            this.typePrompt.AutoSize = true;
            this.typePrompt.ForeColor = System.Drawing.Color.Red;
            this.typePrompt.Location = new System.Drawing.Point(240, 93);
            this.typePrompt.Name = "typePrompt";
            this.typePrompt.Size = new System.Drawing.Size(93, 13);
            this.typePrompt.TabIndex = 5;
            this.typePrompt.Text = "Enter a username!";
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(316, 116);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(75, 22);
            this.RefreshButton.TabIndex = 6;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButtonClick);
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.StatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLabel.Location = new System.Drawing.Point(9, 38);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(54, 18);
            this.StatusLabel.TabIndex = 7;
            this.StatusLabel.Text = "Status:";
            // 
            // ConnectButton
            // 
            this.ConnectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ConnectButton.Location = new System.Drawing.Point(12, 259);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(75, 23);
            this.ConnectButton.TabIndex = 8;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = false;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.DisconnectButton.Enabled = false;
            this.DisconnectButton.Location = new System.Drawing.Point(93, 259);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(75, 23);
            this.DisconnectButton.TabIndex = 9;
            this.DisconnectButton.Text = "Disconnect";
            this.DisconnectButton.UseVisualStyleBackColor = false;
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // ClientNameList
            // 
            this.ClientNameList.FormattingEnabled = true;
            this.ClientNameList.Location = new System.Drawing.Point(12, 423);
            this.ClientNameList.Name = "ClientNameList";
            this.ClientNameList.Size = new System.Drawing.Size(263, 82);
            this.ClientNameList.Sorted = true;
            this.ClientNameList.TabIndex = 10;
            this.ClientNameList.SelectedValueChanged += new System.EventHandler(this.SelectedUserChanged);
            // 
            // MuteButton
            // 
            this.MuteButton.Location = new System.Drawing.Point(281, 423);
            this.MuteButton.Name = "MuteButton";
            this.MuteButton.Size = new System.Drawing.Size(110, 23);
            this.MuteButton.TabIndex = 11;
            this.MuteButton.Text = "Mute Person";
            this.MuteButton.UseVisualStyleBackColor = true;
            this.MuteButton.Click += new System.EventHandler(this.MuteButton_Click);
            // 
            // KickButton
            // 
            this.KickButton.Location = new System.Drawing.Point(281, 452);
            this.KickButton.Name = "KickButton";
            this.KickButton.Size = new System.Drawing.Size(110, 23);
            this.KickButton.TabIndex = 12;
            this.KickButton.Text = "Kick Person";
            this.KickButton.UseVisualStyleBackColor = true;
            this.KickButton.Click += new System.EventHandler(this.KickButton_Click);
            // 
            // PortTextBox
            // 
            this.PortTextBox.Location = new System.Drawing.Point(243, 118);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(67, 20);
            this.PortTextBox.TabIndex = 13;
            this.PortTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PortTextBoxChanged);
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(208, 121);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(29, 13);
            this.portLabel.TabIndex = 14;
            this.portLabel.Text = "Port:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 300);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Host Server";
            // 
            // NewServerNameBox
            // 
            this.NewServerNameBox.Location = new System.Drawing.Point(59, 316);
            this.NewServerNameBox.Name = "NewServerNameBox";
            this.NewServerNameBox.Size = new System.Drawing.Size(100, 20);
            this.NewServerNameBox.TabIndex = 16;
            this.NewServerNameBox.TextChanged += new System.EventHandler(this.NewServerNameBox_TextChanged);
            // 
            // CreateNewServerButton
            // 
            this.CreateNewServerButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.CreateNewServerButton.Location = new System.Drawing.Point(13, 369);
            this.CreateNewServerButton.Name = "CreateNewServerButton";
            this.CreateNewServerButton.Size = new System.Drawing.Size(114, 23);
            this.CreateNewServerButton.TabIndex = 18;
            this.CreateNewServerButton.Text = "Create New Server";
            this.CreateNewServerButton.UseVisualStyleBackColor = false;
            this.CreateNewServerButton.Click += new System.EventHandler(this.CreateNewServerButtonClick);
            // 
            // NewServerPortBox
            // 
            this.NewServerPortBox.Location = new System.Drawing.Point(59, 343);
            this.NewServerPortBox.Name = "NewServerPortBox";
            this.NewServerPortBox.Size = new System.Drawing.Size(100, 20);
            this.NewServerPortBox.TabIndex = 19;
            // 
            // ShutdownServer
            // 
            this.ShutdownServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ShutdownServer.Location = new System.Drawing.Point(133, 369);
            this.ShutdownServer.Name = "ShutdownServer";
            this.ShutdownServer.Size = new System.Drawing.Size(105, 23);
            this.ShutdownServer.TabIndex = 21;
            this.ShutdownServer.Text = "Shutdown Server";
            this.ShutdownServer.UseVisualStyleBackColor = false;
            this.ShutdownServer.Click += new System.EventHandler(this.ShutdownServer_Click);
            // 
            // VolumeBar
            // 
            this.VolumeBar.Location = new System.Drawing.Point(243, 27);
            this.VolumeBar.Maximum = 50;
            this.VolumeBar.Name = "VolumeBar";
            this.VolumeBar.Size = new System.Drawing.Size(148, 45);
            this.VolumeBar.TabIndex = 22;
            this.VolumeBar.Value = 50;
            this.VolumeBar.Scroll += new System.EventHandler(this.VolumeBarScroll);
            // 
            // masterVolumeLabel
            // 
            this.masterVolumeLabel.AutoSize = true;
            this.masterVolumeLabel.Location = new System.Drawing.Point(267, 8);
            this.masterVolumeLabel.Name = "masterVolumeLabel";
            this.masterVolumeLabel.Size = new System.Drawing.Size(109, 13);
            this.masterVolumeLabel.TabIndex = 23;
            this.masterVolumeLabel.Text = "Master Volume: 100%";
            // 
            // RefreshTimer
            // 
            this.RefreshTimer.Enabled = true;
            this.RefreshTimer.Interval = global::CrapChat.Properties.Settings.Default.AutoRefreshInterval;
            this.RefreshTimer.Tick += new System.EventHandler(this.TimerTick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 407);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "Connected Users:";
            // 
            // NotificationIcon
            // 
            this.NotificationIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NotificationIcon.Icon")));
            this.NotificationIcon.Text = "Crap Chat 2";
            this.NotificationIcon.Visible = true;
            this.NotificationIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.IconDoubleClicked);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(400, 24);
            this.menuStrip1.TabIndex = 28;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filesToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // filesToolStripMenuItem
            // 
            this.filesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendFileToolStripMenuItem,
            this.sendFolderToolStripMenuItem,
            this.downloadFileToolStripMenuItem});
            this.filesToolStripMenuItem.Name = "filesToolStripMenuItem";
            this.filesToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.filesToolStripMenuItem.Text = "Files...";
            // 
            // sendFileToolStripMenuItem
            // 
            this.sendFileToolStripMenuItem.Name = "sendFileToolStripMenuItem";
            this.sendFileToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.sendFileToolStripMenuItem.Text = "Send file";
            this.sendFileToolStripMenuItem.ToolTipText = "Upload a single file to the server. Connected users can then download the file.";
            this.sendFileToolStripMenuItem.Click += new System.EventHandler(this.SendFileClicked);
            // 
            // sendFolderToolStripMenuItem
            // 
            this.sendFolderToolStripMenuItem.Name = "sendFolderToolStripMenuItem";
            this.sendFolderToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.sendFolderToolStripMenuItem.Text = "Send folder";
            this.sendFolderToolStripMenuItem.ToolTipText = "Upload a folder to the server. Connected users can then download the folder.";
            this.sendFolderToolStripMenuItem.Click += new System.EventHandler(this.SendFolderClicked);
            // 
            // downloadFileToolStripMenuItem
            // 
            this.downloadFileToolStripMenuItem.Name = "downloadFileToolStripMenuItem";
            this.downloadFileToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.downloadFileToolStripMenuItem.Text = "Download folder or file";
            this.downloadFileToolStripMenuItem.ToolTipText = "Download one of the files or folders available to the server. Normally sent by ot" +
    "her connected users.";
            this.downloadFileToolStripMenuItem.Click += new System.EventHandler(this.DownloadButtonClicked);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openGithubPageToolStripMenuItem,
            this.checkForNewVersionToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // openGithubPageToolStripMenuItem
            // 
            this.openGithubPageToolStripMenuItem.Name = "openGithubPageToolStripMenuItem";
            this.openGithubPageToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.openGithubPageToolStripMenuItem.Text = "Open Github page";
            // 
            // checkForNewVersionToolStripMenuItem
            // 
            this.checkForNewVersionToolStripMenuItem.Name = "checkForNewVersionToolStripMenuItem";
            this.checkForNewVersionToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.checkForNewVersionToolStripMenuItem.Text = "Check for new version";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 516);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.masterVolumeLabel);
            this.Controls.Add(this.VolumeBar);
            this.Controls.Add(this.ShutdownServer);
            this.Controls.Add(hostPortLabel);
            this.Controls.Add(this.NewServerPortBox);
            this.Controls.Add(this.CreateNewServerButton);
            this.Controls.Add(hostNameLabel);
            this.Controls.Add(this.NewServerNameBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.PortTextBox);
            this.Controls.Add(this.KickButton);
            this.Controls.Add(this.MuteButton);
            this.Controls.Add(this.ClientNameList);
            this.Controls.Add(this.DisconnectButton);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.typePrompt);
            this.Controls.Add(this.serversFoundLabel);
            this.Controls.Add(this.ServerList);
            this.Controls.Add(this.confirmNameButton);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.NameInputBox);
            this.Controls.Add(this.menuStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Crap Chat 2";
            ((System.ComponentModel.ISupportInitialize)(this.VolumeBar)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox NameInputBox;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Button confirmNameButton;
        private System.Windows.Forms.Label serversFoundLabel;
        private System.Windows.Forms.Label typePrompt;
        public System.Windows.Forms.ListBox ServerList;
        public System.Windows.Forms.Button RefreshButton;
        public System.Windows.Forms.Label StatusLabel;
        public System.Windows.Forms.Button ConnectButton;
        public System.Windows.Forms.Button DisconnectButton;
        public System.Windows.Forms.Button MuteButton;
        private System.Windows.Forms.Button KickButton;
        private System.Windows.Forms.Label portLabel;
        public System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox NewServerNameBox;
        public System.Windows.Forms.TextBox NewServerPortBox;
        public System.Windows.Forms.Button CreateNewServerButton;
        public System.Windows.Forms.Button ShutdownServer;
        public System.Windows.Forms.ListBox ClientNameList;
        private System.Windows.Forms.Timer RefreshTimer;
        private System.Windows.Forms.Label masterVolumeLabel;
        public System.Windows.Forms.TrackBar VolumeBar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NotifyIcon NotificationIcon;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openGithubPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForNewVersionToolStripMenuItem;
    }
}

