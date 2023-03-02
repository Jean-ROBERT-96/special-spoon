using HelpEditor.Interfaces;
using HelpEditor.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HelpEditor.Services
{
    public class DocsSerializer
    {
        public static Docs DocDeserializer(string path)
        {
            var serializer = new XmlSerializer(typeof(Docs));
            using (var reader = new FileStream(path, FileMode.Open))
            {
                var doc = (Docs)serializer.Deserialize(reader);
                MarkDeserialize(doc);
                return doc;
            }
        }

        private static void MarkDeserialize(IContent doc)
        {
            if(doc.Content != null)
            {
                doc.Content = Regex.Replace(doc.Content, "<p>|</p>", string.Empty);
                doc.Content = Regex.Replace(doc.Content, "<br>", "\r\n");
            }

            foreach (var table in doc.Table)
                MarkDeserialize(table);
        }

        public static void DocSerializer(string path, ObservableCollection<Docs> docs)
        {
            List<string> split = new List<string>(path.Split('\\'));
            var serializer = new XmlSerializer(typeof(Docs));

            foreach(var doc in docs)
            {
                MarkSerialize(doc);
                if (!path.Contains(".xml"))
                {
                    var result = Path.Combine(path, doc.Key + ".xml");
                    using (var writer = new StreamWriter(result, false, Encoding.Unicode))
                    {
                        serializer.Serialize(writer, doc);
                    }
                }

                if (split.Last().Contains(doc.Key) && path.Contains(".xml"))
                {
                    using (var writer = new StreamWriter(path, false, Encoding.Unicode))
                    {
                        serializer.Serialize(writer, doc);
                    }
                }
                MarkDeserialize(doc);
            }

        }

        private static void MarkSerialize(IContent doc)
        {
            if(doc.Content != null)
            {
                doc.Content = Regex.Replace(doc.Content, "\r\n|\n", "<br>");
                doc.Content = $"<p>{doc.Content}</p>";
            }

            foreach (var table in doc.Table)
                MarkSerialize(table);
        }
    }
}
