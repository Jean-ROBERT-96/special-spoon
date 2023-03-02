using Advance_recurs.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advance_recurs.Interfaces
{
    internal interface INode
    {
        [JsonIgnore]
        IList<INode> SousTables { get; }
    }
}
