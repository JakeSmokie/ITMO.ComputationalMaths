using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace HumbleMathsWeb.Models {
    public class IntegratorModel {
        [Required(ErrorMessage = "Не введено поле f(x, y)")]
        public string FFunctionExpression { get; set; } = "cos(x)";

        public string YFunctionExpression { get; set; } = "1";
        public string RealFunctionExpression { get; set; } = "sin(x)";

        [Required(ErrorMessage = "Не введено поле x0")]
        public double CaucheX { get; set; } = 0;

        [Required(ErrorMessage = "Не введено поле y0")]
        public double CaucheY { get; set; } = 0;

        [Required(ErrorMessage = "Не введено поле длины шага")]
        [Range(double.Epsilon, 100000, ErrorMessage = "Длина шага должна находится в промежутке (0, 100000]")]
        public double StepLength { get; set; } = 0.01;

        [Required(ErrorMessage = "Не введено поле количества шагов")]
        [Range(5, 2000, ErrorMessage = "Количество шагов должно находится в промежутке [5, 2000]")]
        public int StepAmount { get; set; } = 100;

        public List<ReadOnlyCollection<(double x, double y)>> Results { get; set; } =
            new List<ReadOnlyCollection<(double x, double y)>>();
    }
}