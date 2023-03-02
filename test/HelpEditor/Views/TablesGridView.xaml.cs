using HelpEditor.Interfaces;
using HelpEditor.Models;
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
    /// Logique d'interaction pour TablesGridView.xaml
    /// </summary>
    public partial class TablesGridView : UserControl
    {
        public static readonly DependencyProperty CurrentContentProperty =
            DependencyProperty.Register(
                "CurrentContent",
                typeof(IContent),
                typeof(TablesGridView),
                new PropertyMetadata(null));

        public IContent CurrentContent
        {
            get => (IContent)GetValue(CurrentContentProperty);
            set => SetValue(CurrentContentProperty, value);
        }

        public DocsViewModel ViewModel { get; set; }
        public List<IContent> List { get; set; }

        public TablesGridView()
        {
            InitializeComponent();
        }

        private void gridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count > 0)
                CurrentContent = (IContent)e.AddedItems[0];
        }

        private void gridView_Loaded(object sender, RoutedEventArgs e)
        {
            if(DocsViewModel.DocsList != null)
            {
                List = new();
                foreach(IContent docs in DocsViewModel.DocsList)
                {
                    var result = ContentReader(docs);
                    foreach(var r in result) List.Add(r);
                }

                DataContext = List;

                key.Width = gridView.ActualWidth / 2;
                name.Width = gridView.ActualWidth / 2;
            }
        }

        static List<IContent> ContentReader(IContent content)
        {
            var docs = new List<IContent>();

            docs.Add(content);

            foreach (var doc in content.Table)
            {
                var result = ContentReader(doc);
                foreach (var res in result) docs.Add(res);
            }

            return docs;
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchBox.Text.Length == 0)
                gridView.ItemsSource = List;
            else
            {
                var result = List.Where(x => x.Key.Contains(searchBox.Text, StringComparison.OrdinalIgnoreCase) ||
                x.Name.Contains(searchBox.Text, StringComparison.OrdinalIgnoreCase)
                ).ToList();
                gridView.ItemsSource = result;
            }
        }
    }
}
