using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                // TODO: deal with it
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

            var partitioner = Partitioner.Create(0, matrix.Width);

            Parallel.ForEach(partitioner, range => {
                for (var column = range.Item1; column < range.Item2; column++) {
                    matrix[row, column] /= divider;
                }
            });
        }

        private static void EliminateRowMainDiagonalElement(Matrix<Fraction> matrix, int row) {
            if (row == 0) {
                return;
            }

            var partitioner = Partitioner.Create(0, row);

            Parallel.ForEach(partitioner, range => {
                for (var destRow = range.Item1; destRow < range.Item2; destRow++) {
                    matrix[destRow, matrix.Width - 1] -= matrix[destRow, row] * matrix[row, matrix.Width - 1];
                    matrix[destRow, row] = 0;
                }
            });
        }
    }
}