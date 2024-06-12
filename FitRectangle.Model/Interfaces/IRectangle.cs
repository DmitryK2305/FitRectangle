using System.Drawing;

namespace FitRectangle.Model.Interfaces;

public interface IRectangle
{
    Color Color { get; }
    Point BotLeft { get; }
    Point TopLeft { get; }
    Point TopRight { get; }
    Point BotRight { get; }

    void Change(Point p1, Point p2);
    ClippingResult Clip(IRectangle otherRect);
    bool IsPointIn(Point point);
}