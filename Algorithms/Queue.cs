using System;

namespace Algorithms.DataStructures
{
    /// <summary>
    /// Classic (resizing array) implementation of Queue data type: https://en.wikipedia.org/wiki/Queue_(abstract_data_type)
    /// This implementation is not for real-time applications. When enqueueing it increaces capacity
    /// twice every time when it is not enough room to hold next item. When dequeueing it decreaces
    /// capacity twice when every time when items take only quarter (or less) of current capacity.
    /// Those operations may take considerable amount of time.
    /// </summary>
    /// <typeparam name="T">Type of item in queue.</typeparam>
    public class Queue<T> : IQueue<T>
    {
        private T[] _items;
        private int _first;
        private int _count;


        /// <summary>
        /// Default constructor. Sets capacity to 2 items.
        /// </summary>
        public Queue()
        {
            _items = new T[2];
            _first = 0;
            _count = 0;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="capacity">Sets initial capacity to value passed.</param>
        public Queue(int capacity)
        {
            _items = new T[capacity];
            _first = 0;
            _count = 0;
        }

        /// <summary>
        /// Constructor. Does default initialization and then enques all elements of collection passed.
        /// </summary>
        /// <param name="collection">Initial collection of elements in queue.</param>
        public Queue(IEnumerable<T> collection) : this()
        {
            foreach (var item in collection)
            {
                Enqueue(item);
            }
        }

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

        private void resize(int max)
        {
            var arr = new T[max];
            for (int i = 0; i < max; i++)
            {
                arr[i] = _items[(_first + i) % _items.Length];
            }
            _items = arr;
            _first = 0;
        }

        /// <summary>
        /// Adds an object to the end of the Queue<T>.
        /// </summary>
        /// <param name="item">Element to add</param>
        public void Enqueue(T item)
        {
            if (_count == _items.Length)
            {
                resize(_count * 2);
            }
            _items[(_first + _count) % _items.Length] = item;
            _count++;
        }

        /// <summary>
        /// Removes and returns the object at the beginning of the Queue<T>.
        /// </summary>
        /// <returns>T</returns>
        public T Dequeue()
        {
            var item = Peek();
            _items[_first] = default(T);
            _first = (_first + 1) % _items.Length;
            _count--;
            if (_count <= _items.Length / 4)
            {
                resize(_items.Length / 2);
            }
            return item;
        }

        /// <summary>
        /// Returns the object at the beginning of the Queue<T> without removing it.
        /// </summary>
        /// <returns>T</returns>
        public T Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException($"The Queue<{typeof(T)}> is empty.");
            }
            return _items[_first];
        }

        public void Clear()
        {
            if (!IsEmpty)
            {
                _items = new T[2];
                _first = 0;
                _count = 0;
            }
        }

        private class Enumerator : IEnumerator<T>
        {
            int _current;
            Queue<T> _outer;

            public Enumerator(Queue<T> q)
            {
                _outer = q;
                _current = -1;
            }

            public T Current
            {
                get
                {
                    return _outer._items[(_outer._first + _current) % _outer._items.Length];
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
                if (_current < _outer._count - 1)
                {
                    _current++;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public void Reset()
            {
                _current = -1;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }
    }
}
