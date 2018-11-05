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
        public Dictionary<string, NetConnection> connections = new Dictionary<string, NetConnection>();
        public List<NetConnection> muted = new List<NetConnection>();

        public bool LocalMuted = false; // Is the local user (host) muted?
        private byte[] buffer = new byte[1024 * 10]; // 10kb buffer, should be more than enough for any single package.

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

                    if(status == NetConnectionStatus.Disconnected)
                    {
                        string n = server.GetUsername(msg.SenderConnection);
                        if(n != null)
                        {
                            Main.Log("User '" + n + "' has disconnected from the server. Cya!");
                            server.SetName(msg.SenderConnection, null);
                            Main.RemoveUser(n);

                            var removeNameMsg = server.CreateMessage();
                            removeNameMsg.Write((byte)DataType.NAME);
                            removeNameMsg.Write(1);
                            removeNameMsg.Write(false);
                            removeNameMsg.Write(n);
                            server.SendToAll(removeNameMsg, NetDeliveryMethod.ReliableUnordered);
                        }
                    }
                    if(status == NetConnectionStatus.Connected)
                    {
                        // Send the names of all connected people.
                        var outMsg = server.CreateMessage();
                        outMsg.Write((byte)DataType.NAME);
                        outMsg.Write(server.names.Count + 1);
                        foreach (var item in server.names)
                        {
                            outMsg.Write(true);
                            outMsg.Write(item.Value);
                        }
                        outMsg.Write(true);
                        outMsg.Write(Main.ActiveUsername);
                        server.SendMessage(outMsg, msg.SenderConnection, NetDeliveryMethod.ReliableUnordered);

                        // Tell everyone else that this person joined...
                        var outMsg2 = server.CreateMessage();
                        outMsg2.Write((byte)DataType.NAME);
                        outMsg2.Write(1);
                        outMsg2.Write(true);
                        outMsg2.Write(server.GetUsername(msg.SenderConnection));
                        server.SendToAll(outMsg2, msg.SenderConnection, NetDeliveryMethod.ReliableUnordered, 0);
                    }
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

                case NetIncomingMessageType.WarningMessage:
                    Main.Log("[SERVER WARNING] " + msg.ReadString());
                    break;

                case NetIncomingMessageType.ConnectionApproval:
                    string name = msg.ReadString().Trim();
                    if (server.names.ContainsValue(name) || Main.ActiveUsername == name)
                    {
                        msg.SenderConnection.Deny("Somebody else on the server already has your username.");
                        Main.Log("Rejected connection from " + msg.SenderConnection + " because they have a duplicate username: " + name);
                    }
                    else
                    {
                        msg.SenderConnection.Approve();
                        server.SetName(msg.SenderConnection, name);

                        Main.Log("Approved connection from " + msg.SenderConnection.RemoteEndPoint + ", called '" + name + "', and sent the current names. (" + (server.names.Count + 1) + ")");
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

                    // Bounce to all other clients.
                    int c = msg.ReadInt32();
                    msg.ReadBytes(c, out s.buffer);

                    // Find speaker ID.
                    int id = Audio.GetOrCreateID(msg.SenderConnection);

                    var outMsg = s.CreateMessage();

                    outMsg.Write((byte)DataType.AUDIO);
                    outMsg.Write(id);
                    outMsg.Write(c);
                    outMsg.Write(s.buffer);

                    s.SendToAll(outMsg, msg.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);

                    // Play on local device.
                    Audio.QueuePlay(id, s.buffer, c);

                    break;
            }
        }

        public string GetUsername(NetConnection n)
        {
            if (!names.ContainsKey(n))
                return null;
            return names[n];
        }

        public NetConnection GetConnection(string name)
        {
            return connections[name];
        }

        protected void SetName(NetConnection n, string name)
        {
            if(name == null)
            {
                string na = names[n];
                connections.Remove(na);
                names.Remove(n);
                return;
            }

            if (names.ContainsKey(n))
            {
                throw new InvalidOperationException("Cannot change the name of an exising client.");
            }
            else
            {
                names.Add(n, name);
                connections.Add(name, n);
                Main.Log("User @ " + n.RemoteEndPoint + " has been assigned name '" + name + "'");
                Main.AddUser(name);
            }
        }

        public void SendAudio(byte[] buffer, int count)
        {
            if (LocalMuted)
                return;

            var msg = CreateMessage();
            msg.Write((byte)DataType.AUDIO);
            msg.Write(Audio.GetOrCreateID(null)); // Null because as the host we don't have a connection...
            msg.Write(count);
            msg.Write(buffer);

            this.SendToAll(msg, NetDeliveryMethod.ReliableOrdered);
        }

        public void Kick(string name)
        {
            if (connections.ContainsKey(name))
                Kick(connections[name]);
        }

        public void Kick(NetConnection n)
        {
            n.Disconnect("You have been kicked from the server by the host.");
        }

        public bool IsMuted(string name)
        {
            if (name == Main.ActiveUsername)
            {
                return LocalMuted;
            }

            if (connections.ContainsKey(name))
                return IsMuted(connections[name]);
            else
                return false;
        }

        public bool IsMuted(NetConnection n)
        {
            return muted.Contains(n);
        }

        public void Mute(string name)
        {
            if(name == Main.ActiveUsername)
            {
                LocalMuted = true;
                return;
            }

            if (connections.ContainsKey(name))
                Mute(connections[name]);
        }

        public void Mute(NetConnection n)
        {
            if (!muted.Contains(n))
            {
                muted.Add(n);

                // Notify the client.
                var msg = CreateMessage();
                msg.Write((byte)DataType.MUTED);
                msg.Write(true);
                SendMessage(msg, n, NetDeliveryMethod.ReliableUnordered);
            }
        }

        public void Unmute(string name)
        {
            if (name == Main.ActiveUsername)
            {
                LocalMuted = false;
                return;
            }

            if (connections.ContainsKey(name))
                Unmute(connections[name]);
        }

        public void Unmute(NetConnection n)
        {
            if (muted.Contains(n))
            {
                muted.Remove(n);

                // Notify the client.
                var msg = CreateMessage();
                msg.Write((byte)DataType.MUTED);
                msg.Write(false);
                SendMessage(msg, n, NetDeliveryMethod.ReliableUnordered);
            }
        }
    }
}
