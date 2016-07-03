using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.SelectionAndSearch
{
    /// <summary>
    /// Class extends IReadOnlyList<T> interface.
    /// It implements binary search in sorted array.
    /// </summary>
    /// <typeparam name="T">Type of array elements.</typeparam>
    public static class BinarySearch
    {
        /// <summary>
        /// Method implements binary search in sorted array. Array passed to this method must be sorted.
        /// It also must contain distinct values. If unsorted array passed, or if values are indistinct
        /// then result is unpredictable.
        /// </summary>
        /// <param name="inp">Sorted array to find value in.</param>
        /// <param name="el">Value to find.</param>
        /// <returns>Returns index of value in array. Returns -1 if value not found.</returns>
        public static int BSearch<T>(this IReadOnlyList<T> inp, T el)
             where T : IComparable<T>
        {
            if ( el.CompareTo(inp[0]) < 0 || el.CompareTo(inp[inp.Count - 1]) > 0)
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
                    if (el.CompareTo(inp[mid]) == 0)
                    {
                        return mid;
                    }
                    else if (el.CompareTo(inp[mid]) < 0)
                    {
                        max = mid;
                    }
                    else
                    {
                        min = mid;
                    }
                }
                if (el.CompareTo(inp[min]) == 0)
                    return min;
                if (el.CompareTo(inp[max]) == 0)
                    return max;
            }
            return -1;
        }
    }
}
