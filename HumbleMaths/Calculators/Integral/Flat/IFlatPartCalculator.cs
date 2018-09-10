using System;

namespace HumbleMaths.Calculators.Integral.Flat {
    public interface IFlatPartCalculator {
        double CalculatePart(Func<double, double> func, double start, double end, double precision, int part);
    }
}