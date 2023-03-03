using OptiEditeur.Models;
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
    /// Logique d'interaction pour TablesTreeView.xaml
    /// </summary>
    public partial class TablesTreeView : UserControl
    {
        private MainWindow _mainWindow;

        public static readonly DependencyProperty CurrentContentProperty =
            DependencyProperty.Register(
                "CurrentContent",
                typeof(Tables),
                typeof(TablesTreeView),
                new PropertyMetadata(null));

        public Tables CurrentContent
        {
            get => (Tables)GetValue(CurrentContentProperty);
            set => SetValue(CurrentContentProperty, value);
        }

        public TablesTreeView()
        {
            _mainWindow = (MainWindow)Application.Current.MainWindow;
            InitializeComponent();
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            CurrentContent = (Tables)e.NewValue;
        }

        public void ActualizeContext()
        {
            DataContext = _mainWindow.DataContext.TableList;
        }

        private void treeView_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = _mainWindow.DataContext.TableList;
        }
    }
}
