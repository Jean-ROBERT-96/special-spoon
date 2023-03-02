using MVVMTest.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTest.ViewModels
{
    public class DocsViewModel
    {
        public ObservableCollection<Docs> DocsList { get; set; }

        public void LoadDocs()
        {
            DocsList = new ObservableCollection<Docs>();

            string[] path = Directory.GetFiles(@"./Documentations/", "*.json");
            foreach (string p in path)
            {
                try
                {
                    using (var file = new StreamReader(p))
                    {
                        string data = file.ReadToEnd();
                        DocsList.Add(JsonConvert.DeserializeObject<Docs>(data));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            foreach (var doc in DocsList)
            {
                doc.Content = $"Tables de \"{doc.Docsname}\" :\n\n";
                foreach (var table in doc.Tables)
                {
                    doc.Content = doc.Content + $" - {table.Name}\n";
                    TreeOrganize(table);
                }
            }
        }

        public void TreeOrganize(Tables tables)
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

        public void SearchKey(string key)
        {
            Tables result;
            foreach(var docs in DocsList)
            {
                foreach(var tables in docs.Tables)
                    result = CheckKeyTables(tables, key);
            }
        }

        public Tables CheckKeyTables(Tables tables, string key)
        {
            if(tables.Key == key)
                return tables;

            foreach(var stables in tables.SousTables)
                CheckKeyTables(stables, key);

            return null;
        }
    }
}
