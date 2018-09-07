using System.Collections.Generic;
using System.Linq;
using HumbleMaths.Structures;

namespace HumbleMaths.Processors {
    public class MatrixFormChecker {
        public bool IsMatrixTriangular(Matrix<Fraction> matrix) {
            return Enumerable.Range(0, matrix.Height)
                .All(IsRowCorrect);

            bool IsRowCorrect(int row) {
                return GetEchelonItems(row)
                    .All(x => x.IsZero());
            }

            // get all items leading to echelon (marked as x)
            // * * *
            // x * *
            // x x *
            IEnumerable<Fraction> GetEchelonItems(int row) {
                return matrix
                    .Skip(row * matrix.Width)
                    .Take(row);
            }

        }
    }
}