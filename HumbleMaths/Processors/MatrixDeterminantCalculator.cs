using HumbleMaths.Structures;

namespace HumbleMaths.Processors {
    public class MatrixDeterminantCalculator {
        private readonly MatrixClamper _matrixClamper = new MatrixClamper();
        private readonly MatrixMinorCalculator _minorCalculator = new MatrixMinorCalculator();

        public Fraction CalculateDeterminant(Matrix<Fraction> matrix) {
            var squareMatrix = _matrixClamper.ClampMatrixToSquare(matrix);

            if (matrix.Height == 2) {
                return CalculateDeterminantForMatrix2D(matrix);
            }

            return CalculateDeterminantUsingMinors(squareMatrix);
        }

        private Fraction CalculateDeterminantUsingMinors(Matrix<Fraction> squareMatrix) {
            var determinant = new Fraction(0);

            for (var column = 0; column < squareMatrix.Width; column++) {
                var minor = _minorCalculator.GetMinor(squareMatrix, 0, column);
                var multiplier = column % 2 == 0 ? 1 : -1;

                determinant += CalculateDeterminant(minor) *
                               squareMatrix[0, column] * multiplier;
            }

            return determinant;
        }

        private static Fraction CalculateDeterminantForMatrix2D(Matrix<Fraction> matrix) {
            var determinant = matrix[0, 0] * matrix[1, 1];
            determinant -= matrix[1, 0] * matrix[0, 1];

            return determinant;
        }
    }
}