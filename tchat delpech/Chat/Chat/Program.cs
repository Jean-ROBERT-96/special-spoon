using Chat.Model.Business;
using Chat.Model.Business.Logger;
using ChatServer.Model.Business;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            int serverPort = 8888;
            IPAddress serverAddress = IPAddress.Parse("127.0.0.1");
            TcpListener serverSocket = null;

            try
            {
                serverSocket = new TcpListener(serverAddress, serverPort);

                // START SOCKET
                serverSocket.Start();
                Console.WriteLine("Bienvenue sur le chat server !");

                // BUFFER FOR READING DATA
                byte[] bytes = new byte[256];

                while (true)
                {
                    TcpClient client = serverSocket.AcceptTcpClient();

                    // ADD NEW USER
                    User user = new User(client);
                    UserPool.Instance.AddUser(user);
                    //user.Attach(new FileText(user));

                    Thread thread = new Thread(() => user.Live());
                    thread.Start();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Exception: {0}", error);
            }
            finally
            {
                serverSocket.Stop();
            }
        }
    }
}
