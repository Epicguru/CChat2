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
        public Dictionary<NetConnection, string> names = new Dictionary<NetConnection, string>();
        public List<NetConnection> muted = new List<NetConnection>();

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
                    // Find the type.
                    var type = (DataType)msg.ReadByte();
                    ProcessData(type, msg, server);
                    break;

                case NetIncomingMessageType.DiscoveryRequest:
                    NetOutgoingMessage response = server.CreateMessage(server.Name);
                    server.SendDiscoveryResponse(response, msg.SenderEndPoint);                    
                    break;

                case NetIncomingMessageType.ConnectionApproval:
                    string name = msg.ReadString().Trim();
                    if (server.names.ContainsValue(name))
                    {
                        msg.SenderConnection.Deny("Somebody else on the server already has your username.");
                    }
                    else
                    {
                        msg.SenderConnection.Approve();
                        server.SetName(msg.SenderConnection, name);
                    }
                    break;

                default:
                    Main.Log("[SERVER UNHANDLED] Type: " + msg.MessageType);
                    break;
            }

            server.Recycle(msg);
        }

        public static void ProcessData(DataType type, NetIncomingMessage msg, CServer s)
        {
            switch (type)
            {
                case DataType.AUDIO:
                    bool muted = s.IsMuted(msg.SenderConnection);
                    // Ignore if muted, just don't send.
                    if (muted)
                        break;

                    var outMsg = s.CreateMessage();
                    outMsg.Write((byte)DataType.AUDIO);
                    int c = msg.ReadInt32();
                    outMsg.Write(c);
                    outMsg.Write(msg.ReadBytes(c));
                    break;
            }
        }

        public string GetUsername(NetConnection n)
        {
            if (!names.ContainsKey(n))
                return "[missing]";
            return names[n];
        }

        protected void SetName(NetConnection n, string name)
        {
            if (names.ContainsKey(n))
            {
                Main.Log("Updated user's name from '" + names[n] + "' to '" + name + "'");
                names[n] = name;
            }
            else
            {
                names.Add(n, name);
                Main.Log("User @ " + n.RemoteEndPoint + " has the assigned name '" + name + "'");
            }
        }

        public bool IsMuted(NetConnection n)
        {
            return muted.Contains(n);
        }

        public void Mute(NetConnection n)
        {
            if(!muted.Contains(n))
                muted.Add(n);
        }

        public void Unmute(NetConnection n)
        {
            if (muted.Contains(n))
                muted.Remove(n);
        }
    }
}
