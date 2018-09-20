using System;
using System.Collections.ObjectModel;
using System.Linq;
using HumbleMaths.Calculators.Approximation;
using HumbleMaths.Parsers;
using HumbleMaths.Tools;
using Xunit;

namespace HumbleMathsCoreTests.Tests.InterpolationTests {
    public class LagrangeTests {
        [Theory]
        [InlineData("sin(x)", 0, 3.14, 0.1, 0.005, 0.1)]
        [InlineData("pow(x, 2)", 0, 30, 1, 0.1, 0.1)]
        [InlineData("x", 0, 300, 1, 5, 0.1)]
        public void Test(string function, double start, double end, double inputStep, double checkStep,
            double maxError) {
            var parser = new LambdaParser();
            var valuesGenerator = new FunctionValuesGenerator();
            var interpolator = new LagrangeInterpolator();

            var func = parser.ParseLambda(function);

            var input = valuesGenerator
                .GenerateValues(func, start, end, inputStep)
                .ToList();

            var interpolationFunction = interpolator
                .InterpolateByPoints(new ReadOnlyCollection<(double x, double y)>(input));

            var expected = valuesGenerator
                .GenerateValues(func, start, end, checkStep)
                .ToList();

            var actual = valuesGenerator
                .GenerateValues(interpolationFunction, start, end, checkStep)
                .ToList();

            var errors = Enumerable.Range(0, expected.Count)
                .Select(i => expected[i].y - actual[i].y)
                .Select(Math.Abs);

            Assert.All(errors, x => Assert.True(x < maxError));
        }
    }
}