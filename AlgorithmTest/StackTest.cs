﻿using System;
using Algorithms.DataStructures;
using NUnit.Framework;

namespace AlgorithmTest
{
    [TestFixture, Category("Data Structures")]
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

        [Test]
        public void Test_Stack_00()
        {
            Stack<string> st = new Stack<string>();
            StringStackTest(st);
        }

        [Test]
        public void Test_LL_Stack_00()
        {
            LLStack<string> st = new LLStack<string>();
            StringStackTest(st);
        }

        [Test]
        public void Test_Stack_Empty()
        {
            Stack<int> st = new Stack<int>();
            IntStackTestEmpty(st);
        }

        [Test]
        public void Test_LL_Stack_Empty()
        {
            LLStack<int> st = new LLStack<int>();
            IntStackTestEmpty(st);
        }
    }
}
