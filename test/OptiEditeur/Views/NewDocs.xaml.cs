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
        private static NewDocs? _newDocs;
        private MainWindow _mainWindow;

        private NewDocs()
        {
            InitializeComponent();
            _mainWindow = (MainWindow)Application.Current.MainWindow;
        }

        public static void Show()
        {
            _newDocs ??= new NewDocs();
            _newDocs.ShowDialog();
        }

        private void FileExplorer(object sender, RoutedEventArgs e)
        {
            var autre = new FolderBrowserDialog { ShowNewFolderButton = true };
            autre.ShowDialog();
            PathDocs.Text = autre.SelectedPath;
        }

        private void Valid(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(PathDocs.Text))
            {
                _mainWindow.DataContext.CreateValue(PathDocs.Text);
                //_mainWindow.tablesTreeView.ActualizeContext();
                //_mainWindow.tablesGridView.ActualizeContext();
                Close();
                _newDocs = null;
            }
            else
                System.Windows.MessageBox.Show("Le chemin spécifié n'existe pas.", "Erreur de chemin", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
            _newDocs = null;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _newDocs = null;
        }
    }
}
