using System;
using HumbleMaths.LinearSystemSolvers;
using HumbleMaths.Parsers;
using HumbleMaths.Structures;
using HumbleMathsWeb.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace HumbleMathsWeb.Controllers {
    public class MathsController : Controller {
        private readonly MatrixAsLinearSystemParser _parser = new MatrixAsLinearSystemParser();
        private readonly GaussSolver _solver = new GaussSolver();

        public IActionResult Index() {
            return View();
        }

        [Route("/Maths/Gauss")]
        public IActionResult Gauss() {
            return View();
        }

        [HttpPost("/Maths/Gauss", Name = "matrix")]
        public IActionResult Gauss(string matrix) {
            if (matrix == null) {
                return View();
            }

            var matrixModel = new MatrixModel {
                Matrix = "",
                Solution = null
            };

            try {
                var system = _parser.ParseMatrix(matrix);
                matrixModel.System = system;
                matrixModel.Matrix = system.ToString();

                var solution = _solver.SolveSystem(system);
                matrixModel.Solution = solution;
            }
            catch {
                return View(matrixModel);
            }

            return View(matrixModel);
        }
    }
}