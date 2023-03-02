using Advance_recurs.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advance_recurs.Models
{
    internal class Tables : INode
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public IList<INode> SousTables { get; } = new ObservableCollection<INode>();
    }
}
