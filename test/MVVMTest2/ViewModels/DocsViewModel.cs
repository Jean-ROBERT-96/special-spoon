using MVVMTest2.Interfaces;
using MVVMTest2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Serialization;

namespace MVVMTest2.ViewModels
{
    public class DocsViewModel : BaseViewModel
    {
        private ObservableCollection<Docs> _docsList;

        public ObservableCollection<Docs> DocsList
        {
            get => _docsList;
            set
            {
                _docsList = value;
                NotifyPropertyChanged();
            }

        }

        public void InitValue()
        {
            DocsList = new ObservableCollection<Docs>();

            string[] path = Directory.GetFiles(@"./Documentations/", "*.xml");
            foreach (string p in path)
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(Docs));
                    using (var reader = new FileStream(p, FileMode.Open))
                    {
                        DocsList.Add((Docs)serializer.Deserialize(reader));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            foreach (var doc in DocsList)
            {
                if(doc.Content == null || doc.Content == string.Empty)
                {
                    doc.Content = $"Tables de \"{doc.Docsname}\" :\n\n";
                    foreach (var table in doc.Tables)
                    {
                        doc.Content = doc.Content + $" - {table.Name}\n";
                        TreeOrganize(table);
                    }
                }
            }
        }

        public IContent SearchTables(string key)
        {
            IContent result;

            foreach(var doc in DocsList)
            {
                if(doc.Key == key)
                    return doc;

                foreach (var table in doc.Tables)
                {
                    result = TablesReader(table, key);
                    if(result != null) return result;
                }
            }

            return null;
        }

        private Tables TablesReader(Tables tables, string key)
        {
            if(tables.Key == key)
                return tables;

            foreach(var sstab in tables.SousTables)
            {
                var res = TablesReader(sstab, key);
                if(res != null) return res;
            }

            return null;
        }

        private void TreeOrganize(Tables tables)
        {
            if (tables.Content == string.Empty || tables.Content == null)
            {
                tables.Content = $"Sous-Tables de \"{tables.Name}\" :\n\n";
                foreach (var s in tables.SousTables)
                    tables.Content = tables.Content + $" - {s.Name}\n";
            }

            foreach (var s in tables.SousTables)
                TreeOrganize(s);
        }
    }
}
