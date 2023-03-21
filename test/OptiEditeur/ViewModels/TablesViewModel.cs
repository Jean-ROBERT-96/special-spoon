using OptiEditeur.Models;
using OptiEditeur.Services;
using OptiEditeur.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace OptiEditeur.ViewModels
{
    public class TablesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Tables> _docsList = new();
        private string[] _path;
        private List<string> _keyList;
        private ObservableCollection<string> _keyToAdd;
        private ObservableCollection<string> _keyToRemove;

        public event PropertyChangedEventHandler PropertyChanged;

        #region PublicVar
        public ObservableCollection<Tables> DocsList
        {
            get => _docsList;
            set
            {
                _docsList = value;
                NotifyPropertyChanged(nameof(DocsList));
            }
        }

        public ObservableCollection<Tables> TablesList
        {
            get => TablesListing(DocsList);
        }

        public string[] Path
        {
            get => _path;
            set
            {
                _path = value;
                NotifyPropertyChanged(nameof(Path));
            }
        }

        public List<string> KeyList
        {
            get => _keyList;
            set
            {
                _keyList = value;
                NotifyPropertyChanged(nameof(KeyList));
            }
        }

        public ObservableCollection<string> KeyToAdd
        {
            get => _keyToAdd;
            set
            {
                _keyToAdd = value;
                NotifyPropertyChanged(nameof(KeyToAdd));
            }
        }

        public ObservableCollection<string> KeyToRemove
        {
            get => _keyToRemove;
            set
            {
                _keyToRemove = value;
                NotifyPropertyChanged(nameof(KeyToRemove));
            }
        }

        #endregion

        public void InitValue(string[] path)
        {
            DocsList.Clear();
            KeyList = new();
            var allKeys = File.ReadAllLines("./Ressources/KeyList.txt").ToList();
            List<string> usedKeys = new();

            foreach(var file in path)
            {
                DocsList.Add(DocsSerializer.TablesDeserialize(file));
                usedKeys = allKeys.Where(x => x.StartsWith(DocsList.Last().Key)).ToList();
                KeyList = KeyChecker.KeyListing(DocsList.Last().Table);
            }

            KeyToAdd = KeyChecker.CheckKeyToAdd(DocsList, usedKeys);
            KeyToRemove = KeyChecker.CheckKeyToRemove(DocsList, usedKeys);

            if(KeyToAdd.Count > 0 && KeyAlert.Show(0, KeyToAdd))
            {
                foreach(var key in KeyToAdd)
                    DocsList.AddChild(key);

                KeyToAdd.Clear();
            }

            if(KeyToRemove.Count > 0 && KeyAlert.Show(1, KeyToRemove))
            {
                foreach (var key in KeyToRemove)
                {
                    foreach(var table in DocsList)
                        DocsList.RemoveChild(x => x.Key == key);
                }
                KeyToRemove.Clear();
            }
            else if(KeyToRemove.Count > 0)
                KeyToRemove = KeyList.Where(x => !x.Equals(KeyToRemove)).ToObservableCollection();

            if (KeyToAdd.Count == 0 && KeyToRemove.Count == 0)
            {
                MessageBox.Show("Vos documents sont à jours.", "Documents à jours", MessageBoxButton.OK, MessageBoxImage.Information);
                KeyToRemove = KeyList.ToObservableCollection();
            }

            NotifyPropertyChanged(nameof(DocsList));
            NotifyPropertyChanged(nameof(TablesList));
        }

        public void CreateValue(string path)
        {
            DocsList.Clear();
            KeyList = File.ReadAllLines("./Ressources/KeyList.txt").ToList();
            KeyList.Sort();
            KeyToAdd = new();
            KeyToRemove = new();

            foreach(var key in KeyList)
            {
                var split = new List<string>(key.Split('.'));
                bool tableExist = false;
                foreach (var table in DocsList)
                {
                    if (table.Key == split[0])
                        tableExist = true;
                }

                if (!tableExist)
                    DocsList.Add(new Tables { Key = split[0], Name = split[0] });
                
                if(split.Count > 1)
                {
                    var keyTableSplit = split.GetRange(0, 2);
                    var keyTable = String.Join('.', keyTableSplit);
                    foreach (var doc in DocsList)
                    {
                        if (doc.Key == split[0])
                        {
                            Tables exist = doc.Table.Where(x => x.Key == keyTable).FirstOrDefault();
                            if (exist != null)
                            {
                                int index = doc.Table.IndexOf(exist);
                                Tables res = TableWriter(2, split, exist);
                                doc.Table[index] = res;
                            }
                            else
                                doc.Table.Add(TableWriter(2, split));
                        }
                    }
                }
            }

            NotifyPropertyChanged(nameof(DocsList));
            NotifyPropertyChanged(nameof(TablesList));
        }

        public void SaveValue(string path)
        {
            bool fileExist = Directory.Exists(path);

            if (!fileExist)
            {
                var explorer = new FolderBrowserDialog();
                if (explorer.ShowDialog() == DialogResult.OK)
                {
                    path = explorer.SelectedPath;
                    fileExist = true;
                }
            }

            if (fileExist)
            {
                DocsSerializer.TableSerialize(path, DocsList);
                MessageBox.Show("La documentation a été sauvegardée.", "Sauvegarde réussi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public ObservableCollection<Tables> TablesListing(ObservableCollection<Tables> docs)
        {
            var list = new ObservableCollection<Tables>();
            foreach(var table in docs)
            {
                list.Add(table);
                if(table.Table.Count > 0)
                {
                    var result = TablesListing(table.Table);
                    foreach(var t in result) list.Add(t);
                }
            }
            return list;
        }

        private Tables TableWriter(int count, List<string> keySplit, Tables table = null)
        {
            if (table == null)
            {
                var keyList = keySplit.GetRange(0, count);
                string key = String.Join('.', keyList);
                table = new Tables { Key = key, Name = key };
            }
            count++;

            if (keySplit.Count >= count)
            {
                var keyTableSplit = keySplit.GetRange(0, count);
                var keyTable = String.Join('.', keyTableSplit);
                Tables exist = table.Table.Where(x => x.Key == keyTable).FirstOrDefault();
                if (exist != null)
                {
                    int index = table.Table.IndexOf(exist);
                    Tables res = TableWriter(count, keySplit, exist);
                    table.Table[index] = res;
                }
                else
                    table.Table.Add(TableWriter(count, keySplit));
            }
            return table;
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
