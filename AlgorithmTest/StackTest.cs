using Algorithms.DataStructures;
using NUnit.Framework;

namespace AlgorithmTest
{
    [TestFixture, Category("Data Structures")]
    class StackTest
    {
        [Test]
        public void Test_Stack_00()
        {
            Stack<string> st = new Stack<string>();
            st.Push("to");
            st.Push("be");
            st.Push("or");
            st.Push("not");
            Assert.AreEqual("not", st.Pop());
            Assert.AreEqual(3, st.Count);
            Stack<string> st0 = new Stack<string>(st);
            Assert.AreEqual("to", st0.Pop());
        }
    }
}
