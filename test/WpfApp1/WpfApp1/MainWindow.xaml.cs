using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Message> messages;
        TcpClient client;
        Thread actualizeThread;

        public MainWindow()
        {
            InitializeComponent();
            messages = new List<Message>();
            client = new TcpClient("127.0.0.1", 1200);
            //actualizeThread = new Thread(Actualize);
            //actualizeThread.Start();
        }

        void Actualize()
        {
            messages = new List<Message>();
            ScrollMessage.Children.Add(new TextBlock { Text = "> Connecté!", Foreground = Brushes.Wheat });
            ScrollMessage.Children.Add(new TextBlock { Text = "> Bienvenue!", Foreground = Brushes.Wheat });
            
            while (true)
            {
                //get Socket
                //ScrollMessage.Children.Add(new TextBlock { Text = msg.GetMessage(), Foreground = Brushes.White, TextWrapping = TextWrapping.Wrap });
                Thread.Sleep(1000);
            }
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            var msg = new Message(message.Text, pseudo.Text);
            ScrollMessage.Children.Add(new TextBlock { Text = msg.GetMessage(), Foreground = Brushes.White, TextWrapping = TextWrapping.Wrap });
            Byte[] data = Encoding.ASCII.GetBytes(msg.GetMessage());
            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);
            //send Socket
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var reponse = MessageBox.Show("Voullez vous quitter?", "Fermeture", MessageBoxButton.YesNo);
            if(reponse == MessageBoxResult.Yes)
            {
                client.Close();
                e.Cancel = false;
            }
            else if(reponse == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
