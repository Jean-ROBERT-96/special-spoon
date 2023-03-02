using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Communication
    {
        public ActionType action { get; set; }
        public string author { get; set; }
        public string body { get; set; }
    }

    public enum ActionType
    {
        SendMessage,
        Connection,
        LogOut,
        GetConnectedUsers
    }
}
