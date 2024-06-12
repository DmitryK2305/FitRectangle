using System.Drawing;
using FitRectangle.Model;
using FitRectangle.Model.Enums;
using FitRectangle.Model.Interfaces;
using Point = FitRectangle.Model.Point;
using Rectangle = FitRectangle.Model.Rectangle;

namespace FitRectangle.Test;

[TestClass]
public class RectangleFitterTest
{
    private static Point MainBigTopLeft = new (-5, 11.6);
    private static Point MainBigBotRight = new (10.1, -3.3);
    private static Point MainSmallTopLeft = new (-1, 6.7);
    private static Point MainSmallBotRight = new (6.9, -1.3);

    private static IRectangle[] Inners = new[]
    {
        new Rectangle(new Point(-3, 7), new Point(2,3), Color.Red),
        new Rectangle(new Point(0,4), new Point(4, 0), Color.Green),
        new Rectangle(new Point(1, 10.2), new Point(5, 6.2), Color.Green),
        new Rectangle(new Point(3.5, 6.4), new Point(8.5, 2.4), Color.Blue)
    };

    [TestMethod]
    public void Fit_Big_Ok()
    {
        Rectangle main = new Rectangle(MainBigBotRight, MainBigTopLeft);
        RectangleFitter fitter = new RectangleFitter();
        
        fitter.Fit(ref main, Inners);

        Assert.AreEqual(-3, main.TopLeft.X);
        Assert.AreEqual(10.2, main.TopLeft.Y);
        Assert.AreEqual(8.5, main.BotRight.X);
        Assert.AreEqual(0, main.BotRight.Y);
    }
    
    [TestMethod]
    public void Fit_BigOnlyInside_Ok()
    {
        Rectangle main = new Rectangle(MainBigBotRight, MainBigTopLeft);
        RectangleFitter fitter = new RectangleFitter();
        fitter.PointStrategy = FitterPointStrategy.OnlyInside;
        
        fitter.Fit(ref main, Inners);

        Assert.AreEqual(-3, main.TopLeft.X);
        Assert.AreEqual(10.2, main.TopLeft.Y);
        Assert.AreEqual(8.5, main.BotRight.X);
        Assert.AreEqual(0, main.BotRight.Y);
    }
    
    [TestMethod]
    public void Fit_BigWL_Ok()
    {
        Rectangle main = new Rectangle(MainBigBotRight, MainBigTopLeft);
        RectangleFitter fitter = new RectangleFitter();
        fitter.ColorStrategy = FitterColorStrategy.WhiteList;
        fitter.Colors.Add(Color.Green);
        
        fitter.Fit(ref main, Inners);

        Assert.AreEqual(0, main.TopLeft.X);
        Assert.AreEqual(10.2, main.TopLeft.Y);
        Assert.AreEqual(5, main.BotRight.X);
        Assert.AreEqual(0, main.BotRight.Y);
    }
    
    [TestMethod]
    public void Fit_BigWL2_Ok()
    {
        Rectangle main = new Rectangle(MainBigBotRight, MainBigTopLeft);
        RectangleFitter fitter = new RectangleFitter();
        fitter.ColorStrategy = FitterColorStrategy.WhiteList;
        fitter.Colors.Add(Color.Red);
        fitter.Colors.Add(Color.Blue);
        
        fitter.Fit(ref main, Inners);

        Assert.AreEqual(-3, main.TopLeft.X);
        Assert.AreEqual(7, main.TopLeft.Y);
        Assert.AreEqual(8.5, main.BotRight.X);
        Assert.AreEqual(2.4, main.BotRight.Y);
    }
    
    [TestMethod]
    public void Fit_BigBL_Ok()
    {
        Rectangle main = new Rectangle(MainBigBotRight, MainBigTopLeft);
        RectangleFitter fitter = new RectangleFitter();
        fitter.ColorStrategy = FitterColorStrategy.BlackList;
        fitter.Colors.Add(Color.Green);
        
        fitter.Fit(ref main, Inners);

        Assert.AreEqual(-3, main.TopLeft.X);
        Assert.AreEqual(7, main.TopLeft.Y);
        Assert.AreEqual(8.5, main.BotRight.X);
        Assert.AreEqual(2.4, main.BotRight.Y);
    }
    
    [TestMethod]
    public void Fit_BigBL2_Ok()
    {
        Rectangle main = new Rectangle(MainBigBotRight, MainBigTopLeft);
        RectangleFitter fitter = new RectangleFitter();
        fitter.ColorStrategy = FitterColorStrategy.BlackList;
        fitter.Colors.Add(Color.Red);
        fitter.Colors.Add(Color.Blue);
        
        fitter.Fit(ref main, Inners);

        Assert.AreEqual(0, main.TopLeft.X);
        Assert.AreEqual(10.2, main.TopLeft.Y);
        Assert.AreEqual(5, main.BotRight.X);
        Assert.AreEqual(0, main.BotRight.Y);
    }
    
    [TestMethod]
    public void Fit_Small_Ok()
    {
        Rectangle main = new Rectangle(MainSmallBotRight, MainSmallTopLeft);
        RectangleFitter fitter = new RectangleFitter();
        
        fitter.Fit(ref main, Inners);

        Assert.AreEqual(-3, main.TopLeft.X);
        Assert.AreEqual(10.2, main.TopLeft.Y);
        Assert.AreEqual(8.5, main.BotRight.X);
        Assert.AreEqual(0, main.BotRight.Y);
    }

    [TestMethod]
    public void Fit_SmallOnlyInside_Ok()
    {
        Rectangle main = new Rectangle(MainSmallBotRight, MainSmallTopLeft);
        RectangleFitter fitter = new RectangleFitter();
        fitter.PointStrategy = FitterPointStrategy.OnlyInside;
        
        fitter.Fit(ref main, Inners);

        Assert.AreEqual(0, main.TopLeft.X);
        Assert.AreEqual(6.4, main.TopLeft.Y);
        Assert.AreEqual(5, main.BotRight.X);
        Assert.AreEqual(0, main.BotRight.Y);
    }
    
    [TestMethod]
    public void Fit_SmallOnlyInsideWL_Ok()
    {
        Rectangle main = new Rectangle(MainSmallBotRight, MainSmallTopLeft);
        RectangleFitter fitter = new RectangleFitter();
        fitter.PointStrategy = FitterPointStrategy.OnlyInside;
        fitter.ColorStrategy = FitterColorStrategy.WhiteList;
        fitter.Colors.Add(Color.Green);
        
        fitter.Fit(ref main, Inners);

        Assert.AreEqual(0, main.TopLeft.X);
        Assert.AreEqual(6.2, main.TopLeft.Y);
        Assert.AreEqual(5, main.BotRight.X);
        Assert.AreEqual(0, main.BotRight.Y);
    }
}