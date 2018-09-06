using System;
using HumbleMaths.Structures;
using Xunit;

namespace HumbleMathsCoreTests.Tests.MatrixTests {
    public class MatrixTests {
        [Theory]
        [InlineData(-1, 5)]
        [InlineData(-1, -2)]
        [InlineData(2, -33)]
        [InlineData(20, 21)]
        [InlineData(21, 21)]
        public void TestThrowsExceptionOnWrongWidthAndHeightValues(int width, int height) {
            Assert.Throws<ArgumentException>(() => {
                var matrix = new Matrix<int>(width, height);
            });
        }

        [Fact]
        public void TestIndexator() {
            var matrix = new Matrix<int>(10, 10) {
                [5, 5] = 223
            };

            Assert.Equal(223, matrix[5, 5]);
        }
    }
}