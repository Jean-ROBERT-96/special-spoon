using MVVMTest.ViewModels;
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

namespace MVVMTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DocsViewModel docsViewModelObject;
        TablesViewModel tablesViewModelObject;
        string key;

        public MainWindow()
        {
            Init();
            InitializeComponent();
        }

        private void Init(string key = null)
        {
            docsViewModelObject = new DocsViewModel();
            docsViewModelObject.LoadDocs();

            tablesViewModelObject = new TablesViewModel();
            tablesViewModelObject.LoadTables(docsViewModelObject.DocsList);
        }

        private void DocsViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            DocsViewControl.DataContext = docsViewModelObject;
            if (key != null)
                docsViewModelObject.SearchKey(key);
        }

        private void TablesViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            TablesViewControl.DataContext = tablesViewModelObject;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Binding myBinding = new Binding();

            switch(tabControl.SelectedIndex)
            {
                case 0:
                    myBinding.ElementName = nameof(DocsViewControl);
                    myBinding.Path = new PropertyPath(nameof(DocsViewControl.CurrentDoc));
                    treeDocs.SetBinding(ContentProperty, myBinding);
                    break;
                case 1:
                    myBinding.ElementName = nameof(TablesViewControl);
                    myBinding.Path = new PropertyPath(nameof(TablesViewControl.CurrentDoc));
                    treeDocs.SetBinding(ContentProperty, myBinding);
                    break;
            }
        }
    }
}
