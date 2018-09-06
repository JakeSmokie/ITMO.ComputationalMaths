using System.Collections.Generic;
using HumbleMaths.Structures;

namespace HumbleMaths.Processors {
    public class MatrixTransformationResult {
        public List<Matrix<double>> StabilizingSteps { get; set; }
        public List<Matrix<double>> EliminationSteps { get; set; }
        public Matrix<double> Result { get; set; }

        public void Deconstruct(
            out List<Matrix<double>> stabilizingSteps,
            out List<Matrix<double>> eliminationSteps,
            out Matrix<double> result)
        {
            stabilizingSteps = StabilizingSteps;
            eliminationSteps = EliminationSteps;
            result = Result;
        }
    }
}