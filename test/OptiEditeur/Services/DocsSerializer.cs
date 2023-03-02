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
    public class DocsSerializer
    {
        public static Tables TableDeserialize(string path)
        {
            var serializer = new XmlSerializer(typeof(Tables));
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
            List<string> split = new(path.Split('\\'));
            var serializer = new XmlSerializer(typeof(Tables));

            foreach(var table in docs)
            {
                MarkSerialize(table);
                if(!path.Contains(".xml"))
                {
                    var result = Path.Combine(path, table.Key + ".xml");
                    using(var writer = new StreamWriter(result, false, Encoding.Unicode))
                    {
                        serializer.Serialize(writer, table);
                    }
                }
                else if(split.Last().Contains(table.Key) && path.Contains(".xml"))
                {
                    using (var writer = new StreamWriter(path, false, Encoding.Unicode))
                    {
                        serializer.Serialize(writer, table);
                    }
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
