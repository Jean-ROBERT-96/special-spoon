using HierarchieTestWPF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HierarchieTestWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Docs> docs { get; set; }
        public ObservableCollection<Tables> tables { get; set; }

        public MainWindow()
        {
            docs = new ObservableCollection<Docs>();
            tables = new ObservableCollection<Tables>();

            Init();
            InitializeComponent();

            this.DataContext = this;
        }

        private void treeView_Loaded(object sender, RoutedEventArgs e)
        {
            Tables? tab = tables.Where(e => e.Key == "devis.eyes").FirstOrDefault();
            SetSelectByKey(treeView, tab);
        }

        public void Init()
        {
            string[] path = Directory.GetFiles(@"./Docs/", "*.json");
            foreach (string p in path)
            {
                try
                {
                    using (var file = new StreamReader(p))
                    {
                        string data = file.ReadToEnd();
                        docs.Add(JsonConvert.DeserializeObject<Docs>(data));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            foreach(var doc in docs)
            {
                doc.Content = $"Tables de \"{doc.DocsName}\" :\n\n";
                foreach (var table in doc.Tables)
                {
                    doc.Content = doc.Content + $" - {table.Name}\n";
                    TreeOrganize(table);
                    var list = TablesReader(table);
                    foreach (var t in list) tables.Add(t);
                }
            }
        }

        public ObservableCollection<Tables> TablesReader(Tables tables)
        {
            var result = new ObservableCollection<Tables>();

            if(tables is Tables)
                result.Add(tables);

            foreach(Tables stable in tables.SousTables)
            {
                var sTable = TablesReader(stable);
                foreach(var r in sTable) result.Add(r);
            }

            return result;
        }

        public void TreeOrganize(Tables tables)
        {
            if(tables.Content == string.Empty || tables.Content == null)
            {
                tables.Content = $"Sous-Tables de \"{tables.Name}\" :\n\n";
                foreach(var s in tables.SousTables)
                    tables.Content = tables.Content + $" - {s.Name}\n";
            }

            foreach(var s in tables.SousTables)
                TreeOrganize(s);
        }



        public bool SetSelectByKey(ItemsControl tree, Tables item)
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

            foreach(var i in tree.Items)
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
