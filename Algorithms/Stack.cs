using System;

namespace Algorithms.DataStructures
{
    class Stack<T> : IEnumerable<T>, IEnumerable
    {
        public Stack()
        {
            throw new NotImplementedException();
        }

        public Stack(int capacity)
        {
            throw new NotImplementedException();
        }

        public Stack(IEnumerable<T> collection)
        {
            throw new NotImplementedException();
        }

        public int Count { get { throw new NotImplementedException(); } }

        public bool IsEmpty { get { return Count == 0; } }

        public void Push(T item)
        {
            throw new NotImplementedException();
        }

        public T Pop()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
