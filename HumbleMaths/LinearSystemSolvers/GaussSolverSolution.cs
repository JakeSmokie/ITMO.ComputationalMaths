using System.Collections.Generic;
using System.Linq;
using HumbleMaths.Structures;

namespace HumbleMaths.LinearSystemSolvers {
    public class GaussSolverSolution {
        public List<Matrix<Fraction>> TriangleStabilizingSteps { get; set; }
        public List<Matrix<Fraction>> TriangleEliminationSteps { get; set; }
        public List<Matrix<Fraction>> SolvingSteps { get; set; }
        public List<Fraction> Result { get; set; }

        public Fraction GetDeterminantByGauss() {
            var stabilizingStepsCount = TriangleStabilizingSteps.Count;
            var matrix = TriangleEliminationSteps.Last();

            var determinant = matrix.MainDiagonalItems
                .Aggregate(new Fraction(1), (x, y) => x * y);

            if (stabilizingStepsCount % 2 == 1) {
                determinant *= -1;
            }

            return determinant;
        }

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