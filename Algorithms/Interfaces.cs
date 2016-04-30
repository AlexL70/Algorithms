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


}