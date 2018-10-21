using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HumbleMaths.Calculators.FunctionIntegrators {
    public class EulerIntegrator : IFunctionIntegrator {
        public ReadOnlyCollection<(double x, double z)> IntegrateFunction(
            Func<double, double, double> fFunction, Func<double, double> yFunction,
            (double x, double z) caucheSolution, double stepLength, int stepsAmount) {
            var values = new List<(double x, double z)>(stepsAmount) {
                caucheSolution
            };

            var (x, y) = caucheSolution;

            for (var i = 1; i < stepsAmount; i++) {
                x += stepLength;
                y += stepLength * fFunction(x, yFunction(x));

                values.Add((x, y));
            }

            return values.AsReadOnly();
        }
    }
}