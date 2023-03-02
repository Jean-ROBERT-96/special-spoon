using MVVMTest2.Interfaces;
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
                propertyType: typeof(IContent),
                ownerType: typeof(DocsTreeView),
                new PropertyMetadata(null));

        public IContent CurrentContent
        {
            get => (IContent)GetValue(CurrentContentProperty);
            set => SetValue(CurrentContentProperty, value);
        }

        public string KeySelect
        {
            set => SelectByKey(value);
        }

        public DocsViewModel ViewModel { get; set; }
        private IContent _selectedContent;

        public DocsTreeView()
        {
            InitializeComponent();
        }

        private void treeView_Loaded(object sender, RoutedEventArgs e)
        {
            if (ViewModel == null)
                LoadViewModel();

            if(_selectedContent != null)
                SetSelectByKey(treeView, _selectedContent);
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            CurrentContent = (IContent)e.NewValue;
        }

        private void SelectByKey(string key)
        {
            if (ViewModel == null)
                LoadViewModel();

            _selectedContent = ViewModel.SearchTables(key);
        }

        private void LoadViewModel()
        {
            ViewModel = new DocsViewModel();
            ViewModel.InitValue();
            DataContext = ViewModel.DocsList;
        }

        private bool SetSelectByKey(ItemsControl tree, IContent item)
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
