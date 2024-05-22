using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SecondTast
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BitmapSource originalBitmap;
        private InversionMode inversionMode = InversionMode.FullInversion;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFileClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog
            {
                Filter = "Bitmap Files (*.bmp)|*.bmp"
            };

            if (openDialog.ShowDialog() == true)
            {
                originalBitmap = new BitmapImage(new Uri(openDialog.FileName));
                ImageDisplay.Source = originalBitmap;
            }
        }

        private void SaveFileClick(object sender, RoutedEventArgs e)
        {
            if (originalBitmap != null)
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Bitmap Files (*.bmp)|*.bmp"
                };

                if (saveDialog.ShowDialog() == true)
                {
                    BitmapSource invertedBitmap = InvertImage(originalBitmap, inversionMode);
                    BitmapEncoder encoder = new BmpBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(invertedBitmap));
                    using (var fileStream = saveDialog.OpenFile())
                    {
                        encoder.Save(fileStream);
                    }
                }
            }
        }

        private void RadioButtonChecked(object sender, RoutedEventArgs e)
        {
            if (sender == FullInversionRadio)
            {
                inversionMode = InversionMode.FullInversion;
            }
            else if (sender == ChannelInversionRadio)
            {
                inversionMode = InversionMode.ChannelInversion;
            }

            if (originalBitmap != null)
            {
                ImageDisplay.Source = InvertImage(originalBitmap, inversionMode);
            }
        }

        private BitmapSource InvertImage(BitmapSource source, InversionMode mode)
        {
            BitmapSource result = new FormatConvertedBitmap(source, PixelFormats.Bgra32, null, 0);

            int stride = (result.PixelWidth * result.Format.BitsPerPixel + 7) / 8;
            int length = stride * result.PixelHeight;
            byte[] pixels = new byte[length];

            result.CopyPixels(pixels, stride, 0);

            for (int i = 0; i < length; i += 4)
            {
                if (mode == InversionMode.FullInversion)
                {
                    pixels[i] = (byte)(255 - pixels[i]); // Blue
                    pixels[i + 1] = (byte)(255 - pixels[i + 1]); // Green
                    pixels[i + 2] = (byte)(255 - pixels[i + 2]); // Red
                }
                else
                {
                    pixels[i] = (byte)(255 - pixels[i]); // Blue
                    pixels[i + 1] = (byte)(255 - pixels[i + 1]); // Green
                    pixels[i + 2] = (byte)(255 - pixels[i + 2]); // Red
                    pixels[i + 3] = (byte)(255 - pixels[i + 3]); // Alpha
                }
            }

            BitmapSource invertedBitmap = BitmapSource.Create(
                result.PixelWidth, result.PixelHeight, result.DpiX, result.DpiY,
                PixelFormats.Bgra32, null, pixels, stride);

            return invertedBitmap;
        }
        private enum InversionMode
        {
            FullInversion,
            ChannelInversion
        }
    }
}