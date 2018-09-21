using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HumbleMathsWeb.Models {
    public class LagrangeModel {
        public string FunctionExpression { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Step { get; set; }
        public string InterpolationStep { get; set; }

        public ReadOnlyCollection<(double x, double y)> Values { get; set; } =
            new List<(double x, double y)>().AsReadOnly();

        public ReadOnlyCollection<(double x, double y)> InterpolatedValues { get; set; } =
            new List<(double x, double y)>().AsReadOnly();

        public ReadOnlyCollection<(double x, double y)> InterpolationNodes { get; set; } =
            new List<(double x, double y)>().AsReadOnly();
    }
}