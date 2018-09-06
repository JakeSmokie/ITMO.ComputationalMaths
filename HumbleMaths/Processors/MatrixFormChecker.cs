using System.Linq;
using HumbleMaths.Extensions;
using HumbleMaths.Structures;

namespace HumbleMaths.Processors {
    public class MatrixFormChecker {
        public bool IsMatrixTriangular(Matrix<double> matrix)
        {
            for (var i = 0; i < matrix.Height; i++) {
                var any = matrix.OfType<double>()
                    .Skip(i * matrix.Width)
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