using OptiEditeur.Services;
using OptiEditeur.ViewModels;
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
using System.Xml.Linq;

namespace OptiEditeur.Views
{
    /// <summary>
    /// Logique d'interaction pour KeyManager.xaml
    /// </summary>
    public partial class KeyManager : Window
    {
        private int _mode;

        public KeyManager(int mode)
        {
            _mode = mode;
            InitializeComponent();
            switch (mode)
            {
                case 0:
                    validButton.Content = "Ajouter la clé";
                    DataContext = TablesViewModel.KeyToAdd;
                    break;
                case 1:
                    validButton.Content = "supprimer la clé";
                    DataContext = TablesViewModel.KeyToDelete;
                    break;
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
                validButton.IsEnabled = true;
            else
                validButton.IsEnabled = false;
        }

        private void ValidButton(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Voulez vous modifier le document?", "Edition", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                for (int i = 0; i < listView.Items.Count; i++)
                {
                    switch (_mode)
                    {
                        case 0:
                            TablesViewModel.TableList.AddChild((string)listView.Items[i]);
                            TablesViewModel.KeyToAdd.Remove((string)listView.Items[i]);
                            DataContext = TablesViewModel.KeyToAdd;
                            break;
                        case 1:
                            foreach (var tables in TablesViewModel.TableList)
                                tables.Table.DeleteChild(x => x.Key == (string)listView.Items[i]);
                            TablesViewModel.KeyToDelete.Remove((string)listView.Items[i]);
                            DataContext = TablesViewModel.KeyToDelete;
                            break;
                    }
                }
            }
        }

        private void SearchKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchKey.Text.Length == 0 && _mode == 0)
                listView.ItemsSource = TablesViewModel.KeyToAdd;
            else if (searchKey.Text.Length == 0 && _mode == 1)
                listView.ItemsSource = TablesViewModel.KeyToDelete;
            else
            {
                List<string> result = new();

                if(_mode == 0)
                    result = TablesViewModel.KeyToAdd.Where(x => x.Contains(searchKey.Text, StringComparison.OrdinalIgnoreCase)).ToList();
                if(_mode == 1)
                    result = TablesViewModel.KeyToDelete.Where(x => x.Contains(searchKey.Text, StringComparison.OrdinalIgnoreCase)).ToList();

                listView.ItemsSource = result;
            }
        }
    }
}
