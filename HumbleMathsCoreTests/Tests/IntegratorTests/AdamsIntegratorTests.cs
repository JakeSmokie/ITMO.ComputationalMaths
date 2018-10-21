using System;
using System.Linq;
using HumbleMaths.Calculators.FunctionIntegrators;
using HumbleMaths.Parsers;
using HumbleMaths.Tools;
using Xunit;

namespace HumbleMathsCoreTests.Tests.IntegratorTests {
    public class AdamsIntegratorTests {
        private readonly LambdaParser parser = new LambdaParser();
        private readonly FunctionValuesGenerator _functionValuesGenerator = new FunctionValuesGenerator();
        private readonly IFunctionIntegrator _integrator = new AdamsIntegrator();

        [Theory]
        [InlineData("1", "1", "x + 1", 0, 1, 10, 1.0, 0.1)]
        [InlineData("1", "1", "x + 1", 0, 1, 10, 0.8, 0.1)]
        [InlineData("2 * x", "1", "x * x", 0, 0, 10, 0.01, 0.1)]
        [InlineData("x + y", "x", "x * x", 0, 0, 10, 0.01, 0.1)]
        [InlineData("cos(x)", "1", "sin(x)", 0, 0, 10, 0.01, 0.1)]
        [InlineData("cos(x)", "1", "sin(x - 1) + 1", 1, 1, 10, 0.01, 0.1)]
        public void Test(string fFunctionExpression, string yFunctionExpression, string realFunctionExpression, 
            double xCauche, double yCauche, int stepAmount, double stepLength, double maxError) {
            var fFunction = parser.ParseLambdaWithSecondParameter(fFunctionExpression);
            var yFunction = parser.ParseLambda(yFunctionExpression);

            var actual = _integrator.IntegrateFunction(fFunction, yFunction,
                (xCauche, yCauche), stepLength, stepAmount);

            var realFunction = parser.ParseLambda(realFunctionExpression);
            var expected = _functionValuesGenerator.GenerateValues(realFunction, xCauche, xCauche + stepAmount * stepLength, stepLength);

            var errors = expected
                .Select((p, i) => p.y - actual[i].z)
                .Select(Math.Abs)
                .ToList();

            Assert.All(errors, x => Assert.True(x < maxError));
        }
    }
}