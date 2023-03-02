using Advance_recurs.Interfaces;
using Advance_recurs.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Advance_recurs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IList<Docs> docsList;
            IList<Tables> tablesList;

            docsList = DocsInit();
            tablesList = TablesExtract((ObservableCollection<Docs>)docsList);
            foreach(Tables tables in tablesList)
            {
                Console.WriteLine(tables.Name);
            }

            Console.WriteLine("\nEntrez une valeur");
            string rep = Console.ReadLine();

            foreach(Tables tables in tablesList)
            {
                if(tables.Name.Contains(rep, StringComparison.OrdinalIgnoreCase))
                    Console.WriteLine(tables.Name);
            }
        }

        static ObservableCollection<Docs> DocsInit ()
        {
            var docsList = new ObservableCollection<Docs>();

            string[] path = Directory.GetFiles(@"./Docs/", "*.json");
            foreach (string p in path)
            {
                try
                {
                    using (var file = new StreamReader(p))
                    {
                        string data = file.ReadToEnd();
                        docsList.Add(JsonConvert.DeserializeObject<Docs>(data));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return docsList;
        }

        static ObservableCollection<Tables> TablesExtract(ObservableCollection<Docs> docsList)
        {
            var tablesList = new ObservableCollection<Tables>();

            foreach(Docs doc in docsList)
            {
                foreach(Tables tab in doc.Tables)
                {
                    List<Tables> result = TablesReader(tab);

                    foreach(var res in result)
                    {
                        tablesList.Add(res);
                    }
                }
            }

            return tablesList;
        }

        static List<Tables> TablesReader(INode tables)
        {
            var list = new List<Tables>();

            if (tables is Tables)
                list.Add((Tables)tables);

            foreach (var tab in tables.SousTables)
            {
                var res = TablesReader(tab);
                foreach (var r in res) list.Add(r);
            }

            /*if (!tables.SousTables.Equals(null))
            {
                
            }*/

            return list;
        }
    }
}