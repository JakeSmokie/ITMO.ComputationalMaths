using System;
using System.Collections.ObjectModel;

namespace HumbleMaths.Calculators.FunctionIntegrators {
    public interface IFunctionIntegrator {
        ReadOnlyCollection<(double x, double z)> IntegrateFunction(
            Func<double, double, double> fFunction, Func<double, double> yFunction,
            (double x, double z) caucheSolution, double stepLength, int stepsAmount);
    }
}