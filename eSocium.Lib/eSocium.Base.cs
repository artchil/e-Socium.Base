using System;
using System.Collections;
using System.Collections.Generic;

namespace eSocium
{
    // Interface members are always public
    // Interface can't have fields, but may have properties
    // IEquatable(Of T) should be implemented for any type that might be stored in a generic collection.
    // If you implement IEquatable(Of T), you should also override the base class implementations of Object.Equals(Object) and GetHashCode so that their behavior is consistent with that of the IEquatable(Of T).Equals method.
    // The following guidelines are for implementing a value type: 
    // Consider overriding Equals to gain increased performance over that provided by the default implementation of Equals on ValueType. 
    // If you override Equals and the language supports operator overloading, you must overload the equality operator for your value type. 
    // The following guidelines are for implementing a reference type: 
    // Consider overriding Equals on a reference type if the semantics of the type are based on the fact that the type represents some value(s). 
    // Most reference types must not overload the equality operator, even if they override Equals. However, if you are implementing a reference type that is intended to have value semantics, such as a complex number type, you must override the equality operator. 
    // Interfaces cannot contain operators

    /// <summary>
    /// Represents a pair of elements of different types.
    /// </summary>
    /// <typeparam name="T1">type of the first element</typeparam>
    /// <typeparam name="T2">type of the second element</typeparam>
    //public struct Pair<T1, T2> : IEquatable<Pair<T1, T2>>
    //{
    //    public T1 First;
    //    public T2 Second;

    //    public Pair(T1 t1, T2 t2)
    //    {
    //        First = t1;
    //        Second = t2;
    //    }

    //    public override int GetHashCode()
    //    {
    //        return First.GetHashCode() ^ Second.GetHashCode();
    //    }

    //    public override bool Equals(object obj)
    //    {
    //        if (!(obj is Pair<T1, T2>))
    //            return false;
    //        Pair<T1, T2> rhs = (Pair<T1, T2>)obj;
    //        return First.Equals(rhs.First) && Second.Equals(rhs.Second);
    //    }

    //    public bool Equals(Pair<T1, T2> other)
    //    {
    //        return First.Equals(other.First) && Second.Equals(other.Second);
    //    }

    //    public static bool operator == (Pair<T1, T2> lhs, Pair<T1, T2> rhs)
    //    {
    //        return lhs.Equals(rhs);
    //    }

    //    public static bool operator !=(Pair<T1, T2> lhs, Pair<T1, T2> rhs)
    //    {
    //        return !lhs.Equals(rhs);
    //    }

    //    public override string ToString()
    //    {
    //        return First.ToString()+","+Second.ToString();
    //    }
    //}

    //// may be used for List.Sort and alike
    //public class PairLexicographicalComparer<T1,T2> : Comparer<Pair<T1,T2>>
    //    where T1 : IComparable<T1>
    //    where T2 : IComparable<T2>
    //{
    //    public override int Compare(Pair<T1, T2> x, Pair<T1, T2> y)
    //    {
    //        int result = x.First.CompareTo(y.First);
    //        if (result == 0)
    //            return x.Second.CompareTo(y.Second);
    //        return result;
    //    }
    //}

} // namespace eSocium
