using System.Collections;
using System.Collections.Generic;
using LinqMetaCore.Intefaces;

namespace LinqMeta.CollectionWrapper.EnumeratorWrapper
{
    public struct CollectOverheadEnumeratorWrapper<TCollect, T> : IEnumerator<T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        private TCollect _collect;
        
        public T Current
        {
            get { return _collect.Value; }
        }

        object IEnumerator.Current
        {
            get{ return Current; }
        }

        public CollectOverheadEnumeratorWrapper(TCollect collect)
        {
            _collect = collect;
        }
        
        public bool MoveNext()
        {
            return _collect.HasNext;
        }

        public void Reset()
        {
        }

        public void Dispose()
        {
        }
    }
}