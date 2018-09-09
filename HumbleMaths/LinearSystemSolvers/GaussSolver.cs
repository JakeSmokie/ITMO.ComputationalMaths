using System;
using System.Collections.Generic;
using System.Linq;
using HumbleMaths.Processors;
using HumbleMaths.Structures;

namespace HumbleMaths.LinearSystemSolvers {
    public class GaussSolver {
        private readonly MatrixRedundantRowsEliminator _rowsEliminator = new MatrixRedundantRowsEliminator();
        private readonly MatrixTransformer _transformer = new MatrixTransformer();

        public GaussSolverSolution SolveSystem(Matrix<Fraction> input) {
            var system = _rowsEliminator.EliminateRedundantRows(input);

            var solution = new GaussSolverSolution {
                EliminationStep = system.CloneMatrix()
            };

            var (steps, matrix) = _transformer.MatrixToTriangular(system);
            solution.TransformationSteps = steps;

            if (matrix.Width != matrix.Height + 1) {
                return solution;
            }

            var solvingSteps = new List<Matrix<Fraction>>();
            var result = new List<Fraction>();

            try {
                for (var row = matrix.Height - 1; row >= 0; row--) {
                    DivideRowByMainDiagonalElement(matrix, row);
                    AddSolvingStep(solvingSteps, matrix);

                    EliminateRowMainDiagonalElement(matrix, row);
                    AddSolvingStep(solvingSteps, matrix);

                    result.Insert(0, matrix[row, matrix.Width - 1]);
                }
            }
            catch {
                // ignored
            }
            finally {
                solution.SolvingSteps = solvingSteps;
                solution.Result = result;
            }

            return solution;
        }

        private static void AddSolvingStep(List<Matrix<Fraction>> solvingSteps, Matrix<Fraction> matrix) {
            if (solvingSteps.LastOrDefault()?.StringEquals(matrix) ?? false) {
                return;
            }

            solvingSteps.Add(matrix.CloneMatrix());
        }

        private static void DivideRowByMainDiagonalElement(Matrix<Fraction> matrix, int row) {
            var divider = matrix[row, row];

            if (divider.IsZero()) {
                throw new ArgumentException("System has no unique solutions");
            }

            for (var column = 0; column < matrix.Width; column++) {
                matrix[row, column] /= divider;
            }
        }

        private static void EliminateRowMainDiagonalElement(Matrix<Fraction> matrix, int row) {
            for (var destRow = 0; destRow < row; destRow++) {
                matrix[destRow, matrix.Width - 1] -= matrix[destRow, row] * matrix[row, matrix.Width - 1];
                matrix[destRow, row] = 0;
            }
        }
    }
}