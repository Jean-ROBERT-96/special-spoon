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
        private ObservableCollection<string> _keys;
        private static bool _result = false;
        private static KeyAlert _instance;

        private KeyAlert(int mode, ObservableCollection<string> keys)
        {
            _keys = keys;
            InitializeComponent();

            switch (mode)
            {
                case 0:
                    messageType.Text = "Les clés suivantes sont manquantes, voulez vous les ajouter?";
                    break;
                case 1:
                    messageType.Text = "Les clés suivantes n'existent pas ou ont été retirés, voulez vous les supprimer?";
                    break;
                default:
                    Close();
                    break;
            }

            DataContext = _keys;
        }

        public static bool Show(int mode, ObservableCollection<string> keys)
        {
            _instance = new KeyAlert(mode, keys);
            _instance.ShowDialog();
            return _result;
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            _result = false;
            _instance.Close();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            _result = true;
            _instance.Close();
        }
    }
}
