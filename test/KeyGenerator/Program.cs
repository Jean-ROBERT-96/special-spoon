using KeyGenerator.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace KeyGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ObservableCollection<Docs> docs = new ObservableCollection<Docs>();
            string[] keyList = File.ReadAllLines("./Key/KeyList.txt");
            Array.Sort(keyList);
            
            foreach (string key in keyList)
            {
                List<string> keySplit = new List<string>(key.Split('.'));

                bool exist = false;
                foreach(Docs checkDocs in docs)
                {
                    if(checkDocs.Key == keySplit[0])
                        exist = true;
                }

                if(!exist)
                    docs.Add(new Docs { Key = keySplit[0] });

                if (keySplit.Count > 1)
                {
                    foreach(var doc in docs)
                    {
                        if (doc.Key == keySplit[0])
                        {
                            bool subExist = false;
                            foreach (Tables subtable in doc.SubTables)
                            {
                                if(subtable.Key == keySplit[1])
                                {
                                    doc.SubTables.Add(TablesCreator(keySplit, 2, subtable));
                                    subExist = true;
                                }
                            }
                            
                            if(!subExist)
                                doc.SubTables.Add(TablesCreator(keySplit, 2));
                        }
                    }
                }
            }

            foreach (Docs checkDocs in docs)
            {
                Console.WriteLine(checkDocs.Key);
            }
        }

        public static Tables TablesCreator(List<string> key, int count, Tables tables = null)
        {
            Tables table = new();

            if (tables == null)
            {
                List<string> keyTable = key.GetRange(0, count);
                table.Key = String.Join(".", keyTable);
            }

            count++;

            if (key.Count >= count)
            {


                table.SubTables.Add(TablesCreator(key, count));
            }

            return table;
        }
    }
}