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

        [HttpPost("/Maths/Gauss/", Name = "matrix")]
        public IActionResult Gauss(string matrix) {
            if (matrix == null) {
                return View();
            }

            Matrix<Fraction> system;

            try {
                system = _parser.ParseMatrix(matrix);
            }
            catch (ArgumentException) {
                return View();
            }

            var matrixModel = new MatrixModel {
                Matrix = system.ToString()
            };

            try {
                var solution = _solver.SolveSystem(system);
                matrixModel.Solution = solution;
            } catch (ArgumentException) {
                return View();
            }

            return View(matrixModel);
        }
    }
}