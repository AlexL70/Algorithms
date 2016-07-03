using System;
using NUnit.Framework;
using Algorithms.Sorting;

namespace AlgorithmTest
{
    [TestFixture, Category(nameof(Algorithms.Sorting))]
    public class MergeSortTest
    {
        [Test]
        public void TestMS_1()
        {
            int[] arr = { 4, 7, 12, 5, 44, 3, 9, 1, 32, 15 };
            arr.MergeSort();
            int[] sorted = { 1, 3, 4, 5, 7, 9, 12, 15, 32, 44 };
            Assert.AreEqual(sorted, arr);
        }

        [Test]
        public void TestMS_2()
        {
            int[] arr = { 3, 5, 1, 4, 2 };
            arr.MergeSort();
            int[] sorted = { 1, 2, 3, 4, 5 };
            Assert.AreEqual(sorted, arr);
        }

        [Test]
        public void TestMS_Inv()
        {
            int[] arr = { 1, 3, 5, 2, 4, 6 };
            long inversions = arr.MergeSort();
            int[] sorted = { 1, 2, 3, 4, 5, 6 };
            Assert.AreEqual(sorted, arr);
            Assert.AreEqual(3, inversions);
        }


    }
}
