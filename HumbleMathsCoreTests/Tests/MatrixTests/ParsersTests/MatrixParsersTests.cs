using System.Linq;
using HumbleMaths.LinearSystemSolvers;
using HumbleMaths.Parsers;
using Xunit;

namespace HumbleMathsCoreTests.Tests.MatrixTests.ParsersTests {
    public class MatrixParsersTests {
        [Fact]
        public void Test() {
            var ints = Enumerable.Range(0, 10)
                .SelectMany(x => Enumerable.Range(0, 9), (y, z) => y * z)
                .ToList();

            var input = string.Join(", ", ints)
                .TrimEnd(',', ' ');

            var parser = new MatrixAsLinearSystemParser();
            var matrix = parser.ParseMatrix(input);

            for (var i = 0; i < ints.Count; i++) {
                Assert.Equal(ints[i], matrix.ElementAt(i));
            }
        }
    }
}