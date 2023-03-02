using OptiEditeur.Models;
using OptiEditeur.Services;
using OptiEditeur.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OptiEditeur.ViewModels
{
    public class TablesViewModel
    {
        private static ObservableCollection<Tables> _tableList = new();
        private static string[] _path;
        private static List<string> _keyList;
        private static ObservableCollection<string> _keyToAdd;
        private static ObservableCollection<string> _keyToDelete;

        #region PublicVar
        public static ObservableCollection<Tables> TableList
        {
            get => _tableList;
            set => _tableList = value;
        }

        public static string[] Path
        {
            get => _path;
            set => _path = value;
        }

        public static List<string> KeyList
        {
            get => _keyList;
            set => _keyList = value;
        }

        public static ObservableCollection<string> KeyToAdd
        {
            get => _keyToAdd;
            set => _keyToAdd = value;
        }

        public static ObservableCollection<string> KeyToDelete
        {
            get => _keyToDelete;
            set => _keyToDelete = value;
        }

        #endregion

        public static void InitValue(string[] path)
        {
            TableList = new();
            KeyList = new();
            var allKeys = File.ReadAllLines("./Ressources/KeyList.txt").ToList();

            foreach(var file in path)
            {
                TableList.Add(DocsSerializer.TableDeserialize(file));
                var result = allKeys.Where(x => x.StartsWith(TableList.Last().Key));
                foreach(var key in result) KeyList.Add(key);
            }

            KeyToAdd = KeyChecker.CheckKeyToAdd(TableList, KeyList);
            KeyToDelete = KeyChecker.CheckKeyToDelete(TableList, KeyList);

            if(KeyToAdd.Count > 0 && KeyAlert.Show(0, KeyToAdd))
            {
                foreach(var key in KeyToAdd)
                    TableList.AddChild(key);

                KeyToAdd.Clear();
            }

            if(KeyToDelete.Count > 0 && KeyAlert.Show(1, KeyToDelete))
            {
                foreach (var key in KeyToDelete)
                {
                    foreach(var table in TableList)
                        TableList.DeleteChild(x => x.Key == key);
                }
                KeyToDelete.Clear();
            }

            if (KeyToAdd.Count == 0 && KeyToDelete.Count == 0)
                MessageBox.Show("Vos documents sont à jours.", "Documents à jours", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void CreateValue(string path)
        {
            TableList = new();
            KeyList = File.ReadAllLines("./Ressources/KeyList.txt").ToList();
            KeyList.Sort();

            foreach(var key in KeyList)
            {
                var split = new List<string>(key.Split('.'));
                bool tableExist = false;
                foreach (var table in TableList)
                {
                    if (table.Key == split[0])
                        tableExist = true;
                }

                if (!tableExist)
                    TableList.Add(new Tables { Key = split[0], Name = split[0] });
                
                if(split.Count > 1)
                {
                    var keyTableSplit = split.GetRange(0, 2);
                    var keyTable = String.Join('.', keyTableSplit);
                    foreach (var doc in TableList)
                    {
                        if (doc.Key == split[0])
                        {
                            Tables? exist = doc.Table.Where(x => x.Key == keyTable).FirstOrDefault();
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
            DocsSerializer.TableSerialize(path, TableList);
        }

        public static void SaveValue(string[] path)
        {
            bool fileExist = true;
            foreach (var p in path)
            {
                if (!File.Exists(p) && !p.Contains(".xml"))
                    fileExist = false;
            }

            if (!fileExist)
            {
                var explorer = new System.Windows.Forms.FolderBrowserDialog();
                explorer.ShowDialog();
                if (explorer.SelectedPath != null)
                {
                    foreach (var p in path)
                    {
                        var split = p.Split('\\');
                        p.Replace(p, $"{explorer.SelectedPath}\\{split.Last()}");
                    }
                    fileExist = true;
                }
            }

            if (fileExist)
            {
                foreach (var p in path)
                    DocsSerializer.TableSerialize(p, TableList);

                MessageBox.Show("La documentation a été sauvegardée.", "Sauvegarde réussi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public static List<Tables> TablesListing(ObservableCollection<Tables> docs)
        {
            var list = new List<Tables>();
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

        private static Tables TableWriter(int count, List<string> keySplit, Tables? table = null)
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
                Tables? exist = table.Table.Where(x => x.Key == keyTable).FirstOrDefault();
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
    }
}
