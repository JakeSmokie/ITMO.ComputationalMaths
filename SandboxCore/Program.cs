using System;
using System.Collections.Generic;
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

            var solution = solver.SolveSystem(matrix);
            var (triangleStabilizingSteps, triangleEliminationSteps, solvingSteps, result) =
                solution;

            triangleStabilizingSteps.ForEach(Console.WriteLine);
            Console.WriteLine("");

            triangleEliminationSteps.ForEach(Console.WriteLine);
            Console.WriteLine("");

            solvingSteps.ForEach(Console.WriteLine);
            Console.WriteLine("");

            result.ForEach(x => Console.WriteLine(x));

            var determinant = determinantCalculator.CalculateDeterminant(matrix);
            Console.WriteLine($"Det (by minors) = {determinant}");

            var secondDet = solution.GetDeterminantByGauss();
            Console.WriteLine($"Det (by gauss) = {secondDet}");

            Console.ReadLine();
        }
    }
}