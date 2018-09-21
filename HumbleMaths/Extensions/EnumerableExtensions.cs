using System.Collections.Generic;
using System.Linq;

namespace HumbleMaths.Extensions {
    public static class EnumerableExtensions {
        public static IEnumerable<T> SkipElementAtIndex<T>(this IEnumerable<T> enumerable, int index) {
            return enumerable.Where((x, i) => i != index);
        }
    }
}