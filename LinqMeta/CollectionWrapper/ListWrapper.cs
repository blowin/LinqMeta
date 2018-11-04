using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace LinqMeta.CollectionWrapper
{
    public struct ListWrapper<T> : ICollectionWrapper<T>
    {
        private List<T> _list;

        public int Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _list.Count; }
        }

        public T this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _list[(int)index];}
        }
        
        public ListWrapper(List<T> list)
        {
            if(list == null)
                throw new ArgumentNullException();

            _list = list;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> ElementAt(uint index)
        {
            return index < _list.Count ? new Option<T>(_list[(int) index]) : Option<T>.None;
        }
    }
}