using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditeurTest.Interfaces
{
    public interface IContent
    {
        string Content { get; set; }
        string Key { get; set; }
    }
}
