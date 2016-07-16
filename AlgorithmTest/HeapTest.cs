using Algorithms.DataStructures;
using NUnit.Framework;

namespace AlgorithmTest
{
    [TestFixture, Category(nameof(Algorithms.DataStructures))]
    class HeapTest
    {
        [Test]
        public void Test_Heap_Heapify()
        {
            int[] arr = new int[] { 12, 7, 9, 3, 5, 8, 1, 2, 3 };
            Heap<int> hp = new Heap<int>();
            foreach (var item in arr)
            {
                hp.Insert(item);
            }
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
