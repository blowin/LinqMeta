using System;

namespace LinqMetaCore
{
    public struct MetaVoid : 
        IEquatable<MetaVoid>, 
        IComparable<MetaVoid>, 
        IComparable
    {
        public static readonly MetaVoid Def = new MetaVoid();

        [System.Runtime.CompilerServices.MethodImpl(
            System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            return obj is MetaVoid;
        }

        [System.Runtime.CompilerServices.MethodImpl(
            System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return 421 << 2;
        }

        [System.Runtime.CompilerServices.MethodImpl(
            System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public int CompareTo(object obj)
        {
            return obj is MetaVoid ? 0 : -1;
        }

        [System.Runtime.CompilerServices.MethodImpl(
            System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return "Void";
        }

        [System.Runtime.CompilerServices.MethodImpl (System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        bool IEquatable<MetaVoid>.Equals(MetaVoid other)
        {
            return true;
        }

        [System.Runtime.CompilerServices.MethodImpl (System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        int IComparable<MetaVoid>.CompareTo(MetaVoid other)
        {
            return 0;
        }

        #region Equals operators

        [System.Runtime.CompilerServices.MethodImpl (System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(MetaVoid lhs, MetaVoid rhs) 
        {
            return true;
        }

        [System.Runtime.CompilerServices.MethodImpl (System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(MetaVoid lhs, MetaVoid rhs) 
        {
            return true;
        }

        [System.Runtime.CompilerServices.MethodImpl (System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static bool operator >(MetaVoid lhs, MetaVoid rhs)
        {
            return true;
        }

        [System.Runtime.CompilerServices.MethodImpl (System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(MetaVoid lhs, MetaVoid rhs)
        {
            return true;
        }

        [System.Runtime.CompilerServices.MethodImpl(
            System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static bool operator <(MetaVoid lhs, MetaVoid rhs)
        {
            return true;
        }

        [System.Runtime.CompilerServices.MethodImpl(
            System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(MetaVoid lhs, MetaVoid rhs)
        {
            return true;
        }

        #endregion
    }
}