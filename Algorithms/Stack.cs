using System;

namespace Algorithms.DataStructures
{
    /// <summary>
    /// Classic implementation of Stack data type: "https://en.wikipedia.org/wiki/Stack_(abstract_data_type)"
    /// This implementation is not for realtime applications.
    /// When pushing it increases capacity twice every time when it is not enough to hold next item.
    /// When popping it decreases capacity twice every time when items take only quater (or less)
    /// of current capacity. Those operations may take considerable amount of time.
    /// </summary>
    /// <typeparam name="T">Type of item in stack.</typeparam>
    public class Stack<T> : IStack<T>
    {
        private T[] _items;
        private int _count;
        private bool _isByRef = typeof(T).IsByRef;

        /// <summary>
        /// Default constructor. Sets capacity to 2 (items).
        /// </summary>
        public Stack()
        {
            _count = 0;
            _items = new T[2];
        }
        /// <summary>
        /// Constructor. Sets initial capacity to value passed.
        /// </summary>
        /// <param name="capacity">Initial capacity of stack.</param>
        public Stack(int capacity)
        {
            _count = 0;
            _items = new T[capacity];
        }
        /// <summary>
        /// Constructor. Does default initialization and then pushes elements of
        /// collections passed one by one to the stack.
        /// </summary>
        /// <param name="collection">Collection to be added to the stack.</param>
        public Stack(IEnumerable<T> collection) : this()
        {
            foreach (var item in collection)
                Push(item);
        }
        /// <summary>
        /// Current count of items in stack.
        /// </summary>
        public int Count { get { return _count; } }
        /// <summary>
        /// Returns "true" if stack is empty.
        /// </summary>
        public bool IsEmpty { get { return Count == 0; } }
        public bool IsReadOnly { get { return true; } }

        private void resize(int max)
        {
            T[] temp = _items;
            _items = new T[max];
            for (int i = 0; i < Count; i++)
            {
                _items[i] = temp[i];
            }
        }
        /// <summary>
        /// Pushes item to the stack.
        /// </summary>
        /// <param name="item">Item to be pushed.</param>
        public void Push(T item)
        {
            if (Count == _items.Length)
            {
                resize(_items.Length * 2);
            }
            _items[_count++] = item;
        }

        /// <summary>
        /// Pops item from the stack.
        /// </summary>
        /// <returns>
        /// Item of type T; the last one pushed to the stack. Throws
        /// InvalidOperationException if stack is empty.
        /// </returns>
        public T Pop()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException($"The Stack<{_items[0].GetType().ToString()}> is empty.");
            }
            if (Count <= _items.Length / 4)
            {
                resize(_items.Length / 2);
            }
            T item = _items[--_count];
            //  Set ref to null for ref types to let GC free memory
            _items[Count] = default(T);
            return item;
        }

        /// <summary>
        /// Removes all objects from Stack<T>
        /// </summary>
        public void Clear()
        {
            if (!IsEmpty)
            {
                if (_isByRef)
                {
                    for (int i = 0; i < _count; i++)
                    {
                        _items[i] = default(T);
                    }
                }
                _count = 0;
                resize(2);
            }
        }

        /// <summary>
        /// Determines whether an element is in the Stack<T>
        /// </summary>
        /// <param name="item">The object to locate in the Stack<T>.The value can be null for reference types.</param>
        /// <returns>
        /// Type: System.Boolean
        /// true if item is found in the Stack<T>; otherwise, false.
        /// </returns>
        public bool Contains(T item)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_items[i].Equals(item))
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
            if (arrayIndex + _count > array.Length)
            {
                throw new ArgumentException("The number of elements in the source Stack<T> is greater than the available space from arrayIndex to the end of the destination array.");
            }

            var i = arrayIndex;
            foreach (var item in this)
            {
                array.SetValue( item, i);
            }
        }

        bool ICollection.IsSynchronized { get { return false; } }

        object ICollection.SyncRoot { get { return null; } }

        private class Enumerator<T> : IEnumerator<T>
        {
            private int _currIndex;
            private Stack<T> _outer;

            public Enumerator(Stack<T> outer)
            {
                _outer = outer;
                _currIndex = outer.Count - 1;
            }

            public T Current
            {
                get
                {
                    if (_currIndex < 0 || _currIndex > _outer.Count - 1)
                    {
                        throw new IndexOutOfRangeException("Enumerator indes is out of bounds");
                    }
                    return _outer._items[_currIndex];
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public void Dispose()
            {
                _outer = null;
            }

            public bool MoveNext()
            {
                if (_currIndex > 0 )
                {
                    _currIndex--;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public void Reset()
            {
                _currIndex = _outer.Count - 1;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator<T>(this);
        }
    }
}
