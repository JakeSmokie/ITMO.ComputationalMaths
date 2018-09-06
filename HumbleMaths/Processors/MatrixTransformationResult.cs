using System.Collections.Generic;
using HumbleMaths.Structures;

namespace HumbleMaths.Processors {
    public class MatrixTransformationResult {
        public List<Matrix<Fraction>> StabilizingSteps { get; set; }
        public List<Matrix<Fraction>> EliminationSteps { get; set; }
        public Matrix<Fraction> Result { get; set; }

        public void Deconstruct(
            out List<Matrix<Fraction>> stabilizingSteps,
            out List<Matrix<Fraction>> eliminationSteps,
            out Matrix<Fraction> result) {
            stabilizingSteps = StabilizingSteps;
            eliminationSteps = EliminationSteps;
            result = Result;
        }
    }
}