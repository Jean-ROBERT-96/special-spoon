using System.Xml;

namespace TestRecursInverse
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string xdoc = "<p>Pour supprimer un <bold>devis</bold>, cliquez sur le bouton supprimer au niveau <br/> du devis à supprimer.</p>";

            Console.WriteLine(XmlRecurs(xdoc));
        }

        static private string XmlRecurs(string xml)
        {
            string result = "";
            var doc = new XmlDocument();
            doc.LoadXml(xml);

            XmlNodeList xmlNodeList = doc.ChildNodes[0].ChildNodes;

            if (xmlNodeList[0].Name == "bold")
            {

            }

            return result;
        }
    }
}