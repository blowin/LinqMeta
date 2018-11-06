using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LinqMeta.CollectionWrapper;

namespace LinqMeta.Operators
{
    [StructLayout(LayoutKind.Auto)]
    public struct SkipOperator<TCollect, T> : ICollectionWrapper<T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        private TCollect _oldCollect;

        private uint _index;
        private uint _skipCount;
        private T _item;
        
        public bool HasIndexOverhead
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _oldCollect.HasIndexOverhead; }
        }

        public bool HasNext
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (_oldCollect.HasIndexOverhead)
                {
                    while (_index++ < _skipCount && _oldCollect.HasNext)
                    {
                    }

                    if (_oldCollect.HasNext)
                    {
                        _item = _oldCollect.Value;
                        return true;
                    }
                    else
                    {
                        _index = 0;
                        return false;
                    }
                }
                else
                {
                    var size = _oldCollect.Size;
                    if (_skipCount < size && _index < size)
                    {
                        _item = _oldCollect[_index++];
                        return true;
                    }
                    else
                    {
                        _index = 0;
                        return false;
                    }
                }
            }
        }

        public T Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _item; }
        }

        public T this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get 
            { 
                return _oldCollect.HasIndexOverhead || index >= _oldCollect.Size - _skipCount ? 
                    default(T) : 
                    _oldCollect[index + _skipCount]; 
            }
        }

        public int Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return (int) (_oldCollect.HasIndexOverhead ? 0 : Math.Min(_oldCollect.Size, _skipCount)); }
        }

        public SkipOperator(TCollect oldCollect, uint skipCount)
        {
            _oldCollect = oldCollect;
            _skipCount = skipCount;
            _index = _oldCollect.HasIndexOverhead ? 0 : _skipCount;
            _item = default(T);
        }
    }
}