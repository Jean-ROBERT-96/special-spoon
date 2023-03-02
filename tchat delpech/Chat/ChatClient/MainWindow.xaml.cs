using Chat.Model.Business;
using ChatClient.Model.Business;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace ChatClient
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BackgroundWorker thread;
        NetworkStream serverStream = default(NetworkStream);
        TcpClient clientSocket = new TcpClient();
        Cryptage cryptage = new Cryptage();

        public MainWindow()
        {
            InitializeComponent();

            thread = new BackgroundWorker();
            thread.WorkerReportsProgress = true;
            thread.WorkerSupportsCancellation = true;
            thread.DoWork += Thread_DoWork;
            thread.ProgressChanged += Thread_ProgressChanged;
            thread.RunWorkerCompleted += Thread_RunWorkerCompleted;

            List<dynamic> securityList = new List<dynamic>();
            securityList.Add(new { Name = "Aucune sécurité", Value = "NONE" });
            securityList.Add(new { Name = "Chiffrement CYPHER", Value = "CYPHER" });
            securitySelect.ItemsSource = securityList;
            securitySelect.DisplayMemberPath = "Name";
            securitySelect.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageAdd(new Message { Text = "Connexion au serveur...", Color = Brushes.Red, Author = "Client" });
                clientSocket.Connect(serverHost.Text, int.Parse(serverPort.Text));
                serverStream = clientSocket.GetStream();

                byte[] outStream = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(new UserAction
                {
                    Author = serverLogin.Text,
                    Type = ActionType.Connection
                }));

                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();

                thread.RunWorkerAsync();
            }
            catch (Exception error)
            {
                MessageAdd(new Message { Text = error.Message, Color = Brushes.Red, Author = "Client" });
            }

        }

        private void Thread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageAdd(new Message { Text = "Ciao !", Color = Brushes.Orange, Author = "Client" });
        }

        private void Thread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Message message = e.UserState as Message;
            MessageAdd(new Message { Text = message.Text, Color = message.Color, Author = message.Author });
        }

        private void Thread_DoWork(object sender, DoWorkEventArgs e)
        {
            Byte[] bytes = new Byte[256];
            BackgroundWorker worker = sender as BackgroundWorker;
            while (worker.CancellationPending == false)
            {
                serverStream = clientSocket.GetStream();
                int i;
                while ((i = serverStream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    string message = Encoding.ASCII.GetString(bytes, 0, i);
                    var userAction = JsonConvert.DeserializeObject<UserAction>(message);

                    //Cryptage cryptage = new Cryptage();
                    //cryptage.SetStrategy(paramList["cryptage"]);

                    worker.ReportProgress(0, new Message
                    {
                        Text = userAction.Body,
                        Color = Brushes.Green,
                        Author = userAction.Author
                    });
                }

                Thread.Sleep(1000);
            }
        }

        private void MessageAdd(Message message)
        {
            Run newLine = new Run("> " + DateTime.Now.ToString("T") + " [" + message.Author + "] "
                + message.Text + "\n")
            { Foreground = message.Color };
            messageBox.Inlines.InsertBefore(messageBox.Inlines.FirstInline, newLine);
        }

        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(messageToSend.Text))
            {
                byte[] outStream = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(new UserAction
                {
                    Type = ActionType.SendMessage,
                    Body = cryptage.Encrypt(messageToSend.Text)
                }));
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();
            }
            else
            {
                MessageBox.Show("Veuillez renseigner un message !");
            }
        }

        private void SecuritySelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string strategy = ((sender as ComboBox).SelectedItem as dynamic).Value;
            cryptage.SetStrategy(strategy);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            thread.CancelAsync();
        }
    }
}
