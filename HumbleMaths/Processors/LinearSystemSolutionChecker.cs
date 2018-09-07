using System.Collections.Generic;
using System.Linq;
using HumbleMaths.Structures;

namespace HumbleMaths.Processors {
    public class LinearSystemSolutionChecker {
        public bool IsSolutionValidForSystem(Matrix<Fraction> system, List<Fraction> solution) {
            return Enumerable.Range(0, system.Height)
                .All(row => GetRowSum(row) == GetFreeElement(row));

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