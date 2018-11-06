using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LinqMeta.CollectionWrapper;
using LinqMeta.Core;
using LinqMeta.Functors;

namespace LinqMeta.Operators
{
    [StructLayout(LayoutKind.Auto)]
    public struct WhereIndexingOperator<TCollect, TFilter, T> : ICollectionWrapper<T>
        where TCollect : struct, ICollectionWrapper<T>
        where TFilter : struct, IFunctor<ZipPair<T>, bool>
    {
        private TCollect _collect;
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
                if (_collect.HasIndexOverhead)
                {
                    while (_collect.HasNext)
                    {
                        _item = _collect.Value;
                        if (_filter.Invoke(new ZipPair<T>(_index, _item)))
                        {
                            ++_index;
                            return true;
                        }
                    }

                    _index = 0;
                    return false;
                }
                else
                {
                    for (; _index < _collect.Size; ++_index)
                    {
                        _item = _collect[(uint) _index];
                        if (_filter.Invoke(new ZipPair<T>(_index, _item)))
                        {
                            ++_index;
                            return true;
                        }
                    }

                    _index = 0;
                    return false;
                }
            }
        }

        public T Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _item; }
        }

        public int Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return 0; }
        }

        public T this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return default(T); }
        }

        public WhereIndexingOperator(TCollect collect, TFilter filter)
        {
            _collect = collect;
            _filter = filter;

            _index = 0;
            _item = default(T);
        }
    }
}