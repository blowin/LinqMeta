using System.Runtime.CompilerServices;
using LinqMeta.DataTypes.Buffers;
using LinqMetaCore.Intefaces;

namespace LinqMeta.DataTypes.Grouping
{
    /// <summary>
    /// Readonly GroupBuffer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct GroupingArray<T> : ICollectionWrapper<T>
    {
        private static GroupBuffer<T> Empty = GroupBuffer<T>.CreateBuff(0);
        
        private GroupBuffer<T> _buff;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GroupingArray(GroupBuffer<T> buff)
        {
            _buff = buff ?? Empty;
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