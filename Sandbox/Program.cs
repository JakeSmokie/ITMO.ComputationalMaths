using System.Linq;

namespace Sandbox {
    internal class Program {
        private static void Main(string[] args) {
            var height = 10;

            var rows = Enumerable.Range(0, height)
                .Select(row => Enumerable.Range(0, height + 1)
                    .Select(x => $"1/{row + 1 + x}"))
                .Select(row => string.Join(", ", row));

            var matrix = string.Join("; ", rows);
        }
    }
}