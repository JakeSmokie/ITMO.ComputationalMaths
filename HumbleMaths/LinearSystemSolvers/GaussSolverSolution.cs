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
            var eliminationSteps = TriangleEliminationSteps;
            var stabilizingStepsCount = TriangleStabilizingSteps.Count;

            var detMatrix = eliminationSteps.Last();

            var secondDet = Enumerable.Range(0, detMatrix.Height)
                .Select(x => detMatrix[x, x])
                .Aggregate(new Fraction(1), (x, y) => x * y);

            if (stabilizingStepsCount % 2 == 1) {
                secondDet *= -1;
            }

            return secondDet;
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