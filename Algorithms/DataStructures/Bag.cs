using System;

namespace Algorithms.DataStructures
{
    public class Bag<T> : IEnumerable<T>, IEnumerable
    {
        public Bag()
        {
            throw new NotImplementedException();
        }

        public Bag(int capacity)
        {
            throw new NotImplementedException();
        }

        public Bag(IEnumerable<T> collection)
        {
            throw new NotImplementedException();
        }

        public int Count { get { throw new NotImplementedException(); } }

        public bool IsEmpty { get { return Count == 0; } }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
