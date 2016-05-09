using System;
using Algorithms.DataStructures;
using NUnit.Framework;
using System.Text;

namespace AlgorithmTest
{
    [TestFixture, Category(nameof(Algorithms.DataStructures))]
    class QueueTest
    {
        private void StringQueueTest(IQueue<string> q)
        {
            q.Enqueue("to");
            q.Enqueue("be");
            q.Enqueue("or");
            q.Enqueue("not");
            Assert.AreEqual("to", q.Peek());
            Assert.AreEqual("to", q.Dequeue());
            Assert.AreEqual(3, q.Count);
            var sb = new StringBuilder();
            foreach (string item in q)
            {
                sb.Append(item).Append(" ");
            }
            Assert.AreEqual("be or not ", sb.ToString());
            Queue<string> q0 = new Queue<string>(q);
            Assert.AreEqual("be", q0.Dequeue());
            q.Clear();
            Assert.IsTrue(q.IsEmpty);
        }

        private void IntQueueTestEmpty(IQueue<int> q)
        {
            q.Enqueue(1);
            q.Dequeue();
            var ex = Assert.Throws<InvalidOperationException>(() => q.Dequeue());
            Assert.That(ex.Message, Is.EqualTo("The Queue<System.Int32> is empty."));
        }

        private void IntQueueTestCopyTo(IQueue<int> q)
        {
            int[] arr = { 1, 2, 3, 4, 5 };
            int[] arr_to_copy = new int[5];
            arr_to_copy[0] = 1;
            arr_to_copy[4] = 5;
            q.Clear();
            q.Enqueue(2);
            q.Enqueue(3);
            q.Enqueue(4);
            q.CopyTo(arr_to_copy, 1);
            Assert.AreEqual(arr, arr_to_copy);
            q.Enqueue(6);
            var ex = Assert.Throws<ArgumentException>(() => q.CopyTo(arr_to_copy, 1));
            Assert.That(ex.Message, Is.EqualTo("The number of elements in the source Queue<T> is greater than the available space from arrayIndex to the end of the destination array."));
        }

        [Test]
        public void Test_Queue_00()
        {
            Queue<string> q = new Queue<string>();
            StringQueueTest(q);
        }

        [Test]
        public void Test_Queue_Empty()
        {
            Queue<int> q = new Queue<int>();
            IntQueueTestEmpty(q);
        }

        [Test]
        public void Test_Queue_CopyTo()
        {
            Queue<int> q = new Queue<int>();
            IntQueueTestCopyTo(q);
        }

        [Test]
        public void Test_LLQueue_00()
        {
            LLQueue<string> q = new LLQueue<string>();
            StringQueueTest(q);
        }

        [Test]
        public void Test_LLQueue_Empty()
        {
            LLQueue<int> q = new LLQueue<int>();
            IntQueueTestEmpty(q);
        }

        [Test]
        public void Test_LLQueue_CopyTo()
        {
            LLQueue<int> q = new LLQueue<int>();
            IntQueueTestCopyTo(q);
        }
    }
}
