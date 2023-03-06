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
        private ObservableCollection<Tables> _docsList;

        public ObservableCollection<Tables> DocsList
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
            DocsList = new ObservableCollection<Tables>();

            string[] path = Directory.GetFiles(@"./Documentations/", "*.xml");
            foreach (string p in path)
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(Tables));
                    using (var reader = new FileStream(p, FileMode.Open))
                    {
                        DocsList.Add((Tables)serializer.Deserialize(reader));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public Tables SearchTables(string key)
        {
            Tables result;

            foreach(var doc in DocsList)
            {
                if(doc.Key == key)
                    return doc;

                foreach (var table in doc.Table)
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

            foreach(var sstab in tables.Table)
            {
                var res = TablesReader(sstab, key);
                if(res != null) return res;
            }

            return null;
        }
    }
}
