using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Message
    {
        string Msg;
        string Pseudo;
        DateTime Date;

        public Message(string msg, string pseudo)
        {
            Msg = msg;
            Pseudo = pseudo;
            Date = DateTime.Now;
        }

        public string GetMessage()
        {
            return $"[{Date.ToString("HH:mm:ss")}] - [{Pseudo}] : {Msg}";
        }
    }
}
