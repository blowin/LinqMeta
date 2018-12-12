using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LinqMeta.DataTypes.Grouping;
using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.CollectOperator
{
    [StructLayout(LayoutKind.Auto)]
    public struct GroupByOperator<TCollect, T, TKeySelector, TResSelector, TComparer, TKey, TRes>
        : ICollectionWrapper<Pair<TKey, GroupingArray<TRes>>>
        
        where TCollect : struct, ICollectionWrapper<T>
        where TKeySelector : struct, IFunctor<T, TKey>
        where TResSelector : struct, IFunctor<T, TRes>
        where TComparer : IEqualityComparer<TKey>
    {
        private KeyGroupMeta<TKey, TRes, TComparer>.KeyGroupingEnumerator _enumerator;
        
        public bool HasIndexOverhead
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return true; }
        }

        public bool HasNext
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _enumerator.MoveNext(); }
        }

        public Pair<TKey, GroupingArray<TRes>> Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _enumerator.Current; }
        }

        public int Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return 0; }
        }

        public Pair<TKey, GroupingArray<TRes>> this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return default(Pair<TKey, GroupingArray<TRes>>); }
        }

        public GroupByOperator(
            ref TCollect collect, 
            ref TKeySelector selector, 
            ref TResSelector resSelector,
            ref TComparer comparer)
        {
            var keyGroupMeta = KeyGroupMeta<TKey, TRes, TComparer>.Create(ref comparer);
            keyGroupMeta.Fill<TCollect, T, TKeySelector, TResSelector>(ref collect, ref selector, ref resSelector);
            _enumerator = keyGroupMeta.GetEnumerator();
        }
    }
}