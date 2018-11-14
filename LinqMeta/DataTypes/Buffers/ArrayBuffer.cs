using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LinqMetaCore.Buffers;
using LinqMetaCore.Intefaces;
using LinqMetaCore.Utils;

namespace LinqMeta.DataTypes.Buffers
{
    [StructLayout(LayoutKind.Auto)]
    internal struct ArrayBuffer<T> : IBuffer<T>
    {
        public const uint DefaultCapacity = 16;
        
        private T[] _buff;
        private uint _size;

        public uint Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _size; }
        }

        public T this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _buff[index]; }
        }
        
        private ArrayBuffer(uint capacity)
        {
            _size = 0;
            _buff = capacity > 0 ? new T[capacity] : CollectUtil.ArrayUtil<T>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T add)
        {
            if (_size == _buff.Length)
                Resize(_size > 0 ? (uint)(_size * 1.5) : DefaultCapacity);

            _buff[_size++] = add;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] ToArray()
        {
            if (_size != _buff.Length && _size > 0)
                ShrinkToFit();
            
            return _buff;
        }

        #region Private methods

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Resize(uint newSize)
        {
            var newArr = new T[newSize];
            Array.Copy(_buff, 0, newArr, 0, (int)newSize);
            _buff = newArr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ShrinkToFit()
        {
            Resize(_size);
        }

        #endregion

        #region Static methods

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArrayBuffer<T> CreateBuff(uint capacity = DefaultCapacity)
        {
            return new ArrayBuffer<T>(capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArrayBuffer<T> CreateBuff<TCollect>(ref TCollect collect, uint capacity = DefaultCapacity)
            where TCollect : struct, ICollectionWrapper<T>
        {
            if (collect.HasIndexOverhead)
            {
                var buff = new ArrayBuffer<T>(capacity);
                while (collect.HasNext)
                    buff.Add(collect.Value);

                return buff;
            }
            else
            {
                var size = collect.Size;
                var buff = new ArrayBuffer<T>((uint) size);
                for (var i = 0u; i < size; ++i)
                    buff.Add(collect[i]);

                return buff;
            }
        }
        
        #endregion
    }
}