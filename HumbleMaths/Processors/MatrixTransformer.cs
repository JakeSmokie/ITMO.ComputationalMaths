using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HumbleMaths.Structures;

namespace HumbleMaths.Processors {
    public class MatrixTransformer {
        private readonly MatrixFormChecker _formChecker = new MatrixFormChecker();

        public MatrixTransformationResult MatrixToTriangular(Matrix<Fraction> input) {
            var matrix = input.CloneMatrix();
            var startMatrix = input.CloneMatrix();
            var steps = new List<(TransformType Type, Matrix<Fraction> Matrix)>();

            var row = 0;
            var column = 0;

            try {
                while (row < matrix.Height && column < matrix.Width) {
                    if (!StabilizeRowEchelon(matrix, row, column, steps)) {
                        column += 1;
                        continue;
                    }

                    EliminateItems(matrix, row, column);

                    if (!matrix.StringEquals(steps.LastOrDefault().Matrix ?? startMatrix)) {
                        steps.Add((TransformType.Elimination, matrix.CloneMatrix()));
                    }

                    row += 1;
                    column += 1;
                }
            }
            catch {
                // ignored
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

        private static bool StabilizeRowEchelon(Matrix<Fraction> matrix, int row, int column,
            List<(TransformType Type, Matrix<Fraction> Matrix)> steps) {
            var maxRow = GetMaxIndexOfColumn(matrix, row, column);

            if (matrix[maxRow, column].IsZero()) {
                return false;
            }

            matrix.SwapRows(maxRow, row);

            if (maxRow != row) {
                steps.Add((TransformType.Stabilizing, matrix.CloneMatrix()));
            }

            return true;
        }

        private static void EliminateItems(Matrix<Fraction> matrix, int row, int column) {
            for (var i = row + 1; i < matrix.Height; i++) {
                var mul = matrix[i, column] / matrix[row, column];
                matrix[i, column] = 0;

                var partitioner = Partitioner.Create(column + 1, matrix.Width);

                Parallel.ForEach(partitioner, range => {
                    for (var j = range.Item1; j < range.Item2; j++) {
                        matrix[i, j] -= matrix[row, j] * mul;
                    }
                });
            }
        }
    }
}