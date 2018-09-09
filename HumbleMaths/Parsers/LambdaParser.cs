using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace HumbleMaths.Parsers {
    public class LambdaParser {
        private static Dictionary<string, string> _aliases =
            new Dictionary<string, string> {
                ["sin"] = "Math.Sin",
                ["cos"] = "Math.Cos",
                ["pow"] = "Math.Pow",
            };

        public Func<double, double> ParseLambda(string function) {
            var parameters = new[] {
                Expression.Parameter(typeof(double), "x")
            };

            var lambda = DynamicExpressionParser
                .ParseLambda(parameters, typeof(double), function);

            return lambda?.Compile() as Func<double, double>;
        }
    }
}