using System;

namespace LinqMeta.DataTypes.Grouping
{
    internal static class GroupUtility
    {
        internal static void Memset(int[] arr, uint startIndex, int val = -1)
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
                        arrPtr[index] = val;
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
    }
}