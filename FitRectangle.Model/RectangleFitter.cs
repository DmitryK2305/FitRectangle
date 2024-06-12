using System.Drawing;
using FitRectangle.Model.Enums;
using FitRectangle.Model.Interfaces;
using NLog;

namespace FitRectangle.Model;

public class RectangleFitter
{
    private static readonly ILogger Logger;

    static RectangleFitter()
    {
        Logger = LogManager.GetCurrentClassLogger();
    }

    public FitterPointStrategy PointStrategy { get; set; }
    public FitterColorStrategy ColorStrategy { get; set; }
    public HashSet<Color> Colors { get; set; } = new HashSet<Color>();


    public void Fit(ref Rectangle main, IEnumerable<IRectangle> inner)
    {
        Logger.Debug($"Fitting ({main.TopLeft.X}, {main.TopLeft.Y})");
        var selectedRects = inner;
        switch (ColorStrategy)
        {
            case FitterColorStrategy.All:
                break;
            case FitterColorStrategy.WhiteList:
                selectedRects = selectedRects.Where(r => Colors.Contains(r.Color));
                break;
            case FitterColorStrategy.BlackList:
                selectedRects = selectedRects.Where(r => !Colors.Contains(r.Color));
                break;
        }
        Logger.Debug($"ColorStrategy: {ColorStrategy}");
        
        RectangleBorders rectangleBorders = new RectangleBorders();
        bool onlyInside = PointStrategy == FitterPointStrategy.OnlyInside;
        Logger.Debug($"PointStrategy: {PointStrategy}");
        foreach (IRectangle rect in selectedRects)
        {
            if (onlyInside)
            {
                ClippingResult result = rect.Clip(main);
                if (result.Clipped)
                {
                    foreach (Point point in result.Points)
                    {
                        rectangleBorders.Expand(point);
                    }
                    
                    continue;
                }
            }
            
            rectangleBorders.Expand(rect);
        }

        main.Change(new Point(rectangleBorders.MinX.Value, rectangleBorders.MaxY.Value), new Point(rectangleBorders.MaxX.Value, rectangleBorders.MinY.Value));
        Logger.Debug($"Fitting result: ({main.TopLeft.X}, {main.TopLeft.Y})");
    }
}

struct RectangleBorders
{
    public double? MinX = null;
    public double? MaxX = null;
    public double? MinY = null;
    public double? MaxY = null;

    public RectangleBorders(double minX, double maxX, double minY, double maxY)
    {
        MinX = minX;
        MaxX = maxX;
        MinY = minY;
        MaxY = maxY;
    }

    public void Expand(IRectangle rectangle)
    {
        if (MinX is null || rectangle.TopLeft.X < MinX)
            MinX = rectangle.TopLeft.X;
        if (MaxX is null || rectangle.BotRight.X > MaxX)
            MaxX = rectangle.BotRight.X;
        if (MinY is null || rectangle.BotRight.Y < MinY)
            MinY = rectangle.BotRight.Y;
        if (MaxY is null || rectangle.TopLeft.Y > MaxY)
            MaxY = rectangle.TopLeft.Y;
    }

    public void Expand(Point point)
    {
        if (MinX is null || point.X < MinX)
            MinX = point.X;
        if (MaxX is null || point.X > MaxX)
            MaxX = point.X;
        if (MinY is null || point.Y < MinY)
            MinY = point.Y;
        if (MaxY is null || point.Y > MaxY)
            MaxY = point.Y;
    }
}