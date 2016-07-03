using System;

namespace Algorithms.DataStructures
{
    public abstract class AbstractQueue<T> : IQueue<T>
    {
        protected int _count;
        protected bool _isByRef = typeof(T).IsByRef;

        /// <summary>
        /// Current count of items in queue.
        /// </summary>
        public int Count { get { return _count; } }

        /// <summary>
        /// Returns "true" if queue is empty.
        /// </summary>
        public bool IsEmpty { get { return Count == 0; } }

        public bool IsSynchronized { get { return false; } }

        public object SyncRoot { get { return null; } }


        public abstract void Clear();
        /// <summary>
        /// Copies the Queue<T> to an existing one-dimensional Array, starting at the specified array index.
        /// </summary>
        /// <param name="array">Array to copy queue to.</param>
        /// <param name="arrayIndex">Index to start from.</param>
        public void CopyTo(Array array, int arrayIndex = 0)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array is null.");
            }
            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("arrayIndex is less then zero.");
            }
            if (arrayIndex + Count >= array.Length)
            {
                throw new ArgumentException("The number of elements in the source Queue<T> is greater than the available space from arrayIndex to the end of the destination array.");
            }

            var i = arrayIndex;
            foreach (var item in this)
            {
                array.SetValue(item, i);
                i++;
            }
        }
        public abstract T Dequeue();
        public abstract void Enqueue(T item);
        public abstract IEnumerator<T> GetEnumerator();
        public abstract T Peek();

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
