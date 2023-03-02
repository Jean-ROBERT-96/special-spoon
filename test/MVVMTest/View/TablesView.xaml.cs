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
    /// Logique d'interaction pour TablesView.xaml
    /// </summary>
    public partial class TablesView : UserControl
    {
        public static readonly DependencyProperty CurrentTablesProperty =
            DependencyProperty.Register(
                name: "CurrentDoc",
                propertyType: typeof(Tables),
                ownerType: typeof(TablesView),
                new PropertyMetadata(null));

        public Tables CurrentDoc
        {
            get => (Tables)GetValue(CurrentTablesProperty);
            set => SetValue(CurrentTablesProperty, value);
        }

        public TablesView()
        {
            InitializeComponent();
        }

        private void searchList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentDoc = (Tables)e.AddedItems[0];
        }
    }
}
