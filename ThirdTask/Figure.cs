using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace ThirdTask
{

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


    public class Pentagon : ShapeElement
    {
        public int SideLength { get; } = 50;

        public Pentagon(string text, Color color, int sideLength)
            : base(text, color)
        {
            SideLength = sideLength;
        }
        public Pentagon(string text, Color color)
            : base(text, color) { }

        public override void Draw(Canvas canvas, int x, int y)
        {
            var polygon = new Polygon
            {
                Stroke = new SolidColorBrush(Color),
                StrokeThickness = 2
            };

            for (int i = 0; i < 5; i++)
            {
                double angle = (2 * Math.PI / 5) * i - Math.PI / 2; // Починаємо з верхньої вершини
                double px = x + SideLength * Math.Cos(angle);
                double py = y + SideLength * Math.Sin(angle);
                polygon.Points.Add(new Point(px, py));
            }

            canvas.Children.Add(polygon);
            AddText(canvas, x, y, Text);
        }
    }

}
