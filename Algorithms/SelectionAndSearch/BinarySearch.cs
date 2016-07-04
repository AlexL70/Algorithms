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
        /// <summary>
        /// Method implements binary search in sorted array. Array passed to this method must be sorted.
        /// It also must contain distinct values. If unsorted array passed, or if values are indistinct
        /// then result is unpredictable.
        /// </summary>
        /// <param name="inp">Sorted array to find value in.</param>
        /// <param name="el">Value to find.</param>
        /// <returns>Returns index of value in array. Returns -1 if value not found.</returns>
        public static int BinarySearch<T, K>(this IReadOnlyList<T> inp, K el)
             where T : IComparable<K>
        {
            //if ( el.CompareTo(inp[0]) < 0 || el.CompareTo(inp[inp.Count - 1]) > 0)
            if (inp[0].CompareTo(el) > 0 || inp[inp.Count - 1].CompareTo(el) < 0)
            {
                return -1;
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
                if (inp[min].CompareTo(el) == 0)
                    return min;
                if (inp[max].CompareTo(el) == 0)
                    return max;
            }
            return -1;
        }
    }
}
