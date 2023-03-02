using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace HelpEditor.Views
{
    /// <summary>
    /// Logique d'interaction pour NewDocs.xaml
    /// </summary>
    public partial class NewDocs : Window
    {
        private static NewDocs? _newDocs;
        MainWindow main;

        private NewDocs()
        {
            InitializeComponent();
            main = (MainWindow)System.Windows.Application.Current.MainWindow;
        }

        public static NewDocs GetInstance()
        {
            if(_newDocs == null)
                _newDocs = new NewDocs();

            return _newDocs;
        }

        private void FileExplorer_Click(object sender, RoutedEventArgs e)
        {
            var autre = new FolderBrowserDialog
            {
                ShowNewFolderButton = true
            };

            autre.ShowDialog();
            PathDocs.Text = autre.SelectedPath;
        }

        private void Valid_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(PathDocs.Text))
            {
                main.docsTreeView.ViewModel.CreateValue(PathDocs.Text);
                main.docsTreeView.TreeViewDataContext();
                this.Close();
                _newDocs = null;
            }
            else
                System.Windows.MessageBox.Show("Le chemin spécifié n'existe pas.", "Erreur de chemin", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            _newDocs = null;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _newDocs = null;
        }
    }
}
