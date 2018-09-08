using System.Collections.Generic;
using HumbleMaths.LinearSystemSolvers;
using HumbleMaths.Structures;

namespace HumbleMathsWeb.Models {
    public class MatrixModel {
        public string Matrix { get; set; }
        public GaussSolverSolution Solution { get; set; }
        public Matrix<Fraction> System { get; set; }
    }
}