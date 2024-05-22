/*using System;
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
    public abstract class Figure
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Size { get; set; }
        public Brush Color { get; set; }

        public abstract void Draw(Canvas canvas);
    }

    public class Pentagon : Figure
    {
        public override void Draw(Canvas canvas)
        {
            Polygon pentagon = new Polygon
            {
                Stroke = Color,
                Fill = Color,
                StrokeThickness = 2,
                Points = new PointCollection()
                {
                    new Point(X, Y),
                    new Point(X + Size, Y),
                    new Point(X + Size * 1.5, Y + Size),
                    new Point(X + Size / 2, Y + Size * 1.5),
                    new Point(X - Size / 2, Y + Size)
                }
            };
            canvas.Children.Add(pentagon);
        }
    }

    public class Rhombus : Figure
    {
        public override void Draw(Canvas canvas)
        {
            Polygon rhombus = new Polygon
            {
                Stroke = Color,
                Fill = Color,
                StrokeThickness = 2,
                Points = new PointCollection()
                {
                    new Point(X, Y),
                    new Point(X + Size, Y - Size / 2),
                    new Point(X + Size * 2, Y),
                    new Point(X + Size, Y + Size / 2)
                }
            };
            canvas.Children.Add(rhombus);
        }
    }

    public class Triangle : Figure
    {
        public string Text { get; set; }

        public override void Draw(Canvas canvas)
        {
            Polygon triangle = new Polygon
            {
                Stroke = Color,
                Fill = Color,
                StrokeThickness = 2,
                Points = new PointCollection()
                {
                    new Point(X, Y),
                    new Point(X + Size, Y),
                    new Point(X + Size / 2, Y - Math.Sqrt(Size * Size - (Size / 2) * (Size / 2)))
                }
            };
            canvas.Children.Add(triangle);

            TextBlock textBlock = new TextBlock
            {
                Text = Text,
                Foreground = Brushes.Black,
                Background = Brushes.White,
                FontSize = 12,
                Width = Size,
                TextAlignment = TextAlignment.Center
            };

            Canvas.SetLeft(textBlock, X);
            Canvas.SetTop(textBlock, Y - Size / 2);
            canvas.Children.Add(textBlock);
        }
    }
}
*/