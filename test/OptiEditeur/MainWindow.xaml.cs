using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using Microsoft.Win32;
using OptiEditeur.ViewModels;
using OptiEditeur.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Application = System.Windows.Application;
using Binding = System.Windows.Data.Binding;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using TextBox = System.Windows.Controls.TextBox;

namespace OptiEditeur
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] path;
        private static bool navigate;
        private int selectStart = -1;
        private int selectLengh;

        public static RoutedCommand InsertMarkCommand = new();
        public static RoutedCommand InsertMarkLinkCommand = new();
        public static RoutedCommand InsertMarkImageCommand = new();
        public static RoutedCommand TablesExistCommand = new();

        public new TablesViewModel DataContext
        {
            get => (TablesViewModel)base.DataContext;
            set => base.DataContext = value;
        }

        public MainWindow()
        {
            DataContext = new TablesViewModel();
            InitializeComponent();
        }

        #region Menu
        private void NewDoc(object sender, RoutedEventArgs e)
        {
            NewDocs.Show();
        }

        private void OpenDoc(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Filter = "Xml files (*.xml)|*.xml",
                Multiselect = true
            };

            bool? result = fileDialog.ShowDialog();

            if (result == true)
            {
                path = fileDialog.FileNames;
                this.DataContext.InitValue(path);
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            DataContext.SaveValue(path);
        }

        private void SaveAs(object sender, RoutedEventArgs e)
        {
            var explorer = new FolderBrowserDialog();
            var ok = explorer.ShowDialog();

            if (ok == System.Windows.Forms.DialogResult.OK)
            {
                for (int i = 0; i < path.Count(); i++)
                {
                    var split = path[i].Split('\\');
                    var newPath = $"{explorer.SelectedPath}\\{split.Last()}";
                    path[i] = newPath;
                }
                DataContext.SaveValue(path);
            }
        }

        private void QuitMenu(object sender, RoutedEventArgs e)
        {
            var rep = MessageBox.Show("Voulez vous quitter l'éditeur? Toutes informations non sauvegardé sera perdu.", "Quitter le logiciel", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (rep == MessageBoxResult.Yes)
                Environment.Exit(0);
        }

        private void AddKey(object sender, RoutedEventArgs e)
        {
            KeyManager manager = new(0);
            manager.ShowDialog();
        }

        private void DeleteKey(object sender, RoutedEventArgs e)
        {
            KeyManager manager = new(1);
            manager.ShowDialog();
        }

        private void Help(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var boxView = (TextBox)sender;

            ActualizeView(boxView.Text);
        }

        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var sd = (TextBox)sender;
            selectStart = sd.SelectionStart;
            selectLengh = sd.SelectionLength;
        }

        private void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox_TextChanged(sender, null);
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var binding = new Binding();

            switch (tabControl.SelectedIndex)
            {
                case 0:
                    binding.ElementName = nameof(tablesTreeView);
                    binding.Path = new PropertyPath(nameof(tablesTreeView.CurrentContent));
                    nameView.SetBinding(ContentProperty, binding);
                    contentView.SetBinding(ContentProperty, binding);
                    break;
                case 1:
                    binding.ElementName = nameof(tablesGridView);
                    binding.Path = new PropertyPath(nameof(tablesGridView.CurrentContent));
                    nameView.SetBinding(ContentProperty, binding);
                    contentView.SetBinding(ContentProperty, binding);
                    break;
            }
        }

        #region WebView
        private async void ActualizeView(string? text)
        {
            await webView.EnsureCoreWebView2Async();
            if (!String.IsNullOrEmpty(text))
            {
                text = Regex.Replace(text, "\r\n|\n", "<br>");
                text = $"<p>{text}</p>";
                webView.NavigateToString(text);
            }
            else
            {
                webView.NavigateToString("<p></p>");
            }
        }

        private void WebViewCustom_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            if (sender is WebView2 custom)
            {
                custom.CoreWebView2.ContextMenuRequested += CoreWebView2_ContextMenuRequested;

                string dir = $"{Environment.CurrentDirectory}\\Ressources\\Images";
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                custom.CoreWebView2.SetVirtualHostNameToFolderMapping("ressources.info", dir, CoreWebView2HostResourceAccessKind.Allow);
            }

            if (!navigate)
            {
                navigate = true;
                return;
            }

            if (e.Uri.StartsWith("https") || e.Uri.StartsWith("http"))
            {
                e.Cancel = true;

                var startInfo = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    FileName = e.Uri
                };

                Process.Start(startInfo);
            }
        }

        private void CoreWebView2_ContextMenuRequested(object? sender, CoreWebView2ContextMenuRequestedEventArgs e)
        {
            var contextMenuItems = (IList<CoreWebView2ContextMenuItem>)e.MenuItems;
            var clearItems = new List<CoreWebView2ContextMenuItem>();
            string[] deleteElement = { "saveAs", "share", "webSelect", "webCapture", "inspectElement", "other", "copyLinkToHighlight", "reload" };

            foreach (var item in contextMenuItems)
            {
                if (deleteElement.Contains(item.Name))
                    clearItems.Add(item);
            }

            foreach (var item in clearItems)
                contextMenuItems.Remove(item);
        }

        #endregion

        #region StyleCommand
        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.selectLengh > 0;
        }

        private void ImageBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = selectStart > -1 & selectLengh == 0;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (selectLengh > 0)
            {
                var table = tabControl.SelectedIndex == 0 ? tablesTreeView.CurrentContent : tablesGridView.CurrentContent;
                table.Content = InsertMark(table.Content, "<{1}>{0}</{1}>", selectStart, selectLengh, e.Parameter?.ToString());
                selectLengh = 0;
                selectStart = 0;
            }
        }

        private string InsertMark(string content, string pattern, int selectStart, int selectLengh, string mark)
        {
            string selected = content.Substring(selectStart, selectLengh);
            string newSelected = String.Format(pattern, selected, mark);
            string before = content.Substring(0, selectStart);
            string after = content.Substring(selectStart + selectLengh);
            return before + newSelected + after;
        }

        private void MarkLink_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string link = AddLink.Show();
            if(!String.IsNullOrEmpty(link))
            {
                var table = tabControl.SelectedIndex == 0 ? tablesTreeView.CurrentContent : tablesGridView.CurrentContent;
                table.Content = InsertMarkLink(table.Content, "<a href=\"{1}\">{0}</a>", selectStart, selectLengh, link);
                selectLengh = 0;
                selectStart = 0;
            }
        }

        private string InsertMarkLink(string content, string pattern, int selectStart, int selectLengh, string link)
        {
            string selected = content.Substring(selectStart, selectLengh);
            string newSelected = String.Format(pattern, selected, link);
            string before = content.Substring(0, selectStart);
            string after = content.Substring(selectStart + selectLengh);
            return before + newSelected + after;
        }

        private void MarkImage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Image files |*.jpg;*.jpeg;*.png",
                Multiselect = false
            };
            
            bool result = (bool)dialog.ShowDialog();
            if(result)
            {
                string dir = $"{Environment.CurrentDirectory}\\Ressources\\Images";
                if(!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                var image = ImageResize.Show(dialog.FileName);
                if(image != null)
                {
                    image.Save($"{dir}\\{dialog.SafeFileName}");
                    var table = tabControl.SelectedIndex == 0 ? tablesTreeView.CurrentContent : tablesGridView.CurrentContent;
                    table.Content = InsertMarkImage(table.Content, "<img src=\"{0}\">", selectStart, $"https://ressources.info/{dialog.SafeFileName}");
                    selectLengh = 0;
                    selectStart = 0;
                }
            }
        }

        private string InsertMarkImage(string content, string pattern, int selectStart, string image)
        {
            string file = String.Format(pattern, image);
            string before = content.Substring(0, selectStart);
            string after = content.Substring(selectStart);

            return before + file + after;
        }

        #endregion
    }
}
