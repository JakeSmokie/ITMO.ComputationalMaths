using HumbleMaths.Parsers;
using HumbleMaths.Processors;
using Xunit;

namespace HumbleMathsCoreTests.Tests.MatrixTests.ProcessorsTests {
    public class MatrixFormTransformerTests {
        [Fact]
        public void TestFormTransformer() {
            var parser = new MatrixParser();
            var transformer = new MatrixTransformer();
            var formChecker = new MatrixFormChecker();

            var input = parser.ParseMatrix("2,1,1,2,1,-1,0,-2,3,-1,2,2");
            var expected = parser.ParseMatrix("2,1,1,2, 0,-3,-1,-6, 0,0,-8,-24");

            var (_, result) = transformer.MatrixToTriangular(input);
            Assert.True(formChecker.IsMatrixTriangular(result));
        }
    }
}