using System;

namespace Algorithms
{
    public static class QuickSort<T>
        where T : IComparable<T>
    {
        private static void Swap(T[] arr, long i, long j)
        {
            if (arr == null)
            {
                throw new ArgumentNullException("Array is null. Cannot swap.");
            }
            if (i < 0 || j < 0 || i > arr.Length || j > arr.Length)
            {
                throw new IndexOutOfRangeException("One of index is out of range. Cannot swap elements.");
            }
            T temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        private static long ChoosePivotIndex(T[] arr, long left, long right)
        {
            long medium = (left + right) / 2;
            if (arr[left].CompareTo(arr[medium]) < 0 && arr[medium].CompareTo(arr[right]) < 0)
            {
                return medium;
            }
            else if ((arr[medium].CompareTo(arr[left]) < 0 && arr[left].CompareTo(arr[right]) < 0)
                  || (arr[right].CompareTo(arr[left]) < 0 && arr[left].CompareTo(arr[medium]) < 0))
            {
                return left;
            }
            else
            {
                return right;
            }
        }

        private static long Pivot(T[] arr, long first, long last, long pivotIndex)
        {
            T pivot = arr[pivotIndex];
            if (first != pivotIndex)
            {
                Swap(arr, first, pivotIndex);
            }
            long less = first;
            for (long scanned = first + 1; scanned <= last; scanned++)
            {
                if (arr[scanned].CompareTo(pivot) < 0)
                {
                    if (less + 1 < scanned)
                    {
                        Swap(arr, ++less, scanned);
                    }
                    else
                    {
                        ++less;
                    }
                }
            }
            Swap(arr, first, less);
            return less;
        }

        public static long Sort( T[] arr, long left = 0, long right = -1)
        {
            if (right == -1 && left == 0)
            {
                right = arr.Length - 1;
            }
            long result = left < right ? right - left : 0;
            if (left < right + 1)
            {
                long pivotInd = ChoosePivotIndex(arr, left, right);
                pivotInd = Pivot(arr, left, right, pivotInd);
                if (left < pivotInd - 1)
                {
                    result += Sort(arr, left, pivotInd - 1);
                }
                if (pivotInd + 1 < right)
                {
                    result += Sort(arr, pivotInd + 1, right);
                }
            }
            return result;
        }
    }
}
