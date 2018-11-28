using System.Runtime.CompilerServices;
using LinqMetaCore.Intefaces;

namespace LinqMeta.DataTypes.Groupin
{
    public struct GroupingArray<T> : ICollectionWrapper<T>
    {
        private T[] _slice;
        private int _size;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GroupingArray(T[] slice, int size)
        {
            _slice = slice;
            _size = size;
        }

        public T this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _slice[index]; }
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
            get { return _size; }
        }

    }
}