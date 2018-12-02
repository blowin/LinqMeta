using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LinqMeta.DataTypes.Buffers;
using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace LinqMeta.DataTypes.Groupin
{
    /// <summary>
    /// Modification LinqSet
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    public struct KeyGroupMeta<TKey, TVal, TCompare>
        where TCompare : IEqualityComparer<TKey>
    {
        private TCompare comparer;
        
        private Node[] Nodes;
        private int[] HashCodes;
        private int[] Nexts;
        private int[] buckets;
        
        private int count;
        private int freeList;
  
        public KeyGroupMeta(TCompare comparer, int capacity = 7)
        {
            this.comparer = comparer;
            
            Nodes = new Node[capacity];
            HashCodes = new int[capacity];
            Memset(HashCodes, 0);
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
            var hashCode = comparer.GetHashCode(key);
            for (var index = buckets[hashCode % buckets.Length] - 1; index >= 0; index = Nexts[index])
            {
                if (HashCodes[index] != hashCode || !comparer.Equals(Nodes[index].Key, key))
                  continue;
                
                Nodes[index].Vals.Add(val);
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
                if (count == Nodes.Length)
                    Resize();
                
                index1 = count;
                ++count;
            }
            
            var index2 = hashCode % buckets.Length;
            HashCodes[index1] = hashCode;
            Nodes[index1].CreateBuffAndAdd(key, val);
            
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
            var hashCode = comparer.GetHashCode(key);
            for (var index = buckets[hashCode % buckets.Length] - 1; index >= 0; index = Nexts[index])
            {
                var node = Nodes[index];
                if (HashCodes[index] == hashCode && comparer.Equals(node.Key, key))
                {
                    val = node.Vals;
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
            
            var newElements = new Node[length];
            Array.Copy(Nodes, 0, newElements, 0, count);
            Nodes = newElements;
                
            var newHashLists = new int[length];
            Array.Copy(HashCodes, 0, newHashLists, 0, count);
            HashCodes = newHashLists;
            Memset(HashCodes, (uint)count);
                
            var newNexts = new int[length];
            Array.Copy(Nexts, 0, newNexts, 0, count);
            Nexts = newNexts;
            
            unsafe
            {
                fixed (int* pointNumArr = numArray, ptrHash = HashCodes, ptrNexts = Nexts)
                {
                    for (var index1 = 0; index1 < count; ++index1)
                    {
                        var index2 = ptrHash[index1] % length;
                        ptrNexts[index1] = pointNumArr[index2] - 1;
                        pointNumArr[index2] = index1 + 1;
                    }
                }
            }
            
            buckets = numArray;
        }
        
        [StructLayout(LayoutKind.Auto)]
        internal struct Node
        {
            internal GroupBuffer<TVal> Vals;
            internal TKey Key;
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void CreateBuffAndAdd(TKey key, TVal val)
            {
                Vals = new GroupBuffer<TVal>(1u);
                Key = key;
                Vals.Add(val);
            }
        }

        internal static void Memset(int[] arr, uint startIndex)
        {
            var block = 32u;
            var len = Math.Min(arr.Length, block);
            var index = startIndex;
            unsafe
            {
                fixed (int* arrPtr = arr)
                {
                    while (index < len)
                    {
                        arrPtr[index] = -1;
                        ++index;
                    }
                }
            }

            len = arr.Length;
            while (index < len)
            {
                Array.Copy(arr, 0, arr, (int)index, (int)Math.Min(block, len - index));
                index += block;
                block *= 2;
            }
        }
        
        internal KeyGroupingEnumerator GetEnumerator()
        {
            return new KeyGroupingEnumerator(Nodes, HashCodes);
        }
        
        internal struct KeyGroupingEnumerator : IEnumerator<Pair<TKey, GroupingArray<TVal>>>
        {
            private Node[] _nodes;
            private int[] _hashCodes;
            private int _index;
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal KeyGroupingEnumerator(Node[] nodes, int[] hashCodes)
            {
                _nodes = nodes;
                _hashCodes = hashCodes;
                _index = -1;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext()
            {
                var size = _nodes.Length;
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
                    var node = _nodes[_index];
                    return new Pair<TKey, GroupingArray<TVal>>(node.Key, new GroupingArray<TVal>(ref node.Vals));
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