using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SecondTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BitmapSource originalBitmap;
        private BitmapSource invertedBitmap;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Bitmap Files (*.bmp)|*.bmp";

            if (openDialog.ShowDialog() == true)
            {
                originalBitmap = new BitmapImage(new Uri(openDialog.FileName));
                ImageControl.Source = originalBitmap;
                SaveButton.IsEnabled = true;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Bitmap Files (*.bmp)|*.bmp";

            if (saveDialog.ShowDialog() == true)
            {
                BitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(invertedBitmap));
                using (var fileStream = saveDialog.OpenFile())
                {
                    encoder.Save(fileStream);
                }
            }
        }

        private void InvertImage(bool fullInversion)
        {
            if (originalBitmap == null)
                return;

            int width = originalBitmap.PixelWidth;
            int height = originalBitmap.PixelHeight;
            int stride = width * ((originalBitmap.Format.BitsPerPixel + 7) / 8);
            byte[] pixels = new byte[height * stride];
            originalBitmap.CopyPixels(pixels, stride, 0);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y * stride + x * 4;
                    byte b = pixels[index];
                    byte g = pixels[index + 1];
                    byte r = pixels[index + 2];
                    byte a = pixels[index + 3];

                    if (fullInversion)
                    {
                        pixels[index] = (byte)(255 - b);
                        pixels[index + 1] = (byte)(255 - g);
                        pixels[index + 2] = (byte)(255 - r);
                    }
                    else
                    {
                        pixels[index] = (byte)(255 - pixels[index]);
                        pixels[index + 1] = (byte)(255 - pixels[index + 1]);
                        pixels[index + 2] = (byte)(255 - pixels[index + 2]);
                    }
                }
            }

            invertedBitmap = BitmapSource.Create(width, height, 96, 96, originalBitmap.Format, null, pixels, stride);
            ImageControl.Source = invertedBitmap;
        }

        private void FullInversionRadio_Checked(object sender, RoutedEventArgs e)
        {
            InvertImage(true);
        }

        private void ComponentInversionRadio_Checked(object sender, RoutedEventArgs e)
        {
            InvertImage(false);
        }
    }
}