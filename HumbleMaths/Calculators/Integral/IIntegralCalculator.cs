using System;

namespace HumbleMaths.Calculators.Integral {
    public interface IIntegralCalculator {
        string Formula { get; }

        (double Integral, int PartsAmount) Calculate(
            Func<double, double> func, double start, double end, double precision);
    }
}