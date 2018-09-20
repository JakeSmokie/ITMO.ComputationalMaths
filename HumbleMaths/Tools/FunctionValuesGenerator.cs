using System;
using System.Collections.Generic;
using System.Linq;

namespace HumbleMaths.Tools {
    public class FunctionValuesGenerator {
        public IEnumerable<(double x, double y)> GenerateValues(
            Func<double, double> function, double start, double end, double step) {
            var count = (int) ((end - start) / step);

            return Enumerable.Range(0, count)
                .AsParallel()
                .Select(i => start + i * step)
                .Select(x => (x, function(x)))
                .OrderBy(p => p.x);
        }
    }
}