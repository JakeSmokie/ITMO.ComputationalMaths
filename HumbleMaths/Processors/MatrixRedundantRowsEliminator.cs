using System.Collections.Generic;
using System.Linq;
using HumbleMaths.Structures;

namespace HumbleMaths.Processors {
    public class MatrixRedundantRowsEliminator {
        public Matrix<Fraction> EliminateRedundantRows(Matrix<Fraction> input) {
            var matrix = input.CloneMatrix();
            var nullRows = new HashSet<int>();
            var eliminatingRows = new HashSet<(int, int)>();

            for (var row = 0; row < matrix.Height; row++) {
                var isRowEmpty = IsRowEmpty(matrix, row);

                if (isRowEmpty) {
                    nullRows.Add(row);
                    continue;
                }

                if (eliminatingRows.Select(x => x.Item1)
                    .Contains(row)) {
                    continue;
                }

                for (var secRow = row + 1; secRow < matrix.Height; secRow++) {
                    if (AreRowsIndependent(matrix, secRow, row)) {
                        continue;
                    }

                    eliminatingRows.Add((row, secRow));
                }
            }

            var rows = GetRemainingRows(matrix, eliminatingRows, nullRows);

            var newMatrix = new Fraction[rows.Count, matrix.Width];
            var pivotRow = 0;

            for (var row = 0; row < matrix.Height; row++) {
                if (!rows.TryGetValue(row, out _)) {
                    continue;
                }

                for (var column = 0; column < matrix.Width; column++) {
                    newMatrix[pivotRow, column] = matrix[row, column];
                }

                pivotRow += 1;
            }

            return new Matrix<Fraction>(newMatrix);
        }

        private static HashSet<int> GetRemainingRows(Matrix<Fraction> matrix, 
            HashSet<(int, int)> eliminatingRows, HashSet<int> nullRows) {
            var rows = Enumerable.Range(0, matrix.Height)
                .ToHashSet();

            rows.ExceptWith(eliminatingRows.Select(x => x.Item2));
            rows.ExceptWith(nullRows);

            return rows;
        }

        private static bool IsRowEmpty(Matrix<Fraction> matrix, int row) {
            return Enumerable.Range(0, matrix.Width)
                .All(c => matrix[row, c].IsZero());
        }

        private static bool AreRowsIndependent(Matrix<Fraction> matrix, int secRow, int row) {
            return GetRowsRatios(matrix, secRow, row)
                       .GroupBy(x => x.ToString())
                       .Count() > 1;
        }

        private static IEnumerable<Fraction> GetRowsRatios(Matrix<Fraction> matrix, int secRow, int row) {
            return Enumerable.Range(0, matrix.Width)
                .Select(column =>
                    matrix[secRow, column].IsZero() ? 0 : matrix[row, column] / matrix[secRow, column]);
        }
    }
}