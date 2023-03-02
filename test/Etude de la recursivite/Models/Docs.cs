using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etude_de_la_recursivite.Models
{
    internal class Docs
    {
        public string DocsName { get; set; }
        public string Key { get; set; }
        public IList<Tables> Tables { get; set; }
    }
}
