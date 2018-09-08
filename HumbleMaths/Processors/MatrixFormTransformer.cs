using System;
using System.Collections.Generic;
using System.Linq;
using HumbleMaths.Structures;

namespace HumbleMaths.Processors {
    public class MatrixFormTransformer {
        private readonly MatrixFormChecker _formChecker = new MatrixFormChecker();

        /// <exception cref="ArgumentException">Threw if matrix cannot be transformed</exception>
        public MatrixTransformationResult MatrixToTriangular(Matrix<Fraction> input) {
            var matrix = input.CloneMatrix();
            var steps = new List<(TransformType Type, Matrix<Fraction> Matrix)>();

            var row = 0;
            var column = 0;

            while (row < matrix.Height && column < matrix.Width) {
                if (!StabilizeRowEchelon(matrix, row, column)) {
                    column += 1;
                    continue;
                }

                EliminateItems(matrix, row, column);

                row += 1;
                column += 1;
            }

            return new MatrixTransformationResult {
                Steps = steps,
                Result = matrix
            };
        }

        private static int GetMaxIndexOfColumn(Matrix<Fraction> matrix, int row, int column) {
            var max = row;

            for (var i = row; i < matrix.Height; i++) {
                if (matrix[i, column] > matrix[max, column]) {
                    max = i;
                }
            }

            return max;
        }

        private static bool StabilizeRowEchelon(Matrix<Fraction> matrix, int row, int column) {
            var maxRow = GetMaxIndexOfColumn(matrix, row, column);

            if (matrix[maxRow, column].IsZero()) {
                return false;
            }

            matrix.SwapRows(maxRow, row);
            return true;
        }

        private static void EliminateItems(Matrix<Fraction> matrix, int row, int column) {
            for (var i = row + 1; i < matrix.Height; i++) {
                var mul = matrix[i, column] / matrix[row, column];
                matrix[i, column] = 0;

                for (var j = column + 1; j < matrix.Width; j++) {
                    matrix[i, j] -= matrix[row, j] * mul;
                }
            }
        }
    }
}