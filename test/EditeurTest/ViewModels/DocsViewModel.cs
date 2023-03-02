using EditeurTest.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

namespace EditeurTest.ViewModels
{
    public class DocsViewModel : BaseViewModel
    {
        private ObservableCollection<Docs> _docsList;
        private string[] path;

        public ObservableCollection<Docs> DocsList
        {
            get => _docsList;
            set
            {
                _docsList = value;
                NotifyPropertyChange();
            }
        }

        public void InitValue()
        {
            DocsList = new ObservableCollection<Docs>();

            path = Directory.GetFiles(@"./Documentation/", "*.xml");
            foreach (string pathItem in path)
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(Docs));
                    using(var reader = new FileStream(pathItem, FileMode.Open))
                    {
                        DocsList.Add((Docs)serializer.Deserialize(reader));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public string AddContent(string key)
        {
            foreach(var p in path)
            {
                var xdoc = new XmlDocument();

                using(var reader = new FileStream(p, FileMode.Open))
                {
                    xdoc.Load(reader);

                    
                }
            }

            return null;
        }
    }
}
