using HelpEditor.Interfaces;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HelpEditor.Views
{
    /// <summary>
    /// Logique d'interaction pour DocsTreeView.xaml
    /// </summary>
    public partial class DocsTreeView : UserControl
    {
        public static readonly DependencyProperty CurrentContentProperty =
            DependencyProperty.Register(
                "CurrentContent",
                typeof(IContent),
                typeof(DocsTreeView),
                new PropertyMetadata(null));

        public IContent CurrentContent
        {
            get => (IContent)GetValue(CurrentContentProperty);
            set => SetValue(CurrentContentProperty, value);
        }

        public DocsViewModel ViewModel { get; set; } = new();

        public DocsTreeView()
        {
            InitializeComponent();
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            CurrentContent = (IContent)e.NewValue;
        }

        public void TreeViewDataContext()
        {
            DataContext = DocsViewModel.DocsList;
        }
    }
}
