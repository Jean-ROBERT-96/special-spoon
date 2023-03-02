using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocFormatage.Models
{
    public interface IContent
    {
        string Key { get; set; }
        ObservableCollection<Tables> Table { get; set; }
    }
}
