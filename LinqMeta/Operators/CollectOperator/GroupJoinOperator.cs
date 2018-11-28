using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LinqMeta.CollectionWrapper;
using LinqMeta.DataTypes.Buffers;
using LinqMeta.DataTypes.Groupin;
using LinqMeta.DataTypes.SetMeta;
using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.CollectOperator
{
    public struct GroupJoinOperator<TCollect, T, TSecond, T2, TSelector, TSelector2, TResSelector, TComparer, TKey, TRes>
        : ICollectionWrapper<TRes>
        
        where TCollect : struct, ICollectionWrapper<T>
        where TSecond : ICollectionWrapper<T2>
        where TSelector : struct, IFunctor<T, TKey>
        where TSelector2 : struct, IFunctor<T2, TKey>
        where TResSelector : struct, IFunctor<Pair<T, GroupingArray<T2>>, TRes>
        where TComparer : IEqualityComparer<TKey>
    {
        private KeyGroupMeta<TKey, T2, TComparer> _keyGroupMeta;
        private TResSelector _resSelector;
        private TCollect _collect;
        private TSelector _selector;

        private TRes _item;
        private int _indexVal;
        
        public bool HasIndexOverhead
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return true; }
        }

        public bool HasNext
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (_collect.HasIndexOverhead)
                {
                    while (_collect.HasNext)
                    {
                        var collectValue = _collect.Value;
                        ArrayBuffer<T2> buff;
                        if (_keyGroupMeta.TryGet(_selector.Invoke(collectValue), out buff))
                        {
                            _item = _resSelector.Invoke(
                                new Pair<T, GroupingArray<T2>>(
                                    collectValue,
                                    new GroupingArray<T2>(buff.RawArray(), (int) buff.Size))
                            );
                            return true;
                        }
                    }
                }
                else
                {
                    var size = _collect.Size;
                    while (++_indexVal < size)
                    {
                        var collectValue = _collect[(uint) _indexVal];
                        ArrayBuffer<T2> buff;
                        if (_keyGroupMeta.TryGet(_selector.Invoke(collectValue), out buff))
                        {
                            _item = _resSelector.Invoke(
                                new Pair<T, GroupingArray<T2>>(
                                    collectValue,
                                    new GroupingArray<T2>(buff.RawArray(), (int) buff.Size))
                            );
                            return true;
                        }
                    }
                }

                _indexVal = -1;
                return false;
            }
        }

        public TRes Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _item; }
        }

        public int Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return 0; }
        }

        public TRes this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return default(TRes); }
        }

        public GroupJoinOperator(
            ref TCollect collect, 
            ref TSecond second, 
            ref TSelector selector, 
            ref TSelector2 selector2,
            ref TResSelector resSelector,
            ref TComparer comparer)
        {
            _collect = collect;
            _selector = selector;
            _resSelector = resSelector;
            
            _keyGroupMeta = KeyGroupMeta<TKey, T2, TComparer>.Create(ref comparer);
            _keyGroupMeta.Fill(ref second, ref selector2);

            _indexVal = -1;

            _item = default(TRes);
        }
    }
}