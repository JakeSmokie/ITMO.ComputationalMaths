using System.Collections.Generic;
using System.Diagnostics;
using HumbleMaths.LinearSystemSolvers;
using HumbleMaths.Structures;

namespace HumbleMathsWeb.Models {
    public class MatrixModel {
        public GaussSolverSolution Solution { get; set; }
        public Matrix<Fraction> System { get; set; }
        public Stopwatch Stopwatch { get; set; }
        public string Input { get; set; }
    }
}