using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LinqMeta.DataTypes.Buffers;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Extensions.Operators.CollectWrapperOperators
{
    [StructLayout(LayoutKind.Auto)]
    public struct ReverseOperator<TCollect, T> : ICollectionWrapper<T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        private TCollect _collect;
        private ArrayBuffer<T> _buffer;
        private int _lastIndexItem;

        public bool HasIndexOverhead
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _collect.HasIndexOverhead; }
        }

        public bool HasNext
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get{ return --_lastIndexItem >= 0; }
        }

        public T Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _buffer[(uint) _lastIndexItem]; }
        }

        public int Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _lastIndexItem + 1; }
        }

        public T this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _collect[(uint) (_lastIndexItem - index)]; }
        }

        public ReverseOperator(TCollect collect)
        {
            _collect = collect;
            
            if (collect.HasIndexOverhead)
            {
                _buffer = ArrayBuffer<T>.CreateBuff(ref collect);
                _lastIndexItem = (int) _buffer.Size;
            }
            else
            {
                _buffer = default(ArrayBuffer<T>);
                _lastIndexItem = _collect.Size - 1;
            }
        }
    }
}