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
                        Net.UponClientConnected();
                    }
                    else if(status == NetConnectionStatus.Disconnected)
                    {
                        Main.Log("Disconnected from server. Reason: " + statusText);
                        Net.UponClientDisconnected();
                        MessageBox.Show("Disconnected from server: " + statusText, "Disconnected", MessageBoxButtons.OK);
                    }
                    else
                    {
                        Main.SetStatus(status.ToString());
                    }
                    break;

                case NetIncomingMessageType.Data:
                    var type = (DataType)msg.ReadByte();
                    ProcessData(type, msg, client);
                    break;

                default:
                    Main.Log("[CLIENT UNHANDLED] Type: " + msg.MessageType);
                    break;
            }

            client.Recycle(msg);
        }

        private static byte[] buffer = new byte[1024 * 10];
        public static void ProcessData(DataType type, NetIncomingMessage msg, CClient c)
        {
            //Main.Log("Got data of type " + type + ", message is " + msg + ", size " + msg.LengthBytes);
            switch (type)
            {
                case DataType.AUDIO:
                    // Play this audio.

                    int id = msg.ReadInt32();
                    int bc = msg.ReadInt32();
                    msg.ReadBytes(bc, out buffer);

                    Audio.QueuePlay(id, buffer, bc);
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
                    Main.Log("Recieving " + count + " names.");
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

        public void SendAudio(byte[] buffer, int count)
        {
            var msg = this.CreateMessage();
            msg.Write((byte)DataType.AUDIO);
            msg.Write(count);
            msg.Write(buffer);
            SendMessage(msg, NetDeliveryMethod.ReliableOrdered);
        }
    }
}
