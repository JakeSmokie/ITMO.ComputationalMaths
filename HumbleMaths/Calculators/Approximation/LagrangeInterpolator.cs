using System;
using System.Collections.ObjectModel;
using System.Linq;
using HumbleMaths.Extensions;

namespace HumbleMaths.Calculators.Approximation {
    public class LagrangeInterpolator : IFunctionInterpolator {
        public Func<double, double> InterpolateByPoints(ReadOnlyCollection<(double x, double y)> points) {
            return x => points
                .AsParallel()
                .Select((p, i) => p.y * CalculateMultipliers(p.x, x, i))
                .Sum();

            double CalculateMultipliers(double first, double x, int index) {
                return points
                    .Select(p => p.x)
                    .SkipElementAtIndex(index)
                    .AsParallel()
                    .Aggregate(1.0, (acc, second) =>
                        acc *
                        (x - second) /
                        (first - second)
                    );
            }
        }
    }
}