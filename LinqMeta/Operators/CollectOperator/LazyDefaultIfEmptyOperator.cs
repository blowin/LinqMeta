using System.Runtime.CompilerServices;
using LinqMeta.Functors;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.CollectOperator
{
    public struct LazyDefaultIfEmptyOperator<TCollect, T, TFactory> : ICollectionWrapper<T>
        where TCollect : struct, ICollectionWrapper<T>
        where TFactory : struct, IFunctor<T>
    {
        private enum State : byte
        {
            IsEmpty,
            NonEmptyOverhead,
            NonEmptyIndex,
            NonCheck
        }
        
        private TCollect _collect;
        private TFactory _factory;
        private T _item;
        private int _index;
        private State _state;

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
                switch (_state)
                {
                    case State.IsEmpty: break;
                    case State.NonEmptyOverhead:
                        if (_collect.HasNext)
                        {
                            _item = _collect.Value;
                            return true;
                        }
                        break;
                    case State.NonEmptyIndex:
                        if (++_index < _collect.Size)
                        {
                            _item = _collect[(uint) _index];
                            return true;
                        }

                        _index = -1;
                        break;
                    case State.NonCheck:
                        if (_collect.HasIndexOverhead)
                        {
                            if (_collect.HasNext)
                            {
                                _state = State.NonEmptyOverhead;
                                _item = _collect.Value;
                                return true;
                            }
                            
                            _state = State.IsEmpty;
                            _item = _factory.Invoke();
                            return true;
                        }
                        else
                        {
                            if (++_index < _collect.Size)
                            {
                                _state = State.NonEmptyIndex;
                                _item = _collect[(uint) _index];
                                return true;
                            }

                            _state = State.IsEmpty;
                            _item = _factory.Invoke();
                            return true;
                        }
                        break;
                }

                return false;
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
            get { return _collect.Size; }
        }

        public T this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _collect[index]; }
        }

        public LazyDefaultIfEmptyOperator(TCollect collect, TFactory factory)
        {
            _collect = collect;
            _factory = factory;
            _item = default(T);
            _state = State.NonCheck;
            _index = -1;
        }
    }
}