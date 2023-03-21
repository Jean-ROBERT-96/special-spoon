using OptiEditeur.Models;
using OptiEditeur.Services;
using OptiEditeur.ViewModels;
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
using System.Xml.Linq;

namespace OptiEditeur.Views
{
    /// <summary>
    /// Logique d'interaction pour KeyManager.xaml
    /// </summary>
    public partial class KeyManager : Window
    {
        private int mode;
        private MainWindow main;

        private KeyManager(int mode)
        {
            main = (MainWindow)Application.Current.MainWindow;
            this.mode = mode;
            InitializeComponent();
            switch (mode)
            {
                case 0:
                    validButton.Content = "Ajouter la clé";
                    DataContext = main.DataContext.KeyToAdd;
                    break;
                case 1:
                    validButton.Content = "supprimer la clé";
                    DataContext = main.DataContext.KeyToRemove;
                    break;
            }
        }

        public static void Show(int mode)
        {
            var instance = new KeyManager(mode);
            instance.ShowDialog();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
                validButton.IsEnabled = true;
            else
                validButton.IsEnabled = false;
        }

        private void SearchKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchKey.Text.Length == 0)
            {
                DataContext = mode switch
                {
                    0 => main.DataContext.KeyToAdd,
                    1 => main.DataContext.KeyToRemove,
                    _ => throw new NotImplementedException()
                };
            }
            else
            {
                DataContext = mode switch
                {
                    0 => main.DataContext.KeyToAdd.Where(x => x.Contains(searchKey.Text, StringComparison.OrdinalIgnoreCase)).ToList(),
                    1 => main.DataContext.KeyToRemove.Where(x => x.Contains(searchKey.Text, StringComparison.OrdinalIgnoreCase)).ToList(),
                    _ => throw new NotImplementedException()
                };
            }
        }

        private bool TablesExist(ObservableCollection<Tables> tables, string key)
        {
            bool result = false;

            foreach (var table in tables)
            {
                if(table.Key.Equals(key))
                    result = true;
                else if(table.Table.Count > 0)
                { 
                    bool res = TablesExist(table.Table, key);
                    if(res) result = true;
                }
            }

            return result;
        }

        private void ValidButton(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Voulez vous modifier le document?", "Edition", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                string[] selected = listView.SelectedItems.Cast<string>().ToArray();
                for (int i = 0; i < selected.Length; i++)
                {
                    if(mode == 0)
                    {
                        main.DataContext.KeyToAdd.Remove(selected[i]);
                        if (!main.DataContext.KeyToRemove.Contains(selected[i]))
                            main.DataContext.KeyToRemove.Add(selected[i]);

                        List<string> split = new(selected[i].Split('.'));
                        for(int j = 2; j <= split.Count; j++)
                        {
                            var getKey = split.GetRange(0, j);
                            var key = String.Join('.', getKey);
                            if (!TablesExist(main.DataContext.DocsList, key))
                                main.DataContext.DocsList.AddChild(key);

                            main.DataContext.KeyToAdd.Remove(key);
                            if (!main.DataContext.KeyToRemove.Contains(key))
                                main.DataContext.KeyToRemove.Add(key);
                        }
                    }

                    if (mode == 0)
                    {
                        foreach(var table in main.DataContext.DocsList)
                            table.Table.RemoveChild(x => x.Key == selected[i]);

                        main.DataContext.KeyToRemove.Remove(selected[i]);
                        if (!main.DataContext.KeyToAdd.Contains(selected[i]))
                            main.DataContext.KeyToAdd.Add(selected[i]);

                        var child = main.DataContext.KeyToRemove.Where(x => x.StartsWith(selected[i])).ToArray();
                        foreach(var item in child)
                        {
                            main.DataContext.KeyToRemove.Remove(item);
                            if(!main.DataContext.KeyToAdd.Contains(item))
                                main.DataContext.KeyToAdd.Add(item);
                        }
                    }
                }
            }
        }
    }
}
