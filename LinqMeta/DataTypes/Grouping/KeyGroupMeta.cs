using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LinqMeta.DataTypes.Buffers;
using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace LinqMeta.DataTypes.Grouping
{
    /// <summary>
    /// Modification LinqSet
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    public struct KeyGroupMeta<TKey, TVal, TCompare>
        where TCompare : IEqualityComparer<TKey>
    {
        private TCompare comparer;
        
        private GroupBuffer<TVal>[] Vals;
        private TKey[] Keys;
        private int[] HashCodes;
        private int[] Nexts;
        private int[] buckets;
        
        private int count;
        private int freeList;
  
        public KeyGroupMeta(TCompare comparer, int capacity = 7)
        {
            this.comparer = comparer;
            
            Vals = new GroupBuffer<TVal>[capacity];
            Keys = new TKey[capacity];
            HashCodes = new int[capacity];
            GroupUtility.Memset(HashCodes, 0);
            Nexts = new int[capacity];
            buckets = new int[capacity];
            
            freeList = -1;
            count = 0;
        }

        internal static KeyGroupMeta<TKey, TVal, TCompare> Create(ref TCompare compare)
        {
            return new KeyGroupMeta<TKey, TVal, TCompare>(compare);
        }
        
        public void Add(TKey key, TVal val)
        {
            var hashCode = Hash(key);
            for (var index = buckets[hashCode % buckets.Length] - 1; index >= 0; index = Nexts[index])
            {
                if (HashCodes[index] != hashCode || !comparer.Equals(Keys[index], key))
                  continue;
                
                Vals[index].Add(val);
                return;
            }
            
            int index1;
            if (freeList >= 0)
            {
                index1 = freeList;
                freeList = Nexts[index1];
            }
            else
            {
                if (count == Vals.Length)
                    Resize();
                
                index1 = count;
                ++count;
            }
            
            var index2 = hashCode % buckets.Length;
            HashCodes[index1] = hashCode;

            Keys[index1] = key;
            
            var buff = GroupBuffer<TVal>.CreateBuff(1u);
            Vals[index1] = buff;
            buff.Add(val);
            
            Nexts[index1] = buckets[index2] - 1;
            buckets[index2] = index1 + 1;
            
        }

        internal void Fill<TContainter, TSelectKey>(ref TContainter containter, ref TSelectKey selectKey)
            where TContainter : ICollectionWrapper<TVal>
            where TSelectKey : struct, IFunctor<TVal, TKey>
        {
            if (containter.HasIndexOverhead)
            {
                while (containter.HasNext)
                {
                    var item = containter.Value;
                    Add(selectKey.Invoke(item), item);
                }
            }
            else
            {
                var size = containter.Size;
                for (var i = 0u; i < size; ++i)
                {
                    var item = containter[i];
                    Add(selectKey.Invoke(item), item);
                }
            }
        }
        
        internal void Fill<TContainter, TOld, TSelectKey, TResSelector>(ref TContainter containter, ref TSelectKey selectKey, ref TResSelector resSelector)
            where TContainter : ICollectionWrapper<TOld>
            where TSelectKey : struct, IFunctor<TOld, TKey>
            where TResSelector : struct, IFunctor<TOld, TVal>
        {
            if (containter.HasIndexOverhead)
            {
                while (containter.HasNext)
                {
                    var oldItem = containter.Value;
                    Add(selectKey.Invoke(oldItem), resSelector.Invoke(oldItem));
                }
            }
            else
            {
                var size = containter.Size;
                for (var i = 0u; i < size; ++i)
                {
                    var oldItem = containter[i];
                    Add(selectKey.Invoke(oldItem), resSelector.Invoke(oldItem));
                }
            }
        }

        internal bool TryGet(TKey key, out GroupBuffer<TVal> val)
        {
            var hashCode = Hash(key);
            for (var index = buckets[hashCode % buckets.Length] - 1; index >= 0u; index = Nexts[index])
            {
                if (HashCodes[index] == hashCode && comparer.Equals(Keys[index], key))
                {
                    val = Vals[index];
                    return true;
                }
            }

            val = default(GroupBuffer<TVal>);
            return false;
        }
        
        private void Resize()
        {
            var length = checked (count * 2 + 1);
            var numArray = new int[length];
            
            var newVals = new GroupBuffer<TVal>[length];
            Array.Copy(Vals, 0, newVals, 0, count);
            Vals = newVals;
                
            var newKeys = new TKey[length];
            Array.Copy(Keys, 0, newKeys, 0, count);
            Keys = newKeys;
            
            var newHashLists = new int[length];
            Array.Copy(HashCodes, 0, newHashLists, 0, count);
            HashCodes = newHashLists;
            GroupUtility.Memset(HashCodes, (uint)count);
                
            var newNexts = new int[length];
            Array.Copy(Nexts, 0, newNexts, 0, count);
            Nexts = newNexts;
            
            unsafe
            {
                fixed (int* pointNumArr = numArray, ptrHash = HashCodes, ptrNexts = Nexts)
                {
                    for (var index1 = 0u; index1 < (uint)count; ++index1)
                    {
                        var index2 = ptrHash[index1] % length;
                        ptrNexts[index1] = pointNumArr[index2] - 1;
                        pointNumArr[index2] = (int)(index1 + 1);
                    }
                }
            }
            
            buckets = numArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int Hash(TKey key)
        {
            return comparer.GetHashCode(key) & 0x7FFFFFFF;
        }
        
        internal KeyGroupingEnumerator GetEnumerator()
        {
            return new KeyGroupingEnumerator(Vals, Keys, HashCodes);
        }
        
        internal struct KeyGroupingEnumerator : IEnumerator<Pair<TKey, GroupingArray<TVal>>>
        {
            private GroupBuffer<TVal>[] _vals;
            private TKey[] _keys;
            private int[] _hashCodes;
            private int _index;
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal KeyGroupingEnumerator(GroupBuffer<TVal>[] vals, TKey[] keys, int[] hashCodes)
            {
                _vals = vals;
                _keys = keys;
                _hashCodes = hashCodes;
                _index = -1;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext()
            {
                var size = _vals.Length;
                while (++_index < size)
                {
                    if (_hashCodes[_index] < 0) 
                        continue;
                    
                    return true;
                }
                
                Reset();
                return false;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Reset()
            {
                _index = -1;
            }

            public Pair<TKey, GroupingArray<TVal>> Current
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return new Pair<TKey, GroupingArray<TVal>>(_keys[_index], new GroupingArray<TVal>(_vals[_index]));
                }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public void Dispose()
            {
            }
        }
  }
}