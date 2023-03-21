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
        private string result;

        private AddLink()
        {
            InitializeComponent();
        }

        public static new string Show()
        {
            var instance = new AddLink();
            instance.ShowDialog();
            return instance.result;
        }

        private void ValidButton(object sender, RoutedEventArgs e)
        {
            if (!linkBox.Text.StartsWith("http://") || !linkBox.Text.StartsWith("https://"))
                result = $"http://{linkBox.Text}";
            else
                result = linkBox.Text;
            Close();
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            result = null;
            Close();
        }
    }
}
