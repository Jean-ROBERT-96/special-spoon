﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckClé.Models
{
    public class Tables : IContent
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public ObservableCollection<Tables> Table { get; set; } = new ObservableCollection<Tables>();
    }
}
