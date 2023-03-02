using HelpEditor.Ressources;
using HelpEditor.ViewModels;
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

namespace HelpEditor.Views
{
    /// <summary>
    /// Logique d'interaction pour ManageKeys.xaml
    /// </summary>
    public partial class ManageKeys : Window
    {
        private int _mode;

        public ManageKeys(int mode)
        {
            _mode = mode;

            InitializeComponent();

            switch (mode)
            {
                case 0:
                    validButton.Content = "Ajouter la clé";
                    DataContext = DocsViewModel.KeyToAdd;
                    break;
                case 1:
                    validButton.Content = "supprimer la clé";
                    DataContext = DocsViewModel.KeyToRemove;
                    break;
            }
        }

        private void validButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Voulez vous effectuer l'action?", "Edition", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                switch(_mode)
                {
                    case 0:
                        DocsViewModel.DocsList.AddChild((string)listView.SelectedItem);
                        DocsViewModel.KeyToAdd.Remove((string)listView.SelectedItem);
                        DataContext = DocsViewModel.KeyToAdd;
                        break;
                    case 1:
                        foreach(var docs in DocsViewModel.DocsList)
                            docs.Table.RemoveChild(x => x.Key == (string)listView.SelectedItem);
                        DocsViewModel.KeyToRemove.Remove((string)listView.SelectedItem);
                        DataContext = DocsViewModel.KeyToRemove;
                        break;
                }
            }
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count > 0)
                validButton.IsEnabled = true;
            else
                validButton.IsEnabled = false;
        }
    }
}
