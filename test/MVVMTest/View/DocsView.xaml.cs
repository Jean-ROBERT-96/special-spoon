using MVVMTest.Interfaces;
using MVVMTest.Models;
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

namespace MVVMTest.View
{
    /// <summary>
    /// Logique d'interaction pour DocsView.xaml
    /// </summary>
    public partial class DocsView : UserControl
    {
        public static readonly DependencyProperty CurrentDocProperty =
            DependencyProperty.Register(
                name: "CurrentDoc",
                propertyType: typeof(INode),
                ownerType: typeof(DocsView),
                new PropertyMetadata(null));

        public INode CurrentDoc
        {
            get => (INode)GetValue(CurrentDocProperty);
            set => SetValue(CurrentDocProperty, value);
        }

        public DocsView()
        {
            InitializeComponent();
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            CurrentDoc = (INode)e.NewValue;
        }

        private static void OnCurrentDocChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (DocsView)d;
        }

        private void treeView_Loaded(object sender, RoutedEventArgs e)
        {
            Tables? tab = null;
            SetSelectByKey(treeView, tab);
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
