using System;
using Algorithms.DataStructures;
using NUnit.Framework;
using System.Text;

namespace AlgorithmTest
{
    [TestFixture, Category(nameof(Algorithms.DataStructures))]
    class StackTest
    {
        private void StringStackTest(IStack<string> st)
        {
            st.Push("to");
            st.Push("be");
            st.Push("or");
            st.Push("not");
            Assert.AreEqual("not", st.Peek());
            Assert.AreEqual("not", st.Pop());
            Assert.AreEqual(3, st.Count);
            var sb = new StringBuilder();
            foreach (string item in st)
            {
                sb.Append(item).Append(" ");
            }
            Assert.AreEqual("or be to ", sb.ToString());
            Stack<string> st0 = new Stack<string>(st);
            Assert.AreEqual("to", st0.Pop());
            st.Clear();
            Assert.IsTrue(st.IsEmpty);
        }

        private void IntStackTestEmpty(IStack<int> st)
        {
            st.Push(1);
            st.Pop();
            var ex = Assert.Throws<InvalidOperationException>(() => st.Pop());
            Assert.That(ex.Message, Is.EqualTo("The Stack<System.Int32> is empty."));
        }

        private void IntStackTestCopyTo(IStack<int> st)
        {
            int[] arr = { 1, 2, 3, 4, 5 };
            int[] arr_to_copy = new int[5];
            arr_to_copy[0] = 1;
            arr_to_copy[4] = 5;
            st.Clear();
            st.Push(4);
            st.Push(3);
            st.Push(2);
            st.CopyTo(arr_to_copy, 1);
            Assert.AreEqual(arr, arr_to_copy);
            st.Push(6);
            var ex = Assert.Throws<ArgumentException>(() => st.CopyTo(arr_to_copy, 1));
            Assert.That(ex.Message, Is.EqualTo("The number of elements in the source Stack<T> is greater than the available space from arrayIndex to the end of the destination array."));
        }

        [Test]
        public void Test_Stack_00()
        {
            Stack<string> st = new Stack<string>();
            StringStackTest(st);
        }

        [Test]
        public void Test_Stack_Empty()
        {
            Stack<int> st = new Stack<int>();
            IntStackTestEmpty(st);
        }

        [Test]
        public void Test_Stack_CopyTo()
        {
            Stack<int> st = new Stack<int>();
            IntStackTestCopyTo(st);
        }

        [Test]
        public void Test_LL_Stack_00()
        {
            LLStack<string> st = new LLStack<string>();
            StringStackTest(st);
        }

        [Test]
        public void Test_LL_Stack_Empty()
        {
            LLStack<int> st = new LLStack<int>();
            IntStackTestEmpty(st);
        }

        [Test]
        public void Test_LLStack_CopyTo()
        {
            LLStack<int> st = new LLStack<int>();
            IntStackTestCopyTo(st);
        }
    }
}
