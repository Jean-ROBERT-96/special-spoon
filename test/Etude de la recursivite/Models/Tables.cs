using Etude_de_la_recursivite.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etude_de_la_recursivite.Models
{
    internal class Tables : INode
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public IList<INode> SousTables { get; set; } = new ObservableCollection<INode>();

        //public IList<INode> Children { get; } = new ObservableCollection<INode>();
    }
}
