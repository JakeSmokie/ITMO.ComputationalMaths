using HumbleMaths.Calculators.Matrix;
using HumbleMaths.Parsers;
using Xunit;

namespace HumbleMathsCoreTests.Tests.MatrixTests.ProcessorsTests {
    public class MatrixDeterminantCalculatorTests {
        [Theory]
        [InlineData("2, 1, 1, 2; 1, -1, 0, -2; 3, -1, 2, 2", -4)]
        public void Test(string input, double expectedDeterminant) {
            var parser = new MatrixParser();
            var matrix = parser.ParseMatrix(input);

            var determinantCalculator = new MatrixDeterminantCalculator();
            var determinant = determinantCalculator.CalculateDeterminant(matrix);

            Assert.Equal(expectedDeterminant, determinant);
        }
    }
}