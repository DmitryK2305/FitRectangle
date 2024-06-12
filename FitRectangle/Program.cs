// See https://aka.ms/new-console-template for more information

using System.Drawing;
using FitRectangle.Model;
using FitRectangle.Model.Interfaces;
using Point = FitRectangle.Model.Point;
using Rectangle = FitRectangle.Model.Rectangle;

Point mainBigTopLeft = new (-5, 11.6);
Point mainBigBotRight = new (10.1, -3.3);

Rectangle main = new Rectangle(mainBigBotRight, mainBigTopLeft);
IRectangle[] Inners = new[]
{
    new Rectangle(new Point(-3, 7), new Point(2,3), Color.Red),
    new Rectangle(new Point(0,4), new Point(4, 0), Color.Green),
    new Rectangle(new Point(1, 10.2), new Point(5, 6.2), Color.Green),
    new Rectangle(new Point(3.5, 6.4), new Point(8.5, 2.4), Color.Blue)
};
RectangleFitter fitter = new RectangleFitter();
        
fitter.Fit(ref main, Inners);