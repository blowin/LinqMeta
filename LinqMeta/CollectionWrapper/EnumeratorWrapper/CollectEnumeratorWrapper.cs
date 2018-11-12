using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LinqMetaCore.Intefaces;

namespace LinqMeta.CollectionWrapper.EnumeratorWrapper
{
    public struct CollectEnumeratorWrapper<TCollect, T> : IEnumerator<T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        private TCollect _collect;
        private T _item;
        private int _index;
        private bool _endIter;

        public CollectEnumeratorWrapper(TCollect collect)
        {
            _collect = collect;
            _item = default(T);
            _index = -1;
            _endIter = false;
        }
        
        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _item; }
        }

        object IEnumerator.Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return Current; }
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (_collect.HasIndexOverhead)
            {
                if (_endIter == false)
                {
                    if (_collect.HasNext)
                    {
                        _item = _collect.Value;
                        return true;
                    }
                    else
                    {
                        _endIter = true;
                    }
                }
            }
            else
            {
                if (_endIter == false)
                {
                    var size = _collect.Size;
                    if (++_index < size)
                    {
                        _item = _collect[(uint) _index];
                        return true;
                    }
                    else
                    {
                        _endIter = true;
                    }
                }
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            _index = -1;
            _endIter = false;
            _item = default(T);
        }
    }
}