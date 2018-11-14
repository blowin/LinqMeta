using System.Runtime.CompilerServices;
using LinqMetaCore.Intefaces;
using LinqMetaCore.Utils;

namespace LinqMeta.CollectionWrapper
{
    public struct CollectBoxWrapper<T> : ICollectionWrapper<T>
    {
        private ICollectionWrapper<T> _collect;

        public CollectBoxWrapper(ICollectionWrapper<T> wrapper)
        {
            ErrorUtil.NullCheck(wrapper, "wrapper");
            _collect = wrapper;
        }
        
        public bool HasIndexOverhead
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _collect.HasIndexOverhead; }
        }

        public bool HasNext
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _collect.HasNext; }
        }

        public T Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _collect.Value; }
        }

        public int Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _collect.Size; }
        }

        public T this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _collect[index]; }
        }
    }
}