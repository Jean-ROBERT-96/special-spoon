using MVVMTest2.Models;
using MVVMTest2.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVVMTest2.Views
{
    /// <summary>
    /// Logique d'interaction pour TablesListView.xaml
    /// </summary>
    public partial class TablesListView : UserControl
    {
        public static readonly DependencyProperty CurrentContentProperty =
            DependencyProperty.Register(
                name: "CurrentContent",
                propertyType: typeof(Tables),
                ownerType: typeof(TablesListView),
                new PropertyMetadata(null));

        public Tables CurrentContent
        {
            get => (Tables)GetValue(CurrentContentProperty);
            set => SetValue(CurrentContentProperty, value);
        }

        private MainWindow _main;

        public TablesListView()
        {
            _main = (MainWindow)Application.Current.MainWindow;
            InitializeComponent();
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count > 0)
                CurrentContent = (Tables)e.AddedItems[0];
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchBox.Text.Length == 0)
                listBox.ItemsSource = _main.DataContext.TablesList;
            else
            {
                var result = new ObservableCollection<Tables>();
                foreach(var item in _main.DataContext.TablesList)
                {
                    if(item.Name.Contains(searchBox.Text, StringComparison.OrdinalIgnoreCase))
                        result.Add(item);
                }
                listBox.ItemsSource = result;
            }
        }
    }
}
