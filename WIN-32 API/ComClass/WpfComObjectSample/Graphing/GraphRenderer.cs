using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfComObjectSample.Graphing;

public sealed class GraphRenderer
{
    private const double Padding = 40;
    private readonly GraphCalculator _calculator = new();

    public void Draw(Canvas canvas, GraphOptions options)
    {
        canvas.Children.Clear();

        var width = canvas.ActualWidth;
        var height = canvas.ActualHeight;

        if (width <= Padding * 2 || height <= Padding * 2)
        {
            return;
        }

        var scale = Math.Min((width - Padding * 2) / (options.XRange * 2), (height - Padding * 2) / 10);
        var originX = width / 2;
        var originY = height / 2;

        DrawGrid(canvas, width, height, originX, originY, scale);
        DrawAxes(canvas, width, height, originX, originY);
        DrawFunction(canvas, options, width, originX, originY, scale);
    }

    private static void DrawGrid(Canvas canvas, double width, double height, double originX, double originY, double scale)
    {
        var gridBrush = new SolidColorBrush(Color.FromRgb(230, 234, 240));

        for (var x = originX; x < width; x += scale)
        {
            AddLine(canvas, x, 0, x, height, gridBrush, 1);
        }

        for (var x = originX - scale; x > 0; x -= scale)
        {
            AddLine(canvas, x, 0, x, height, gridBrush, 1);
        }

        for (var y = originY; y < height; y += scale)
        {
            AddLine(canvas, 0, y, width, y, gridBrush, 1);
        }

        for (var y = originY - scale; y > 0; y -= scale)
        {
            AddLine(canvas, 0, y, width, y, gridBrush, 1);
        }
    }

    private static void DrawAxes(Canvas canvas, double width, double height, double originX, double originY)
    {
        var axisBrush = new SolidColorBrush(Color.FromRgb(45, 55, 72));

        AddLine(canvas, 0, originY, width, originY, axisBrush, 2);
        AddLine(canvas, originX, 0, originX, height, axisBrush, 2);
    }

    private void DrawFunction(Canvas canvas, GraphOptions options, double width, double originX, double originY, double scale)
    {
        var polyline = new Polyline
        {
            Stroke = new SolidColorBrush(Color.FromRgb(12, 111, 214)),
            StrokeThickness = 3
        };

        for (var screenX = 0; screenX <= width; screenX += 1)
        {
            var x = (screenX - originX) / scale;
            var y = _calculator.CalculateY(x, options);
            var screenY = originY - y * scale;

            if (!double.IsNaN(screenY) && !double.IsInfinity(screenY))
            {
                polyline.Points.Add(new System.Windows.Point(screenX, screenY));
            }
        }

        canvas.Children.Add(polyline);
    }

    private static void AddLine(Canvas canvas, double x1, double y1, double x2, double y2, Brush stroke, double thickness)
    {
        canvas.Children.Add(new Line
        {
            X1 = x1,
            Y1 = y1,
            X2 = x2,
            Y2 = y2,
            Stroke = stroke,
            StrokeThickness = thickness
        });
    }
}
