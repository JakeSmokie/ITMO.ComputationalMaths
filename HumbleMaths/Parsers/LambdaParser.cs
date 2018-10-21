using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace HumbleMaths.Parsers {
    public class LambdaParser {
        private static readonly Dictionary<string, string> _aliases =
            new Dictionary<string, string> {
                ["sin"] = "Math.Sin",
                ["cos"] = "Math.Cos",
                ["pow"] = "Math.Pow"
            };

        public Func<double, double> ParseLambda(string function) {
            var parameters = new[] {
                Expression.Parameter(typeof(double), "x")
            };

            return LambdaExpression(function, parameters)?.Compile() as Func<double, double>;
        }

        public Func<double, double, double> ParseLambdaWithSecondParameter(string function) {
            var parameters = new[] {
                Expression.Parameter(typeof(double), "x"),
                Expression.Parameter(typeof(double), "y")
            };

            return LambdaExpression(function, parameters)?.Compile() as Func<double, double, double>;
        }

        private static LambdaExpression LambdaExpression(string function, ParameterExpression[] parameters) {
            foreach (var (key, value) in _aliases) {
                function = function.Replace(key, value);
            }

            return DynamicExpressionParser
                .ParseLambda(parameters, typeof(double), function);
        }
    }
}