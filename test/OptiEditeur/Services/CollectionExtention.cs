using OptiEditeur.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OptiEditeur.Services
{
    public static class CollectionExtention
    {
        public static void RemoveChild(this ObservableCollection<Tables> collection, Func<Tables, bool> condition)
        {
            for (int i = collection.Count - 1; i >= 0; i--)
            {
                if (collection[i].Table != null)
                    RemoveChild(collection[i].Table, condition);

                if (condition(collection[i]))
                    collection.Remove(collection[i]);
            }
        }

        public static void AddChild(this ObservableCollection<Tables> collection, string key)
        {
            var keySplit = new List<string>(key.Split('.'));
            var keyList = keySplit.GetRange(0, keySplit.Count - 1);
            string keyTable = String.Join('.', keyList);

            for (int i = collection.Count - 1; i >= 0; i--)
            {
                if (keyTable.Contains(collection[i].Key) && collection[i].Table.Count > 0)
                    AddChild(collection[i].Table, key);

                if (collection[i].Key.Equals(keyTable))
                    collection[i].Table.Add(new Tables { Key = key, Name = key });
            }
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> _LinkResult)
        {
            return new(_LinkResult);
        }
    }
}
