using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrapChat
{
    public class CClient : NetClient
    {
        public delegate void ServerFound(FoundServer server);
        public string Name { get; set; }
        private ServerFound callback;

        public CClient(string name, NetPeerConfiguration config)
            :base(config)
        {
            this.Name = name;
            this.RegisterReceivedCallback(new SendOrPostCallback(GotMessage));
        }

        public void StartDiscovery(int port, ServerFound callback)
        {
            this.callback = callback;
            this.DiscoverLocalPeers(port);
        }

        private static void GotMessage(object peer)
        {
            var client = ((CClient)peer);
            var msg = client.ReadMessage();

            switch (msg.MessageType)
            {
                case NetIncomingMessageType.DiscoveryResponse:
                    string name = msg.ReadString();
                    var s = new FoundServer() { EndPoint = msg.SenderEndPoint, Name = name };

                    if (client.callback != null)
                        client.callback.Invoke(s);
                    break;

                case NetIncomingMessageType.StatusChanged:
                    NetConnectionStatus status = (NetConnectionStatus)msg.ReadByte();
                    string statusText = msg.ReadString();
                    Main.Log("[STATUS UPDATE] " + status + " - " + statusText);
                    if(status == NetConnectionStatus.Connected)
                    {
                        Main.SetStatus("Connected to " + msg.SenderEndPoint.Address);
                        Main.Log("Connected!");
                    }
                    else if(status == NetConnectionStatus.Disconnected)
                    {
                        Main.Log("Disconnected from server. Reason: " + statusText);
                        Net.UponClientDisconnected();
                        MessageBox.Show(Main.Instance, "Disconnected from server: " + statusText, "Disconnected", MessageBoxButtons.OK);
                    }
                    else
                    {
                        Main.SetStatus(status.ToString());
                    }
                    break;

                default:
                    Main.Log("[CLIENT UNHANDLED] Type: " + msg.MessageType);
                    break;
            }

            client.Recycle(msg);
        }

        public static void ProcessData(DataType type, NetIncomingMessage msg, CClient c)
        {
            switch (type)
            {
                case DataType.AUDIO:
                    // Play this audio.
                    int bc = msg.ReadInt32();
                    byte[] bytes = msg.ReadBytes(bc);
                    Audio.QueuePlay(bytes, bc);
                    break;
                case DataType.MUTED:
                    bool muted = msg.ReadBoolean();
                    if (muted)
                    {
                        MessageBox.Show("You have been muted by the server owner. Nobody can hear you.", "Muted", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("You have been un-muted by the server owner. Welcome back!", "Un-Muted", MessageBoxButtons.OK);
                    }
                    break;
                case DataType.NAME:
                    int count = msg.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        bool add = msg.ReadBoolean();
                        string name = msg.ReadString();
                        if (add)
                            Main.AddUser(name);
                        else
                            Main.RemoveUser(name);
                    }
                    break;
            }
        }
    }
}
