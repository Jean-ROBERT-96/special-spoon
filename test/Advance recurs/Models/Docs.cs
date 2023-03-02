using Advance_recurs.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advance_recurs.Models
{
    internal class Docs
    {
        public string DocsName { get; set; }
        public string Key { get; set; }
        public ObservableCollection<Tables> Tables { get; set; }
    }
}
