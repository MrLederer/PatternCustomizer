using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternCustomizer
{
    static class IEnumerableExtensions
    {
        public static IEnumerable<T> ToEnumerable<T>(this T item)
        {
            yield return item;
        }

        public static IEnumerable<T> NullToEmpty<T>(this IEnumerable<T> collection)
        {
            return collection ?? Enumerable.Empty<T>();
        }

        public static BindingList<T> ToBindingList<T>(this IEnumerable<T> collection)
        {
            var result = new BindingList<T>();
            foreach (var item in collection)
            {
                result.Add(item);
            }
            return result;
        }
    }
}
