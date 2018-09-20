using System.Collections.Generic;
using System.Linq;

namespace HumbleMaths.Extensions {
    public static class EnumerableExtensions {
        public static IEnumerable<T> SkipElementAtIndex<T>(this IEnumerable<T> enumerable, int index) {
            return enumerable.Take(index).Concat(enumerable.Skip(index + 1));
        }
    }
}