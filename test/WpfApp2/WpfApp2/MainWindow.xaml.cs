using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Window1 window1;
        Window2 window2;
        Window3 window3;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenWindow1_Click(object sender, RoutedEventArgs e)
        {
            window1 = new Window1();
            window1.Owner = this;
            window1.Show();
        }

        private void OpenWindow2_Click(object sender, RoutedEventArgs e)
        {
            window2 = new Window2();
            window2.Owner = this;
            window2.Show();
        }

        private void OpenWindow3_Click(object sender, RoutedEventArgs e)
        {
            window3 = new Window3();
            window3.Owner = this;
            window3.Show();
        }

        public static void Destruct(Window window)
        {
            MessageBox.Show("Fenêtre détruite!");
        }
    }
}
