using HelpEditor.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace HelpEditor.Models
{
    public class Tables : INotifyPropertyChanged, IContent
    {
        private string _key;
        private string _name;
        private string _content;
        private ObservableCollection<Tables> _table = new ObservableCollection<Tables>();

        public string Key
        {
            get => _key;
            set
            {
                _key = value;
                OnNotifyChange(nameof(Key));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnNotifyChange(nameof(Name));
            }
        }

        [XmlIgnore]
        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                OnNotifyChange(nameof(Content));
            }
        }

        [XmlElement("Content")]
        public XmlCDataSection CDataSection
        {
            get => new XmlDocument().CreateCDataSection(Content);
            set => Content = value.Value;
        }

        public ObservableCollection<Tables> Table
        {
            get => _table;
            set
            {
                _table = value;
                OnNotifyChange(nameof(Table));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnNotifyChange(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
