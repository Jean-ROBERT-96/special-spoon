using OptiEditeur.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OptiEditeur.Views
{
    /// <summary>
    /// Logique d'interaction pour TablesGridView.xaml
    /// </summary>
    public partial class TablesGridView : UserControl
    {
        private MainWindow main;

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(
                "CurrentContent",
                typeof(Tables),
                typeof(TablesGridView),
                new PropertyMetadata(null));

        public Tables CurrentContent
        {
            get => (Tables)GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        public TablesGridView()
        {
            main = (MainWindow)Application.Current.MainWindow;
            InitializeComponent();
        }

        private void gridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count > 0)
                CurrentContent = (Tables)e.AddedItems[0];
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchBox.Text.Length > 0)
                gridView.ItemsSource = main.DataContext.TablesList;
            else
            {
                gridView.ItemsSource = main.DataContext.TablesList.Where(x => x.Key.Contains(searchBox.Text, StringComparison.OrdinalIgnoreCase) ||
                                                                              x.Name.Contains(searchBox.Text, StringComparison.OrdinalIgnoreCase));
            }
        }
    }
}
