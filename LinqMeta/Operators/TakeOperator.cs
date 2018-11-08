using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LinqMeta.CollectionWrapper;

namespace LinqMeta.Operators
{
    [StructLayout(LayoutKind.Auto)]
    public struct TakeOperator<TCollect, T> : ICollectionWrapper<T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        private TCollect _oldCollect;

        private int _index;
        private uint _takeCount;
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
                    if(_oldCollect.HasNext && ++_index < _takeCount)
                    {
                        _item = _oldCollect.Value;
                        return true;
                    }
                }
                else
                {
                    if (_oldCollect.Size < _index && ++_index < _takeCount)
                    {
                        _item = _oldCollect[(uint) _index];
                        return true;
                    }
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
            get 
            { 
                return _oldCollect.HasIndexOverhead ? 
                    default(T) : 
                    _oldCollect[index]; 
            }
        }

        public int Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return (int) (_oldCollect.HasIndexOverhead ? 0 : Math.Min(_oldCollect.Size, _takeCount)); }
        }

        public TakeOperator(TCollect oldCollect, uint takeCount)
        {
            _oldCollect = oldCollect;
            _takeCount = takeCount;
            _index = -1;
            _item = default(T);
        }
    }
}