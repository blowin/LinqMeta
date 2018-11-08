using System;
using System.Runtime.InteropServices;

namespace LinqMetaCore
{
    [StructLayout(LayoutKind.Auto)]
    public struct ZipPair<T> : 
        IEquatable<ZipPair<T>>,
        IComparable<ZipPair<T>>,
        IComparable
    {
        public int Index;
        public T Item;

        public ZipPair(int index, T item)
        {
            Index = index;
            Item = item;
        }

        public override bool Equals(object obj)
        {
            return obj is ZipPair<T> && 
                   Equals((ZipPair<T>)obj);
        }

        public override int GetHashCode()
        {
            return Index | (Item != null ? Item.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return string.Concat(Index, ",", Item);
        }

        bool IEquatable<ZipPair<T>>.Equals(ZipPair<T> other)
        {
            return Equals(other);
        }

        int IComparable<ZipPair<T>>.CompareTo(ZipPair<T> other)
        {
            return CompareTo(other);
        }

        int IComparable.CompareTo(object obj)
        {
            return CompareTo(obj);
        }

        public bool Equals(ZipPair<T> other)
        {
            return Item != null && 
                   Item.Equals(other.Item) &&
                   Index.Equals(other.Index);
        }

        public int CompareTo(ZipPair<T> other)
        {
            int num = Index.CompareTo(other.Index);
            if (num != 0)
                return num;
            
            return ComparatorUtil<T>.Default.Compare(this.Item, other.Item);;
        }

        public int CompareTo(object obj)
        {
            return obj is ZipPair<T> ? 
                CompareTo((ZipPair<T>)obj) :
                1;
        }
    }
}