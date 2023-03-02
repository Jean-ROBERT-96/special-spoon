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
        public static readonly DependencyProperty CurrentContentProperty =
            DependencyProperty.Register(
                "CurrentContent",
                typeof(Tables),
                typeof(TablesGridView),
                new PropertyMetadata(null));

        public Tables CurrentContent
        {
            get => (Tables)GetValue(CurrentContentProperty);
            set => SetValue(CurrentContentProperty, value);
        }

        public TablesGridView()
        {
            InitializeComponent();
        }

        private void gridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count > 0)
                CurrentContent = (Tables)e.AddedItems[0];
        }

        private void gridView_Loaded(object sender, RoutedEventArgs e)
        {
            if (TablesViewModel.TableList.Count > 0)
                DataContext = TablesViewModel.TablesListing(TablesViewModel.TableList);
        }

        public void ActualizeContext()
        {
            if (TablesViewModel.TableList.Count > 0)
                DataContext = TablesViewModel.TablesListing(TablesViewModel.TableList);
        }
    }
}
