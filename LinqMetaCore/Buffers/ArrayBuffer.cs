using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LinqMetaCore.Utils;

namespace LinqMetaCore.Buffers
{
    [StructLayout(LayoutKind.Auto)]
    public struct ArrayBuffer<T> : IBuffer<T>
    {
        public const uint DefaultCapacity = 16;
        
        private T[] _buff;
        private uint _size;

        public uint Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _size; }
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

            _buff[_size] = add;
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

        #endregion
    }
}