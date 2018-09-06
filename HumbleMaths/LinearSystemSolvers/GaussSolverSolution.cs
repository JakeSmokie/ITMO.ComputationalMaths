using System.Collections.Generic;
using HumbleMaths.Structures;

namespace HumbleMaths.LinearSystemSolvers {
    public class GaussSolverSolution {
        public List<Matrix<double>> TriangleStabilizingSteps { get; set; }
        public List<Matrix<double>> TriangleEliminationSteps { get; set; }
        public List<Matrix<double>> SolvingSteps { get; set; }
        public List<double> Result { get; set; }

        public void Deconstruct(
            out List<Matrix<double>> triangleStabilizingSteps,
            out List<Matrix<double>> triangleEliminationSteps,
            out List<Matrix<double>> solvingSteps,
            out List<double> results)
        {
            triangleStabilizingSteps = TriangleStabilizingSteps;
            triangleEliminationSteps = TriangleEliminationSteps;
            solvingSteps = SolvingSteps;
            results = Result;
        }
    }
}