using System;
using System.Collections.ObjectModel;

namespace HumbleMaths.Calculators.Approximation {
    public interface IFunctionInterpolator {
        Func<double, double> InterpolateByPoints(ReadOnlyCollection<(double x, double y)> points);
    }
}