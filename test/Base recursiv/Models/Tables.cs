using Base_recursiv.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base_recursiv.Models
{
    public class Tables : INode
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public IList<Tables> SousTables { get; set; }

        public IList<INode> Children { get; set; } = new ObservableCollection<INode>();
    }
}
