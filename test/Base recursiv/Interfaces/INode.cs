using Base_recursiv.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base_recursiv.Interfaces
{
    public interface INode
    {
        public IList<INode> Children { get; set; }
    }
}
