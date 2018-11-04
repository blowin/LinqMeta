using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace LinqMeta.CollectionWrapper
{
    public struct ArrayWrapper<T> : ICollectionWrapper<T>
    {
        private T[] _arr;

        public T this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _arr[index];}
        }
        
        public int Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _arr.Length; }
        }

        public ArrayWrapper(T[] arr)
        {
            if(arr == null)
                throw new ArgumentNullException("arr");

            _arr = arr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> ElementAt(uint index)
        {
            return index < _arr.Length ? new Option<T>(_arr[index]) : Option<T>.None;
        }
    }
}