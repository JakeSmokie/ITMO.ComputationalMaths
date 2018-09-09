using System;

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
        public double IntegralValue { get; set; }
        public int PartsAmount { get; set; }
        public TimeSpan CalculationTime { get; set; }
    }
}