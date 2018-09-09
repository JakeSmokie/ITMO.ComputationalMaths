using System.Linq;
using System.Text;
using HumbleMaths.Structures;

namespace HumbleMaths.Converters {
    public class MatrixLatexConverter {
        public string ConvertToLatex<T>(Matrix<T> matrix, bool overrideCheck = true) {
            var style = "{}";

            if (matrix.Width != 1 && (matrix.Height != matrix.Width || overrideCheck)) {
                style = "{" + string.Concat(Enumerable.Repeat("r", matrix.Width - 1)) + "|r}";
            }

            var stringBuilder = new StringBuilder($@"\left({{\begin{{array}}{style} ");

            var rows = Enumerable.Range(0, matrix.Height)
                .Select(row => Enumerable.Range(0, matrix.Width)
                    .Select(i => $"{{{matrix[row, i]}}}"))
                .Select(rowItems => string.Join(" & ", rowItems));

            stringBuilder.Append(string.Join(@" \\ ", rows));
            stringBuilder.Append(@"\end{array}}\right)");

            return stringBuilder.ToString();
        }
    }
}