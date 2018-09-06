using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HumbleMaths.Structures;

namespace HumbleMaths.Parsers {
    public class MatrixAsLinearSystemParser {
        public Matrix<Fraction> ParseMatrix(string input) {
            var numbers = input.Split(',')
                .Select(x => new Fraction(double.Parse(x, NumberFormatInfo.InvariantInfo)))
                .ToList();

            var rowsAmount = GetRowsAmount(numbers);
            var matrix = CreateMatrix(rowsAmount, numbers);

            return matrix;
        }

        private static Matrix<Fraction> CreateMatrix(int rowsAmount, List<Fraction> numbers) {
            var matrix = new Matrix<Fraction>(rowsAmount + 1, rowsAmount);

            for (var i = 0; i < rowsAmount; i++)
            for (var j = 0; j < rowsAmount + 1; j++) {
                matrix[i, j] = numbers[(rowsAmount + 1) * i + j];
            }

            return matrix;
        }

        private static int GetRowsAmount(List<Fraction> numbers) {
            var rowsAmount = Enumerable.Range(1, Matrix<Fraction>.MaxHeight - 1)
                .FirstOrDefault(x => x * (x + 1) == numbers.Count);

            if (rowsAmount == 0) {
                throw new ArgumentException("Illegal amount of elements");
            }

            return rowsAmount;
        }
    }
}