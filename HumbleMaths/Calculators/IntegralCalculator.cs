using System;
using System.Linq;

namespace HumbleMaths.Calculators {
    public class IntegralCalculator : IIntegralCalculator {
        private readonly double _precision;

        public IntegralCalculator(double precision) {
            _precision = precision;
        }

        public double Calculate(Func<double, double> func, double start, double end) {
            var resultMultiplier = 1;

            if (start > end) {
                (end, start) = (start, end);
                resultMultiplier = -1;
            }

            var partsAmount = (int) ((end - start) / _precision);

            return Enumerable.Range(0, partsAmount)
                .AsParallel()
                .Select(x => func(start + x * _precision) * _precision)
                .Sum() * resultMultiplier;
        }
    }
}