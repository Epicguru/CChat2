using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CrapChat
{
    public struct FoundServer
    {
        public string Name;
        public IPEndPoint EndPoint;

        public override string ToString()
        {
            return Name == null ? "" : Name.Trim();
        }

        public bool IsInvalid()
        {
            return this.EndPoint == null;
        }
    }
}
