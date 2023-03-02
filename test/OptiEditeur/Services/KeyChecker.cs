using OptiEditeur.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiEditeur.Services
{
    public class KeyChecker
    {
        public static ObservableCollection<string> CheckKeyToAdd(ObservableCollection<Tables> docs, List<string> keys)
        {
            ObservableCollection<string> list = new();
            foreach(var table in docs)
            {
                var content = ContentReader(table);
                foreach (var result in content) list.Add(result.Key);
            }

            ObservableCollection<string> toAdd = new();
            var res = keys.Where(x => !list.Contains(x)).ToList();
            foreach (var r in res) toAdd.Add(r);

            return toAdd;
        }

        public static ObservableCollection<string> CheckKeyToDelete(ObservableCollection<Tables> docs, List<string> keys)
        {
            ObservableCollection<string> list = new();
            foreach (var table in docs)
            {
                var content = ContentReader(table);
                foreach (var result in content)
                {
                    if(!keys.Contains(result.Key) && result.Table.Count == 0)
                        list.Add(result.Key);
                }
            }

            ObservableCollection<string> toDelete = new();
            var res = list.Where(x => !keys.Contains(x)).ToList();
            foreach(var r in res) toDelete.Add(r);
            return toDelete;
        }

        private static List<Tables> ContentReader(Tables tables)
        {
            List<Tables> result = new() { tables };

            foreach(var st in tables.Table)
            {
                var content = ContentReader(st);
                foreach(var res in content) result.Add(res);
            }

            return result;
        }
    }
}
