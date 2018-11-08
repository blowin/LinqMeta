using System;
using System.Runtime.InteropServices;

namespace LinqMetaCore
{
    [StructLayout(LayoutKind.Auto)]
    public struct Pair<T, T2> : 
        IEquatable<Pair<T, T2>>,
        IComparable<Pair<T, T2>>,
        IComparable
    {
        public T First;
        public T2 Second;

        public Pair(T first, T2 second)
        {
            First = first;
            Second = second;
        }

        public override bool Equals(object obj)
        {
            return obj is Pair<T, T2> && 
                   Equals((Pair<T, T2>)obj);
        }

        public override int GetHashCode()
        {
            var firstCode = First != null ? First.GetHashCode() : 0;
            var secondCode = Second != null ? Second.GetHashCode() : 0;
            
            return firstCode | secondCode;
        }

        public override string ToString()
        {
            return string.Concat(First, ",", Second);
        }

        bool IEquatable<Pair<T, T2>>.Equals(Pair<T, T2> other)
        {
            return Equals(other);
        }

        int IComparable<Pair<T, T2>>.CompareTo(Pair<T, T2> other)
        {
            return CompareTo(other);
        }

        int IComparable.CompareTo(object obj)
        {
            return CompareTo(obj);
        }

        public bool Equals(Pair<T, T2> other)
        {
            return First != null && 
                   First.Equals(other.First) &&
                   Second != null &&
                   Second.Equals(other.Second);
        }

        public int CompareTo(Pair<T, T2> other)
        {
            int num = ComparatorUtil<T>.Default.Compare(First, other.First);
            if (num != 0)
                return num;
            
            return ComparatorUtil<T2>.Default.Compare(this.Second, other.Second);
        }

        public int CompareTo(object obj)
        {
            return obj is Pair<T, T2> ? 
                CompareTo((Pair<T, T2>)obj) :
                1;
        }
    }
}