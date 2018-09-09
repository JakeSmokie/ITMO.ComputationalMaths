using System.Collections.Generic;
using System.Linq;
using HumbleMaths.Structures;

namespace HumbleMaths.Calculators {
    public class LinearSystemSolutionErrorsCalculator {
        public List<Fraction> CalculateErrors(Matrix<Fraction> system, List<Fraction> solution) {
            return Enumerable.Range(0, system.Height)
                .Select(row => GetRowSum(row) - GetFreeElement(row))
                .ToList();

            Fraction GetRowSum(int row) {
                return Enumerable.Range(0, system.Width - 1)
                    .Aggregate(new Fraction(0),
                        (x, i) => x + system[row, i] * solution[i]);
            }

            Fraction GetFreeElement(int row) {
                return system[row, system.Width - 1];
            }
        }
    }
}