
using System;
using System.Linq;

namespace SandboxCore {
    internal class Program {
        private static void Main(string[] args) {
            var enumerable = new[] { 1, 2, 3, 4, 4, 4 }.Except(new[] { 4 });
        }
    }
}