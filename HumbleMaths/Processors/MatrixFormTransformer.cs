using System;
using System.Collections.Generic;
using System.Linq;
using HumbleMaths.Extensions;
using HumbleMaths.Structures;

namespace HumbleMaths.Processors {
    public class MatrixFormTransformer {
        private readonly MatrixFormChecker _matrixFormChecker = new MatrixFormChecker();

        /// <exception cref="ArgumentException">Threw if matrix cannot be transformed</exception>
        public MatrixTransformationResult MatrixToTriangular(Matrix<double> input)
        {
            var (stabilizingSteps, matrix) = StabilizeMainDiagonal(input);
            var (eliminationSteps, result) = EliminateRows(matrix);

            return new MatrixTransformationResult {
                StabilizingSteps = stabilizingSteps,
                EliminationSteps = eliminationSteps,
                Result = result
            };
        }

        private static (List<Matrix<double>> steps, Matrix<double> result) EliminateRows(Matrix<double> input)
        {
            var matrix = input.CloneMatrix();
            var steps = new List<Matrix<double>>();

            for (var src = 0; src < matrix.Height; src++) {
                for (var dest = src + 1; dest < matrix.Height; dest++) {
                    EliminateRow(matrix, src, dest);
                    steps.Add(matrix.CloneMatrix());
                }
            }

            return (steps, matrix);
        }

        private static void EliminateRow(Matrix<double> matrix, int src, int dest)
        {
            var destMultiplier = matrix[src, src];
            var srcMultiplier = matrix[dest, src];

            for (var i = 0; i < matrix.Width; i++) {
                matrix[dest, i] *= destMultiplier;
                matrix[dest, i] -= matrix[src, i] * srcMultiplier;

                if (matrix[dest, i] == -0.0) {
                    matrix[dest, i] = 0.0;
                }
            }
        }

        private static (List<Matrix<double>> steps, Matrix<double> result) StabilizeMainDiagonal(
            Matrix<double> input)
        {
            var matrix = input.CloneMatrix();
            var steps = new List<Matrix<double>>();

            // Tries to swap rows with zero valued main diagonal elements
            // with rows those have non - zero valued elements
            // on same column as diagonal element

            for (var column = 0; column < matrix.Width - 1; column++) {
                if (matrix[column, column] != 0) {
                    continue;
                }

                SwapRowWithNonZeroRow(matrix, column, steps);
            }

            return (steps, steps.LastOrDefault() ?? matrix);
        }

        private static void SwapRowWithNonZeroRow(
            Matrix<double> matrix, int column, List<Matrix<double>> steps)
        {
            for (var row = column + 1; row < matrix.Height; row++) {
                if (matrix[row, column].IsZero()) {
                    continue;
                }

                var stepMatrix = matrix.CloneMatrix();
                stepMatrix.SwapRows(column, row);

                steps.Add(stepMatrix);
                return;
            }

            // no non-zero element found
            throw new ArgumentException("Input matrix cannot be transformed into triangular form");
        }
    }
}