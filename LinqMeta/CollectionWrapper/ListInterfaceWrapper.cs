using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LinqMetaCore.Intefaces;
using LinqMetaCore.Utils;

namespace LinqMeta.CollectionWrapper
{
    public struct ListInterfaceWrapper<T> : ICollectionWrapper<T>
    {
        private IList<T> _collection;

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
            get { return _collection.Count; }
        }

        public T this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _collection[(int) index]; }
        }
        
        public ListInterfaceWrapper(IList<T> list)
        {
            ErrorUtil.NullCheck(list, "list");
            _collection = list;
        }
    }
}