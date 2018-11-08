using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace LinqMetaCore
{
    public struct Option<T> : 
        IEquatable<T>, 
        IEquatable<Option<T>>,
        IComparable<Option<T>>,
        IComparable
    {
        private T _val;
        private bool _hasValue;

        public bool HasValue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _hasValue; }
        }
        
        public Option(T val)
        {
            _val = val;
            _hasValue = _val != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValueOr(T val)
        {
            return _hasValue ? _val : val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValueOrDefault()
        {
            return _hasValue ? _val : default(T);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(T other)
        {
            return _hasValue && other.Equals(_val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo(object obj)
        {
            return !(obj is Option<T>) ? 1 : CompareTo((Option<T>)obj);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo(Option<T> opt)
        {
            if (_hasValue)
                return opt._hasValue ? Comparer<T>.Default.Compare(_val, opt._val) : 1;
            
            return opt._hasValue ? -1 : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Option<T> other)
        {
            return _hasValue && other._hasValue && _val.Equals(other._val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            return obj is Option<T> && 
                   base.Equals((Option<T>)obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return _hasValue ? _val.GetHashCode() : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return _hasValue ? string.Concat("Option(", _val.ToString(), ")") : "None";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        bool IEquatable<T>.Equals(T other)
        {
            return _hasValue && other.Equals(_val);
        }
        
        public static Option<T> None
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get{ return new Option<T>(); }
        }
    }
}