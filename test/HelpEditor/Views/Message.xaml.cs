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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HelpEditor.Views
{
    /// <summary>
    /// Logique d'interaction pour Message.xaml
    /// </summary>
    public partial class Message : Window
    {
        private ObservableCollection<string> _keys;
        private static DialogResult _result = System.Windows.Forms.DialogResult.No;
        private static Message _instance;

        public Message(int mode, ObservableCollection<string> key)
        {
            _instance= this;

            _keys = key;

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

        public static DialogResult Show(int mode, ObservableCollection<string> key)
        {
            _instance = new(mode, key);
            _instance.ShowDialog();
            return _result;
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            _result = System.Windows.Forms.DialogResult.No;
            _instance.Close();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            _result = System.Windows.Forms.DialogResult.Yes;
            _instance.Close();
        }
    }
}
