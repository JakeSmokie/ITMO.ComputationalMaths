using System;

namespace HumbleMaths.Calculators.Integral {
    public interface IIntegralCalculator {
        (double Integral, int PartsAmount) Calculate(
            Func<double, double> func, double start, double end, double precision);
    }
}