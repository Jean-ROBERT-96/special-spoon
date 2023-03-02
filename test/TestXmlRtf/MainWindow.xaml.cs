using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml.Serialization;
using TestXmlRtf.Models;

namespace TestXmlRtf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Docs> Docs { get; set; }

        public MainWindow()
        {
            Docs = new ObservableCollection<Docs>();

            Init();
            InitializeComponent();

            DataContext = Docs;
        }

        public void Init()
        {
            string[] path = Directory.GetFiles(@"./Docs/", "*.xml");
            foreach (string p in path)
            {
                try
                {
                    Docs.Add(MyXmlDeserializer(p));
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        #region "Deserializer Basic"
        public void XmlDeserializerBasic(string file)
        {
            var serializer = new XmlSerializer(typeof(Docs));

            using (var reader = new FileStream(file, FileMode.Open))
            {
                Docs.Add((Docs)serializer.Deserialize(reader));
            }
        }
        #endregion

        #region "Deserializer Custom"
        public Docs MyXmlDeserializer(string file)
        {
            var result = new Docs();
            var xdoc = new XmlDocument();

            var reader = new FileStream(file, FileMode.Open);
            xdoc.Load(reader);

            XmlNodeList nodeList = xdoc.ChildNodes[1].ChildNodes;

            for(int i = 0; i < nodeList.Count; i++)
            {
                switch(nodeList[i].Name)
                {
                    case "Key":
                        result.Key = nodeList[i].InnerText;
                        break;
                    case "DocsName":
                        result.DocsName = nodeList[i].InnerText;
                        break;
                    case "Content":
                        result.Content = nodeList[i].InnerXml;
                        break;
                    case "Tables":
                        string test = nodeList[i].InnerXml;
                        foreach(XmlNode child in nodeList[i].ChildNodes)
                        {
                            result.Tables.Add(TablesCreators($"<Tables>{child.InnerXml}</Tables>"));
                        }
                        break;
                }
            }

            return result;
        }

        public Tables TablesCreators(string tables)
        {
            var result = new Tables();
            var xdoc = new XmlDocument();
            xdoc.LoadXml(tables);
            XmlNodeList nodeList = xdoc.ChildNodes[0].ChildNodes;

            for(int i =0; i < nodeList.Count; i++)
            {
                switch (nodeList[i].Name)
                {
                    case "Key":
                        result.Key = nodeList[i].InnerText;
                        break;
                    case "Name":
                        result.Name = nodeList[i].InnerText;
                        break;
                    case "Content":
                        result.Content = nodeList[i].InnerXml;
                        break;
                    case "SousTables":
                        string test = nodeList[i].InnerXml;
                        foreach (XmlNode child in nodeList[i].ChildNodes)
                        {
                            result.SousTables.Add(TablesCreators($"<Tables>{child.InnerXml}</Tables>"));
                        }
                        break;
                }
            }

            return result;
        }



        #endregion

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            INode xvalue = (INode)e.NewValue;
            if(xvalue.Content != string.Empty && xvalue.Content.Contains("<"))
            {
                var textBlock = new TextBlock();
                var xdoc = new XmlDocument();
                xdoc.LoadXml(xvalue.Content);
                XmlNodeList nodes = xdoc.ChildNodes[0].ChildNodes;

                for(int i = 0; i < nodes.Count; i++)
                {
                    switch(nodes[i].Name)
                    {
                        case "br":
                            textBlock.Inlines.Add(new LineBreak());
                            break;
                        case "span":
                            textBlock.Inlines.Add(ColorText(nodes[i].InnerText));
                            break;
                        case "p":
                            textBlock.Inlines.Add(nodes[i].InnerText);
                            break;
                    }
                }

                Content = textBlock;
            }
        }

        private TextBlock ColorText(string text)
        {
            var textResult = new TextBlock();

            textResult.Foreground = Brushes.BlueViolet;
            textResult.Inlines.Add(text);

            return textResult;
        }
    }
}
