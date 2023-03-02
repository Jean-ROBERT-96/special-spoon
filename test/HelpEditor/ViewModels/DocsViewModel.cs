using HelpEditor.Interfaces;
using HelpEditor.Models;
using HelpEditor.Ressources;
using HelpEditor.Services;
using HelpEditor.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Serialization;
using MessageBox = System.Windows.MessageBox;

namespace HelpEditor.ViewModels
{
    public class DocsViewModel
    {
        private static ObservableCollection<Docs> _docsList = new ObservableCollection<Docs>();
        private static string[] _path;
        private static List<string> _keyList;
        private static ObservableCollection<string> _keyToAdd;
        private static ObservableCollection<string> _keyToRemove;

        #region Variable public
        public static ObservableCollection<Docs> DocsList
        {
            get => _docsList;
            set => _docsList = value;
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

        public static ObservableCollection<string> KeyToRemove
        {
            get => _keyToRemove;
            set => _keyToRemove = value;
        }

        #endregion

        public void InitValue(string[] path)
        {
            DocsList = new ObservableCollection<Docs>();
            KeyList = new();
            KeyToRemove = new();
            var keys = File.ReadAllLines("./Ressources/KeyList.txt").ToList();
            foreach(var p in path)
            {
                DocsList.Add(DocsSerializer.DocDeserializer(p));
                var result = keys.Where(x => x.StartsWith(DocsList.Last().Key));
                foreach(var r in result) KeyList.Add(r);
            }

            foreach (var doc in DocsList)
            {
                var result = KeyManager.CheckKeyToRemove(doc, KeyList);
                foreach(var r in result) KeyToRemove.Add(r);
            }

            KeyToAdd = KeyManager.CheckKeyToAdd(DocsList, KeyList);

            if (KeyToAdd.Count != 0)
            {
                var result = Views.Message.Show(0, KeyToAdd);
                if (result.Equals(DialogResult.Yes))
                {
                    foreach (var item in KeyToAdd)
                    {
                        DocsList.AddChild(item);
                    }
                    KeyToAdd.Clear();
                }
            }

            if (KeyToRemove.Count != 0)
            {
                var result = Views.Message.Show(1, KeyToRemove);
                if (result.Equals(DialogResult.Yes))
                {
                    foreach (var item in KeyToRemove)
                    {
                        foreach (var doc in DocsList)
                        {
                            doc.Table.RemoveChild(x => x.Key == item);
                        }
                    }
                    KeyToRemove.Clear();
                }
            }

            if (KeyToAdd.Count == 0 && KeyToRemove.Count == 0)
                MessageBox.Show("La documentation est à jour.", "Documentation à jour.", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void CreateValue(string path)
        {
            DocsList = new ObservableCollection<Docs>();
            string[] keyList = File.ReadAllLines("./Ressources/KeyList.txt");
            Array.Sort(keyList);

            foreach (string key in keyList)
            {
                var split = new List<string>(key.Split('.'));
                bool docExist = false;
                foreach (var doc in DocsList)
                {
                    if (doc.Key == split[0])
                        docExist = true;
                }

                if (!docExist)
                    DocsList.Add(new Docs { Key = split[0], Name = split[0] });

                if (split.Count > 1)
                {
                    var keyTableSplit = split.GetRange(0, 2);
                    var keyTable = String.Join('.', keyTableSplit);
                    foreach (var doc in DocsList)
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

            DocsSerializer.DocSerializer(path, DocsList);
        }

        public void SaveValue(string[] path)
        {
            bool fileExist = true;
            foreach(var p in path)
            {
                if(!File.Exists(p) && !p.Contains(".xml"))
                    fileExist = false;
            }

            if (!fileExist)
            {
                var explorer = new FolderBrowserDialog();
                explorer.ShowDialog();

                if(explorer.SelectedPath != null)
                {
                    foreach (var p in path)
                    {
                        var split = p.Split('\\');
                        p.Replace(p, $"{explorer.SelectedPath}\\{split.Last()}");
                    }

                    fileExist = true;
                }
            }

            if(fileExist)
            {
                foreach (var p in path)
                    DocsSerializer.DocSerializer(p, DocsList);

                MessageBox.Show("La documentation a été sauvegardée.", "Sauvegarde réussi.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private Tables TableWriter(int count, List<string> keySplit, Tables? table = null)
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
