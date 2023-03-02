using HelpEditor.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HelpEditor.Interfaces
{
    public interface IContent
    {
        string Key { get; set; }
        string Name { get; set; }
        string Content { get; set; }
        ObservableCollection<Tables> Table { get; set; }
    }
}
