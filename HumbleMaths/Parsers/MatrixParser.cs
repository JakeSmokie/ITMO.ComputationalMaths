using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HumbleMaths.Structures;

namespace HumbleMaths.Parsers {
    public class MatrixParser {
        public Matrix<Fraction> ParseMatrix(string input) {
            var numbers = new List<Fraction>();

            try {
                var rows = input.TrimEnd(' ', ';')
                    .Split(';')
                    .Select(ParseRow)
                    .ToList();

                rows.ForEach(x => numbers.AddRange(x));

                var width = rows.First().Count();
                var height = rows.Count;

                return CreateMatrix(numbers, width, height);
            }
            catch (Exception e) {
                throw new ArgumentException("Cannot parse matrix", e);
            }
        }

        private static IEnumerable<Fraction> ParseRow(string row) {
            return row.Split(',')
                .Select(ParseItem);
        }

        private static Fraction ParseItem(string x) {
            return new Fraction(double.Parse(x, NumberFormatInfo.InvariantInfo));
        }

        private static Matrix<Fraction> CreateMatrix(List<Fraction> numbers, int width, int height) {
            var matrix = new Matrix<Fraction>(width, height);

            for (var i = 0; i < height; i++) {
                for (var j = 0; j < width; j++) {
                    matrix[i, j] = numbers[width * i + j];
                }
            }

            return matrix;
        }
    }
}