using OptiEditeur.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiEditeur.Services
{
    public static class ObservableCollectionExtention
    {
        public static void DeleteChild(this ObservableCollection<Tables> collection, Func<Tables, bool> condition)
        {
            for (int i = collection.Count - 1; i >= 0; i--)
            {
                if (condition(collection[i]))
                    collection.Remove(collection[i]);

                if (collection[i].Table != null)
                    DeleteChild(collection[i].Table, condition);
            }
        }

        public static void AddChild(this ObservableCollection<Tables> collection, string key)
        {
            var keySplit = new List<string>(key.Split('.'));
            var keyList = keySplit.GetRange(0, keySplit.Count - 1);
            string keyTable = String.Join(".", keyList);

            for (int i = collection.Count - 1; i >= 0; i--)
            {
                if (collection[i].Key.Equals(keyTable))
                    collection[i].Table.Add(new Tables { Key = key, Name = key });

                if (keyTable.Contains(collection[i].Key) && collection[i].Table.Count != 0)
                    AddChild(collection[i].Table, key);
            }
        }
    }
}
