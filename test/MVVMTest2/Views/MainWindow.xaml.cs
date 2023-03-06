using Microsoft.Web.WebView2.Core;
using MVVMTest2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVVMTest2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string key = "commande.ajouter";
        private static bool navigate;

        public MainWindow()
        {
            InitializeComponent();

            if (key != null)
                docsTreeView.KeySelect = key;
        }

        private void tabSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Binding binding = new Binding();

            switch (tabSelect.SelectedIndex)
            {
                case 0:
                    binding.ElementName = nameof(docsTreeView);
                    binding.Path = new PropertyPath(nameof(docsTreeView.CurrentContent));
                    helpView.SetBinding(ContentProperty, binding);
                    break;
                case 1:
                    binding.ElementName = nameof(tablesListView);
                    binding.Path = new PropertyPath(nameof(tablesListView.CurrentContent));
                    helpView.SetBinding(ContentProperty, binding);
                    break;
            }
        }

        private void WebViewCustom_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            if(sender is WebViewCustom)
                ((WebViewCustom)sender).CoreWebView2.ContextMenuRequested += CoreWebView2_ContextMenuRequested;

            if (!navigate)
            {
                navigate = true;
                return;
            }

            if(e.Uri.StartsWith("https") || e.Uri.StartsWith("http"))
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
    }
}
