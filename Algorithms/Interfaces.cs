using System;

namespace Algorithms.DataStructures
{
    //
    // Summary:
    //     Supports a simple iteration over a non-generic collection.
    public interface IEnumerator
    {
        //
        // Summary:
        //     Gets the current element in the collection.
        //
        // Returns:
        //     The current element in the collection.
        object Current { get; }

        //
        // Summary:
        //     Advances the enumerator to the next element of the collection.
        //
        // Returns:
        //     true if the enumerator was successfully advanced to the next element; false if
        //     the enumerator has passed the end of the collection.
        //
        // Exceptions:
        //   T:System.InvalidOperationException:
        //     The collection was modified after the enumerator was created.
        bool MoveNext();
        //
        // Summary:
        //     Sets the enumerator to its initial position, which is before the first element
        //     in the collection.
        //
        // Exceptions:
        //   T:System.InvalidOperationException:
        //     The collection was modified after the enumerator was created.
        void Reset();
    }

    //
    // Summary:
    //     Provides a mechanism for releasing unmanaged resources.
    public interface IDisposable
    {
        //
        // Summary:
        //     Performs application-defined tasks associated with freeing, releasing, or resetting
        //     unmanaged resources.
        void Dispose();
    }

    //
    // Summary:
    //     Exposes an enumerator, which supports a simple iteration over a non-generic collection.
    public interface IEnumerable
    {
        //
        // Summary:
        //     Returns an enumerator that iterates through a collection.
        //
        // Returns:
        //     An System.Collections.IEnumerator object that can be used to iterate through
        //     the collection.
        IEnumerator GetEnumerator();
    }


    //
    // Summary:
    //     Supports a simple iteration over a generic collection.
    //
    // Type parameters:
    //   T:
    //     The type of objects to enumerate.This type parameter is covariant. That is, you
    //     can use either the type you specified or any type that is more derived. For more
    //     information about covariance and contravariance, see Covariance and Contravariance
    //     in Generics.
    public interface IEnumerator<out T> : IDisposable, IEnumerator
    {
        //
        // Summary:
        //     Gets the element in the collection at the current position of the enumerator.
        //
        // Returns:
        //     The element in the collection at the current position of the enumerator.
        T Current { get; }
    }

    //
    // Summary:
    //     Exposes the enumerator, which supports a simple iteration over a collection of
    //     a specified type.
    //
    // Type parameters:
    //   T:
    //     The type of objects to enumerate.This type parameter is covariant. That is, you
    //     can use either the type you specified or any type that is more derived. For more
    //     information about covariance and contravariance, see Covariance and Contravariance
    //     in Generics.
    public interface IEnumerable<out T> : IEnumerable
    {
        //
        // Summary:
        //     Returns an enumerator that iterates through the collection.
        //
        // Returns:
        //     A System.Collections.Generic.IEnumerator`1 that can be used to iterate through
        //     the collection.
        IEnumerator<T> GetEnumerator();
    }

    //
    // Summary:
    //     Defines size, enumerators, and synchronization methods for all nongeneric collections.
    public interface ICollection : IEnumerable
    {
        //
        // Summary:
        //     Gets the number of elements contained in the System.Collections.ICollection.
        //
        // Returns:
        //     The number of elements contained in the System.Collections.ICollection.
        int Count { get; }
        //
        // Summary:
        //     Gets a value indicating whether access to the System.Collections.ICollection
        //     is synchronized (thread safe).
        //
        // Returns:
        //     true if access to the System.Collections.ICollection is synchronized (thread
        //     safe); otherwise, false.
        bool IsSynchronized { get; }
        //
        // Summary:
        //     Gets an object that can be used to synchronize access to the System.Collections.ICollection.
        //
        // Returns:
        //     An object that can be used to synchronize access to the System.Collections.ICollection.
        object SyncRoot { get; }

        //
        // Summary:
        //     Copies the elements of the System.Collections.ICollection to an System.Array,
        //     starting at a particular System.Array index.
        //
        // Parameters:
        //   array:
        //     The one-dimensional System.Array that is the destination of the elements copied
        //     from System.Collections.ICollection. The System.Array must have zero-based indexing.
        //
        //   index:
        //     The zero-based index in array at which copying begins.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     array is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than zero.
        //
        //   T:System.ArgumentException:
        //     array is multidimensional.-or- The number of elements in the source System.Collections.ICollection
        //     is greater than the available space from index to the end of the destination
        //     array.-or-The type of the source System.Collections.ICollection cannot be cast
        //     automatically to the type of the destination array.
        void CopyTo(Array array, int index);
    }

    //
    // Summary:
    //     Defines methods to manipulate generic collections.
    //
    // Type parameters:
    //   T:
    //     The type of the elements in the collection.
    public interface ICollection<T> : IEnumerable<T>, IEnumerable
    {
        //
        // Summary:
        //     Gets the number of elements contained in the System.Collections.Generic.ICollection`1.
        //
        // Returns:
        //     The number of elements contained in the System.Collections.Generic.ICollection`1.
        int Count { get; }
        //
        // Summary:
        //     Gets a value indicating whether the System.Collections.Generic.ICollection`1
        //     is read-only.
        //
        // Returns:
        //     true if the System.Collections.Generic.ICollection`1 is read-only; otherwise,
        //     false.
        bool IsReadOnly { get; }

        //
        // Summary:
        //     Adds an item to the System.Collections.Generic.ICollection`1.
        //
        // Parameters:
        //   item:
        //     The object to add to the System.Collections.Generic.ICollection`1.
        //
        // Exceptions:
        //   T:System.NotSupportedException:
        //     The System.Collections.Generic.ICollection`1 is read-only.
        void Add(T item);
        //
        // Summary:
        //     Removes all items from the System.Collections.Generic.ICollection`1.
        //
        // Exceptions:
        //   T:System.NotSupportedException:
        //     The System.Collections.Generic.ICollection`1 is read-only.
        void Clear();
        //
        // Summary:
        //     Determines whether the System.Collections.Generic.ICollection`1 contains a specific
        //     value.
        //
        // Parameters:
        //   item:
        //     The object to locate in the System.Collections.Generic.ICollection`1.
        //
        // Returns:
        //     true if item is found in the System.Collections.Generic.ICollection`1; otherwise,
        //     false.
        bool Contains(T item);
        //
        // Summary:
        //     Copies the elements of the System.Collections.Generic.ICollection`1 to an System.Array,
        //     starting at a particular System.Array index.
        //
        // Parameters:
        //   array:
        //     The one-dimensional System.Array that is the destination of the elements copied
        //     from System.Collections.Generic.ICollection`1. The System.Array must have zero-based
        //     indexing.
        //
        //   arrayIndex:
        //     The zero-based index in array at which copying begins.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     array is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     arrayIndex is less than 0.
        //
        //   T:System.ArgumentException:
        //     The number of elements in the source System.Collections.Generic.ICollection`1
        //     is greater than the available space from arrayIndex to the end of the destination
        //     array.
        void CopyTo(T[] array, int arrayIndex);
        //
        // Summary:
        //     Removes the first occurrence of a specific object from the System.Collections.Generic.ICollection`1.
        //
        // Parameters:
        //   item:
        //     The object to remove from the System.Collections.Generic.ICollection`1.
        //
        // Returns:
        //     true if item was successfully removed from the System.Collections.Generic.ICollection`1;
        //     otherwise, false. This method also returns false if item is not found in the
        //     original System.Collections.Generic.ICollection`1.
        //
        // Exceptions:
        //   T:System.NotSupportedException:
        //     The System.Collections.Generic.ICollection`1 is read-only.
        bool Remove(T item);
    }

    //
    // Summary:
    //     Represents a strongly-typed, read-only collection of elements.
    //
    // Type parameters:
    //   T:
    //     The type of the elements.This type parameter is covariant. That is, you can use
    //     either the type you specified or any type that is more derived. For more information
    //     about covariance and contravariance, see Covariance and Contravariance in Generics.
    public interface IReadOnlyCollection<out T> : IEnumerable<T>, IEnumerable
    {
        //
        // Summary:
        //     Gets the number of elements in the collection.
        //
        // Returns:
        //     The number of elements in the collection.
        int Count { get; }
    }

    public interface IStack<T> : IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
    {
        int Count { get; }
        bool IsEmpty { get; }
        void Push(T item);
        T Pop();
    }
}