using HelpEditor.Interfaces;
using HelpEditor.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpEditor.Services
{
    public class KeyManager
    {
        public static ObservableCollection<string> CheckKeyToAdd(ObservableCollection<Docs> docs, List<string> keys)
        {
            ObservableCollection<string> list = new();
            foreach (var doc in docs)
            {
                var result = ContentReader(doc);
                foreach (var r in result) list.Add(r.Key);
            }

            ObservableCollection<string> toAdd = new();
            var res = keys.Where(x => !list.Contains(x)).ToList();
            foreach (var r in res) toAdd.Add(r);

            return toAdd;
        }

        public static ObservableCollection<string> CheckKeyToRemove(IContent docs, List<string> keys)
        {
            ObservableCollection<string> list = new();

            if (!keys.Contains(docs.Key) && docs.Table.Count == 0)
                list.Add(docs.Key);

            foreach (var doc in docs.Table)
            {
                var result = CheckKeyToRemove(doc, keys);
                foreach (var r in result) list.Add(r);
            }

            return list;
        }

        private static List<IContent> ContentReader(IContent docs)
        {
            List<IContent> result = new();

            result.Add(docs);
            foreach (var doc in docs.Table)
            {
                var content = ContentReader(doc);
                foreach (var res in content) result.Add(res);
            }

            return result;
        }
    }
}
