using System;
using System.Collections.Generic;
using HumbleMaths.Processors;
using HumbleMaths.Structures;

namespace HumbleMaths.LinearSystemSolvers {
    public class GaussSolver {
        private readonly MatrixFormTransformer _formTransformer = new MatrixFormTransformer();

        public GaussSolverSolution SolveSystem(Matrix<Fraction> system) {
                var (steps, matrix) = _formTransformer.MatrixToTriangular(system);

            var solvingSteps = new List<Matrix<Fraction>>();
            var result = new List<Fraction>();

            for (var row = matrix.Height - 1; row >= 0; row--) {
                DivideRowByMainDiagonalElement(matrix, row);
                solvingSteps.Add(matrix.CloneMatrix());

                EliminateRowMainDiagonalElement(matrix, row);
                solvingSteps.Add(matrix.CloneMatrix());

                result.Insert(0, matrix[row, matrix.Width - 1]);
            }

            return new GaussSolverSolution {
                TransformationSteps = steps,
                SolvingSteps = solvingSteps,
                Result = result
            };
        }

        private static void DivideRowByMainDiagonalElement(Matrix<Fraction> matrix, int row) {
            var divider = matrix[row, row];

            if (divider.IsZero()) {
                throw new ArgumentException("System has no unique solutions");
            }

            for (var column = 0; column < matrix.Width; column++) {
                matrix[row, column] /= divider;
            }
        }

        private static void EliminateRowMainDiagonalElement(Matrix<Fraction> matrix, int row) {
            for (var destRow = 0; destRow < row; destRow++) {
                matrix[destRow, matrix.Width - 1] -= matrix[destRow, row] * matrix[row, matrix.Width - 1];
                matrix[destRow, row] = 0;
            }
        }
    }
}