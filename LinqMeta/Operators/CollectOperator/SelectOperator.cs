using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.CollectOperator
{
    [StructLayout(LayoutKind.Auto)]
    public struct SelectOperator<TOldCollect, TSelector, TOld, TNew> : ICollectionWrapper<TNew>
        where TOldCollect : struct, ICollectionWrapper<TOld>
        where TSelector : struct , IFunctor<TOld, TNew>
    {
        private TOldCollect _oldCollect;
        private TSelector _selector;

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
                if(_oldCollect.HasNext)
                {
                    _item = _selector.Invoke(_oldCollect.Value);
                    return true;
                }
                else
                {
                    return false;
                }
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
            get { return _selector.Invoke(_oldCollect[index]); }
        }

        public int Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _oldCollect.Size; }
        }

        public SelectOperator(TOldCollect oldCollect, TSelector selector)
        {
            _oldCollect = oldCollect;
            _selector = selector;

            _item = default(TNew);
        }
    }
}