using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;
using GénérationClé.Models;

namespace GénérationClé
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ObservableCollection<Docs> docs = new ObservableCollection<Docs>();
            string[] keyList = File.ReadAllLines("./Key/KeyList.txt");
            Array.Sort(keyList);

            foreach (string key in keyList)
            {
                var split = new List<string>(key.Split('.'));
                bool docExist = false;
                foreach (var doc in docs)
                {
                    if(doc.Key == split[0])
                        docExist = true;
                }

                if(!docExist)
                    docs.Add(new Docs { Key = split[0], Name = split[0] });

                if(split.Count > 1)
                {
                    var keyTableSplit = split.GetRange(0, 2);
                    var keyTable = String.Join('.', keyTableSplit);
                    foreach (var doc in docs)
                    {
                        if (doc.Key == split[0])
                        {

                            Tables? exist = doc.Table.Where(x => x.Key == keyTable).FirstOrDefault();
                            if (exist != null)
                            {
                                int index = doc.Table.IndexOf(exist);
                                Tables res = TableWriter(2, split, exist);
                                doc.Table[index] = res;
                            }
                            else
                                doc.Table.Add(TableWriter(2, split));
                        }
                    }
                }
            }

            foreach(var doc in docs)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Docs));

                using(var wr = new StreamWriter($"./Docs/{doc.Key}_doc.xml", false))
                {
                    serializer.Serialize(wr, doc);
                }
            }
        }

        static Tables TableWriter(int count, List<string> keySplit, Tables? table = null)
        {
            if(table == null)
            {
                var keyList = keySplit.GetRange(0, count);
                string key = String.Join('.', keyList);
                table = new Tables { Key = key, Name = key };
            }

            count++;

            if (keySplit.Count >= count)
            {
                var keyTableSplit = keySplit.GetRange(0, count);
                var keyTable = String.Join('.', keyTableSplit);
                Tables? exist = table.Table.Where(x => x.Key == keyTable).FirstOrDefault();

                if (exist != null)
                {
                    int index = table.Table.IndexOf(exist);
                    Tables res = TableWriter(count, keySplit, exist);
                    table.Table[index] = res;
                }
                else
                    table.Table.Add(TableWriter(count, keySplit));
            }

            return table;
        }
    }
}
