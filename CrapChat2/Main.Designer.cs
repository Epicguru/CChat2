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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
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
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(14, 298);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(38, 13);
            label2.TabIndex = 17;
            label2.Text = "Name:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(14, 325);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(29, 13);
            label3.TabIndex = 20;
            label3.Text = "Port:";
            // 
            // NameInputBox
            // 
            this.NameInputBox.Location = new System.Drawing.Point(12, 62);
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
            this.usernameLabel.Location = new System.Drawing.Point(12, 42);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(55, 13);
            this.usernameLabel.TabIndex = 1;
            this.usernameLabel.Text = "Username";
            // 
            // confirmNameButton
            // 
            this.confirmNameButton.Location = new System.Drawing.Point(142, 62);
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
            this.ServerList.Location = new System.Drawing.Point(12, 122);
            this.ServerList.Name = "ServerList";
            this.ServerList.ScrollAlwaysVisible = true;
            this.ServerList.Size = new System.Drawing.Size(379, 108);
            this.ServerList.TabIndex = 3;
            this.ServerList.SelectedIndexChanged += new System.EventHandler(this.SelectedServerChanged);
            // 
            // serversFoundLabel
            // 
            this.serversFoundLabel.AutoSize = true;
            this.serversFoundLabel.Location = new System.Drawing.Point(12, 102);
            this.serversFoundLabel.Name = "serversFoundLabel";
            this.serversFoundLabel.Size = new System.Drawing.Size(79, 13);
            this.serversFoundLabel.TabIndex = 4;
            this.serversFoundLabel.Text = "Servers Found:";
            // 
            // typePrompt
            // 
            this.typePrompt.AutoSize = true;
            this.typePrompt.ForeColor = System.Drawing.Color.Red;
            this.typePrompt.Location = new System.Drawing.Point(240, 66);
            this.typePrompt.Name = "typePrompt";
            this.typePrompt.Size = new System.Drawing.Size(93, 13);
            this.typePrompt.TabIndex = 5;
            this.typePrompt.Text = "Enter a username!";
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(316, 93);
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
            this.StatusLabel.Location = new System.Drawing.Point(12, 9);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(54, 18);
            this.StatusLabel.TabIndex = 7;
            this.StatusLabel.Text = "Status:";
            // 
            // ConnectButton
            // 
            this.ConnectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ConnectButton.Location = new System.Drawing.Point(12, 236);
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
            this.DisconnectButton.Location = new System.Drawing.Point(93, 236);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(75, 23);
            this.DisconnectButton.TabIndex = 9;
            this.DisconnectButton.Text = "Disconnect";
            this.DisconnectButton.UseVisualStyleBackColor = false;
            // 
            // ClientNameList
            // 
            this.ClientNameList.FormattingEnabled = true;
            this.ClientNameList.Location = new System.Drawing.Point(12, 392);
            this.ClientNameList.Name = "ClientNameList";
            this.ClientNameList.Size = new System.Drawing.Size(263, 82);
            this.ClientNameList.TabIndex = 10;
            // 
            // MuteButton
            // 
            this.MuteButton.Location = new System.Drawing.Point(281, 392);
            this.MuteButton.Name = "MuteButton";
            this.MuteButton.Size = new System.Drawing.Size(110, 23);
            this.MuteButton.TabIndex = 11;
            this.MuteButton.Text = "Mute Person";
            this.MuteButton.UseVisualStyleBackColor = true;
            // 
            // KickButton
            // 
            this.KickButton.Location = new System.Drawing.Point(281, 421);
            this.KickButton.Name = "KickButton";
            this.KickButton.Size = new System.Drawing.Size(110, 23);
            this.KickButton.TabIndex = 12;
            this.KickButton.Text = "Kick Person";
            this.KickButton.UseVisualStyleBackColor = true;
            // 
            // PortTextBox
            // 
            this.PortTextBox.Location = new System.Drawing.Point(243, 95);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(67, 20);
            this.PortTextBox.TabIndex = 13;
            this.PortTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PortTextBoxChanged);
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(208, 98);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(29, 13);
            this.portLabel.TabIndex = 14;
            this.portLabel.Text = "Port:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 280);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Host Server";
            // 
            // NewServerNameBox
            // 
            this.NewServerNameBox.Location = new System.Drawing.Point(58, 296);
            this.NewServerNameBox.Name = "NewServerNameBox";
            this.NewServerNameBox.Size = new System.Drawing.Size(100, 20);
            this.NewServerNameBox.TabIndex = 16;
            this.NewServerNameBox.TextChanged += new System.EventHandler(this.NewServerNameBox_TextChanged);
            // 
            // CreateNewServerButton
            // 
            this.CreateNewServerButton.Location = new System.Drawing.Point(12, 349);
            this.CreateNewServerButton.Name = "CreateNewServerButton";
            this.CreateNewServerButton.Size = new System.Drawing.Size(114, 23);
            this.CreateNewServerButton.TabIndex = 18;
            this.CreateNewServerButton.Text = "Create New Server";
            this.CreateNewServerButton.UseVisualStyleBackColor = true;
            this.CreateNewServerButton.Click += new System.EventHandler(this.CreateNewServerButtonClick);
            // 
            // NewServerPortBox
            // 
            this.NewServerPortBox.Location = new System.Drawing.Point(58, 323);
            this.NewServerPortBox.Name = "NewServerPortBox";
            this.NewServerPortBox.Size = new System.Drawing.Size(100, 20);
            this.NewServerPortBox.TabIndex = 19;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 486);
            this.Controls.Add(label3);
            this.Controls.Add(this.NewServerPortBox);
            this.Controls.Add(this.CreateNewServerButton);
            this.Controls.Add(label2);
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
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.HelpButton = true;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Crap Chat 2";
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
        private System.Windows.Forms.ListBox ClientNameList;
        public System.Windows.Forms.Button MuteButton;
        private System.Windows.Forms.Button KickButton;
        private System.Windows.Forms.Label portLabel;
        public System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox NewServerNameBox;
        public System.Windows.Forms.TextBox NewServerPortBox;
        public System.Windows.Forms.Button CreateNewServerButton;
    }
}

