namespace HumbleMaths.Extensions {
    public static class DoubleExtensions {
        public static bool IsZero(this double value)
        {
            return value == 0.0 || value == -0.0;
        }
    }
}