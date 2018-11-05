using System;
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

        public string ActiveUsername = null;

        public Main()
        {
            Instance = this;

            InitializeComponent();

            // Some more custom form setup:
            this.MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Text = "Crap Chat 2 - ";
            this.FormClosing += AppClosing;

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

            if (!IsValidUsername)
                DisplayUsernamePrompt(true, "Please type a username");
            else
                DisplayUsernamePrompt(true, "Please confirm username");

            SetPortNumber(Properties.Settings.Default.DefaultPort);
            this.NewServerNameBox.Text = Properties.Settings.Default.DefaultNewServerName;
            this.NewServerPortBox.Text = Properties.Settings.Default.DefaultNewServerPort.ToString();
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
            return (FoundServer)this.ServerList.SelectedItem;
        }

        private void SetPortNumber(int port)
        {
            this.PortTextBox.Text = port.ToString();
        }

        private void SelectedServerChanged(object sender, EventArgs e)
        {
            Log("Selected " + GetSelectedServer());

            if(!Net.IsServer && !Net.IsConnecting)
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
            Net.ConnectClient(s.EndPoint.Address.ToString(), s.EndPoint.Port);
        }

        private void ShutdownServer_Click(object sender, EventArgs e)
        {
            Net.StopServer();
            RefreshButtonClick(null, null);
            SetButtonState(ShutdownServer, false);
            SetStatus("Disconnected");
        }
    }
}
