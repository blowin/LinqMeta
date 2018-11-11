using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LinqMeta.DataTypes;
using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.CollectOperator
{
    [StructLayout(LayoutKind.Auto)]
    public struct ZipOperator<TFirstCollection, T, TSecondCollection, T2> : ICollectionWrapper<Pair<T, T2>>
        where TFirstCollection : struct, ICollectionWrapper<T>
        where TSecondCollection : struct, ICollectionWrapper<T2>
    {
        private TFirstCollection _firstCollection;
        private TSecondCollection _secondCollection;
        private Pair<T, T2> _item;
        private StateInfo _stateInfo;
        
        public bool HasIndexOverhead
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _stateInfo.HasOverhead; }
        }

        public bool HasNext
        {
            get
            {
                switch (_stateInfo.TypeIterate)
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
                        if (_stateInfo.IteratePack.OverHeadWithIndex.Index < secondSize && 
                            _firstCollection.HasNext)
                        {
                            _item = new Pair<T, T2>(
                                _firstCollection.Value, 
                                _secondCollection[(uint) _stateInfo.IteratePack.OverHeadWithIndex.Index++]
                            );
                            return true;
                        }

                        _stateInfo.IteratePack.OverHeadWithIndex.Index = 0;
                        return false;
                    case TypeIterate.FirstMayIterSecondHasOverhead:
                        var firstSize = _firstCollection.Size;
                        if (_stateInfo.IteratePack.OverHeadWithIndex.Index < firstSize && _secondCollection.HasNext)
                        {
                            _item = new Pair<T, T2>(
                                _firstCollection[(uint) _stateInfo.IteratePack.OverHeadWithIndex.Index++], 
                                _secondCollection.Value
                            );
                            return true;
                        }

                        _stateInfo.IteratePack.OverHeadWithIndex.Index = 0;
                        return false;
                    case TypeIterate.FirstMayIterSecondMayIter:
                    default: return false;
                }
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
            get { return new Pair<T, T2>(_firstCollection[index], _secondCollection[index]); }
        }

        public int Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _stateInfo.IteratePack.Size; }
        }

        public ZipOperator(TFirstCollection firstCollection, TSecondCollection secondCollection)
        {
            _firstCollection = firstCollection;
            _secondCollection = secondCollection;
            _stateInfo = StateInfo.Create<TFirstCollection, TSecondCollection, T, T2>(ref firstCollection, ref _secondCollection);
            _stateInfo.IteratePack.Size = Math.Min(_stateInfo.IteratePack.Size, _secondCollection.Size);
            _item = default(Pair<T, T2>);
        }
    }
}