using System.Linq;
using HumbleMaths.Structures;

namespace HumbleMaths.Processors {
    public class MatrixFormChecker {
        public bool IsMatrixTriangular(Matrix<Fraction> matrix) {
            for (var i = 1; i < matrix.Height; i++) {
                // get all items leading to echelon (marked as x)
                // * * *
                // x * *
                // x x *
                var any = matrix.Skip(i * matrix.Width)
                    .Take(i)
                    .Any(x => !x.IsZero());

                if (any) {
                    return false;
                }
            }

            return true;
        }
    }
}