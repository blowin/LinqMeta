using System.Runtime.CompilerServices;
using LinqMeta.DataTypes.Buffers;
using LinqMetaCore.Intefaces;

namespace LinqMeta.DataTypes.Groupin
{
    /// <summary>
    /// Readonly GroupBuffer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct GroupingArray<T> : ICollectionWrapper<T>
    {
        private GroupBuffer<T> _buff;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GroupingArray(ref GroupBuffer<T> buff)
        {
            _buff = buff;
        }

        public T this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _buff[index]; }
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
            get { return (int)_buff.Size; }
        }

    }
}