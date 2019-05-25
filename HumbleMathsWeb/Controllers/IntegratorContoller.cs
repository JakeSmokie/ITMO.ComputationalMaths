using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HumbleMaths.Calculators.FunctionIntegrators;
using HumbleMaths.Parsers;
using HumbleMaths.Tools;
using HumbleMathsWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace HumbleMathsWeb.Controllers {
    public class IntegratorContoller : Controller {
        private readonly LambdaParser _lambdaParser = new LambdaParser();

        private readonly ReadOnlyCollection<IFunctionIntegrator> _integrators =
            new IFunctionIntegrator[] {
                new AdamsIntegrator(),
                new EulerIntegrator()
            }.ToList().AsReadOnly();

        private readonly FunctionValuesGenerator _functionValuesGenerator = new FunctionValuesGenerator();

        [Route("/Maths/Integrator")]
        public IActionResult Index(IntegratorModel model) {
            if (!ModelState.IsValid)
                return View(model);

            Func<double, double, double> fFunction;
            Func<double, double> yFunction;

            try {
                fFunction = _lambdaParser.ParseLambdaWithSecondParameter(model.FFunctionExpression);
                yFunction = _lambdaParser.ParseLambda(model.YFunctionExpression);
            }
            catch {
                return View(model);
            }

            var results = new List<ReadOnlyCollection<(double x, double y)>>();

            foreach (var integrator in _integrators) {
                var result = integrator.IntegrateFunction(fFunction, yFunction, (model.CaucheX, model.CaucheY),
                    model.StepLength, model.StepAmount);

                results.Add(result);
            }

            model.Results = results;

            try {
                var realFunction = _lambdaParser.ParseLambda(model.RealFunctionExpression);

                var values = _functionValuesGenerator.GenerateValues(realFunction, model.CaucheX,
                    model.CaucheX + model.StepAmount * model.StepLength, model.StepLength);

                model.Results.Add(values.ToList().AsReadOnly());
            }
            catch {
                model.Results.Add(new ReadOnlyCollection<(double x, double y)>(new List<(double x, double y)>()));
            }

            return View(model);
        }
    }
}