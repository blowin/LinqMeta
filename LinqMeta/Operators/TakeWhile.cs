using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LinqMeta.CollectionWrapper;
using LinqMeta.Functors;

namespace LinqMeta.Operators
{
    [StructLayout(LayoutKind.Auto)]
    public struct TakeWhile<TCollect, TFilter, T> : ICollectionWrapper<T>
        where TCollect : struct, ICollectionWrapper<T>
        where TFilter : struct, IFunctor<T, bool>
    {
        private TCollect _oldCollect;
        private TFilter _filter;

        private int _index;
        private T _item;
        
        public bool HasIndexOverhead
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return true; }
        }

        public bool HasNext
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (_oldCollect.HasIndexOverhead)
                {
                    if (_oldCollect.HasNext)
                    {
                        _item = _oldCollect.Value;
                        if (_filter.Invoke(_item))
                            return true;
                    }
                }
                else
                {
                    var size = _oldCollect.Size;
                    if (_index < size)
                    {
                        _item = _oldCollect[(uint) _index];
                        if (_filter.Invoke(_item))
                        {
                            ++_index;
                            return true;
                        }
                        
                        _index = 0;   
                    }
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
            get { return default(T); }
        }

        public int Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return 0; }
        }

        public TakeWhile(TCollect oldCollect, TFilter filter)
        {
            _oldCollect = oldCollect;
            _filter = filter;

            _index = 0;
            _item = default(T);
        }
    }
}