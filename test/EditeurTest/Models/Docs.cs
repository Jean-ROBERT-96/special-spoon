using EditeurTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Xml.Serialization;

namespace EditeurTest.Models
{
    public class Docs : INotifyPropertyChanged, IContent
    {
        private string _key;
        private string _name;
        private string _content;
        private ObservableCollection<Tables> _tables = new ObservableCollection<Tables>();

        public string Key
        {
            get => _key;
            set
            {
                _key = value;
                NotifyPropertyChange(nameof(Key));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChange(nameof(Name));
            }
        }

        [XmlIgnore]
        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                NotifyPropertyChange(nameof(Content));
            }
        }

        public ObservableCollection<Tables> Tables
        {
            get => _tables;
            set
            {
                _tables = value;
                NotifyPropertyChange(nameof(Tables));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChange(string propertyName)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
