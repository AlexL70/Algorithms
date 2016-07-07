using System;
using System.Collections.Generic;

namespace Algorithms.SelectionAndSearch
{
    /// <summary>
    /// Class extends IReadOnlyList<T> interface.
    /// It implements binary search in sorted array.
    /// </summary>
    /// <typeparam name="T">Type of array elements.</typeparam>
    public static class GBinarySearch
    {
        public enum Option
        {
            Equals,
            EqOrGreater,
            EqOrLess
        }

        /// <summary>
        /// Method implements binary search in sorted array. Array passed to this method must be sorted.
        /// It also must contain distinct values. If unsorted array passed, or if values are indistinct
        /// then result is unpredictable.
        /// </summary>
        /// <param name="inp">Sorted array to find value in.</param>
        /// <param name="el">Value to find.</param>
        /// <param name="option">
        /// Equals - find exact value; return -1 if not found.
        /// EqOrGreater - find nearest equal or greater value; return -1 if all values are less then el.
        /// EqOrLess - find nearest equal of less value; return -1 if all values are greater tnen el.
        /// </param>
        /// <returns>Returns index of value in array. Returns -1 if value not found.</returns>
        public static int BinarySearch<T, K>(this IReadOnlyList<T> inp, K el, Option option = Option.Equals)
             where T : IComparable<K>
        {
            if (inp.Count == 0)
                return -1;
            if (inp[0].CompareTo(el) > 0)
            {
                if (option == Option.EqOrGreater)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else if (inp[inp.Count - 1].CompareTo(el) < 0)
            {
                if (option == Option.EqOrLess)
                {
                    return inp.Count - 1;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                int min = 0;
                int max = inp.Count - 1;
                while (min + 1 < max)
                {
                    int mid = (min + max) / 2;
                    if (inp[mid].CompareTo(el) == 0)
                    {
                        return mid;
                    }
                    else if (inp[mid].CompareTo(el) > 0)
                    {
                        max = mid;
                    }
                    else
                    {
                        min = mid;
                    }
                }
                switch (option)
                {
                    case Option.Equals:
                        if (inp[min].CompareTo(el) == 0)
                            return min;
                        if (inp[max].CompareTo(el) == 0)
                            return max;
                        break;
                    case Option.EqOrGreater:
                        return max;
                    case Option.EqOrLess:
                        return min;
                }
            }
            return -1;
        }
    }
}
