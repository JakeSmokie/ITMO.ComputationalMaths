using System;
using System.Collections.Generic;
using HumbleMaths.Extensions;
using HumbleMaths.Processors;
using HumbleMaths.Structures;

namespace HumbleMaths.LinearSystemSolvers {
    public class GaussSolver {
        private readonly MatrixFormTransformer _formTransformer = new MatrixFormTransformer();

        public GaussSolverSolution SolveSystem(Matrix<double> system)
        {
            var (stabilizingSteps, eliminationSteps, matrix) =
                _formTransformer.MatrixToTriangular(system);

            var solvingSteps = new List<Matrix<double>>();
            var result = new List<double>();

            for (var row = matrix.Height - 1; row >= 0; row--) {
                DivideRowByMainDiagonalElement(matrix, row);
                solvingSteps.Add(matrix.CloneMatrix());

                EliminateRowMainDiagonalElement(matrix, row);
                solvingSteps.Add(matrix.CloneMatrix());

                result.Insert(0, matrix[row, matrix.Width - 1]);
            }

            return new GaussSolverSolution {
                TriangleStabilizingSteps = stabilizingSteps,
                TriangleEliminationSteps = eliminationSteps,
                SolvingSteps = solvingSteps,
                Result = result
            };
        }

        private static void DivideRowByMainDiagonalElement(Matrix<double> matrix, int row)
        {
            var divider = matrix[row, row];

            if (divider.IsZero()) {
                throw new ArgumentException("System has no unique solutions");
            }

            for (var column = 0; column < matrix.Width; column++) {
                matrix[row, column] /= divider;
            }
        }

        private static void EliminateRowMainDiagonalElement(Matrix<double> matrix, int row)
        {
            for (var destRow = 0; destRow < row; destRow++) {
                matrix[destRow, matrix.Width - 1] -= matrix[destRow, row] * matrix[row, matrix.Width - 1];
                matrix[destRow, row] = 0;
            }
        }
    }
}