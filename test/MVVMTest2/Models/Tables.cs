﻿using MVVMTest2.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTest2.Models
{
    public class Tables : INotifyPropertyChanged, IContent
    {
        private string _key;
        private string _name;
        private string _content;
        private ObservableCollection<Tables> _sousTables = new ObservableCollection<Tables>();

        public string Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
                NotifyPropertyChanged("Key");
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
                NotifyPropertyChanged("Content");
            }
        }

        public ObservableCollection<Tables> SousTables
        {
            get
            {
                return _sousTables;
            }
            set
            {
                _sousTables = value;
                NotifyPropertyChanged("SousTables");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
