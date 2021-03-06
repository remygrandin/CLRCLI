﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CLIForms.Extentions
{
    public static class LinqExtensions
    {
        public static IEnumerable<IEnumerable<T>> GroupAdjacentBy<T>(this IEnumerable<T> source, Func<T, T, bool> predicate)
        {
            using (var e = source.GetEnumerator())
            {
                if (e.MoveNext())
                {
                    var list = new List<T> { e.Current };
                    var pred = e.Current;
                    while (e.MoveNext())
                    {
                        if (predicate(pred, e.Current))
                        {
                            list.Add(e.Current);
                        }
                        else
                        {
                            yield return list;
                            list = new List<T> { e.Current };
                        }
                        pred = e.Current;
                    }
                    yield return list;
                }
            }
        }

        public static IEnumerable<T> Flatten<T>(this T[,] map)
        {
            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    yield return map[row, col];
                }
            }
        }

        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> e,  Func<T, IEnumerable<T>> f)
        {
            return e?.SelectMany(c => f(c).Flatten(f)).Concat(e);
        }

        public static IEnumerable<T> Parents<T>(this T item, Func<T, T> predicate)
        {
            List<T> list = new List<T>();

            list.Add(item);

            T parent = predicate(item);

            if(parent != null)
                list.AddRange(parent.Parents(predicate));

            return list;
        }

        public static bool IsLast<T>(this IEnumerable<T> items, T item)
        {
            T last = items.LastOrDefault();
            if (last == null)
                return false;
            return item.Equals(last); // OR Object.ReferenceEquals(last, item)
        }

        public static bool IsFirst<T>(this IEnumerable<T> items, T item)
        {
            T first = items.FirstOrDefault();
            if (first == null)
                return false;
            return item.Equals(first);
        }

        public static T Next<T>(this IList<T> items, T item)
        {
            int pos = items.IndexOf(item);

            return items[pos + 1];
        }

        public static T Prev<T>(this IList<T> items, T item)
        {
            int pos = items.IndexOf(item);

            return items[pos - 1];
        }
    }
}
