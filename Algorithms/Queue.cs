using System;

namespace Algorithms.DataStructures
{
    public class Queue<T>: IEnumerable<T>, IEnumerable
    {
        public Queue()
        {
            throw new NotImplementedException();
        }

        public Queue(int capacity)
        {
            throw new NotImplementedException();
        }

        public Queue(IEnumerable<T> collection)
        {
            throw new NotImplementedException();
        }

        public int Count { get { throw new NotImplementedException(); } }

        public bool IsEmpty { get { return Count == 0; } }

        public void Enqueue(T item)
        {
            throw new NotImplementedException();
        }

        public T Dequeue()
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
