using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.CollectOperator
{
    [StructLayout(LayoutKind.Auto)]
    public struct TakeOperator<TCollect, T> : ICollectionWrapper<T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        private TCollect _colllect;

        private int _index;
        private uint _takeCount;
        private T _item;
        
        public bool HasIndexOverhead
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _colllect.HasIndexOverhead; }
        }

        public bool HasNext
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if(_colllect.HasNext && ++_index < _takeCount)
                {
                    _item = _colllect.Value;
                    return true;
                }
                
                _index = -1;
                return false;
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
            get { return _colllect[index]; }
        }

        public int Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return (int)Math.Min(_colllect.Size, _takeCount); }
        }

        public TakeOperator(ref TCollect colllect, uint takeCount)
        {
            _colllect = colllect;
            _takeCount = takeCount;
            _index = -1;
            _item = default(T);
        }
    }
}