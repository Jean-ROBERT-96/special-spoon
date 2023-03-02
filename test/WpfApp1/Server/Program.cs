using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int id = 0;

            Console.WriteLine("Starting TCP listener...");
            TcpListener listener = new TcpListener(IPAddress.Any, 1200);

            listener.Start();
            Console.WriteLine("Start!");

            while(true)
            {
                id++;
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("New connection.");

                User user = new User(id, client);
                UserPool.GetInstance().AddUser(user);
                Thread th = new Thread(user.Live);
                th.Start();
            }
        }

        public static void Message(TcpClient client)
        {
            while (true)
            {
                byte[] buffer = new byte[1024];
                var recep = client.ReceiveBufferSize;
                string msg = Encoding.ASCII.GetString(buffer, 0, recep);
                Console.WriteLine(msg);
            }
        }
    }
}