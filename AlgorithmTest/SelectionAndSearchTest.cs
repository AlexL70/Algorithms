using NUnit.Framework;
using Algorithms.SelectionAndSearch;

namespace AlgorithmTest
{

    [TestFixture, Category(nameof(Algorithms.SelectionAndSearch))]
    public class BinarySearchTest
    {
        [Test]
        public void TestBS_0()
        {
            int[] arr = new int[] { 3, 5, 8, 12, 15, 22 };
            int[] arr0 = new int[] { 1, 3, 5, 7, 9 };
            int[] arr1 = new int[] { 1, 2, 3, 4, 5, 7, 8, 9, 10, 11, 12 };
            Assert.AreEqual( 3, arr.BSearch(12));
            Assert.AreEqual(-1, arr.BSearch(7));
            Assert.AreEqual(-1, arr.BSearch(-1));
            Assert.AreEqual(-1, arr.BSearch(23));

            Assert.AreEqual(2, arr0.BSearch(5));
            Assert.AreEqual(-1, arr0.BSearch(4));
            Assert.AreEqual(-1, arr0.BSearch(6));
            Assert.AreEqual(0, arr0.BSearch(1));
            Assert.AreEqual(4, arr0.BSearch(9));

            Assert.AreEqual(-1, arr1.BSearch(6));
            Assert.AreEqual(4, arr1.BSearch(5));
            Assert.AreEqual(5, arr1.BSearch(7));
        }
    }
}
