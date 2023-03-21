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
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(
                "CurrentContent",
                typeof(Tables),
                typeof(TablesTreeView),
                new PropertyMetadata(null));

        public Tables CurrentContent
        {
            get => (Tables)GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        public TablesTreeView()
        {
            InitializeComponent();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            CurrentContent = (Tables)e.NewValue;
        }
    }
}
