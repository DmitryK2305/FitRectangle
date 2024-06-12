using System.Drawing;
using FitRectangle.Model.Interfaces;
using static System.Math;

namespace FitRectangle.Model;

public class Rectangle : IRectangle
{
    public Color Color { get; }
    public Point BotLeft { get; private set; }
    public Point TopLeft { get; private set; }
    public Point TopRight { get; private set; }
    public Point BotRight { get; private set; }

    public Rectangle(Point p1, Point p2)
    {
        Change(p1, p2);
    }

    public Rectangle(Point p1, Point p2, Color color) : this(p1, p2)
    {
        Color = color;
    }

    public void Change(Point p1, Point p2)
    {
        double xMin = Min(p1.X, p2.X);
        double xMax = Max(p1.X, p2.X);
        double yMin = Min(p1.Y, p2.Y);
        double yMax = Max(p1.Y, p2.Y);

        BotLeft = new Point(xMin, yMin);
        TopLeft = new Point(xMin, yMax);
        TopRight = new Point(xMax, yMax);
        BotRight = new Point(xMax, yMin);
    }

    public ClippingResult Clip(IRectangle otherRect)
    {
        List<Point> points = new List<Point>();

        if (PointIn(BotLeft))
        {
            points.Add(BotLeft);
        }
        if (PointIn(TopLeft))
        {
            points.Add(TopLeft);
        }
        if (PointIn(TopRight))
        {
            points.Add(TopRight);
        }
        if (PointIn(BotRight))
        {
            points.Add(BotRight);
        }

        return new ClippingResult() { Clipped = points.Count != 4, Points = points.ToArray() };
    }

    private bool PointIn(Point point)
    {
        return point.X <= BotRight.X && point.X >= TopLeft.X && point.Y <= TopLeft.Y && point.Y >= BotRight.Y;
    }
}