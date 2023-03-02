using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HierarchieTestWPF.Models
{
    public class Docs
    {
        public string DocsName { get; set; }
        public string Key { get; set; }
        public string Content { get; set; }
        public ObservableCollection<Tables> Tables { get; set; }
    }
}
