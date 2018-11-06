using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace LinqMeta.CollectionWrapper
{
    public struct ListWrapper<T> 
        : ICollectionWrapper<T>
    {
        private List<T> _list;

        public bool HasIndexOverhead
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return false; }
        }

        public bool HasNext
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return false; }
        }

        public T Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return default(T); }
        }

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
            ErrorUtil.NullCheck(list, "list");
            _list = list;
        }
    }
}