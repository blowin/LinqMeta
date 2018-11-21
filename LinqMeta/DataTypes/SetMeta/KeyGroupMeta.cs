using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LinqMeta.DataTypes.Buffers;
using LinqMetaCore.Intefaces;

namespace LinqMeta.DataTypes.SetMeta
{
    /// <summary>
    /// Modification LinqSet
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    public struct KeyGroupMeta<TKey, TVal, TCompare>
        where TCompare : IEqualityComparer<TKey>
    {
        private TCompare comparer;
        
        internal Node[] Nodes;
        internal int[] HashCodes;
        internal int[] Nexts;
        private int[] buckets;
        private int count;
        private int freeList;
  
        public KeyGroupMeta(TCompare comparer, int capacity = 7)
        {
            this.comparer = comparer;
            
            Nodes = new Node[capacity];
            HashCodes = new int[capacity];
            Nexts = new int[capacity];
            buckets = new int[capacity];
            
            freeList = -1;
            count = 0;
        }

        internal static KeyGroupMeta<TKey, TVal, TCompare> Create(ref TCompare compare)
        {
            return new KeyGroupMeta<TKey, TVal, TCompare>(compare);
        }
        
        public void Add(TKey key, TVal val, uint? capacity = null)
        {
            var hashCode = CalcHashCode(key);
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
            Nodes[index1].CreateBuffAndAdd(key, val, capacity);
            
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
                var capacity = size > ArrayBuffer<TVal>.DefaultCapacity ? ArrayBuffer<TVal>.DefaultCapacity : (uint)size;
                for (var i = 0u; i < size; ++i)
                {
                    var item = containter[i];
                    Add(selectKey.Invoke(item), item, capacity);
                }
            }
        }
        
        public bool Contains(TKey key)
        {
            var hashCode = CalcHashCode(key);
            for (var index = buckets[hashCode % buckets.Length] - 1; index >= 0; index = Nexts[index])
            {
                if (HashCodes[index] == hashCode && comparer.Equals(Nodes[index].Key, key))
                  return true;
            }
      
            return false;
        }

        internal bool TryGet(TKey key, out ArrayBuffer<TVal> val)
        {
            var hashCode = CalcHashCode(key);
            for (var index = buckets[hashCode % buckets.Length] - 1; index >= 0; index = Nexts[index])
            {
                var node = Nodes[index];
                if (HashCodes[index] == hashCode && comparer.Equals(node.Key, key))
                {
                    val = node.Vals;
                    return true;
                }
            }

            val = default(ArrayBuffer<TVal>);
            return false;
        }
        
        internal ArrayBuffer<TVal> Get(TKey key)
        {
            var hashCode = CalcHashCode(key);
            for (var index = buckets[hashCode % buckets.Length] - 1; index >= 0; index = Nexts[index])
            {
                var node = Nodes[index];
                if (HashCodes[index] == hashCode && comparer.Equals(node.Key, key))
                    return node.Vals;
            }
      
            return default(ArrayBuffer<TVal>);
        }
        
        public bool Remove(TKey key)
        {
            var hashCode = CalcHashCode(key);
            var index1 = hashCode % buckets.Length;
            var index2 = -1;
            for (var index3 = buckets[index1] - 1; index3 >= 0; index3 = Nexts[index3])
            {
                if (HashCodes[index3] != hashCode || !comparer.Equals(Nodes[index3].Key, key))
                {
                    index2 = index3;
                    continue;
                }
                
                if (index2 >= 0)
                {
                    Nexts[index2] = Nexts[index3]; 
                }
                else
                {
                    buckets[index1] = Nexts[index3] + 1;
                }
                    
                HashCodes[index3] = -1;
                Nexts[index3] = freeList;
                freeList = index3;
                return true;
            }
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal int CalcHashCode(TKey key)
        {
            var calcHash = comparer.GetHashCode(key);
            return calcHash >= 0 ? calcHash : -calcHash;
        }
        
        [StructLayout(LayoutKind.Auto)]
        internal struct Node
        {
            internal ArrayBuffer<TVal> Vals;
            internal TKey Key;
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void CreateBuffAndAdd(TKey key, TVal val, uint? capacity)
            {
                Vals = ArrayBuffer<TVal>.CreateBuff(capacity.GetValueOrDefault(ArrayBuffer<TVal>.DefaultCapacity));
                Key = key;
                Vals.Add(val);
            }
        }
  }
}