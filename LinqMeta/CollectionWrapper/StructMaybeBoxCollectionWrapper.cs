using System.Runtime.CompilerServices;
using LinqMetaCore.Intefaces;

namespace LinqMeta.CollectionWrapper
{
    public struct StructMaybeBoxCollectionWrapper<TCollect, T> : ICollectionWrapper<T>
        where TCollect : ICollectionWrapper<T>
    {
        private TCollect _collect;

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

        public StructMaybeBoxCollectionWrapper(TCollect collect)
        {
            _collect = collect;
        }
    }
}