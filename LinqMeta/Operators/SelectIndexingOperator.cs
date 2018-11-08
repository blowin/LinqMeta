using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LinqMeta.CollectionWrapper;
using LinqMetaCore;
using LinqMeta.Functors;

namespace LinqMeta.Operators
{
    [StructLayout(LayoutKind.Auto)]
    public struct SelectIndexingOperator<TOldCollect, TSelector, TOld, TNew> : ICollectionWrapper<TNew>
        where TOldCollect : struct, ICollectionWrapper<TOld>
        where TSelector : struct, IFunctor<ZipPair<TOld>, TNew>
    {
        private TOldCollect _oldCollect;
        private TSelector _selector;

        private int _index;
        private TNew _item;

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
                    if (_oldCollect.HasNext)
                    {
                        _item = _selector.Invoke(new ZipPair<TOld>(++_index, _oldCollect.Value));
                        return true;
                    }
                }
                else
                {
                    if (_oldCollect.Size < ++_index)
                    {
                        _item = _selector.Invoke(new ZipPair<TOld>(_index, _oldCollect[(uint) _index]));
                        return true;
                    }
                }
                
                _index = -1;
                return false;
            }
        }

        public TNew Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _item; }
        }

        public TNew this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _oldCollect.HasIndexOverhead
                    ? default(TNew)
                    : _selector.Invoke(new ZipPair<TOld>((int) index, _oldCollect[index]));
            }
        }

        public int Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _oldCollect.HasIndexOverhead ? 0 : _oldCollect.Size; }
        }

        public SelectIndexingOperator(TOldCollect oldCollect, TSelector selector)
        {
            _oldCollect = oldCollect;
            _selector = selector;

            _index = -1;
            _item = default(TNew);
        }
    }
}