using System;

namespace HumbleMaths.Processors {
    public static class LinearEquationSolver {
        /// <summary>Computes the solution of a linear equation system.</summary>
        /// <param name="M">
        ///     The system of linear equations as an augmented matrix[row, col] where (rows + 1 == cols).
        ///     It will contain the solution in "row canonical form" if the function returns "true".
        /// </param>
        /// <returns>Returns whether the matrix has a unique solution or not.</returns>
        public static bool Solve(float[,] M)
        {
            // input checks
            var rowCount = M.GetUpperBound(0) + 1;
            if (M == null || M.Length != rowCount * (rowCount + 1)) {
                throw new ArgumentException("The algorithm must be provided with a (n x n+1) matrix.");
            }

            if (rowCount < 1) {
                throw new ArgumentException("The matrix must at least have one row.");
            }

            // pivoting
            for (var col = 0; col + 1 < rowCount; col++) {
                if (M[col, col] == 0)
// check for zero coefficients
                {
                    // find non-zero coefficient
                    var swapRow = col + 1;
                    for (; swapRow < rowCount; swapRow++) {
                        if (M[swapRow, col] != 0) {
                            break;
                        }
                    }

                    if (M[swapRow, col] != 0) // found a non-zero coefficient?
                    {
                        // yes, then swap it with the above
                        var tmp = new float[rowCount + 1];
                        for (var i = 0; i < rowCount + 1; i++) {
                            tmp[i] = M[swapRow, i];
                            M[swapRow, i] = M[col, i];
                            M[col, i] = tmp[i];
                        }
                    }
                    else {
                        return false; // no, then the matrix has no unique solution
                    }
                }
            }

            // elimination
            for (var sourceRow = 0; sourceRow + 1 < rowCount; sourceRow++)
            for (var destRow = sourceRow + 1; destRow < rowCount; destRow++) {
                var df = M[sourceRow, sourceRow];
                var sf = M[destRow, sourceRow];
                for (var i = 0; i < rowCount + 1; i++) {
                    M[destRow, i] = M[destRow, i] * df - M[sourceRow, i] * sf;
                }
            }

            // back-insertion
            for (var row = rowCount - 1; row >= 0; row--) {
                var f = M[row, row];
                if (f == 0) {
                    return false;
                }

                for (var i = 0; i < rowCount + 1; i++) {
                    M[row, i] /= f;
                }

                for (var destRow = 0; destRow < row; destRow++) {
                    M[destRow, rowCount] -= M[destRow, row] * M[row, rowCount];
                    M[destRow, row] = 0;
                }
            }

            return true;
        }
    }
}