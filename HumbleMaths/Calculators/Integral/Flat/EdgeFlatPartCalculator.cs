using System;

namespace HumbleMaths.Calculators.Integral.Flat {
    public class EdgeFlatPartCalculator : IFlatPartCalculator {
        private readonly double _offset;

        public EdgeFlatPartCalculator(double offset) {
            if (offset < 0 || offset > 1) {
                throw new ArgumentException("Offset value should be between 0 and 1");
            }

            _offset = offset;
        }

        public double CalculatePart(Func<double, double> func, double start, double end, double precision, int part) {
            return func(start + (part + _offset) * precision) * precision;
        }
    }
}