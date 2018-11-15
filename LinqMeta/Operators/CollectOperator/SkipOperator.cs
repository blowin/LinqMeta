using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.CollectOperator
{
    [StructLayout(LayoutKind.Auto)]
    public struct SkipOperator<TCollect, T> : ICollectionWrapper<T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        private TCollect _oldCollect;

        private int _index;
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
                while (++_index < _skipCount && _oldCollect.HasNext)
                {
                }

                if (_oldCollect.HasNext)
                {
                    _item = _oldCollect.Value;
                    return true;
                }
                
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
            get { return _oldCollect[index + _skipCount]; }
        }

        public int Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                var collectSize = _oldCollect.Size;
                if (collectSize > _skipCount)
                    return (int) (collectSize - _skipCount);
                return 0;
            }
        }

        public SkipOperator(ref TCollect oldCollect, uint skipCount)
        {
            _oldCollect = oldCollect;
            _skipCount = skipCount;
            _index = _oldCollect.HasIndexOverhead ? -1 : (int) _skipCount;
            _item = default(T);
        }
    }
}