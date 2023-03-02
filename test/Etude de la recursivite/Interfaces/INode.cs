using Etude_de_la_recursivite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etude_de_la_recursivite.Interfaces
{
    internal interface INode
    {
        IList<INode> SousTables { get; set; }
    }
}
