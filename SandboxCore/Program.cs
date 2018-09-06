using System;
using System.Linq;
using HumbleMaths.LinearSystemSolvers;
using HumbleMaths.Parsers;
using HumbleMaths.Processors;
using HumbleMaths.Structures;

namespace SandboxCore {
    internal class Program {
        private static void Main(string[] args)
        {
            var parser = new MatrixParser();
            var solver = new GaussSolver();
            var determinantCalculator = new MatrixDeterminantCalculator();

            var matrix = parser.ParseMatrix("2, 1, 1, 2, 1, -1, 0, -2, 3, -1, 2, 2");
            var (triangleStabilizingSteps, triangleEliminationSteps, solvingSteps, result) =
                solver.SolveSystem(matrix);

            triangleStabilizingSteps.ForEach(Console.WriteLine);
            Console.WriteLine("");

            triangleEliminationSteps.ForEach(Console.WriteLine);
            Console.WriteLine("");

            var determinant = determinantCalculator.CalculateDeterminant(matrix);
            Console.WriteLine($"Det = {determinant}");

            solvingSteps.ForEach(Console.WriteLine);
            Console.WriteLine("");

            result.ForEach(Console.WriteLine);
            Console.ReadLine();
        }

        private static double MultiplicateMainDiagonalElements(Matrix<double> matrix)
        {
            return matrix.MainDiagonalElements
                .Aggregate(1.0, (x, y) => x * y);
        }
    }
}