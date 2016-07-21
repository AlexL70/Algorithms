using Algorithms.DataStructures;
using NUnit.Framework;

namespace AlgorithmTest
{
    [TestFixture, Category(nameof(Algorithms.DataStructures))]
    class BinaryHeapTest
    {
        [Test]
        public void Test_BinaryHeap_Heapify()
        {
            int[] arr = new int[] { 12, 7, 9, 3, 5, 8, 1, 2, 3 };
            BinaryHeap<int> hp = new BinaryHeap<int>();
            foreach (var item in arr)
            {
                hp.Insert(item);
            }
            Assert.AreEqual(1, hp.FindMin());
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = hp.ExtractMin();
            }
            Assert.AreEqual(
                new int[] { 1, 2, 3, 3, 5, 7, 8, 9, 12 }, 
                arr);
        }
    }
}
