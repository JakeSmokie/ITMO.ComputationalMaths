using System.Linq.Dynamic.Core.Exceptions;
using HumbleMaths.Parsers;
using Xunit;

namespace HumbleMathsCoreTests.Tests.IntegralTests {
    public class LambdaParserTests {
        [Theory]
        [InlineData("x", 3.0, 3.0)]
        [InlineData("x * x", 3.0, 9.0)]
        [InlineData("Math.Pow(x, 3)", 3.0, 27.0)]
        [InlineData("Math.Sin(x)", 4.0, -0.756802495307928)]
        public void TestExpressions(string function, double x, double result) {
            var lambdaParser = new LambdaParser();
            var method = lambdaParser.ParseLambda(function);

            Assert.Equal(result.ToString(), method(x).ToString());
        }

        [Theory]
        [InlineData("abcdef")]
        [InlineData("Mats.Sin(x)")]
        public void TestFailingExpressions(string function) {
            var lambdaParser = new LambdaParser();

            Assert.Throws<ParseException>(() => { lambdaParser.ParseLambda(function); });
        }
    }
}