using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic.Core.Exceptions;
using System.Threading;
using System.Threading.Tasks;
using HumbleMaths.Calculators;
using HumbleMaths.Calculators.Approximation;
using HumbleMaths.LinearSystemSolvers;
using HumbleMaths.Parsers;
using HumbleMaths.Structures;
using HumbleMaths.Tools;
using HumbleMathsWeb.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace HumbleMathsWeb.Controllers {
    public class MathsController : Controller {
        private readonly MatrixParser _parser = new MatrixParser();
        private readonly GaussSolver _solver = new GaussSolver();

        public IActionResult Index() {
            return View();
        }

        [Route("/Maths/Gauss")]
        public IActionResult Gauss() {
            return View();
        }

        [HttpPost("/Maths/Gauss", Name = "matrix")]
        public IActionResult Gauss(MatrixModel model) {
            var matrix = model.Input;

            if (matrix == null) {
                return View();
            }

            var matrixModel = new MatrixModel {
                Solution = null,
                Stopwatch = new Stopwatch()
            };

            matrixModel.Stopwatch.Start();

            try {
                var system = _parser.ParseMatrix(matrix);
                matrixModel.System = system;

                var solution = _solver.SolveSystem(system);
                matrixModel.Solution = solution;
            }
            catch {
                matrixModel.Stopwatch.Stop();
                return View(matrixModel);
            }

            matrixModel.Stopwatch.Stop();
            return View(matrixModel);
        }

        [Route("/Maths/Integral")]
        [HttpPost("/Maths/Integral")]
        public IActionResult Integral(IntegralModel model) {
            if (!ParseFields(model)) {
                return View(model);
            }

            if (model.Precision < 0) {
                return View(model);
            }

            var lambdaParser = new LambdaParser();

            try {
                var function = lambdaParser.ParseLambda(model.FunctionExpression);
                model.Func = function;
            }
            catch {
                return View(model);
            }

            try {
                var function = lambdaParser.ParseLambda(model.IntegralFunctionExpression);
                model.IntegralFunc = function;
            }
            catch {
                // ignored
            }

            var found = model.Methods.TryGetValue(model.MethodAsText, out var method);

            if (!found) {
                return View(model);
            }

            model.IntegralCalculator = method;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            model.CalculationFinished = false;

            var task = Task.Run(() => {
                (model.IntegralValue, model.PartsAmount) =
                    method.Calculate(model.Func, model.Start, model.End, model.Precision);

                model.CalculationFinished = true;
            });

            task.Wait(20000);

            stopwatch.Stop();
            model.CalculationTime = stopwatch.Elapsed;

            return View(model);
        }

        private static bool ParseFields(IntegralModel model) {
            if (!double.TryParse(model.StartAsText, NumberStyles.Any,
                CultureInfo.InvariantCulture, out var start)) {
                return false;
            }

            if (!double.TryParse(model.EndAsText, NumberStyles.Any,
                CultureInfo.InvariantCulture, out var end)) {
                return false;
            }

            if (!double.TryParse(model.PrecisionAsText, NumberStyles.Any,
                CultureInfo.InvariantCulture, out var precision)) {
                return false;
            }

            model.Start = start;
            model.End = end;
            model.Precision = precision;

            return true;
        }

        [Route("/Maths/Lagrange")]
        [HttpPost("/Maths/Lagrange")]
        public IActionResult Lagrange(LagrangeModel model) {
            if (model == null) {
                return View();
            }

            var parser = new LambdaParser();
            var generator = new FunctionValuesGenerator();
            var interpolator = new LagrangeInterpolator();

            try {
                var func = parser.ParseLambda(model.FunctionExpression);
                var start = double.Parse(model.Start, CultureInfo.InvariantCulture);
                var end = double.Parse(model.End, CultureInfo.InvariantCulture);
                var step = double.Parse(model.Step, CultureInfo.InvariantCulture);

                if ((end - start) / step > 10000) {
                    return View(model);
                }

                model.Values = generator.GenerateValues(func, start, end, step)
                    .ToList()
                    .AsReadOnly();

                var interpolationStep = double.Parse(model.InterpolationStep, CultureInfo.InvariantCulture);

                if ((end - start) / interpolationStep > 500) {
                    return View(model);
                }

                model.InterpolationNodes = generator
                    .GenerateValues(func, start, end, interpolationStep)
                    .ToList()
                    .AsReadOnly();

                var interpolationFunction = interpolator
                    .InterpolateByPoints(model.InterpolationNodes);

                model.InterpolatedValues = generator
                    .GenerateValues(interpolationFunction, start, end, step)
                    .ToList()
                    .AsReadOnly();
            } catch {
                // ignored
            }

            return View(model);
        }
    }
}