using System.IO;
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

namespace ThirdTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<ShapeElement> elements = new List<ShapeElement>();
        private Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddElementButton_Click(object sender, RoutedEventArgs e)
        {
            string colorName = ((ComboBoxItem)ColorComboBox.SelectedItem).Content.ToString();
            Color color = (Color)ColorConverter.ConvertFromString(colorName);
            string text = ElementTextTextBox.Text;
            ShapeElement element = null;

            if (RhombusRadioButton.IsChecked == true)
            {
                int diag1 = int.Parse(RhombusParam1TextBox.Text);
                int diag2 = int.Parse(RhombusParam2TextBox.Text);
                element = new Rhombus(text, color, diag1, diag2);
            }
            else if (TriangleRadioButton.IsChecked == true)
            {
                element = new EquilateralTriangle(text, color, int.Parse(UniversalField.Text));
            }
            else if (SquareRadioButton.IsChecked == true)
            {
                element = new Pentagon(text, color, int.Parse(UniversalField.Text));
            }

            if (element != null)
            {
                elements.Add(element);
            }
        }

        private void GenerateDrawingButton_Click(object sender, RoutedEventArgs e)
        {
            DrawingCanvas.Children.Clear();
            int elementCount = int.Parse(ElementsCountTextBox.Text);

            for (int i = 0; i < elementCount; i++)
            {
                int x = random.Next((int)DrawingCanvas.ActualWidth);
                int y = random.Next((int)DrawingCanvas.ActualHeight);
                var element = elements[random.Next(elements.Count)];
                element.Draw(DrawingCanvas, x, y);
            }
        }
    }



}
