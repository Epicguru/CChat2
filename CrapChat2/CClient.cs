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
    }
}
