using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace Html
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string font = "<base href=\"" + System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) + "\"/>";

        string xdoc = "<p>Modifiez <a href=\"https://google.fr\">le prix</a> par la case prix.<br/><img src=\"file://image/test.jpg\" alt=\"Pourquoi???\"></p>";
        static bool navigate;

        public MainWindow()
        {
            InitializeComponent();
            Init(xdoc);
            
        }

        private void Init(string xdoc)
        {
            //string mypath = "C:\\Users\\jean_\\OneDrive\\Documents\\CSharp\\test\\Html\\";
            //webTest.NavigateToString($"{font}{File.ReadAllText("Master.html")}{xdoc.Replace("\"file://", "\""+mypath)}");
            //webTest.Navigate(new Uri(System.IO.Path.GetFullPath("./fonts/test.html")));
            InitializeAsync();
        }

        private void webTest_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if(!navigate)
            {
                navigate = true;
                return;
            }

            e.Cancel = true;

            var startInfo = new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = e.Uri.ToString()
            };

            Process.Start(startInfo);
        }

        void InitializeAsync()
        {
            wvTest.Source = new Uri(Path.GetFullPath("./fonts/test.html"));
        }

        private void wvTest_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            wvTest.CoreWebView2.ContextMenuRequested += CoreWebView2_ContextMenuRequested;
            if (!navigate)
            {
                navigate = true;
                return;
            }

            e.Cancel = true;

            var startInfo = new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = e.Uri.ToString()
            };

            Process.Start(startInfo);
        }

        private void CoreWebView2_ContextMenuRequested(object? sender, CoreWebView2ContextMenuRequestedEventArgs e)
        {
            var contextMenuItems = (IList<CoreWebView2ContextMenuItem>)e.MenuItems;
            var clearItems = new List<CoreWebView2ContextMenuItem>();
            string[] deleteElement = { "saveAs", "share", "webSelect", "webCapture", "inspectElement", "other", "copyLinkToHighlight", "reload" };

            foreach (var item in contextMenuItems)
            {
                if(deleteElement.Contains(item.Name))
                    clearItems.Add(item);
            }

            foreach (var item in clearItems)
                contextMenuItems.Remove(item);
        }
    }
}
