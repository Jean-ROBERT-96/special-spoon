using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Image = System.Drawing.Image;
using Size = System.Drawing.Size;

namespace OptiEditeur.Views
{
    /// <summary>
    /// Logique d'interaction pour ImageResize.xaml
    /// </summary>
    public partial class ImageResize : Window
    {
        private static ImageResize _instance;
        public static Image result;
        public static BitmapImage image;

        public ImageResize(string path)
        {
            image = new(new Uri(path, UriKind.Absolute));
            result = new Bitmap(Image.FromFile(path));
            InitializeComponent();

            HeightImage.Text = Math.Round(image.Height).ToString();
            WidthImage.Text = Math.Round(image.Width).ToString();

            DataContext = image;
        }

        public static Image Show(string path)
        {
            _instance ??= new(path);
            _instance.ShowDialog();
            return result;
        }

        private void Image_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(String.IsNullOrEmpty(HeightImage.Text) || String.IsNullOrEmpty(WidthImage.Text))
            {
                imageView.Source = image;
            }
            else
            {
                var res = ResizeImage((Bitmap)result, new Size
                { 
                    Height = int.Parse(HeightImage.Text),
                    Width = int.Parse(WidthImage.Text)
                });
                imageView.Source = SourceBitmapConverter((Bitmap)res);
            }
        }

        private Image ResizeImage(Bitmap img, Size size)
        {
            return new Bitmap(img, size);
        }

        private BitmapSource SourceBitmapConverter(Bitmap bmp)
        {
            return Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        private void DefaultValueClick(object sender, RoutedEventArgs e)
        {
            HeightImage.Text = Math.Round(image.Height).ToString();
            WidthImage.Text = Math.Round(image.Width).ToString();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            result = null;
            _instance.Close();
        }

        private void ValidButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(WidthImage.Text) && !String.IsNullOrEmpty(HeightImage.Text))
            {
                result = ResizeImage((Bitmap)result, new Size
                {
                    Height = int.Parse(HeightImage.Text),
                    Width = int.Parse(WidthImage.Text)
                });
                _instance.Close();
            }
            else
                MessageBox.Show("Valeurs entrées incorrectes.", "Erreur d'entrée", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Image_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
