using CheckClé.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;

namespace CheckClé
{
    public class Program
    {
        static void Main(string[] args)
        {
            ObservableCollection<Docs> docs = new ObservableCollection<Docs>();
            string[] path = Directory.GetFiles(@"./Docs/", "*.xml");
            string[] keyList = File.ReadAllLines("./Key/KeyList.txt");
            Array.Sort(keyList);

            foreach (var p in path)
                docs.Add(XmlDeserializer(p));

            var contentList = new List<string>();

            foreach(var doc in docs)
            {
                var delete = KeyDeleteChecker(keyList, doc);
                foreach(var res in delete) contentList.Add(res);
            }

            var exist = KeyIsNoExist(keyList, docs);
            foreach (var res in exist) contentList.Add(res);

            foreach (var doc in contentList)
            {
                Console.WriteLine(doc);
            }

            if (contentList.Count == 0)
                Console.WriteLine("Rien à relever.");
        }

        static List<string> KeyIsNoExist(string[] key, ObservableCollection<Docs> content)
        {
            var docs = new List<IContent>();
            foreach (var doc in content)
            {
                var res = ContentReader(doc);
                foreach(var r in res ) docs.Add(r);
            }

            var keyDocs = new List<string>();
            foreach (var doc in docs)
                keyDocs.Add(doc.Key);

            return key.Where(x => !keyDocs.Contains(x)).ToList();
        }

        static List<IContent> ContentReader(IContent content)
        {
            var docs = new List<IContent>();

            docs.Add(content);

            foreach (var doc in content.Table)
            {
                var result = ContentReader(doc);
                foreach (var res in result) docs.Add(res);
            }

            return docs;
        }

        static List<string> KeyDeleteChecker(string[] key, IContent docs)
        {
            List<string> result = new();

            if (!key.Contains(docs.Key) && docs.Table.Count == 0)
                result.Add(docs.Key);

            foreach (var item in docs.Table)
            {
                var res = KeyDeleteChecker(key, item);
                foreach (var r in res) result.Add(r);
            }

            return result;
        }

        static Docs XmlDeserializer(string path)
        {
            var serializer = new XmlSerializer(typeof(Docs));
            using(var reader = new FileStream(path, FileMode.Open))
            {
                return (Docs)serializer.Deserialize(reader);
            }
        }
    }
}