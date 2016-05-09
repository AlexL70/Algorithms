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
    }
}
