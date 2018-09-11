using System;
using System.Collections.Generic;
using HumbleMaths.Calculators;
using HumbleMaths.Calculators.Integral;
using HumbleMaths.Calculators.Integral.Flat;

namespace HumbleMathsWeb.Models {
    public class IntegralModel {
        public string FunctionExpression { get; set; }
        public string StartAsText { get; set; }
        public string EndAsText { get; set; }
        public string PrecisionAsText { get; set; }
        public double Start { get; set; }
        public double End { get; set; }
        public double Precision { get; set; }
        public Func<double, double> Func { get; set; }
        public Func<double, double> IntegralFunc { get; set; }
        public double IntegralValue { get; set; }
        public int PartsAmount { get; set; }
        public TimeSpan CalculationTime { get; set; }
        public string MethodAsText { get; set; }
        public string IntegralFunctionExpression { get; set; }
        public bool CalculationFinished { get; set; }
        public IIntegralCalculator IntegralCalculator { get; set; }

        public Dictionary<string, IIntegralCalculator> Methods { get; } =
            new Dictionary<string, IIntegralCalculator> {
                ["Метод прямоугольников (левые)"] = new FlatIntegralCalculator(new EdgeFlatPartCalculator(0.0)),
                ["Метод прямоугольников (средние)"] = new FlatIntegralCalculator(new EdgeFlatPartCalculator(0.5)),
                ["Метод прямоугольников (правые)"] = new FlatIntegralCalculator(new EdgeFlatPartCalculator(1.0)),
                ["Метод трапеций"] = new FlatIntegralCalculator(new TrapezoidalPartCalculator())
            };
    }
}