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
            return source.Select(x => equalityComparer(x) ? item : x);
        }
    }
}
