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
            T replacementValue,
            Func<T, bool> equalityComparer)
        {
            if (equalityComparer == null)
                throw new ArgumentNullException("equalityComparer");

            return source.Select(
                x => equalityComparer(x) ? replacementValue : x);
        }

        public static IEnumerable<T> Replace<T>(
            this IEnumerable<T> source,
            T replacementValue,
            IEquatable<T> equalityComparer)
        {
            return source;
        }
    }
}
