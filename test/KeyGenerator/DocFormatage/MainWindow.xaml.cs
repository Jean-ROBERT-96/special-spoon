using DocFormatage.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Xml.Serialization;

namespace DocFormatage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Docs> ListDocs { get; set; }
        public List<string> KeyToAdd { get; set; } = new();
        public List<string> KeyToDelete { get; set; } = new();

        private string[] _keyList;

        public MainWindow()
        {
            Init();
            InitializeComponent();

            DataContext = ListDocs;
        }

        private void Init()
        {
            ListDocs = new ObservableCollection<Docs>();
            _keyList = File.ReadAllLines("./Key/KeyList.txt");
            string[] path = Directory.GetFiles(@"./Docs/", "*.xml");
            foreach (string pathItem in path)
                XmlDeserializer(pathItem);

            foreach(var doc in ListDocs)
            {
                var result = CheckKeyToDelete(doc);
                foreach(var res in result) KeyToDelete.Add(res);
            }
                
            KeyToAdd = CheckKeyToAdd(ListDocs);

            if(KeyToAdd.Count != 0)
            {
                string message = "";
                foreach (string key in KeyToAdd) message += $"{key}\n";

                var result = MessageBox.Show($"Les clés suivantes sont manquantes, voulez vous les ajouter?\n{message}", "Clés à ajouter.", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(result.Equals(MessageBoxResult.Yes))
                {
                    foreach (var item in KeyToAdd)
                    {
                        ListDocs.AddChild(item);
                    }
                    KeyToAdd.Clear();
                }
            }

            if (KeyToDelete.Count != 0)
            {
                string message = "";
                foreach (string key in KeyToDelete) message += $"{key}\n";

                var result = MessageBox.Show($"Les clés suivantes ne sont plus d'actualités, voulez vous les enlever?\n{message}", "Clés à supprimer.", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    foreach(var item in KeyToDelete)
                    {
                        foreach(var doc in ListDocs)
                        {
                            doc.Table.RemoveChild(x => x.Key == item);
                        }
                    }
                    KeyToDelete.Clear();
                }
            }
            
            if(KeyToAdd.Count == 0 && KeyToDelete.Count == 0)
                MessageBox.Show("Le document est à jour.", "Document à jour.", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private List<string> CheckKeyToDelete(IContent docs)
        {
            List<string> keys = new();

            if(!_keyList.Contains(docs.Key) && docs.Table.Count == 0)
                keys.Add(docs.Key);

            foreach (var item in docs.Table)
            {
                var result = CheckKeyToDelete(item);
                foreach (var res in result) keys.Add(res);
            }

            return keys;
        }

        private List<string> CheckKeyToAdd(ObservableCollection<Docs> docs)
        {
            List<string> keyDocs = new();
            foreach(var doc in docs)
            {
                var result = ContentReader(doc);
                foreach(var res in result) keyDocs.Add(res.Key);
            }

            var toAdd = _keyList.Where(x => !keyDocs.Contains(x)).ToList();

            return toAdd;
        }

        private List<IContent> ContentReader(IContent docs)
        {
            List<IContent> result = new();

            result.Add(docs);
            foreach(var doc in docs.Table)
            {
                var content = ContentReader(doc);
                foreach(var res in content) result.Add(res);
            }

            return result;
        }

        private void XmlDeserializer(string path)
        {
            var serializer = new XmlSerializer(typeof(Docs));
            using (var reader = new FileStream(path, FileMode.Open))
            {
                ListDocs.Add((Docs)serializer.Deserialize(reader));
            }
        }
    }
}
