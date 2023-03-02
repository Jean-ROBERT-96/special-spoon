using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace RecursXmlWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string xdoc = "<p><h1>Bonjour, je suis un <bold>paragraphe plutôt long</bold>, mais je vais vous expliquer pourquoi.</h1><br/><br/>En faite je suis un test pour le <italic>formatage de texte</italic> de l'aide wesav, du coup, <span color=\"red\">je suis bien utile pour la réalisation du projet</span>.<br/><br/>Sinon, <span size=\"16\" color=\"blue\"><a href=\"https://www.google.fr/\">tout va pour le mieux</a>, je fait ma vie, tranquillement</span> tout en étant utile en quelques sorte.<br/>Bon c'est à peu près tout, à la prochaine.</p>";

        public MainWindow()
        {
            InitializeComponent();
            test.Children.Add(XmlRecurs(xdoc));
        }

        private RichTextBox XmlRecurs(string xml)
        {
            RichTextBox text = new RichTextBox { IsReadOnly = true };
            FlowDocument flowDocument = new FlowDocument();
            var doc = new XmlDocument();
            doc.LoadXml(xml);

            XmlNode node = doc.ChildNodes[0];

            var result = XmlTest(node);
            flowDocument.Blocks.Add(result);
            text.Document= flowDocument;

            return text;
        }

        private TextBlock XmlNodeChecker(XmlNode node)
        {
            TextBlock text = new TextBlock { TextWrapping = TextWrapping.Wrap };

            if (node.FirstChild != null && node.Name != "p")
            {
                text.Text += node.FirstChild.InnerText;
            }

            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                if (node.ChildNodes[i].NodeType == XmlNodeType.Element)
                {
                    var result = XmlNodeChecker(node.ChildNodes[i]);
                    text.Text += result.Text;
                }
            }

            if(node.LastChild != null && node.LastChild != node.FirstChild)
            {
                text.Text += node.LastChild.InnerText;
            }

            switch (node.Name)
            {
                case "h1":
                    break;
                case "h2":
                    break;
                case "h3":
                    break;
                case "h4":
                    break;
                case "h5":
                    break;
                case "h6":
                    break;
                case "a":
                    break;
                case "img":
                    break;
                case "span":
                    break;
                case "bold":
                    break;
                case "italic":
                    break;
            }

            return text;
        }

        private Paragraph XmlTest(XmlNode xmlNode)
        {
            var text = new Paragraph();

            if (xmlNode.Value != null && xmlNode.NodeType == XmlNodeType.Text)
                text.Inlines.Add(xmlNode.InnerText);

            switch (xmlNode.Name)
            {
                case "bold":
                    text.Inlines.Add(new Run { Text = xmlNode.InnerText, FontWeight = FontWeights.Bold });
                    break;
                case "italic":
                    text.Inlines.Add(new Run { Text = xmlNode.InnerText, FontStyle = FontStyles.Italic });
                    break;
                case "h1":
                    text.Inlines.Add(new Run { Text = xmlNode.InnerText, FontSize = 25 });
                    break;
                case "h2":
                    text.Inlines.Add(new Run { Text = xmlNode.InnerText, FontSize = 23 });
                    break;
                case "h3":
                    text.Inlines.Add(new Run { Text = xmlNode.InnerText, FontSize = 21 });
                    break;
                case "h4":
                    text.Inlines.Add(new Run { Text = xmlNode.InnerText, FontSize = 19 });
                    break;
                case "h5":
                    text.Inlines.Add(new Run { Text = xmlNode.InnerText, FontSize = 17 });
                    break;
                case "h6":
                    text.Inlines.Add(new Run { Text = xmlNode.InnerText, FontSize = 15 });
                    break;
                case "a":
                    text.Inlines.Add(new Hyperlink(new Run(xmlNode.InnerText)) { NavigateUri = new Uri("https://google.fr") });
                    break;
                case "span":
                    text.Inlines.Add(new Run(xmlNode.InnerText));
                    break;
                case "img":
                    text.Inlines.Add(new Run(xmlNode.InnerText));
                    break;
                case "br":
                    text.Inlines.Add(new LineBreak());
                    break;
            }

            for (int j = 0; j < xmlNode.ChildNodes.Count; j++)
            {
                var result = XmlTest(xmlNode.ChildNodes[j]);
                for (int i = 0; i < result.Inlines.Count; i++)
                {
                    text.Inlines.Add(result.Inlines.ElementAt(i));
                }
            }

            return text;
        }

        private List<Run> RunCreator(XmlNode node)
        {
            var text = new List<Run>();

            if (node.Value != null)
                text.Add(new Run(node.InnerText));

            switch (node.Name)
            {
                case "bold":
                    text.Add(new Run { Text = node.InnerText, FontWeight = FontWeights.Bold });
                    break;
                case "italic":
                    text.Add(new Run { Text = node.InnerText, FontStyle = FontStyles.Italic });
                    break;
                case "h1":
                    text.Add(new Run { Text = node.InnerText, FontSize = 25 });
                    break;
                case "br":
                    text.Add(new Run("\n"));
                    break;
            }

            foreach(XmlNode child in node.ChildNodes)
            {
                var result = RunCreator(child);
                foreach(var r in result)
                {
                    text.Add(r);
                }
            }

            return text;
        }
    }
}
