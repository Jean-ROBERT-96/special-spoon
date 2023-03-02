using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestDiversXMLtoWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AddText();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

        private void AddText()
        {
            TextBlock textBlock = new TextBlock();

            textBlock.Inlines.Add(TextOne("Bonjour, ", 15));
            textBlock.Inlines.Add(TextTwo("comment ça va?", "blue"));
            textBlock.Inlines.Add("Moi bien en tout ca.");

            AddElementText.Children.Add(textBlock);
        }

        private TextBlock TextOne(string text, int size)
        {
            TextBlock textAdd = new TextBlock();

            textAdd.FontSize = size;
            textAdd.Inlines.Add(text);

            return textAdd;
        }

        private TextBlock TextTwo(string text, string color)
        {
            TextBlock textAdd = new TextBlock();

            textAdd.Foreground = Brushes.Blue;
            textAdd.Inlines.Add(text);
            textAdd.Inlines.Add(new LineBreak());
            textAdd.Inlines.Add("Autre texte...");

            return textAdd;
        }

        private TextBlock TextThree(string text)
        {
            TextBlock textAdd = new TextBlock();

            textAdd.Inlines.Add(text);

            return textAdd;
        }
    }
}
