using System.Runtime.CompilerServices;
using LinqMetaCore;
using LinqMetaCore.Intefaces;
using LinqMetaCore.Utils;

namespace LinqMeta.CollectionWrapper
{
    public struct ArrayWrapper<T> : 
        ICollectionWrapper<T>
    {
        private T[] _arr;

        public T this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _arr[index]; }
        }

        public bool HasIndexOverhead
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return false; }
        }

        public bool HasNext
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return false; }
        }

        public T Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return default(T); }
        }

        public int Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _arr.Length; }
        }

        public ArrayWrapper(T[] arr)
        {
            ErrorUtil.NullCheck(arr, "arr");
            _arr = arr;
        }
    }
}