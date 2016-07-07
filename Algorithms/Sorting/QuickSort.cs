using System;

namespace Algorithms.Sorting
{
    public static class GQuickSort
    {
        public enum PivotChoice
        {
            AlwaysFirst,
            AlwaysLast,
            CalcMedium,
            ChooseRandom
        }

        private static void Swap<T>(T[] arr, int i, int j)
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

        private delegate int ChoosePivotIndex<T>(T[] arr, int left, int right)
            where T : IComparable<T>;

        private static int CalcMedium<T>(T[] arr, int left, int right)
            where T : IComparable<T>
        {
            int medium = (left + right) / 2;
            if ((arr[left].CompareTo(arr[medium]) < 0 && arr[medium].CompareTo(arr[right]) < 0)
            || (arr[right].CompareTo(arr[medium]) < 0 && arr[medium].CompareTo(arr[left]) < 0))
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

        private static int Pivot<T>(T[] arr, int first, int last, int pivotIndex)
            where T : IComparable<T>
        {
            T pivot = arr[pivotIndex];
            if (first != pivotIndex)
            {
                Swap(arr, first, pivotIndex);
            }
            int less = first;
            for (int scanned = first + 1; scanned <= last; scanned++)
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

        public static int QuickSort<T>(this T[] arr, PivotChoice choice = PivotChoice.ChooseRandom)
            where T : IComparable<T>
        {
            ChoosePivotIndex<T> pivotIntFunc = CalcMedium;
            switch (choice)
            {
                case PivotChoice.AlwaysFirst:
                    pivotIntFunc = (T[] ar, int left, int right) => left;
                    break;
                case PivotChoice.AlwaysLast:
                    pivotIntFunc = (T[] ar, int left, int right) => right;
                    break;
                case PivotChoice.CalcMedium:
                    pivotIntFunc = CalcMedium;
                    break;
                case PivotChoice.ChooseRandom:
                    Random rnd = new Random();
                    pivotIntFunc = (T[] ar, int left, int right) => rnd.Next(left, right + 1);
                    break;
            }
            return Sort<T>(arr, 0, arr.Length - 1, pivotIntFunc);
        }

        private static int Sort<T>( T[] arr, int left, int right, ChoosePivotIndex<T> pivotIntFunc)
            where T : IComparable<T>
        {
            int result = left < right ? right - left : 0;
            if (left < right + 1)
            {
                int pivotInd = pivotIntFunc(arr, left, right);
                pivotInd = Pivot(arr, left, right, pivotInd);
                if (left < pivotInd - 1)
                {
                    result += Sort<T>(arr, left, pivotInd - 1, pivotIntFunc);
                }
                if (pivotInd + 1 < right)
                {
                    result += Sort<T>(arr, pivotInd + 1, right, pivotIntFunc);
                }
            }
            return result;
        }
    }
}
