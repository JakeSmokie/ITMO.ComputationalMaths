using System.Collections.Generic;
using System.Linq;
using HumbleMaths.Processors;
using HumbleMaths.Structures;

namespace HumbleMaths.LinearSystemSolvers {
    public class GaussSolverSolution {
        public List<(TransformType Type, Matrix<Fraction> Matrix)> TransformationSteps { get; set; } =
            new List<(TransformType Type, Matrix<Fraction> Matrix)>();

        public List<Matrix<Fraction>> SolvingSteps { get; set; } = new List<Matrix<Fraction>>();
        public List<Fraction> Result { get; set; } = new List<Fraction>();
        public Matrix<Fraction> EliminationStep { get; set; }

        public Fraction GetDeterminantByGauss() {
            var stabilizingStepsCount = TransformationSteps.Count(x => x.Type == TransformType.Stabilizing);

            var matrix = TransformationSteps
                .Last(x => x.Type == TransformType.Elimination).Matrix;

            var determinant = matrix.MainDiagonalItems
                .Aggregate(new Fraction(1), (x, y) => x * y);

            if (stabilizingStepsCount % 2 == 1) {
                determinant *= -1;
            }

            return determinant;
        }
    }
}