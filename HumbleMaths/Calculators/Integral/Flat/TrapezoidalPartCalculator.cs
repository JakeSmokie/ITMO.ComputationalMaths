using System;

namespace HumbleMaths.Calculators.Integral.Flat {
    public class TrapezoidalPartCalculator : IFlatPartCalculator {
        public double CalculatePart(Func<double, double> func, double start, double end, double precision, int part) {
            var baseSum = func(start + (part + 1) * precision) + func(start + part * precision);
            var trapezoid = baseSum / 2 * precision;

            return trapezoid;
        }
    }
} 
