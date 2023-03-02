using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRecursiv
{
    public interface INode
    {
        IList<INode> Children { get; }

        bool IsFeuille();
    }
}
