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
using System.Windows.Shapes;

namespace OptiEditeur.Views
{
    /// <summary>
    /// Logique d'interaction pour AddLink.xaml
    /// </summary>
    public partial class AddLink : Window
    {
        private static AddLink link;
        private static string result;

        public AddLink()
        {
            InitializeComponent();
        }

        public static string Show()
        {
            link = new AddLink();
            link.ShowDialog();
            return result;
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            link.Close();
        }

        private void ValidButton(object sender, RoutedEventArgs e)
        {
            result = linkBox.Text;
            link.Close();
        }
    }
}
