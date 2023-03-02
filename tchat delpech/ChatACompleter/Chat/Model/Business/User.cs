using Chat.Model.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace ChatServer.Model.Business
{
    public class User
    {
        //  DEFINITION ???
        public string data;
        public TcpClient client;
        Byte[] bytes = new Byte[256];

        private List<ILogger> logger = new List<ILogger>();

        public User(TcpClient client)
        {
            // ???
        }

        public void Chat()
        {
            // GET STREAM
            NetworkStream stream = client.GetStream();

            // WHILE => GET ALL DATA
            int i;
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                byte[] msg;

                // CONVERT DATA BYTES TO ASCII STRING.
                data = Encoding.ASCII.GetString(bytes, 0, i);
                Console.WriteLine("IN: {0}", data);

                Dictionary<string, string> paramList = data.Split('&').Select(m => m.Split('|'))
                    .ToDictionary(m => m.FirstOrDefault(), m => m.Skip(1).FirstOrDefault());

                // CONTROL IF FIRST CONNECTION
                if (paramList["action"] == "connection")
                {
                    login = paramList["user"];
                    msg = Encoding.ASCII.GetBytes("user|serveur&cryptage|NONE&message|CONNECTION OK");
                }
                else
                {
                    msg = Encoding.ASCII.GetBytes("user|" + login + "&" + data);
                }

                // RESPONSE SEND BACK
                Console.WriteLine("OUT: {0}", data);
                UserPool.Instance.SendBack(msg);

                // WRITE LOG
                Notify();
            }

            client.Close();
        }

        public void Detach(ILogger logger)
        {
            this.logger.Remove(logger);
        }

        public void Attach(ILogger logger)
        {
            this.logger.Add(logger);
        }

        public void Notify()
        {
            foreach (ILogger logger in this.logger)
            {
                logger.Update();
            }
        }
    }
}
