using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Server
{
    public class User
    {
        public int Id { get; init; }
        public string Data { get; set; }
        public TcpClient TcpClient { get; init; }
        private byte[] bytes = new byte[8192];

        public User(int id, TcpClient tcpClient)
        {
            Id = id;
            TcpClient = tcpClient;
        }

        public void Live()
        {
            NetworkStream stream = TcpClient.GetStream();

            int i;
            while((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                byte[] msg;

                Data = Encoding.ASCII.GetString(bytes, 0, i);
                Console.WriteLine($"IN: {Data}");
            }
        }

        void Stream(Communication communication)
        {

        }
    }
}
