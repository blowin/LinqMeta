using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.CollectOperator
{
    [StructLayout(LayoutKind.Auto)]
    public struct SkipWhileIndexingOperator<TCollect, TFilter, T> : ICollectionWrapper<T>
        where TCollect : struct, ICollectionWrapper<T>
        where TFilter : struct, IFunctor<ZipPair<T>, bool>
    {
        private TCollect _oldCollect;
        private TFilter _filter;
        private T _item;
        private int _index;
        private bool _checkFilter;
        
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
                    if (_checkFilter)
                    {
                        if (_oldCollect.HasNext)
                        {
                            _item = _oldCollect.Value;
                            return true;
                        }
                    }
                    else
                    {
                        while (_oldCollect.HasNext)
                        {
                            _item = _oldCollect.Value;
                            if (_filter.Invoke(new ZipPair<T>(++_index, _item)) == false)
                            {
                                _checkFilter = true;
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    if (_checkFilter)
                    {
                        var size = _oldCollect.Size;
                        if (++_index < size)
                        {
                            _item = _oldCollect[(uint) _index];
                            return true;   
                        }
                    }
                    else
                    {
                        var size = _oldCollect.Size;
                        while (++_index < size)
                        {
                            _item = _oldCollect[(uint) _index];
                            if (_filter.Invoke(new ZipPair<T>(_index, _item)) == false)
                            {
                                _checkFilter = true;
                                return true;
                            }   
                        }
                    }
                }
                
                _index = -1;
                _checkFilter = false;
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

        public SkipWhileIndexingOperator(TCollect oldCollect, TFilter filter)
        {
            _oldCollect = oldCollect;
            _filter = filter;

            _index = -1;
            _checkFilter = false;
            _item = default(T);
        }
    }
}