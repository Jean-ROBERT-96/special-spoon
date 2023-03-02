using Base_recursiv.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base_recursiv.Models
{
    public class Docs : INode
    {
        public string DocsName { get; set; }
        public string Key { get; set; }
        public IList<Tables> Tables { get; set; }

        public IList<INode> Children { get; set; } = new ObservableCollection<INode>();
    }
}
