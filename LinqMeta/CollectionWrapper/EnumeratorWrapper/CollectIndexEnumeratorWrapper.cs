using System.Collections;
using System.Collections.Generic;
using LinqMetaCore.Intefaces;

namespace LinqMeta.CollectionWrapper.EnumeratorWrapper
{
    public struct CollectIndexEnumeratorWrapper<TCollect, T> : IEnumerator<T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        private TCollect _collect;
        private int _index;

        public T Current
        {
            get { return _collect[(uint) _index]; }
        }

        object IEnumerator.Current
        {
            get{ return Current; }
        }
        
        public CollectIndexEnumeratorWrapper(TCollect collect)
        {
            _collect = collect;
            _index = -1;
        }
        
        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            return ++_index < _collect.Size;
        }

        public void Reset()
        {
            _index = -1;
        }
    }
}