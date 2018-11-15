using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LinqMeta.DataTypes;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.CollectOperator
{
    [StructLayout(LayoutKind.Auto)]
    public struct ConcatOperator<TFirst, TSecond, T> : ICollectionWrapper<T>
        where TFirst : struct, ICollectionWrapper<T>
        where TSecond : struct, ICollectionWrapper<T>
    {
        private TFirst _first;
        private TSecond _second;
        private StateInfo _stateInfo;
        private T _item;

        public bool HasIndexOverhead
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _stateInfo.HasOverhead; }
        }

        public bool HasNext
        {
            get
            {
                var iterPack = _stateInfo.IteratePack;
                switch (_stateInfo.TypeIterate)
                {
                    case TypeIterate.FirstHasOverHeadSecondHasOverhead:
                        if (iterPack.OverheadTwoCollect.EndFirst == false)
                        {
                            if (_first.HasNext)
                            {
                                _item = _first.Value;
                                return true;
                            }
                            else
                            {
                                if (_second.HasNext)
                                {
                                    _stateInfo.IteratePack.OverheadTwoCollect.EndFirst = true;
                                    _item = _second.Value;
                                    return true;
                                }
                                else
                                {
                                    _stateInfo.IteratePack.OverheadTwoCollect.EndFirst = false;
                                    return false;
                                }
                            }
                        }
                        else if (_second.HasNext)
                        {
                            _item = _second.Value;
                            return true;
                        }
                        else
                        {
                            _stateInfo.IteratePack.OverheadTwoCollect.EndFirst = false;
                            return false;
                        }
                    case TypeIterate.FirsHasOverheadSecondMayIter:
                        if (_stateInfo.IteratePack.OverHeadWithIndex.EndOverheadIter == false)
                        {
                            if (_first.HasNext)
                            {
                                _item = _first.Value;
                                return true;
                            }
                            else
                            {
                                _stateInfo.IteratePack.OverHeadWithIndex.EndOverheadIter = true;
                                var size = _second.Size;
                                if (size > 0)
                                {
                                    _item = _second[(uint) _stateInfo.IteratePack.OverHeadWithIndex.Index++];
                                    return true;
                                }
                                else
                                {
                                    _stateInfo.IteratePack.OverHeadWithIndex.EndOverheadIter = false;
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            var size = _second.Size;
                            if (size > _stateInfo.IteratePack.OverHeadWithIndex.Index)
                            {
                                _item = _second[(uint) _stateInfo.IteratePack.OverHeadWithIndex.Index++];
                                return true;
                            }
                            else
                            {
                                _stateInfo.IteratePack.OverHeadWithIndex.EndOverheadIter = false;
                                _stateInfo.IteratePack.OverHeadWithIndex.Index = 0;
                                return false;
                            }
                        }
                    case TypeIterate.FirstMayIterSecondHasOverhead:
                        var firstSize = _first.Size;
                        if (firstSize > _stateInfo.IteratePack.OverHeadWithIndex.Index)
                        {
                            _item = _first[(uint) _stateInfo.IteratePack.OverHeadWithIndex.Index++];
                            return true;
                        }
                        else if(_second.HasNext)
                        {
                            _item = _second.Value;
                            return true;
                        }
                        else
                        {
                            _stateInfo.IteratePack.OverHeadWithIndex.EndOverheadIter = false;
                            _stateInfo.IteratePack.OverHeadWithIndex.Index = 0;
                            return false;
                        }
                    case TypeIterate.FirstMayIterSecondMayIter:
                    default: return false;
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
            get { return _stateInfo.IteratePack.Size + _second.Size; }
        }

        public T this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                var firstSize = _stateInfo.IteratePack.Size;
                return index < firstSize ? _first[index] : _second[(uint) (index - firstSize)];
            }
        }
        
        public ConcatOperator(ref TFirst first, ref TSecond second)
        {
            _first = first;
            _second = second;
            _item = default(T);
            _stateInfo = StateInfo.Create<TFirst, TSecond, T>(ref first, ref _second);
        }
    }
}