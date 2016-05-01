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
    public class Stack<T> : IEnumerable<T>, IEnumerable
    {
        private T[] _items;
        private long _count;

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
        public Stack(long capacity)
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
        public long Count { get { return _count; } }
        /// <summary>
        /// Returns "true" if stack is empty.
        /// </summary>
        public bool IsEmpty { get { return Count == 0; } }

        private void resize(long max)
        {
            T[] temp = _items;
            _items = new T[max];
            for (long i = 0; i < Count; i++)
            {
                _items[i] = temp[i];
            }
        }
        /// <summary>
        /// Pushes item to the stack.
        /// </summary>
        /// <param name="item"></param>
        public void Push(T item)
        {
            if (Count == _items.LongLength)
            {
                resize(_items.LongLength * 2);
            }
            _items[_count++] = item;
        }

        public T Pop()
        {
            if (Count <= _items.LongLength / 4)
            {
                resize(_items.LongLength / 2);
            }
            T item = _items[--_count];
            _items[Count] = default(T);
            return item;
        }

        private class Enumerator<T> : IEnumerator<T>
        {
            private long _currIndex;
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
