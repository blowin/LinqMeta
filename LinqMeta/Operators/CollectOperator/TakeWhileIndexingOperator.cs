using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.CollectOperator
{
    [StructLayout(LayoutKind.Auto)]
    public struct TakeWhileIndexingOperator<TCollect, TFilter, T> : ICollectionWrapper<T>
        where TCollect : struct, ICollectionWrapper<T>
        where TFilter : struct, IFunctor<ZipPair<T>, bool>
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
                        if (_filter.Invoke(new ZipPair<T>(++_index, _item)))
                            return true;
                    }
                }
                else
                {
                    var size = _oldCollect.Size;
                    if (++_index < size)
                    {
                        _item = _oldCollect[(uint) _index];
                        if (_filter.Invoke(new ZipPair<T>(_index, _item)))
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
            get { return default(T); }
        }

        public int Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return 0; }
        }

        public TakeWhileIndexingOperator(ref TCollect oldCollect, ref TFilter filter)
        {
            _oldCollect = oldCollect;
            _filter = filter;

            _index = -1;
            _item = default(T);
        }
    }
}