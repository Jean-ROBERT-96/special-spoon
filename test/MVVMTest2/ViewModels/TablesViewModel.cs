using MVVMTest2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTest2.ViewModels
{
    public class TablesViewModel : BaseViewModel
    {
        private ObservableCollection<Tables> _tablesList;

        public ObservableCollection<Tables> TablesList
        {
            get => _tablesList;
            set
            {
                _tablesList = value;
                NotifyPropertyChanged();
            }
        }

        public void InitValue(ObservableCollection<Docs> docsList)
        {
            TablesList = new ObservableCollection<Tables>();

            foreach (Docs doc in docsList)
            {
                foreach (var table in doc.Tables)
                {
                    var sstab = TablesReader(table);
                    foreach(var result in sstab) TablesList.Add(result);
                }
            }
        }

        private ObservableCollection<Tables> TablesReader(Tables tables)
        {
            var result = new ObservableCollection<Tables>();

            if(tables is Tables)
                result.Add(tables);

            foreach(var table in tables.SousTables)
            {
                var sstab = TablesReader(table);
                foreach(var r in sstab) TablesList.Add(r);
            }

            return result;
        }
    }
}
