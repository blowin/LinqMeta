using System;

namespace LinqMetaCore
{
    public struct MetaVoid : 
        IEquatable<MetaVoid>, 
        IComparable<MetaVoid>, 
        IComparable
    {
        public static readonly MetaVoid Def = new MetaVoid();

        [System.Runtime.CompilerServices.MethodImpl (System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is MetaVoid;

        [System.Runtime.CompilerServices.MethodImpl (System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => 421 << 2;

        [System.Runtime.CompilerServices.MethodImpl (System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public int CompareTo(object obj) => obj is MetaVoid ? 0 : -1;

        [System.Runtime.CompilerServices.MethodImpl (System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override string ToString() => "Void";

        [System.Runtime.CompilerServices.MethodImpl (System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        bool IEquatable<MetaVoid>.Equals(MetaVoid other) => true;

        [System.Runtime.CompilerServices.MethodImpl (System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        int IComparable<MetaVoid>.CompareTo(MetaVoid other) => 0;

        #region Equals operators

        [System.Runtime.CompilerServices.MethodImpl (System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(MetaVoid lhs, MetaVoid rhs) => true;

        [System.Runtime.CompilerServices.MethodImpl (System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(MetaVoid lhs, MetaVoid rhs) => true;

        [System.Runtime.CompilerServices.MethodImpl (System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static bool operator >(MetaVoid lhs, MetaVoid rhs) => true;

        [System.Runtime.CompilerServices.MethodImpl (System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(MetaVoid lhs, MetaVoid rhs) => true;
        
        [System.Runtime.CompilerServices.MethodImpl (System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static bool operator <(MetaVoid lhs, MetaVoid rhs) => true;

        [System.Runtime.CompilerServices.MethodImpl (System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(MetaVoid lhs, MetaVoid rhs) => true;

        #endregion
    }
}