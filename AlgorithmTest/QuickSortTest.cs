using NUnit.Framework;
using Algorithms;

namespace AlgorithmTest
{
    [TestFixture]
    public class QuickSortTest
    {
        [Test]
        public void TestQS_1()
        {
            int[] arr = { 4, 7, 12, 5, 44, 3, 9, 1, 32, 15 };
            QuickSort<int>.Sort(arr);
            int[] sorted = { 1, 3, 4, 5, 7, 9, 12, 15, 32, 44};
            Assert.AreEqual(sorted, arr);
        }

        [Test]
        public void TestQS_2()
        {
            int[] arr = { 3, 5, 1, 4, 2};
            QuickSort<int>.Sort(arr);
            int[] sorted = { 1, 2, 3, 4, 5 };
            Assert.AreEqual(sorted, arr);
        }
    }
}
