﻿using NUnit.Framework;
using Algorithms.SelectionAndSearch;

namespace AlgorithmTest
{

    [TestFixture, Category(nameof(Algorithms.SelectionAndSearch))]
    public class BinarySearchTest
    {
        int[] arr = new int[] { 3, 5, 8, 12, 15, 22 };
        int[] arr0 = new int[] { 1, 3, 5, 7, 9 };
        int[] arr1 = new int[] { 1, 2, 3, 4, 5, 7, 8, 9, 10, 11, 12 };

        [Test]
        public void Test_BS_EQ()
        {
            Assert.AreEqual( 3, arr.BinarySearch(12));
            Assert.AreEqual(-1, arr.BinarySearch(7));
            Assert.AreEqual(-1, arr.BinarySearch(-1));
            Assert.AreEqual(-1, arr.BinarySearch(23));

            Assert.AreEqual(2, arr0.BinarySearch(5));
            Assert.AreEqual(-1, arr0.BinarySearch(4));
            Assert.AreEqual(-1, arr0.BinarySearch(6));
            Assert.AreEqual(0, arr0.BinarySearch(1));
            Assert.AreEqual(4, arr0.BinarySearch(9));

            Assert.AreEqual(-1, arr1.BinarySearch(6));
            Assert.AreEqual(4, arr1.BinarySearch(5));
            Assert.AreEqual(5, arr1.BinarySearch(7));
        }

        [Test]
        public void TestBS_GT()
        {
            Assert.AreEqual(5, arr1.BinarySearch(6, GBinarySearch.Option.EqOrGreater));
            Assert.AreEqual(2, arr1.BinarySearch(3, GBinarySearch.Option.EqOrGreater));
            Assert.AreEqual(0, arr1.BinarySearch(0, GBinarySearch.Option.EqOrGreater));
            Assert.AreEqual(-1, arr1.BinarySearch(14, GBinarySearch.Option.EqOrGreater));
        }

        [Test]
        public void Test_BS_LT()
        {
            Assert.AreEqual(4, arr1.BinarySearch(6, GBinarySearch.Option.EqOrLess));
            Assert.AreEqual(2, arr1.BinarySearch(3, GBinarySearch.Option.EqOrLess));
            Assert.AreEqual(-1, arr1.BinarySearch(0, GBinarySearch.Option.EqOrLess));
            Assert.AreEqual(10, arr1.BinarySearch(14, GBinarySearch.Option.EqOrLess));
        }
    }
}
