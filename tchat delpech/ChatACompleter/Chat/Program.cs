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
                Byte[] bytes = new Byte[256];
                String data = null;

                while (true)
                {
                    TcpClient client = serverSocket.AcceptTcpClient();

                    // ADD NEW USER
                    User user = new User(client);
                    user.Attach(new FileText(user));

                    // AJOUT USER AU POOL DE USER ???
                    
                    // DEMARRAGE DU THREAD ???
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
