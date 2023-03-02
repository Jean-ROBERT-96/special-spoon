using Chat.Model.Business;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace ChatServer.Model.Business
{
    public class User
    {
        public string login;
        public string data;
        public TcpClient client;
        byte[] bytes = new byte[256];

        private List<ILogger> logger = new List<ILogger>();

        public User(TcpClient client)
        {
            this.client = client;
        }

        public void Live()
        {
            // GET STREAM
            NetworkStream stream = client.GetStream();

            // WHILE => GET ALL DATA
            int i;
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                byte[] msg;

                // CONVERT DATA BYTES TO STRING.
                data = Encoding.ASCII.GetString(bytes, 0, i);
                Console.WriteLine("IN: {0}", data);

                // CONVERT TO OBJECT ACTION
                var action = JsonConvert.DeserializeObject<UserAction>(data);

                // CONTROL IF FIRST CONNECTION
                if (action.Type == ActionType.Connection)
                {
                    login = action.Author;
                    msg = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(new UserAction
                    {
                        Author = login,
                        Type = ActionType.Answer,
                        Body = $"{login} connected !"
                    }));
                }
                else
                {
                    msg = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(new UserAction
                    {
                        Author = login,
                        Type = ActionType.Answer,
                        Body = action.Body
                    }));
                }

                // RESPONSE SEND BACK
                Console.WriteLine("OUT: {0}", data);
                UserPool.Instance.SendBack(msg);

                // WRITE LOG
                // Notify();
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
