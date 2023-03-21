using OptiEditeur.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using Application = System.Windows.Application;

namespace OptiEditeur.Views
{
    /// <summary>
    /// Logique d'interaction pour NewDocs.xaml
    /// </summary>
    public partial class NewDocs : Window
    {
        private MainWindow main;
        private bool result = false;

        private NewDocs()
        {
            InitializeComponent();
            main = (MainWindow)Application.Current.MainWindow;
        }

        public static new string Show()
        {
            var instance = new NewDocs();
            instance.ShowDialog();
            if (instance.result)
                return instance.PathDocs.Text;

            return string.Empty;
        }

        private void FileExplorer(object sender, RoutedEventArgs e)
        {
            var autre = new FolderBrowserDialog { ShowNewFolderButton = true };
            if(autre.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                PathDocs.Text = autre.SelectedPath;
        }

        private void Valid(object sender, RoutedEventArgs e)
        {
            if(Directory.Exists(PathDocs.Text))
            {
                result = true;
                Close();
            }
            else
                System.Windows.MessageBox.Show("Le chemin spécifié n'existe pas.", "Erreur de chemin", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
