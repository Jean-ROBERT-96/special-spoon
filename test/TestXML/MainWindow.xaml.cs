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
using TestXML.Models;

namespace TestXML
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

        private void Init()
        {
            string[] path = Directory.GetFiles(@"./Docs/", "*.xml");
            foreach (string p in path)
            {
                try
                {
                    MyXmlDeserializer(p);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        #region "Deserializer Custom"
        public void MyXmlDeserializer(string file)
        {
            var serializer = new XmlSerializer(typeof(Docs));

            using (var reader = new FileStream(file, FileMode.Open))
            {
                Docs.Add((Docs)serializer.Deserialize(reader));
            }
        }

        #endregion

        
    }
}
