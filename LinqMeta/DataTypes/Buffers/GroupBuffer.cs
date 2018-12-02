using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LinqMetaCore.Intefaces;

namespace LinqMeta.DataTypes.Buffers
{
    [StructLayout(LayoutKind.Auto)]
    public struct GroupBuffer<T> : IReadonlyBuffer<T>
    {
        internal const int ItemCount = 8;
        
        public const uint DefaultCapacity = 8;
        
        private T[] _buff;
        private T _it0;
        private T _it1;
        private T _it2;
        private T _it3;
        private T _it4;
        private T _it5;
        private T _it6;
        private T _it7;
        private uint _size;

        public uint Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _size; }
        }

        public T this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                switch (index)
                {
                    case 0:     return _it0;
                    case 1:     return _it1;
                    case 2:     return _it2;
                    case 3:     return _it3;
                    case 4:     return _it4;
                    case 5:     return _it5;
                    case 6:     return _it6;
                    case 7:     return _it7;
                    default:    return _buff[index - ItemCount];
                }
            }
        }
        
        internal GroupBuffer(uint capacity)
        {
            _size = 0;
            _buff = capacity > ItemCount ? new T[capacity] : null;
            _it0 = default(T);
            _it1 = default(T);
            _it2 = default(T);
            _it3 = default(T);
            _it4 = default(T);
            _it5 = default(T);
            _it6 = default(T);
            _it7 = default(T);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T add)
        {
            if (_size > ItemCount)
            {
                var arrSize = _buff.Length;
                if ((_size - ItemCount) == arrSize)
                    Resize((uint)(arrSize * 2), arrSize);

                _buff[_size - ItemCount] = add;
            }
            else if (_size == ItemCount)
            {
                _buff = new T[DefaultCapacity];
                _buff[0] = add;
            }
            else
            {
                switch (_size)
                {
                    case 0: 
                        _it0 = add;
                        break;
                    case 1: 
                        _it1 = add;
                        break;
                    case 2: 
                        _it2 = add;
                        break;
                    case 3: 
                        _it3 = add;
                        break;
                    case 4: 
                        _it4 = add;
                        break;
                    case 5: 
                        _it5 = add;
                        break;
                    case 6: 
                        _it6 = add;
                        break;
                    case 7: 
                        _it7 = add;
                        break;
                }

            }
            _size += 1;
        }

        #region Private methods

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Resize(uint newSize, int arrSize)
        {
            var newArr = new T[newSize];
            Array.Copy(_buff, 0, newArr, 0, arrSize);
            _buff = newArr;
        }

        #endregion

        #region Static methods

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GroupBuffer<T> CreateBuff(uint capacity = DefaultCapacity)
        {
            return new GroupBuffer<T>(capacity);
        }
        
        #endregion
    }
}