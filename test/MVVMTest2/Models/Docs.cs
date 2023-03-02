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
    public class Docs : INotifyPropertyChanged, IContent
    {
        private string _docsname;
        private string _key;
        private string _content;
        private ObservableCollection<Tables> _tables = new ObservableCollection<Tables>();

        public string Docsname
        {
            get
            {
                return _docsname;
            }
            set
            {
                _docsname = value;
                NotifyPropertyChanged("DocsName");
            }
        }

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

        public ObservableCollection<Tables> Tables
        {
            get
            {
                return _tables;
            }
            set
            {
                _tables = value;
                NotifyPropertyChanged("Tables");
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
