using System.Globalization;
using System.Linq;
using HumbleMaths.LinearSystemSolvers;
using HumbleMaths.Parsers;
using HumbleMaths.Structures;
using Xunit;

namespace HumbleMathsCoreTests.Tests.MatrixTests.SolversTests {
    public class GaussSolverTests {
        [Theory]
        [InlineData("2, 1, 1, 2; 1, -1, 0, -2; 3, -1, 2, 2", "-1, 1, 3")]
        [InlineData("3, 2, -1, 1; 2, -2, 4, -2; -1, 0.5, -1, 0", "1, -2, -2")]
        [InlineData("2, 6, -4, 10; 0, 0, 0 ,0; 1, 3, -2, 5; 3, 5, 6, 7; 2, 4, 3, 8", "-15, 8, 2")]
        [InlineData("1, 3, -2, 5; 3, 5, 6, 7; 2, 4, 3, 8", "-15, 8, 2")]
        [InlineData("2, 1, 3, 2, 0; 2, 1, 5, 1, 2; 2, 1, 4, 2, 1; 1, 3, 3, 2, 6", "-2.4, 1.8, 1, 0")]
        public void TestGaussSolver(string system, string expectedResult) {
            var parser = new MatrixParser();
            var solver = new GaussSolver();

            var input = parser.ParseMatrix(system);
            var solution = solver.SolveSystem(input);

            var expected = expectedResult.Split(',')
                .Select(x => new Fraction(double.Parse(x, CultureInfo.InvariantCulture)))
                .ToList();

            Assert.Equal(expected, solution.Result);
        }
    }
}