using MVVMTest.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTest.ViewModels
{
    class TablesViewModel
    {
        public ObservableCollection<Tables> TablesList { get; set; }

        public void LoadTables(ObservableCollection<Docs> docsList)
        {
            TablesList = new ObservableCollection<Tables>();

            foreach (Docs docs in docsList)
            {
                foreach(Tables tables in docs.Tables)
                {
                    var list = TablesReader(tables);
                    foreach (var table in list) TablesList.Add(table);
                }
            }
        }

        private ObservableCollection<Tables> TablesReader(Tables tables)
        {
            var result = new ObservableCollection<Tables>();

            if (tables is Tables)
                result.Add(tables);

            foreach (Tables stable in tables.SousTables)
            {
                var sTable = TablesReader(stable);
                foreach (var r in sTable) result.Add(r);
            }

            return result;
        }
    }
}
