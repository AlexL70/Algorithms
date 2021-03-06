﻿using System;

namespace Algorithms.Sorting
{
    public static class GMergeSort
    {
        public static long MergeSort<T>(this T[] arr)
            where T : IComparable<T>
        {
            long Inversions;
            T[] copy1 = new T[arr.Length];
            Inversions = Sort(arr, copy1, 0, arr.Length - 1);
            copy1.CopyTo(arr, 0);
            return Inversions;
        }

        private static long Sort<T>(T[] src, T[] dest, int left, int right, int level = 1)
            where T : IComparable<T>
        {
            if (left < right)
            {
                var middle = left + (right - left) / 2;
                long inv = Sort(dest, src, left, middle, level + 1);
                inv += Sort(dest, src, middle + 1, right, level + 1);
                inv += MergeAndCountInversions(src, dest, left, middle, right);
                return inv;
            }
            else
            {
                if (level % 2 == 1)
                    dest[left] = src[left];
                return 0;
            }
        }

        private static long MergeAndCountInversions<T>(T[] src, T[] dest, int left, int middle, int right)
            where T : IComparable<T>
        {
            long inv = 0;
            int i = left;
            int j = middle + 1;
            for (int x = left; x <= right; x++)
            {
                if (j > right || (i <= middle && src[i].CompareTo(src[j]) <= 0))
                {
                    dest[x] = src[i];
                    i++;
                }
                else
                {
                    dest[x] = src[j];
                    j++;
                    inv += middle - i + 1;
                }
            }
            return inv;
        }
    }
}
