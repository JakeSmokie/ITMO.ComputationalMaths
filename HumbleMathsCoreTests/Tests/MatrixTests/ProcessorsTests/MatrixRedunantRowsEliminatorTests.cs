using HumbleMaths.Parsers;
using HumbleMaths.Processors;
using Xunit;

namespace HumbleMathsCoreTests.Tests.MatrixTests.ProcessorsTests {
    public class MatrixRedunantRowsEliminatorTests {
        [Theory]
        [InlineData("1, 2, 3; 0, 0, 0; 20, 1, 2; 2, 4, 6; 3, 2, 1", "1, 2, 3; 20, 1, 2; 3, 2, 1")]
        public void Test(string input, string expectedResult) {
            var eliminator = new MatrixRedundantRowsEliminator();
            var parser = new MatrixParser();

            var matrix = parser.ParseMatrix(input);

            var actual = eliminator.EliminateRedundantRows(matrix);
            var expected = parser.ParseMatrix(expectedResult);

            Assert.Equal(expected.ToString(), actual.ToString());
        }
    }
}