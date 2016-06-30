using System;

namespace Algorithms.Sorting
{
    public class QuickSort<T>
        where T : IComparable<T>
    {
        public enum PivotChoice
        {
            AlwaysFirst,
            AlwaysLast,
            CalcMedium,
            ChooseRandom
        }

        public QuickSort(PivotChoice choice = PivotChoice.ChooseRandom)
        {
            switch (choice)
            {
                case PivotChoice.AlwaysFirst:
                    pivotIntFunc = (T[] arr, int left, int right) => left;
                    break;
                case PivotChoice.AlwaysLast:
                    pivotIntFunc = (T[] arr, int left, int right) => right;
                    break;
                case PivotChoice.CalcMedium:
                    pivotIntFunc = CalcMedium;
                    break;
                case PivotChoice.ChooseRandom:
                    Random rnd = new Random();
                    pivotIntFunc = (T[] arr, int left, int right) => rnd.Next(left, right);
                    break;
            }
        }

        private static void Swap(T[] arr, int i, int j)
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

        private delegate int ChoosePivotIndex(T[] arr, int left, int right);
        private ChoosePivotIndex pivotIntFunc;

        private int CalcMedium(T[] arr, int left, int right)
        {
            int medium = (left + right) / 2;
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

        private int Pivot(T[] arr, int first, int last, int pivotIndex)
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

        public int Sort( T[] arr, int left = 0, int right = -1)
        {
            if (right == -1 && left == 0)
            {
                right = arr.Length - 1;
            }
            int result = left < right ? right - left : 0;
            if (left < right + 1)
            {
                int pivotInd = pivotIntFunc(arr, left, right);
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
