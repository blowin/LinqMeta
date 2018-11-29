using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LinqMeta.DataTypes.Buffers;
using LinqMeta.DataTypes.SetMeta;
using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.CollectOperator
{
    [StructLayout(LayoutKind.Auto)]
    public struct JoinOperator<TCollect, T, TSecond, T2, TSelector, TSelector2, TResSelector, TComparer, TKey, TRes> 
        : ICollectionWrapper<TRes>
        
        where TCollect : struct, ICollectionWrapper<T>
        where TSecond : ICollectionWrapper<T2>
        where TSelector : struct, IFunctor<T, TKey>
        where TSelector2 : struct, IFunctor<T2, TKey>
        where TResSelector : struct, IFunctor<Pair<T, T2>, TRes>
        where TComparer : IEqualityComparer<TKey>
    {
        private KeyGroupMeta<TKey, T2, TComparer> _keyGroupMeta;
        private TResSelector _resSelector;
        private TCollect _collect;
        private TSelector _selector;

        private GroupBuffer<T2> _curBuff;
        private T _itemForCombine;
        private TRes _item;
        private int _buffIndex;
        private int _indexVal;

        private bool _nowGetFromBuff;
        
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
                getFromBuff:
                if (_nowGetFromBuff)
                {
                    var size = _curBuff.Size;
                    if (++_buffIndex < size)
                    {
                        var combineItem = _curBuff[(uint) _buffIndex];
                        _item = _resSelector.Invoke(new Pair<T, T2>(_itemForCombine, combineItem));
                        return true;
                    }

                    _buffIndex = -1;
                    _nowGetFromBuff = false;
                }
                
                if (_collect.HasIndexOverhead)
                {
                    while (_collect.HasNext)
                    {
                        _itemForCombine = _collect.Value;
                        if (_keyGroupMeta.TryGet(_selector.Invoke(_itemForCombine), out _curBuff))
                        {
                            _nowGetFromBuff = true;
                            goto getFromBuff;
                        }
                    }
                }
                else
                {
                    var size = _collect.Size;
                    while (++_indexVal < size)
                    {
                        _itemForCombine = _collect[(uint) _indexVal];
                        if (_keyGroupMeta.TryGet(_selector.Invoke(_itemForCombine), out _curBuff))
                        {
                            _nowGetFromBuff = true;
                            goto getFromBuff;
                        }
                    }
                }

                _buffIndex = -1;
                _indexVal = -1;
                _nowGetFromBuff = false;
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

        public JoinOperator(
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

            _buffIndex = -1;
            _indexVal = -1;

            _item = default(TRes);
            _nowGetFromBuff = false;
            _curBuff = default(GroupBuffer<T2>);
            _itemForCombine = default(T);
        }
    }
}