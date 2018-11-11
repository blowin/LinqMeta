using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LinqMetaCore.Intefaces;

namespace LinqMeta.CollectionWrapper
{
    public struct ConcreteEnumeratorWrapper<TEnumerator, T> : ICollectionWrapper<T>
        where TEnumerator : struct, IEnumerator<T>
    {
        private TEnumerator _enumerator;

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
                if (_enumerator.MoveNext())
                    return true;
                
                _enumerator.Reset();
                return false;
            }
        }

        public T Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _enumerator.Current; }
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

        public ConcreteEnumeratorWrapper(TEnumerator enumerator)
        {
            _enumerator = enumerator;
        }
    }
}