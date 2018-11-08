using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LinqMeta.CollectionWrapper;
using LinqMeta.Functors;
using LinqMetaCore;

namespace LinqMeta.Operators
{
    [StructLayout(LayoutKind.Auto)]
    public struct ZipSelectOperator<TCollect, T, T2, TSelector> : ICollectionWrapper<T2>
        where TCollect : struct, ICollectionWrapper<Pair<T, T2>>
        where TSelector : struct, IFunctor<Pair<T, T2>, T2>
    {
        private TCollect _collect;
        private TSelector _selector;
        
        public bool HasIndexOverhead
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _collect.HasIndexOverhead; }
        }

        public bool HasNext
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get{ return _collect.HasNext; }
        }

        public T2 Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _selector.Invoke(_collect.Value); }
        }

        public T2 this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return HasIndexOverhead ? 
                default(T2) : 
                _selector.Invoke(_collect[index]); }
        }

        public int Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _collect.HasIndexOverhead ? 0 : _collect.Size; }
        }

        public ZipSelectOperator(TCollect collect, TSelector selector)
        {
            _collect = collect;
            _selector = selector;
        }
    }
}