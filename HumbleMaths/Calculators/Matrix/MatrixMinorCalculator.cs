using HumbleMaths.Structures;

namespace HumbleMaths.Calculators.Matrix {
    public class MatrixMinorCalculator {
        public Matrix<Fraction> GetMinor(Matrix<Fraction> matrix, int skippedRow, int skippedColumn) {
            var size = matrix.Height - 1;
            var cells = new Fraction[size, size];

            for (var row = 0; row < matrix.Height; row++) {
                if (row == skippedRow) {
                    continue;
                }

                for (var column = 0; column < matrix.Width; column++) {
                    if (column == skippedColumn) {
                        continue;
                    }

                    var actualRow = row > skippedRow ? row - 1 : row;
                    var actualColumn = column > skippedColumn ? column - 1 : column;

                    cells[actualRow, actualColumn] = matrix[row, column];
                }
            }

            return new Matrix<Fraction>(cells);
        }
    }
}