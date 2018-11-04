using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CrapChat
{
    public class CServer : NetServer
    {
        public string Name { get; protected set; }

        public CServer(string name, NetPeerConfiguration config)
            : base(config)
        {
            Name = name;
            this.RegisterReceivedCallback(new SendOrPostCallback(GotMessage));
        }

        public static void GotMessage(object peer)
        {
            var server = (CServer)peer;
            var msg = server.ReadMessage();

            switch (msg.MessageType)
            {
                case NetIncomingMessageType.Error:
                    string errorText;
                    bool worked = msg.ReadString(out errorText);
                    Main.Log("[SERVER ERROR] " + (worked ? errorText : "(???)"));
                    break;

                case NetIncomingMessageType.StatusChanged:
                    byte b = msg.ReadByte();
                    string statusText = msg.ReadString();
                    NetConnectionStatus status = (NetConnectionStatus)b;

                    Main.Log("[STATUS UPDATE] " + status + " - " + statusText);
                    break;

                case NetIncomingMessageType.Data:
                    break;

                case NetIncomingMessageType.DiscoveryRequest:
                    NetOutgoingMessage response = server.CreateMessage(server.Name);
                    server.SendDiscoveryResponse(response, msg.SenderEndPoint);                    
                    break;

                case NetIncomingMessageType.ConnectionApproval:
                    string name = msg.ReadString();
                    Thread.Sleep(1000);
                    msg.SenderConnection.Deny("Duplicate name!");
                    break;

                default:
                    Main.Log("[SERVER UNHANDLED] Type: " + msg.MessageType);
                    break;
            }

            server.Recycle(msg);
        }
    }
}
