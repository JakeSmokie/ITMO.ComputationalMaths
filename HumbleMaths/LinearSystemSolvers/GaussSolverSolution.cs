using System.Collections.Generic;
using HumbleMaths.Structures;

namespace HumbleMaths.LinearSystemSolvers {
    public class GaussSolverSolution {
        public List<Matrix<Fraction>> TriangleStabilizingSteps { get; set; }
        public List<Matrix<Fraction>> TriangleEliminationSteps { get; set; }
        public List<Matrix<Fraction>> SolvingSteps { get; set; }
        public List<Fraction> Result { get; set; }

        public void Deconstruct(
            out List<Matrix<Fraction>> triangleStabilizingSteps,
            out List<Matrix<Fraction>> triangleEliminationSteps,
            out List<Matrix<Fraction>> solvingSteps,
            out List<Fraction> results) {
            triangleStabilizingSteps = TriangleStabilizingSteps;
            triangleEliminationSteps = TriangleEliminationSteps;
            solvingSteps = SolvingSteps;
            results = Result;
        }
    }
}