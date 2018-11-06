using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrapChat
{
    public partial class Main : Form
    {
        public static Main Instance;

        private bool IsValidUsername
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.NameInputBox.Text.Trim());
            }
        }
        private bool IsNewServerNameValid
        {
            get
            {
                return !string.IsNullOrWhiteSpace(GetNewServerName());
            }
        }
        private bool IsNewServerPortvalid
        {
            get
            {
                return GetNewServerPort() >= 0;
            }
        }

        public static string ActiveUsername = null;

        public Main()
        {
            Instance = this;

            InitializeComponent();

            // Some more custom form setup:
            this.MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Text = "Crap Chat 2 - ";
            this.FormClosing += AppClosing;
            var contextMenu = new ContextMenu();
            var contentExit = new MenuItem();
            contextMenu.MenuItems.Add("Exit", new EventHandler(this.ContextExitClicked));
            contextMenu.MenuItems.Add(new MenuItem("Disconnect", new System.EventHandler(this.DisconnectButton_Click)));
            contextMenu.MenuItems.Add(new MenuItem("Mute", new System.EventHandler(this.ContextMuteClicked)));
            this.NotificationIcon.ContextMenu = contextMenu;

            // Main entry point here...
            SetStatus("Disconnected");

            NameInputBox.Text = Properties.Settings.Default.DefaultUsername;
            SetButtonState(confirmNameButton, IsValidUsername);
            SetButtonState(MuteButton, false);
            SetButtonState(KickButton, false);
            SetButtonState(DisconnectButton, false);
            SetButtonState(ConnectButton, false);
            SetButtonState(CreateNewServerButton, false);
            SetButtonState(RefreshButton, false);
            SetButtonState(ShutdownServer, false);
            SetMasterVolume(Properties.Settings.Default.MasterVolume);

            if (!IsValidUsername)
                DisplayUsernamePrompt(true, "Please type a username");
            else
                DisplayUsernamePrompt(true, "Please confirm username");

            SetPortNumber(Properties.Settings.Default.DefaultPort);
            this.NewServerNameBox.Text = Properties.Settings.Default.DefaultNewServerName;
            this.NewServerPortBox.Text = Properties.Settings.Default.DefaultNewServerPort.ToString();

            bool autoLogin = Properties.Settings.Default.AutoLogin;
            if (autoLogin)
            {
                ConfirmNameClicked(null, null);
            }        
        }

        public static void AddUser(string name)
        {
            if(!Instance.ClientNameList.Items.Contains(name))
                Instance.ClientNameList.Items.Add(name);
        }

        public static void RemoveUser(string name)
        {
            if (Instance.ClientNameList.Items.Contains(name))
                Instance.ClientNameList.Items.Remove(name);
        }

        public static void ClearUsers()
        {
            Instance.ClientNameList.Items.Clear();
        }

        private void SetMasterVolume(float percentage)
        {
            // Where percentage is in the range 0 - 100
            percentage = Math.Min(Math.Max(percentage, 0f), 100f);
            this.VolumeBar.Value = (int)Math.Round(percentage / 2f);
            UpdateVolumeLabel();
            Audio.MasterVolume = percentage;

            
        }

        private void UpdateVolumeLabel()
        {
            this.masterVolumeLabel.Text = "Master Volume: " + this.VolumeBar.Value * 2 + "%";
        }

        private void AppClosing(object sender, FormClosingEventArgs e)
        {
            Main.Log("Closing...");

            if (Net.IsClient)
            {
                Net.StopClient();
            }
            if (Net.IsServer)
            {
                Net.StopServer();
            }

            // Update settings.
            Properties.Settings.Default.MasterVolume = this.VolumeBar.Value * 2;
            Properties.Settings.Default.Save();
        }

        private void ConfirmNameClicked(object sender, EventArgs e)
        {
            if (!IsValidUsername)
            {
                Log("Invalid username '" + NameInputBox.Text + "'");
                DisplayUsernamePrompt(true, "Invalid username!");
                return;
            }

            if (ActiveUsername == this.NameInputBox.Text.Trim())
                return;

            ActiveUsername = this.NameInputBox.Text.Trim();
            Log("Confirmed new name - " + ActiveUsername);
            this.Text = "Crap Chat 2 - " + ActiveUsername;

            DisplayUsernamePrompt(false);
            SetButtonState(CreateNewServerButton, true);

            if (!RefreshButton.Enabled)
                RefreshButtonClick(null, null);
            SetButtonState(RefreshButton, true);

            if(!string.IsNullOrWhiteSpace(GetNewServerName()) && !Net.IsServer && !string.IsNullOrWhiteSpace(ActiveUsername))
            {
                SetButtonState(CreateNewServerButton, true);
            }
        }

        public static void Log(object o)
        {
            if(o != null)
            {
                Console.WriteLine(o.ToString());
            }
            else
            {
                Console.WriteLine("null");
            }
        }

        private FoundServer GetSelectedServer()
        {
            if (ServerList.SelectedItem != null)
                return (FoundServer)this.ServerList.SelectedItem;
            else
                return new FoundServer();
        }

        private void SetPortNumber(int port)
        {
            this.PortTextBox.Text = port.ToString();
        }

        private void SelectedServerChanged(object sender, EventArgs e)
        {
            Log("Selected " + GetSelectedServer());

            if(!Net.IsServer && !Net.IsConnecting && !GetSelectedServer().IsInvalid())
                SetButtonState(ConnectButton, true);
        }

        private void NameInputKeyDown(object sender, KeyEventArgs k)
        {
            if(k.KeyCode == Keys.Enter || k.KeyCode == Keys.Return)
            {
                this.ConfirmNameClicked(sender, null);
            }
        }

        public static void SetStatus(string status)
        {
            Instance.StatusLabel.Text = "Status: " + status.Trim();
        }

        private void DisplayUsernamePrompt(bool visible, string text = null)
        {
            if (visible)
            {
                this.typePrompt.Text = text != null ? text.Trim() : this.typePrompt.Text;
                this.typePrompt.Visible = true;
            }
            else
            {
                this.typePrompt.Visible = false;
            }
        }

        private void SetButtonState(Button b, bool enabled)
        {
            if(b != null)
            {
                if(b == DisconnectButton)
                {
                    this.NotificationIcon.ContextMenu.MenuItems[1].Enabled = enabled;
                }

                if(b.Enabled != enabled)
                    b.Enabled = enabled;
            }
        }

        private void NameInputValueChanged(object sender, EventArgs e)
        {
            SetButtonState(confirmNameButton, IsValidUsername);
            if (!IsValidUsername)
            {
                DisplayUsernamePrompt(true, "Invalid username!");
            }
            else
            {
                DisplayUsernamePrompt(false);
            }
        }

        private void PortTextBoxChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private int GetRefreshPort()
        {
            if (string.IsNullOrWhiteSpace(PortTextBox.Text))
                return Properties.Settings.Default.DefaultPort;

            int port = int.Parse(PortTextBox.Text);
            return port;
        }

        private int GetNewServerPort()
        {
            if (string.IsNullOrWhiteSpace(NewServerPortBox.Text))
                return Properties.Settings.Default.DefaultNewServerPort;

            return int.Parse(NewServerPortBox.Text);
        }

        private string GetNewServerName()
        {
            return NewServerNameBox.Text.Trim();
        }        

        private void RefreshButtonClick(object sender, EventArgs e)
        {
            if (!Net.IsClient)
            {
                Net.StartClient();
            }
            ServerList.Items.Clear();
            Net.DiscoverPeers(GetRefreshPort(), ServerDiscovered);
            SetButtonState(ConnectButton, false);
            System.GC.Collect();
        }

        private void ServerDiscovered(FoundServer s)
        {
            ServerList.Items.Add(s);
            Log("Discovered server '" + s.Name + "' @ " + s.EndPoint);
        }

        private void CreateNewServerButtonClick(object sender, EventArgs e)
        {
            var name = GetNewServerName();
            int port = GetNewServerPort();

            bool worked = Net.StartServer(port, name);
            if (worked)
            {
                SetButtonState(CreateNewServerButton, false);
                SetButtonState(ShutdownServer, true);
                RefreshButtonClick(null, null);
                ClearUsers();
                AddUser(ActiveUsername);
            }
        }

        private void NewServerNameBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(GetNewServerName()))
                SetButtonState(CreateNewServerButton, false);
            else if(!Net.IsServer && !string.IsNullOrWhiteSpace(ActiveUsername))
                SetButtonState(CreateNewServerButton, true);
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (!Net.IsClient)
                Net.StartClient();

            var s = GetSelectedServer();
            if (string.IsNullOrWhiteSpace(s.Name))
                return;

            Log("Attempting to connect to " + s);
            bool worked = Net.ConnectClient(s.EndPoint.Address.ToString(), s.EndPoint.Port);
            if (worked)
            {
                SetButtonState(ConnectButton, false);
            }
        }

        public static void UponDisconnected()
        {
            Instance.SetButtonState(Instance.DisconnectButton, false);
            Instance.SetButtonState(Instance.ConnectButton, false);
            Instance.ServerList.ClearSelected();
            SetStatus("Disconnected");
            ClearUsers();            
            Instance.RefreshButtonClick(null, null);
        }

        public static void UponConnected()
        {
            Instance.SetButtonState(Instance.DisconnectButton, true);
            Instance.SetButtonState(Instance.ConnectButton, false);
        }

        public static void UponServerStop()
        {
            Instance.SetButtonState(Instance.MuteButton, false);
            Instance.SetButtonState(Instance.KickButton, false);
        }

        private void ShutdownServer_Click(object sender, EventArgs e)
        {
            Net.StopServer();
            RefreshButtonClick(null, null);
            SetButtonState(ShutdownServer, false);
            if (!string.IsNullOrWhiteSpace(GetNewServerName()) && !Net.IsServer && !string.IsNullOrWhiteSpace(ActiveUsername))
            {
                SetButtonState(CreateNewServerButton, true);
            }
            ClearUsers();
            SetStatus("Disconnected");
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            if(Net.IsClient && Net.IsConnected)
            {
                if (DisconnectButton.Enabled == false)
                    return;

                SetButtonState(DisconnectButton, false);
                Net.StopClient();
            }
            else
            {
                SetButtonState(DisconnectButton, false);
            }
        }

        private string GetSelectedUser()
        {
            return ClientNameList.SelectedItem == null ? null : (string)ClientNameList.SelectedItem;
        }

        private void KickButton_Click(object sender, EventArgs e)
        {
            string name = GetSelectedUser();
            if (!string.IsNullOrWhiteSpace(name))
            {
                if (Net.IsServer)
                {
                    Net.Server.Kick(name);
                }
            }
        }

        private void MuteButton_Click(object sender, EventArgs e)
        {
            string name = GetSelectedUser();
            if (!string.IsNullOrWhiteSpace(name))
            {
                if (Net.IsServer)
                {
                    bool isMuted = Net.Server.IsMuted(name);
                    if (isMuted)
                    {
                        Net.Server.Unmute(name);
                        MuteButton.Text = "Mute Person";
                    }
                    else
                    {
                        Net.Server.Mute(name);
                        MuteButton.Text = "Un-mute Person";
                    }
                }
            }
        }

        private void SelectedUserChanged(object sender, EventArgs e)
        {
            if (!Net.IsServer)
                return;

            string selected = this.ClientNameList.SelectedItem as string;

            if (!string.IsNullOrWhiteSpace(selected))
            {
                if(selected == ActiveUsername)
                {
                    SetButtonState(KickButton, false);
                    SetButtonState(MuteButton, true);
                    MuteButton.Text = "Mute Person";
                }
                else
                {
                    SetButtonState(KickButton, true);
                    SetButtonState(MuteButton, true);

                    bool muted = Net.Server.IsMuted(selected);
                    MuteButton.Text = muted ? "Un-mute Person" : "Mute Person";
                }
            }
            else
            {
                SetButtonState(KickButton, false);
                SetButtonState(MuteButton, false);
                MuteButton.Text = "Mute Person";
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            // This is the refresh timer.
            if(RefreshButton.Enabled && !Net.IsConnected)
                RefreshButtonClick(null, null);
        }

        private void VolumeBarScroll(object sender, EventArgs e)
        {
            UpdateVolumeLabel();
        }

        private void IconDoubleClicked(object sender, MouseEventArgs e)
        {
            Log("Double clicked icon, bringing into focus.");
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void ContextExitClicked(object sender, EventArgs e)
        {
            // Exit the application.
            this.Close();
        }

        private void ContextMuteClicked(object sender, EventArgs e)
        {
            // Mute the whole thing by setting Master Volume to 0.
            SetMasterVolume(0f);
            Log("Muted from context menu.");            
        }

        private bool GetUploadFolder(out string selected)
        {
            using (var fd = new FolderBrowserDialog())
            {
                fd.Description = "Select the folder to send...";
                var result = fd.ShowDialog();

                if(result != DialogResult.OK || fd.SelectedPath == Environment.GetFolderPath(fd.RootFolder))
                {
                    selected = null;
                    return false;
                }
                else
                {
                    selected = fd.SelectedPath;
                    return true;
                }
            }
        }

        private void SendFileClicked(object sender, EventArgs e)
        {
            Main.Log("Starting new file upload...");
        }

        private async void SendFolderClicked(object sender, EventArgs e)
        {
            Main.Log("Starting new folder upload...");
            string s;
            bool worked = GetUploadFolder(out s);
            if (worked)
            {
                Log("User wants to upload " + s);

                using (LoadingForm loading = new LoadingForm())
                {
                    loading.SetTitle("Finding all files");
                    loading.SetDetailsText("Finding all files in the selected directory...");
                    loading.SetProgressText("");
                    loading.SetStyle(ProgressBarStyle.Marquee);
                    loading.ControlBox = false;
                    loading.Show();

                    string[] files = null;
                    try
                    {
                        await Task.Run(() => files = Directory.EnumerateFiles(s, ".", SearchOption.AllDirectories).ToArray());
                    }
                    catch (Exception ex)
                    {
                        Main.Log("Failed to list files in directory.");
                        Log(ex);
                        MessageBox.Show("There was an error while reading the files in the selected directory: " + ex.Message, "Failed to read files in folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int fc = files.Length;

                    bool warn = fc >= 20;
                    string desc = "many";
                    if (fc >= 1500)
                    {
                        desc = "a mind-blowingly humungous number of";
                    }
                    else if (fc >= 1000)
                    {
                        desc = "an absolutely absurd number of";
                    }
                    else if (fc >= 500)
                    {
                        desc = "a stupidly high number of";
                    }
                    else if (fc >= 100)
                    {
                        desc = "loads of";
                    }
                    else if (fc > 50)
                    {
                        desc = "many, many";
                    }
                    else if (fc >= 20)
                    {
                        desc = "quite a few";
                    }

                    Log("Found " + fc + " files in the directory");
                    if (warn)
                    {
                        var result = MessageBox.Show("The folder you selected contains " + fc + " files. That's " + desc + " files to send over the network, and it many take a long time. Do you want to continue with the upload?", "File count warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (result != DialogResult.Yes)
                        {
                            Log("Due to the number of files prompt, the user has decided to cancel the upload.");
                        }
                    }

                    long totalSize = 0;
                    foreach (var file in files)
                    {
                        totalSize += new FileInfo(file).Length;
                    }
                    const long KB = 1024;
                    const long MB = KB * KB;
                    const long GB = MB * KB;
                    Log(GB);

                    string ending = totalSize >= GB ? "GB" : totalSize >= MB ? "MB" : "KB";
                    float div = totalSize >= GB ? (float)totalSize / GB : totalSize >= MB ? (float)totalSize / MB : (float)totalSize / KB;
                    string final = div.ToString("N2") + ending;

                    Log("Total size of all files combined: " + final);

                    System.GC.Collect();
                }                    
            }
            else
            {
                Log("Folder upload was canceled before it started.");
            }

        }

        private void DownloadButtonClicked(object sender, EventArgs e)
        {
            Main.Log("Starting file or folder download process...");
        }
    }
}
