using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LinqMeta.DataTypes.SetMeta
{
    /// <summary>
    /// Modification LinqSet
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    public struct SetMeta<TElement, TCompare>
        where TCompare : IEqualityComparer<TElement>
    {
        private SlotBuffer buff;
        private TCompare comparer;
        private int[] buckets;
        private int count;
        private int freeList;
  
        public SetMeta(TCompare comparer, int capacity = 7)
        {
            this.comparer = comparer;
            buff = new SlotBuffer(capacity);
            buckets = new int[capacity];
            freeList = -1;
            count = 0;
        }
        
        public bool Add(TElement value)
        {
            var hashCode = comparer.GetHashCode(value);
            for (var index = buckets[hashCode % buckets.Length] - 1; index >= 0; index = buff.Nexts[index])
            {
                if (buff.HashCodes[index] != hashCode || !comparer.Equals(buff.Elements[index], value))
                  continue;
                
                return false;
            }
            
            int index1;
            if (freeList >= 0)
            {
                index1 = freeList;
                freeList = buff.Nexts[index1];
            }
            else
            {
                if (count == buff.Elements.Length)
                    Resize();
                
                index1 = count;
                ++count;
            }
            
            var index2 = hashCode % buckets.Length;
            buff.HashCodes[index1] = hashCode;
            buff.Elements[index1] = value;
            buff.Nexts[index1] = buckets[index2] - 1;
            buckets[index2] = index1 + 1;
            return true;
        }
        
        public bool Contains(TElement value)
        {
            var hashCode = comparer.GetHashCode(value);
            for (var index = buckets[hashCode % buckets.Length] - 1; index >= 0; index = buff.Nexts[index])
            {
                if (buff.HashCodes[index] == hashCode && comparer.Equals(buff.Elements[index], value))
                  return true;
            }
      
            return false;
        }
        
        public bool Remove(TElement value)
        {
            var hashCode = comparer.GetHashCode(value);
            var index1 = hashCode % buckets.Length;
            var index2 = -1;
            for (var index3 = buckets[index1] - 1; index3 >= 0; index3 = buff.Nexts[index3])
            {
                if (buff.HashCodes[index3] != hashCode || !comparer.Equals(buff.Elements[index3], value))
                {
                    index2 = index3;
                    continue;
                }
                
                if (index2 >= 0)
                {
                    buff.Nexts[index2] = buff.Nexts[index3]; 
                }
                else
                {
                    buckets[index1] = buff.Nexts[index3] + 1;
                }
                    
                buff.HashCodes[index3] = -1;
                buff.Nexts[index3] = freeList;
                freeList = index3;
                return true;
            }
            return false;
        }
        
        private void Resize()
        {
            var length = checked (count * 2 + 1);
            var numArray = new int[length];
            buff.Resize(count, length);
            unsafe
            {
                fixed (int* pointNumArr = numArray, ptrHash = buff.HashCodes, ptrNexts = buff.Nexts)
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
        private struct SlotBuffer
        {
            internal TElement[] Elements;
            internal int[] HashCodes;
            internal int[] Nexts;
  
            public SlotBuffer(int size)
            {
                Elements = new TElement[size];
                HashCodes = new int[size];
                Nexts = new int[size];
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Resize(int oldLen, int newLen)
            {
                var newElements = new TElement[newLen];
                Array.Copy(Elements, 0, newElements, 0, oldLen);
                Elements = newElements;
                
                var newHashLists = new int[newLen];
                Array.Copy(HashCodes, 0, newHashLists, 0, oldLen);
                HashCodes = newHashLists;
                
                var newNexts = new int[newLen];
                Array.Copy(Nexts, 0, newNexts, 0, oldLen);
                Nexts = newNexts;
            }
      }
  }
}