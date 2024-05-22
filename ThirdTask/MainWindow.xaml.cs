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
                element = new EquilateralTriangle(text, color);
            }
            else if (SquareRadioButton.IsChecked == true)
            {
                element = new Square(text, color);
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

    public abstract class ShapeElement
    {
        public string Text { get; }
        public Color Color { get; }

        protected ShapeElement(string text, Color color)
        {
            Text = text;
            Color = color;
        }

        public abstract void Draw(Canvas canvas, int x, int y);

        protected void AddText(Canvas canvas, int x, int y, string text)
        {
            var textBlock = new TextBlock
            {
                Text = text,
                Foreground = new SolidColorBrush(Colors.Black),
                Background = new SolidColorBrush(Colors.White)
            };

            Canvas.SetLeft(textBlock, x - 10);
            Canvas.SetTop(textBlock, y - 10);

            canvas.Children.Add(textBlock);
        }
    }


    public class Rhombus : ShapeElement
    {
        public int Diagonal1 { get; }
        public int Diagonal2 { get; }

        public Rhombus(string text, Color color, int diagonal1, int diagonal2)
            : base(text, color)
        {
            Diagonal1 = diagonal1;
            Diagonal2 = diagonal2;
        }

        public override void Draw(Canvas canvas, int x, int y)
        {
            var polygon = new Polygon
            {
                Stroke = new SolidColorBrush(Color),
                StrokeThickness = 2
            };

            polygon.Points.Add(new Point(x, y - Diagonal2 / 2));
            polygon.Points.Add(new Point(x + Diagonal1 / 2, y));
            polygon.Points.Add(new Point(x, y + Diagonal2 / 2));
            polygon.Points.Add(new Point(x - Diagonal1 / 2, y));

            canvas.Children.Add(polygon);
            AddText(canvas, x, y, Text);
        }
    }


    public class EquilateralTriangle : ShapeElement
    {
        public EquilateralTriangle(string text, Color color)
            : base(text, color)
        {
        }

        public override void Draw(Canvas canvas, int x, int y)
        {
            var polygon = new Polygon
            {
                Stroke = new SolidColorBrush(Color),
                StrokeThickness = 2
            };

            double height = Math.Sqrt(3) / 2 * 50;
            polygon.Points.Add(new Point(x, y - height / 2));
            polygon.Points.Add(new Point(x - 25, y + height / 2));
            polygon.Points.Add(new Point(x + 25, y + height / 2));

            canvas.Children.Add(polygon);
            AddText(canvas, x, y, Text);
        }
    }


    public class Square : ShapeElement
    {
        public Square(string text, Color color)
            : base(text, color)
        {
        }

        public override void Draw(Canvas canvas, int x, int y)
        {
            var rect = new Rectangle
            {
                Width = 50,
                Height = 50,
                Stroke = new SolidColorBrush(Color),
                StrokeThickness = 2
            };

            Canvas.SetLeft(rect, x - 25);
            Canvas.SetTop(rect, y - 25);

            canvas.Children.Add(rect);
            AddText(canvas, x, y, Text);
        }
    }

    /*private static void AddText(Canvas canvas, int x, int y, string text)
    {
        var textBlock = new TextBlock
        {
            Text = text,
            Foreground = new SolidColorBrush(Colors.Black),
            Background = new SolidColorBrush(Colors.White)
        };

        Canvas.SetLeft(textBlock, x - 10);
        Canvas.SetTop(textBlock, y - 10);

        canvas.Children.Add(textBlock);
    }*/

}
