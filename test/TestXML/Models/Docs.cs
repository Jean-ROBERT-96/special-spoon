using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestXML.Models
{
    public class Docs : INode
    {
        public string Key { get; set; }
        public string DocsName { get; set; }
        public string Content { get; set; }
        public ObservableCollection<Tables> Tables { get; set; } = new ObservableCollection<Tables>();
    }
}
