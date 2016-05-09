using System;

namespace Algorithms.DataStructures
{
    public abstract class AbstractStack<T> : IStack<T>
    {
        protected int _count;
        protected bool _isByRef = typeof(T).IsByRef;

        /// <summary>
        /// Current count of items in stack.
        /// </summary>
        public int Count { get { return _count; } }
        /// <summary>
        /// Returns "true" if stack is empty.
        /// </summary>
        public bool IsEmpty { get { return Count == 0; } }
        public bool IsReadOnly { get { return true; } }

        public abstract void Push(T item);
        public abstract T Peek();
        public abstract T Pop();
        public abstract void Clear();
        /// <summary>
        /// Determines whether an element is in the Stack<T>
        /// </summary>
        /// <param name="item">The object to locate in the Stack<T>.The value can be null for reference types.</param>
        /// <returns>
        /// Type: System.Boolean
        /// true if item is found in the Stack<T>; otherwise, false.
        /// </returns>
        public virtual bool Contains(T item)
        {
            foreach (T item0 in this)
            {
                if (item0.Equals(item))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Copies the Stack<T> to an existing one-dimensional Array, starting at the specified array index.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
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
                throw new ArgumentException("The number of elements in the source Stack<T> is greater than the available space from arrayIndex to the end of the destination array.");
            }

            var i = arrayIndex;
            foreach (var item in this)
            {
                array.SetValue(item, i);
                i++;
            }
        }

        bool ICollection.IsSynchronized { get { return false; } }

        object ICollection.SyncRoot { get { return null; } }


        public abstract IEnumerator<T> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
