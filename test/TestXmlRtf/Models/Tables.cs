using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestXmlRtf.Models
{
    public class Tables : INode
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public ObservableCollection<Tables> SousTables { get; set; } = new ObservableCollection<Tables>();
    }
}
