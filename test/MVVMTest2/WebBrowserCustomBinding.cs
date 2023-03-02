using MVVMTest2.Interfaces;
using MVVMTest2.Views;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MVVMTest2
{
    public class WebBrowserCustomBinding
    {
        public static readonly DependencyProperty CurrentContentProperty =
            DependencyProperty.RegisterAttached(
                "Value",
                typeof(IContent),
                typeof(WebBrowserCustomBinding),
                new PropertyMetadata(null, OnContentChanged));

        public static void SetValue(DependencyObject obj, IContent value)
        {
            obj.SetValue(CurrentContentProperty, value);
        }

        public static IContent GetValue(DependencyObject obj)
        {
            return (IContent)obj.GetValue(CurrentContentProperty);
        }

        public static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (WebBrowser)d;
            IContent content = (IContent)e.NewValue;

            if (content != null)
                self.NavigateToString($"{File.ReadAllText("./Documentations/Master.html")}{content.Content}");
            else
                self.NavigateToString("<p></p>");
        }
    }
}
