using System;

namespace Algorithms.DataStructures
{
    /// <summary>
    /// Implementation of Stack data type: "https://en.wikipedia.org/wiki/Stack_(abstract_data_type)"
    /// This implementation is for realtime applications.
    /// It uses LinkedList as internal data structure. So though it takes a bit more
    /// memory for each item then array implementation does, in return, push/pull operations
    /// always take the same amount of time independently on capacity.
    /// </summary>
    /// <typeparam name="T">Type of item in stack.</typeparam>
    public class LLStack<T> : AbstractStack<T>, IStack<T>
    {
        private class Node<U> {
            public U item;
            public Node<U> next;
        }

        private Node<T> _first;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public LLStack()
        {
            _count = 0;
            _first = null;
        }

        /// <summary>
        /// Constructor. Does default initialization and then pushes elements of
        /// collections passed one by one to the stack.
        /// </summary>
        /// <param name="collection">Collection to be added to the stack.</param>
        public LLStack(IEnumerable<T> items) : this()
        {
            foreach (var item in items)
            {
                Push(item);
            }
        }

        /// <summary>
        /// Pushes item to the stack.
        /// </summary>
        /// <param name="item">Item to be pushed.</param>
        public override void Push(T item)
        {
            var node = new Node<T> { item = item, next = _first };
            _first = node;
            _count++;
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
            return _first.item;
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
            var node = Peek();
            var temp = _first.next;
            _first.next = null;
            _first = temp;
            _count--;
            return node;
        }

        /// <summary>
        /// Removes all objects from Stack<T>
        /// </summary>
        public override void Clear()
        {
            while(_first != null)
            {
                var temp = _first.next;
                _first.next = null;
                _first = temp;
            }
            _count = 0;
        }

        private class Enumerator<U> : IEnumerator<U>
        {
            private LLStack<U>.Node<U> _current;
            private LLStack<U> _outer;

            public Enumerator(LLStack<U> outer)
            {
                _outer = outer;
                _current = null;
            }

            public U Current
            {
                get
                {
                    return _current.item;
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
                _current = null;
                _outer = null;
            }

            public bool MoveNext()
            {
                if (_current == null)
                {
                    _current = _outer._first;
                    return true;
                }
                else if (_current?.next != null)
                {
                    _current = _current.next;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public void Reset()
            {
                _current = _outer._first;
            }
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return new Enumerator<T>(this);
        }
    }
}
