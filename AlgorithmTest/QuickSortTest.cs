using NUnit.Framework;
using Algorithms.Sorting;

namespace AlgorithmTest
{
    [TestFixture, Category(nameof(Algorithms.Sorting))]
    public class QuickSortTest
    {
        [Test]
        public void TestQS_1()
        {
            var qs = new QuickSort<int>();
            int[] arr = { 4, 7, 12, 5, 44, 3, 9, 1, 32, 15 };
            qs.Sort(arr);
            int[] sorted = { 1, 3, 4, 5, 7, 9, 12, 15, 32, 44 };
            Assert.AreEqual(sorted, arr);
        }

        [Test]
        public void TestQS_2()
        {
            var qs = new QuickSort<int>();
            int[] arr = { 3, 5, 1, 4, 2 };
            qs.Sort(arr);
            int[] sorted = { 1, 2, 3, 4, 5 };
            Assert.AreEqual(sorted, arr);
        }

        [Test]
        public void TestQS_MediumPivot()
        {
            var qs = new QuickSort<int>(QuickSort<int>.PivotChoice.CalcMedium);
            int[] arr = { 8, 2, 4, 5, 7, 1 };
            int comp = qs.Sort(arr);
            int[] sorted = { 1, 2, 4, 5, 7, 8 };
            Assert.AreEqual(sorted, arr);
            Assert.AreEqual(8, comp);
        }
    }
}
