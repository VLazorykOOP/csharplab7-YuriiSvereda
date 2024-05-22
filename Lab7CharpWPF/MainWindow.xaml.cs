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
using System.Windows.Threading;

namespace Lab7CharpWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private List<string> metafiles;
        private int currentIndex;

        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(IntervalTextBox.Text, out int interval))
            {
                timer.Interval = TimeSpan.FromMilliseconds(interval);
                LoadMetafiles();
                currentIndex = 0;
                if (metafiles.Count > 0)
                {
                    timer.Start();
                }
                else
                {
                    MessageBox.Show("No metafiles loaded.");
                }
            }
            else
            {
                MessageBox.Show("Invalid interval.");
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void LoadMetafiles()
        {
            metafiles = Memo.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (metafiles.Count == 0) return;

            var metafile = metafiles[currentIndex];
            var bitmap = new BitmapImage(new Uri(metafile, UriKind.RelativeOrAbsolute));
            PictureBox.Source = bitmap;

            currentIndex = (currentIndex + 1) % metafiles.Count;
        }

    }
}