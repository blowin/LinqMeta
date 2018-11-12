using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.CollectOperator
{
    [StructLayout(LayoutKind.Auto)]
    public struct SkipWhileOperator<TCollect, TFilter, T> : ICollectionWrapper<T>
        where TCollect : struct, ICollectionWrapper<T>
        where TFilter : struct, IFunctor<T, bool>
    {
        private TCollect _collection;
        private TFilter _filter;
        private T _item;
        private int _index;
        private bool _find;
        
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
                if (_collection.HasIndexOverhead)
                {
                    if (_find)
                    {
                        if (_collection.HasNext)
                        {
                            _item = _collection.Value;
                            return true;
                        }
                    }
                    else
                    {
                        while (_collection.HasNext)
                        {
                            _item = _collection.Value;
                            if (!_filter.Invoke(_item))
                            {
                                _find = true;
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    if (_find)
                    {
                        if (++_index < _collection.Size)
                        {
                            _item = _collection[(uint) _index];
                            return true;
                        }
                    }
                    else
                    {
                        var size = _collection.Size;
                        while (++_index < size)
                        {
                            _item = _collection[(uint) _index];
                            if (!_filter.Invoke(_item))
                            {
                                _find = true;
                                return true;
                            }
                        }
                    }
                }

                _index = -1;
                _find = false;
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

        public SkipWhileOperator(TCollect collection, TFilter filter)
        {
            _collection = collection;
            _filter = filter;

            _index = -1;
            _find = false;
            _item = default(T);
        }
    }
}