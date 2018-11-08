using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LinqMetaCore
{
    [StructLayout(LayoutKind.Auto)]
    public struct ArrayBuffer<T>
    {
        private T[] _buff;
        private uint _size;

        public uint Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _size; }
        }
        
        public ArrayBuffer(uint capacity)
        {
            _size = 0;
            _buff = capacity > 0 ? new T[capacity] : CollectUtil.ArrayUtil<T>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T add)
        {
            if (_size == _buff.Length)
                Resize(_size > 0 ? (uint)(_size * 1.5) : 16);

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
        public static ArrayBuffer<T> CreateBuff(uint capacity = 16)
        {
            return new ArrayBuffer<T>(capacity);
        }

        #endregion
    }
}