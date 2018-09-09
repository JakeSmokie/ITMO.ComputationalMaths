using System;
using HumbleMaths.Calculators;
using HumbleMaths.Parsers;
using Xunit;

namespace HumbleMathsCoreTests.Tests.IntegralTests {
    public class IntegralCalculatorTests {
        [Theory]
        [InlineData("x", 0.0, 10.0, 50.0, 0.001, 0.1)]
        [InlineData("x", 10.0, 0.0, -50.0, 0.001, 0.1)]
        [InlineData("x", 0.0, 5.0, 12.5, 0.001, 0.1)]
        [InlineData("x", 5.0, 0.0, -12.5, 0.001, 0.1)]
        [InlineData("3 * x * x", 0.0, 10.0, 1000, 0.0001, 0.1)]
        [InlineData("sin(x)", 0.0, 2 * Math.PI, 0, 0.0001, 0.1)]
        public void Test(string funcExpression, double start, double end,
            double expected, double precision, double maxError) {
            var lambdaParser = new LambdaParser();
            var func = lambdaParser.ParseLambda(funcExpression);

            var calculator = new IntegralCalculator(precision);
            var result = calculator.Calculate(func, start, end).Integral;

            Assert.True(Math.Abs(expected - result) < maxError);
        }
    }
}