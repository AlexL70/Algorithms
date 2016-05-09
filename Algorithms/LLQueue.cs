using System;

namespace Algorithms.DataStructures
{
    /// <summary>
    /// Classic (linked list) implementation of Queue data type: https://en.wikipedia.org/wiki/Queue_(abstract_data_type)
    /// </summary>
    /// <typeparam name="T">Type of item in queue.</typeparam>
    public class LLQueue<T> : AbstractQueue<T>, IQueue<T>
    {
        private Node _first;
        private Node _last;

        public LLQueue()
        {
            _first = null;
            _last = null;
        }

        public LLQueue(IEnumerable<T> collection) : this()
        {
            foreach (var item in collection)
            {
                Enqueue(item);
            }
        }

        private class Node
        {
            public T item;
            public Node next;
        }

        public override T Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException($"The Queue<{typeof(T)}> is empty.");
            }
            return _first.item;
        }

        public override T Dequeue()
        {
            var item = Peek();
            _count--;
            _first = _first.next;
            if (IsEmpty)
            {
                _last = null;
            }
            return item;
        }

        public override void Enqueue(T item)
        {
            Node oldLast = _last;
            _last = new Node();
            _last.item = item;
            _last.next = null;
            if (IsEmpty)
            {
                _first = _last;
            }
            else
            {
                oldLast.next = _last;
            }
            _count++;
        }

        public override void Clear()
        {
            while (!IsEmpty) Dequeue();
        }

        private class Enumerator : IEnumerator<T>
        {
            private Node _current;
            private LLQueue<T> _outer;

            public Enumerator(LLQueue<T> outer)
            {
                _outer = outer;
                _current = null;
            }

            public T Current
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
                    if (_current != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (_current?.next != null)
                    {
                        _current = _current.next;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            public void Reset()
            {
                _current = null;
            }
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
