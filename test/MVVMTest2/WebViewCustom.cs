using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using MVVMTest2.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVMTest2
{
    public class WebViewCustom : WebView2
    {
        public static readonly DependencyProperty CurrentContentProperty =
            DependencyProperty.Register(
                "CurrentContent",
                typeof(IContent),
                typeof(WebViewCustom),
                new PropertyMetadata(null, OnContentChanged));

        public IContent CurrentContent
        {
            get => (IContent)GetValue(CurrentContentProperty);
            set => SetValue(CurrentContentProperty, value);
        }

        private static async void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (WebViewCustom)d;
            await self.EnsureCoreWebView2Async();

            if (self.CurrentContent != null)
                self.NavigateToString($"{File.ReadAllText("./Documentations/Master.html")}{self.CurrentContent.Content}");
            else
                self.NavigateToString("<p></p>");
        }
    }
}
