using System.Collections.Generic;
using HumbleMaths.Structures;

namespace HumbleMaths.Processors {
    public class MatrixTransformationResult {
        public List<(TransformType Type, Matrix<Fraction> Matrix)> Steps { get; set; }
        public Matrix<Fraction> Result { get; set; }

        public void Deconstruct(
            out List<(TransformType Type, Matrix<Fraction> Matrix)> steps,
            out Matrix<Fraction> result) {
            steps = Steps;
            result = Result;
        }
    }
}