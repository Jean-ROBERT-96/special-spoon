using HelpEditor.ViewModels;
using HelpEditor.Views;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
using static System.Net.Mime.MediaTypeNames;
using Binding = System.Windows.Data.Binding;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using TextBox = System.Windows.Controls.TextBox;

namespace HelpEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] path;
        private static bool navigate;
        string select;
        string beforeSelect;
        string afterSelect;

        string CurrentText
        {
            get => docsTreeView.CurrentContent.Content;
            set => docsTreeView.CurrentContent.Content = value;
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Menu
        private void NewDoc(object sender, RoutedEventArgs e)
        {
            var newDoc = NewDocs.GetInstance();
            newDoc.ShowDialog();
        }

        private void OpenDoc(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Filter = "Xml files (*.xml)|*.xml",
                Multiselect = true
            };

            bool? result = fileDialog.ShowDialog();

            if(result == true)
            {
                path = fileDialog.FileNames;
                docsTreeView.ViewModel.InitValue(path);
                docsTreeView.TreeViewDataContext();
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            docsTreeView.ViewModel.SaveValue(path);
        }

        private void SaveAs(object sender, RoutedEventArgs e)
        {
            var explorer = new FolderBrowserDialog();
            var ok = explorer.ShowDialog();

            if(ok == System.Windows.Forms.DialogResult.OK)
            {
                for (int i = 0; i < path.Count(); i++)
                {
                    var split = path[i].Split('\\');
                    var newPath = $"{explorer.SelectedPath}\\{split.Last()}";
                    path[i] = newPath;
                }
                docsTreeView.ViewModel.SaveValue(path);
            }
        }

        private void QuitMenu(object sender, RoutedEventArgs e)
        {
            var rep = MessageBox.Show("Voulez vous quitter l'éditeur? Toutes informations non sauvegardé sera perdu.", "Quitter le logiciel", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(rep == MessageBoxResult.Yes)
                Environment.Exit(0);
        }

        private void AddKey(object sender, RoutedEventArgs e)
        {
            ManageKeys manage = new(0);
            manage.ShowDialog();
        }

        private void DeleteKey(object sender, RoutedEventArgs e)
        {
            ManageKeys manage = new(1);
            manage.ShowDialog();
        }

        private void Help(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var binding = new Binding();

            switch(tabControl.SelectedIndex)
            {
                case 0:
                    binding.ElementName = nameof(docsTreeView);
                    binding.Path = new PropertyPath(nameof(docsTreeView.CurrentContent));
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

        private void docsTreeView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(DocsViewModel.DocsList.Count > 0)
            {
                save.IsEnabled = true;
                saveAs.IsEnabled = true;
                keyMenu.IsEnabled = true;
                buttonStack.IsEnabled = true;
                dockButton.IsEnabled = true;
            }
            else
            {
                save.IsEnabled = false;
                saveAs.IsEnabled = false;
                keyMenu.IsEnabled = false;
                buttonStack.IsEnabled = false;
                dockButton.IsEnabled = false;
            }
        }

        #region StyleButton
        private void boldButton_Click(object sender, RoutedEventArgs e)
        {
            if(!String.IsNullOrEmpty(select))
            {
                string text = $"<strong>{select}</strong>";
                CurrentText = beforeSelect + text + afterSelect;
            }
        }

        private void italicButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(select))
            {
                string text = $"<i>{select}</i>";
                CurrentText = beforeSelect + text + afterSelect;
            }
        }

        private void underlineButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(select))
            {
                string text = $"<u>{select}</u>";
                CurrentText = beforeSelect + text + afterSelect;
            }
        }

        private void hyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(select))
            {
                string text = $"<a href=\"\">{select}</a>";
                CurrentText = beforeSelect + text + afterSelect;
            }
        }

        private void h1Button_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(select))
            {
                string text = $"<h1>{select}</h1>";
                CurrentText = beforeSelect + text + afterSelect;
            }
        }

        private void h2Button_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(select))
            {
                string text = $"<h2>{select}</h2>";
                CurrentText = beforeSelect + text + afterSelect;
            }
        }

        private void h3Button_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(select))
            {
                string text = $"<h3>{select}</h3>";
                CurrentText = beforeSelect + text + afterSelect;
            }
        }

        private void imageButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(select))
            {
                string text = $"<img href=\"\">{select}</img>";
                CurrentText = beforeSelect + text + afterSelect;
            }
        }

        private void saveText_Click(object sender, RoutedEventArgs e)
        {
            if(sender is TextBox text)
            {
                docsTreeView.CurrentContent.Content = text.Text;
                docsTreeView.ViewModel.SaveValue(path);
            }
        }

        #endregion

        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if(sender is TextBox sd)
            {
                var selectStart = sd.SelectionStart;
                var selectLengh = sd.SelectionLength;
                if(selectLengh > 0)
                {
                    select = sd.Text.Substring(selectStart, selectLengh);
                    beforeSelect = sd.Text.Substring(0, selectStart);
                    afterSelect = sd.Text.Substring(selectStart + selectLengh);
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(sender is TextBox text)
            {
                if (!String.IsNullOrEmpty(text.Text) && tabControl.SelectedIndex == 0)
                    Actualize(text.Text);
                else if (!String.IsNullOrEmpty(text.Text) && tabControl.SelectedIndex == 1)
                    Actualize(text.Text);
                else
                    Actualize(null);
                /*else if (!String.IsNullOrEmpty(CurrentText) && tabControl.SelectedIndex == 0)
                    Actualize(CurrentText);
                else if (!String.IsNullOrEmpty(CurrentText) && tabControl.SelectedIndex == 1)
                    Actualize(CurrentText);*/
            }
        }

        #region View
        public async void Actualize(string text)
        {
            await view.EnsureCoreWebView2Async();
            if (!String.IsNullOrEmpty(text))
            {
                text = Regex.Replace(text, "\r\n|\n", "<br>");
                text = $"<p>{text}</p>";
                view.NavigateToString(text);
            }
            else
            {
                view.NavigateToString("<p></p>");
            }
        }

        private void WebViewCustom_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            if (sender is WebView2 custom)
                custom.CoreWebView2.ContextMenuRequested += CoreWebView2_ContextMenuRequested;

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

        private void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox_TextChanged(sender, null);
        }
    }
}
