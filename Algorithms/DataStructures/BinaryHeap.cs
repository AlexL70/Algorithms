using System;
using System.Runtime.CompilerServices;


namespace Algorithms.DataStructures
{
    /// <summary>
    /// Classic binary heap data structure implementation. 
    /// "https://en.wikipedia.org/wiki/Heap_(data_structure)"
    /// </summary>
    /// <typeparam name="T">Type of item in Heap. It must be ordered
    /// (support IComparable interface).</typeparam>
    public class BinaryHeap<T> where T : IComparable<T>
    {
        private T[] _items;
        private int _count;
        private bool _autoshrink;
        protected bool _isByRef = typeof(T).IsByRef;

        public delegate void PostProcess(T element, int oldIndex, int newIndex);
        /// <summary>
        /// Assigned method is called at the end of method(s) moving elements inside heap
        /// it allow, for instance, 
        /// </summary>
        public PostProcess postProcess { get; set; }

        /// <summary>
        /// Default contructor. Sets capacity to 2 items by default.
        /// </summary>
        public BinaryHeap() : this(2, true) { }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="capacity">Initial capacity.</param>
        /// <param name="autoshrink">When true passed, Heap frees extra memory
        /// when extracting values.</param>
        public BinaryHeap(int capacity = 2, bool autoshrink = true)
        {
            _items = new T[capacity];
            _count = 0;
            _autoshrink = autoshrink;
        }
        /// <summary>
        /// Construction. Initializes heap with collection of elements.
        /// </summary>
        /// <param name="collection"></param>
        public BinaryHeap(IEnumerable<T> collection, bool autoshrink = true) : this(2, autoshrink)
        {
            foreach (var item in collection)
                Insert(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected int ParentIndex(int index)
        {
            return (index - 1) / 2;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected int LeftChildIndex(int index)
        {
            return (index + 1) * 2 - 1;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected int RightChildIndex(int index)
        {
            return (index + 1) * 2;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void Swap(int first, int second)
        {
            T temp = _items[first];
            _items[first] = _items[second];
            postProcess?.Invoke(_items[first], second, first);
            _items[second] = temp;
            postProcess?.Invoke(_items[second], first, second);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected int MinChildIndex(int index)
        {
            int left = LeftChildIndex(index);
            int right = RightChildIndex(index);
            return right >= _count || _items[left].CompareTo(_items[right]) <= 0 ? left : right;
        }
        protected void BubbleUp(int index)
        {
            int parInd = ParentIndex(index);
            while (_items[index].CompareTo(_items[parInd]) < 0)
            {
                Swap(index, parInd);
                index = parInd;
                parInd = ParentIndex(index);
            }
        }
        protected void BubbleDown(int index = 0)
        {
            int min = MinChildIndex(index);
            while (min < _count && _items[index].CompareTo(_items[min]) > 0)
            {
                Swap(min, index);
                index = min;
                min = MinChildIndex(index);
            }
        }
        protected void resize(int max)
        {
            var arr = new T[max];
            for (int i = 0; i < Count; i++)
                arr[i] = _items[i];
            _items = arr;
        }

        /// <summary>
        /// Count of elements in Heap
        /// </summary>
        public int Count { get { return _count; } }
        /// <summary>
        /// Capacity * sizeof(T) roughly shows amount of memory used by Heap at the moment.
        /// </summary>
        public int Capacity { get { return _items.Length; } }
        /// <summary>
        /// Returns true if Heap contains no elements and false if it contains at least
        /// one element.
        /// </summary>
        public bool IsEmpty { get { return _count == 0; } }
        /// <summary>
        /// Adds one element to heap.
        /// </summary>
        /// <param name="key">Element (key) to be added.</param>
        public void Insert(T key)
        {
            if (Capacity == _count)
                resize(Capacity * 2);
            _items[_count] = key;
            postProcess?.Invoke(key, -1, _count);
            BubbleUp(_count++);
        }
        /// <summary>
        /// Extract the smallest element of heap and delete it. If there are several 
        /// smallest elements (equals), it returns the arbitrary one.
        /// </summary>
        /// <returns></returns>
        public T ExtractMin()
        {
            T min = _items[0];
            RemoveAt(0);
            return min;
        }

        private void ThrowIfEmpty()
        {
            if (IsEmpty)
                throw new IndexOutOfRangeException("Heap is empty.");
        }

        public T FindMin()
        {
            ThrowIfEmpty();
            return _items[0];
        }

        public void RemoveAt(int index)
        {
            ThrowIfEmpty();
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException($"{index} index is out of bounds.");
            }
            Swap(index, --_count);
            if (_isByRef)
                _items[_count] = default(T);
            if (Count > index)
            {
                BubbleDown(index);
                if (_autoshrink && _count <= Capacity / 4)
                    resize(Capacity / 2);
            }
        }
    }
}
