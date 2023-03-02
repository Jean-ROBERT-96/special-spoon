using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anneau.Object
{
    public class Tower
    {
        public List<Ring> Rings { get; set; }

        public Tower()
        {
            Rings = new List<Ring>();
        }
    }
}
