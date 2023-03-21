using OptiEditeur.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OptiEditeur.Services
{
    public static class DocsSerializer
    {
        private static XmlSerializer serializer = new(typeof(Tables));

        public static Tables TablesDeserialize(string path)
        {
            using(var reader = new FileStream(path, FileMode.Open))
            {
                var doc = (Tables)serializer.Deserialize(reader);
                MarkDeserialize(doc);
                return doc;
            }
        }

        private static void MarkDeserialize(Tables table)
        {
            if (table.Content != null)
            {
                table.Content = Regex.Replace(table.Content, "<p>|</p>", string.Empty);
                table.Content = Regex.Replace(table.Content, "<br>", "\r\n");
            }
            foreach (var st in table.Table)
                MarkDeserialize(st);
        }

        public static void TableSerialize(string path, ObservableCollection<Tables> docs)
        {
            foreach(var table in docs)
            {
                MarkSerialize(table);
                var docName = Path.Combine(path, table.Key + ".xml");
                using (var writer = new StreamWriter(docName, false, Encoding.Unicode))
                {
                    serializer.Serialize(writer, table);
                }
                MarkDeserialize(table);
            }
        }

        private static void MarkSerialize(Tables table)
        {
            if (table.Content != null)
            {
                table.Content = Regex.Replace(table.Content, "\r\n|\n", "<br>");
                table.Content = $"<p>{table.Content}</p>";
            }

            foreach(var st in table.Table)
                MarkSerialize(st);
        }
    }
}
