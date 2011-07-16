using System;
using System.Collections.Generic;

namespace Tetris
{
    public static class EachExtension
    {
        public static void Each<T>(this IEnumerable<T> enumberable, Action<T> action)
        {
            foreach (var item in enumberable)
            {
                action(item);
            }
        }

        public static void Second<T>(this IEnumerable<T> enumberable, Action<T> action)
        {
            var en = enumberable.GetEnumerator();
            en.MoveNext();
            action(en.Current);
        }
    }
}