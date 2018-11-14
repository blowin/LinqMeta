using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LinqMetaCore.Intefaces;
using LinqMetaCore.Utils;

namespace LinqMeta.CollectionWrapper
{
    public struct EnumeratorWrapper<T> : ICollectionWrapper<T>
    {
        private IEnumerator<T> _enumerator;

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

                try
                {
                    _enumerator.Reset();
                }
                catch (Exception)
                {
                    // ignored
                }

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
            get { return default(T); }
        }
        
        public EnumeratorWrapper(IEnumerator<T> enumerator)
        {
            ErrorUtil.NullCheck(enumerator, "enumerator");
            _enumerator = enumerator;
        }
    }
}