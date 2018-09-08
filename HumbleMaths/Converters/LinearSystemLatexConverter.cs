using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HumbleMaths.Structures;

namespace HumbleMaths.Converters {
    [Obsolete]
    public class LinearSystemLatexConverter {
        public string ConvertToLatex(Matrix<Fraction> system) {
            var stringBuilder = new StringBuilder(@"\begin {cases} ");
            var rows = new List<string>();

            for (var row = 0; row < system.Height; row++) {
                var variables = Enumerable.Range(0, system.Width - 1)
                    .Select(i => $"{system[row, i]}x_{i + 1}");

                rows.Add($"{string.Join(" + ", variables)} = {system[row, system.Width - 1]}");
            }

            stringBuilder.Append(string.Join(@" \\ ", rows));
            stringBuilder.Append(@"\end {cases}");

            return $"${stringBuilder}$";
        }
    }
}