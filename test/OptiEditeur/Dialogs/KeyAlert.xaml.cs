using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour KeyAlert.xaml
    /// </summary>
    public partial class KeyAlert : Window
    {
        private ObservableCollection<string> keys;
        private bool result = false;

        private KeyAlert(int mode, ObservableCollection<string> keys)
        {
            this.keys = keys;
            InitializeComponent();

            messageType.Text = mode switch
            {
                0 => "Les clés suivantes sont manquantes, voulez vous les ajouter?",
                1 => "Les clés suivantes n'existent pas ou ont été retirés, voulez vous les supprimer?",
                _ => string.Empty
            };

            DataContext = this.keys;
        }

        public static bool Show(int mode, ObservableCollection<string> keys)
        {
            var instance = new KeyAlert(mode, keys);
            instance.ShowDialog();
            return instance.result;
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            result = false;
            Close();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            result = true;
            Close();
        }
    }
}
