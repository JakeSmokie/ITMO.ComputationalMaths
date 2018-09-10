using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq.Dynamic.Core.Exceptions;
using System.Threading;
using System.Threading.Tasks;
using HumbleMaths.Calculators;
using HumbleMaths.LinearSystemSolvers;
using HumbleMaths.Parsers;
using HumbleMaths.Structures;
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
    }
}