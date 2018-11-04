using CrapChat;
using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrapChat
{
    public static class Net
    {
        public const string APP_ID = "Crap Chat 2";
        public static CClient Client;
        public static CServer Server;

        public static bool IsClient
        {
            get
            {
                return Client != null;
            }
        }

        public static bool IsServer
        {
            get
            {
                return Server != null;
            }
        }

        public static bool IsConnected
        {
            get
            {
                return IsClient && Client.Status == NetPeerStatus.Running && Client.ConnectionStatus == NetConnectionStatus.Connected;
            }
        }

        public static bool IsConnecting
        {
            get
            {
                return IsClient && Client.Status == NetPeerStatus.Running && Client.ConnectionStatus != NetConnectionStatus.Disconnected;
            }
        }

        public static void StartClient()
        {
            if (IsClient)
            {
                Main.Log("Client is already started, cannot start a new one.");
                return;
            }

            var config = new NetPeerConfiguration(APP_ID);
            config.EnableMessageType(NetIncomingMessageType.DiscoveryResponse);

            Client = new CClient(null, config);
            Client.Start();

            Main.Log("Started client...");
        }

        public static void StopClient()
        {
            if (!IsClient)
            {
                Main.Log("Client is not running...");
                return;
            }

            Client.Shutdown("The user has disconnected from the server. Bye!");
            Client = null;
        }

        public static bool ConnectClient(string ip, int port)
        {
            if (!IsClient)
                return false;
            if (IsConnected || IsConnecting)
                return false;

            Main.Log("Attempting to connect to " + ip + " on port " + port);

            try
            {
                Client.Connect(ip, port);
                return true;
            }
            catch (Exception e)
            {
                Main.Log(e);
                return false;
            }
        }

        public static void UponClientDisconnected()
        {
            Main.SetStatus("Disconnected");
            Client = null;
        }

        public static void DiscoverPeers(int port, CClient.ServerFound callback)
        {
            if (!IsClient)
            {
                Main.Log("Cannot discover peers, client is not started.");
                return;
            }
            Main.Log("Scanning for local peers on " + port + "...");
            Client.StartDiscovery(port, callback);
        }

        public static bool StartServer(int port, string name)
        {
            if (IsServer)
            {
                Main.Log("Server already started, shut it down before you open a new one.");
                return false;
            }
            if (IsClient && (IsConnected || IsConnecting))
            {
                Main.Log("Cannot host server while client is active.");
                return false;
            }

            var config = new NetPeerConfiguration(APP_ID);
            config.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);
            config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            config.Port = port;

            Server = new CServer(name, config);
            Server.Start();

            Main.Log("Started hosting server on port " + port + " called " + name);

            Main.SetStatus("Hosting Server (" + name.Trim() + ", " + port + ")");

            return true;
        }

        public static void StopServer()
        {
            if (!IsServer)
                return;

            Server.Shutdown("The host has shutdown the server.");
            Server = null;

            Main.Log("The locally hosted server has been shut down.");
        }
    }
}
