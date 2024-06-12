namespace FitRectangle.Model;

public struct ClippingResult
{
    public bool Clipped { get; set; }
    public Point[] Points { get; set; }
}