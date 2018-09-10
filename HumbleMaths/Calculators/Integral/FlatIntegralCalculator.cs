using System;
using System.Linq;
using HumbleMaths.Calculators.Integral.Flat;

namespace HumbleMaths.Calculators.Integral {
    public class FlatIntegralCalculator : IIntegralCalculator {
        private readonly IFlatPartCalculator _flatPartCalculator;

        public FlatIntegralCalculator(IFlatPartCalculator flatPartCalculator) {
            _flatPartCalculator = flatPartCalculator;
        }

        public (double Integral, int PartsAmount) Calculate(
            Func<double, double> func, double start, double end, double precision) {
            var resultMultiplier = 1;

            if (start > end) {
                (end, start) = (start, end);
                resultMultiplier = -1;
            }

            var partsAmount = (int) ((end - start) / precision);

            if (partsAmount < 0) {
                partsAmount = 0;
            }

            if (start + partsAmount * precision >= end) {
                partsAmount -= 1;
            }

            return (
                Enumerable.Range(0, partsAmount - 1)
                    .AsParallel()
                    .Sum(i => _flatPartCalculator.CalculatePart(func, start, end, precision, i)) * resultMultiplier,
                partsAmount
            );
        }

        public string Formula { get; }
    }
}