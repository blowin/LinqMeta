using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LinqMeta.CollectionWrapper;
using LinqMetaCore;

namespace LinqMeta.Operators
{
    [StructLayout(LayoutKind.Auto)]
    public struct ZipOperator<TFirstCollection, T, TSecondCollection, T2> : ICollectionWrapper<Pair<T, T2>>
        where TFirstCollection : struct, ICollectionWrapper<T>
        where TSecondCollection : struct, ICollectionWrapper<T2>
    {
        [Flags]
        private enum TypeIterate : byte
        {
            FirstHasOverhead = 1,
            FirstMayIterate = 1 << 1,
            SecondHasOverhead = 1 << 2,
            SecondMayIterate = 1 << 3,
            
            FirstHasOverHeadSecondHasOverhead = FirstHasOverhead | SecondHasOverhead,
            FirsHasOverheadSecondMayIter = FirstHasOverhead | SecondMayIterate,
            FirstMayIterSecondHasOverhead = FirstMayIterate | SecondHasOverhead,
            FirstMayIterSecondMayIter = FirstMayIterate | SecondMayIterate
        }
        
        private TFirstCollection _firstCollection;
        private TSecondCollection _secondCollection;

        private TypeIterate _typeIterate;
        
        private int _indexFirst;
        private int _indexSecond;
        private Pair<T, T2> _item;
        
        public bool HasIndexOverhead
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _typeIterate != TypeIterate.FirstMayIterSecondMayIter; }
        }

        public bool HasNext
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                switch (_typeIterate)
                {
                    case TypeIterate.FirstHasOverHeadSecondHasOverhead:
                        if (_firstCollection.HasNext && _secondCollection.HasNext)
                        {
                            _item = new Pair<T, T2>(_firstCollection.Value, _secondCollection.Value);
                            return true;
                        }

                        return false;
                    case TypeIterate.FirsHasOverheadSecondMayIter:
                        var secondSize = _secondCollection.Size;
                        if (_indexSecond < secondSize && _firstCollection.HasNext)
                        {
                            _item = new Pair<T, T2>(_firstCollection.Value, _secondCollection[(uint) _indexSecond++]);
                            return true;
                        }

                        return false;
                    case TypeIterate.FirstMayIterSecondHasOverhead:
                        var firstSize = _firstCollection.Size;
                        if (_indexFirst < firstSize && _secondCollection.HasNext)
                        {
                            _item = new Pair<T, T2>(_firstCollection[(uint) _indexFirst++], _secondCollection.Value);
                            return true;
                        }

                        return false;
                    case TypeIterate.FirstMayIterSecondMayIter:
                        var firstSz = _firstCollection.Size;
                        var secondSz = _secondCollection.Size;
                        if (_indexFirst < firstSz && _indexSecond < secondSz)
                        {
                            _item = new Pair<T, T2>(_firstCollection[(uint) _indexFirst++], _secondCollection[(uint) _indexSecond++]);
                            return true;
                        }

                        return false;
                }

                _indexFirst = -1;
                _indexSecond = -1;
                return false;
            }
        }

        public Pair<T, T2> Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _item; }
        }

        public Pair<T, T2> this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return HasIndexOverhead ? 
                default(Pair<T, T2>) : 
                new Pair<T, T2>(_firstCollection[index], _secondCollection[index]); }
        }

        public int Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return HasIndexOverhead ? 0 : Math.Min(_firstCollection.Size, _secondCollection.Size); }
        }

        public ZipOperator(TFirstCollection firstCollection, TSecondCollection secondCollection)
        {
            _firstCollection = firstCollection;
            _secondCollection = secondCollection;

            _typeIterate = _firstCollection.HasIndexOverhead
                ? TypeIterate.FirstHasOverhead
                : TypeIterate.FirstMayIterate;

            _typeIterate |= _secondCollection.HasIndexOverhead
                ? TypeIterate.SecondHasOverhead
                : TypeIterate.SecondMayIterate;
            
            _indexFirst = -1;
            _indexSecond = -1;
            _item = default(Pair<T, T2>);
        }
    }
}