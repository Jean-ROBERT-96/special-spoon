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
    /// Logique d'interaction pour DocsTreeView.xaml
    /// </summary>
    public partial class DocsTreeView : UserControl
    {
        public static readonly DependencyProperty CurrentContentProperty =
            DependencyProperty.Register(
                name: "CurrentContent",
                propertyType: typeof(Tables),
                ownerType: typeof(DocsTreeView),
                new PropertyMetadata(null));

        public Tables CurrentContent
        {
            get => (Tables)GetValue(CurrentContentProperty);
            set => SetValue(CurrentContentProperty, value);
        }

        public string KeySelect
        {
            set => SelectByKey(value);
        }

        private Tables _selectedContent;
        private MainWindow _main;

        public DocsTreeView()
        {
            _main = (MainWindow)Application.Current.MainWindow;
            InitializeComponent();
        }

        private void treeView_Loaded(object sender, RoutedEventArgs e)
        {
            if(_selectedContent != null)
                SetSelectByKey(treeView, _selectedContent);
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            CurrentContent = (Tables)e.NewValue;
        }

        private void SelectByKey(string key)
        {
            _selectedContent = _main.DataContext.SearchTables(key);
        }

        private bool SetSelectByKey(ItemsControl tree, Tables item)
        {
            foreach (var i in tree.Items)
            {
                TreeViewItem currentContent = (TreeViewItem)tree.ItemContainerGenerator.ContainerFromItem(i);
                if ((currentContent != null) && (i == item))
                {
                    currentContent.IsSelected = true;
                    currentContent.BringIntoView();
                    return true;
                }
            }

            foreach (var i in tree.Items)
            {
                TreeViewItem currentContent = (TreeViewItem)tree.ItemContainerGenerator.ContainerFromItem(i);
                if ((currentContent != null) && (currentContent.Items.Count > 0))
                {
                    currentContent.IsExpanded = true;
                    currentContent.UpdateLayout();
                    if (!SetSelectByKey(currentContent, item))
                        currentContent.IsExpanded = false;
                    else
                        return true;
                }
            }
            return false;
        }
    }
}
