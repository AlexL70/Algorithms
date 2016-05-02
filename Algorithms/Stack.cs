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
    public class Stack<T> : AbstractStack<T>, IStack<T>
    {
        private T[] _items;

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
        public override void Push(T item)
        {
            if (Count == _items.Length)
            {
                resize(_items.Length * 2);
            }
            _items[_count++] = item;
        }

        /// <summary>
        /// Return last added item without removint it from the stack;
        /// </summary>
        /// <returns>
        /// Item of type T; the last one pushed to the stack. Throws
        /// InvalidOperationException if stack is empty.
        /// </returns>
        public override T Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException($"The Stack<{typeof(T)}> is empty.");
            }
            return _items[_count - 1]; ;
        }

        /// <summary>
        /// Remove last added item from the stack and then return it.
        /// </summary>
        /// <returns>
        /// Item of type T; the last one pushed to the stack. Throws
        /// InvalidOperationException if stack is empty.
        /// </returns>
        public override T Pop()
        {
            T item = Peek();
            _count--;
            //  Shrink array twice if it is four times larger than cound or items in Stack<T>
            if (Count <= _items.Length / 4)
            {
                resize(_items.Length / 2);
            }
            //  Set ref to null for ref types to let GC free memory
            _items[Count] = default(T);
            return item;
        }

        /// <summary>
        /// Removes all objects from Stack<T>
        /// </summary>
        public override void Clear()
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

        private class Enumerator<U> : IEnumerator<U>
        {
            private int _currIndex;
            private Stack<U> _outer;

            public Enumerator(Stack<U> outer)
            {
                _outer = outer;
                _currIndex = outer.Count - 1;
            }

            public U Current
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

        public override IEnumerator<T> GetEnumerator()
        {
            return new Enumerator<T>(this);
        }
    }
}
