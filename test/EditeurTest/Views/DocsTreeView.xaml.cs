using EditeurTest.Interfaces;
using EditeurTest.ViewModels;
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

namespace EditeurTest.Views
{
    /// <summary>
    /// Logique d'interaction pour DocsTreeView.xaml
    /// </summary>
    public partial class DocsTreeView : UserControl
    {
        public static readonly DependencyProperty CurrentContentProperty =
            DependencyProperty.Register(
                name: "CurrentContent",
                propertyType: typeof(IContent),
                ownerType: typeof(DocsTreeView),
                new PropertyMetadata(null));

        public IContent CurrentContent
        {
            get => (IContent)GetValue(CurrentContentProperty);
            set => SetValue(CurrentContentProperty, value);
        }

        public DocsViewModel ViewModel { get; set; }

        public DocsTreeView()
        {
            InitializeComponent();
        }

        private void treeView_Loaded(object sender, RoutedEventArgs e)
        {
            if (ViewModel == null)
                LoadViewModel();
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var value = (IContent)e.NewValue;

            value.Content = ViewModel.AddContent(value.Key);

            CurrentContent = value;
        }

        private void LoadViewModel()
        {
            ViewModel = new DocsViewModel();
            ViewModel.InitValue();
            DataContext = ViewModel.DocsList;
        }
    }
}
