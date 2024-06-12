using System.Drawing;
using FitRectangle.Model.Enums;

namespace FitRectangle.Model.Interfaces;

public interface IRectangleFitter
{
    FitterPointStrategy PointStrategy { get; set; }
    FitterColorStrategy ColorStrategy { get; set; }
    HashSet<Color> Colors { get; set; }

    void Fit(ref Rectangle main, IEnumerable<IRectangle> inner);
}