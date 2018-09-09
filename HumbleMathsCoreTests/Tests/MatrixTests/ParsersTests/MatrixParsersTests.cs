using System.Linq;
using HumbleMaths.LinearSystemSolvers;
using HumbleMaths.Parsers;
using Xunit;

namespace HumbleMathsCoreTests.Tests.MatrixTests.ParsersTests {
    public class MatrixParsersTests {
        private const int Width = 15;
        private const int Height = 10;

        [Fact]
        public void Test() {
            var ints = Enumerable.Range(0, Height)
                .SelectMany(x => Enumerable.Range(0, Width), (y, z) => y * z)
                .ToList();

            var rows = Enumerable.Range(0, Height)
                .Select(GenerateRow);

            var input = string.Join("; ", rows);

            var parser = new MatrixParser();
            var matrix = parser.ParseMatrix(input);

            Assert.Equal(Height, matrix.Height);
            Assert.Equal(Width, matrix.Width);

            for (var i = 0; i < ints.Count; i++) {
                Assert.Equal(ints[i], matrix.ElementAt(i));
            }

            string GenerateRow(int row) {
                return string.Join(", ", ints.Skip(row * Width).Take(Width));
            }
        }
    }
}