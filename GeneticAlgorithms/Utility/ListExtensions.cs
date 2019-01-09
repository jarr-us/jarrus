using System;
using System.Collections.Generic;
using System.Linq;

namespace Jarrus.GA.Utility
{
    public static class ListExtensions
    {
        public static int IndexOf<T>(this IEnumerable<T> list, Predicate<T> condition)
        {
            int i = -1;
            return list.Any(x => { i++; return condition(x); }) ? i : -1;
        }
    }
}
