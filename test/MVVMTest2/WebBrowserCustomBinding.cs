using MVVMTest2.Models;
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
                typeof(Tables),
                typeof(WebBrowserCustomBinding),
                new PropertyMetadata(null, OnContentChanged));

        public static void SetValue(DependencyObject obj, Tables value)
        {
            obj.SetValue(CurrentContentProperty, value);
        }

        public static Tables GetValue(DependencyObject obj)
        {
            return (Tables)obj.GetValue(CurrentContentProperty);
        }

        public static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (WebBrowser)d;
            Tables content = (Tables)e.NewValue;

            if (content != null)
                self.NavigateToString($"{File.ReadAllText("./Documentations/Master.html")}{content.Content}");
            else
                self.NavigateToString("<p></p>");
        }
    }
}
