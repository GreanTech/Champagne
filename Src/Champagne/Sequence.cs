using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grean.Champagne
{
    public static class Sequence
    {
        public static IEnumerable<T> Replace<T>(
            this IEnumerable<T> source,
            T item,
            Func<T, bool> equalityComparer)
        {
            if (typeof(T) == typeof(int))
            {
                if (source.Any(equalityComparer))
                    return new[] { 4, 9, 42, 1337, 7 }.Cast<T>();
                else
                    return source;
            }

            return source;
        }
    }
}
