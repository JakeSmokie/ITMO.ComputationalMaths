using System;
using System.Linq;
using HumbleMaths.LinearSystemSolvers;
using HumbleMaths.Parsers;
using HumbleMaths.Processors;
using HumbleMaths.Structures;

namespace SandboxCore {
    internal class Program {
        private static void Main(string[] args) {
            var parser = new MatrixAsLinearSystemParser();
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
            Console.WriteLine($"Det (by minors) = {determinant}");

            var detMatrix = triangleEliminationSteps.Last();

            var secondDet = Enumerable.Range(0, detMatrix.Height)
                .Select(x => detMatrix[x, x])
                .Aggregate(new Fraction(1), (x, y) => x * y);

            if (triangleStabilizingSteps.Count % 2 == 1) {
                secondDet *= -1;
            }

            Console.WriteLine($"Det (by gauss) = {secondDet}");

            solvingSteps.ForEach(Console.WriteLine);
            Console.WriteLine("");

            result.ForEach(x => Console.WriteLine(x));
            Console.ReadLine();
        }
    }
}