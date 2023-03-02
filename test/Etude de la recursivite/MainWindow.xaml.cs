using Etude_de_la_recursivite.Interfaces;
using Etude_de_la_recursivite.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace Etude_de_la_recursivite
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Docs> docsList;
        private ObservableCollection<Tables> tablesList;

        public MainWindow()
        {
            docsList = new ObservableCollection<Docs>();
            tablesList = new ObservableCollection<Tables>();

            Init();
            foreach(Docs doc in docsList)
            {
                foreach(Tables table in doc.Tables)
                    MessageBox.Show(CountTables(table).ToString());
            }
            
            Environment.Exit(0);
            InitializeComponent();
        }

        private void Init()
        {
            string[] path = Directory.GetFiles(@"./Docs/", "*.json");
            foreach(string p in path)
            {
                try
                {
                    using(var file = new StreamReader(p))
                    {
                        string data = file.ReadToEnd();
                        docsList.Add(JsonConvert.DeserializeObject<Docs>(data));
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private int CountTables(INode node)
        {
            int count = 0;

            if (node is Tables)
                count++;

            foreach (var c in node.SousTables)
            {
                count += CountTables(c);
            }

            return count;
        }
    }
}
