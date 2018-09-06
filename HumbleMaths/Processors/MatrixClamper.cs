using HumbleMaths.Structures;

namespace HumbleMaths.Processors {
    public class MatrixClamper {
        public Matrix<Fraction> ClampMatrixToSquare(Matrix<Fraction> matrix) {
            if (matrix.Height == matrix.Width) {
                return matrix;
            }

            var cells = new Fraction[matrix.Height, matrix.Height];

            for (var i = 0; i < matrix.Height; i++) {
                for (var j = 0; j < matrix.Height; j++) {
                    cells[i, j] = matrix[i, j];
                }
            }

            return new Matrix<Fraction>(cells);
        }
    }
}