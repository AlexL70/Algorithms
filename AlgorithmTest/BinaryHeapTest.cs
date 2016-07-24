using Algorithms.DataStructures;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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

        private class Trackable : IComparable<Trackable>
        {
            public int Value { get; set; }
            public int Tag { get; set; }

            public Trackable(int value)
            {
                Value = value;
                Tag = -1;
            }

            public int CompareTo(Trackable other)
            {
                return this.Value.CompareTo(other.Value);
            }
        }

        [TestCase(new int[] { 12, 7, 9, 3, 5, 8, 1, 2, 3 },
            new int[] { 1, 3, 5, 7 }, new int[] { 1, 3, 5, 9, 12 })]
        [TestCase(new int[] { 11, 9, 7, 5, 3, 1, 0, -1, -3, -5, -7 },
            new int[] { 9, 6, 3, 0}, 
            new int[] { -7, -3, -1, 1, 3, 7, 9})]
        public void Test_BinaryHeap_RemoveAt(int[] input, int[] delIndexes, int[] result)
        {
            List<Trackable> list = new List<Trackable>();
            foreach (var item in input)
            {
                list.Add(new Trackable(item));
            }
            BinaryHeap<Trackable>.PostProcess exp = (Trackable item, int oldIndex, int newIndex) =>
                item.Tag = newIndex;
            BinaryHeap<Trackable> heap = new BinaryHeap<Trackable>();
            heap.postProcess = exp;
            foreach (var item in list)
            {
                heap.Insert(item);
            }
            foreach (var delInd in delIndexes)
            {
                heap.RemoveAt(list[delInd].Tag);
            }
            var newList = new List<int>();
            while (heap.Count > 0)
            {
                newList.Add(heap.ExtractMin().Value);
            }
            Assert.AreEqual(result, newList.ToArray());
        }

    }
}
