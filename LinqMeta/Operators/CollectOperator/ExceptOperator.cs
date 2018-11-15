using System.Collections.Generic;
using System.Runtime.InteropServices;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.CollectOperator
{
    [StructLayout(LayoutKind.Auto)]
    public struct ExceptOperator<TCollect, TSecondCollect, T> : ICollectionWrapper<T>
        where TCollect : struct, ICollectionWrapper<T>
        where TSecondCollect : struct, ICollectionWrapper<T>
    {
        private HashSet<T> _exceptCollect;
        private TCollect _collect;
        private T _item;
        private int _index;
        
        public bool HasIndexOverhead
        {
            get { return true; }
        }

        public bool HasNext
        {
            get
            {
                if (_collect.HasIndexOverhead)
                {
                    while (_collect.HasNext)
                    {
                        _item = _collect.Value;
                        if (_exceptCollect.Add(_item))
                            return true;
                    }
                }
                else
                {
                    var size = _collect.Size;
                    while (++_index < size)
                    {
                        _item = _collect[(uint) _index];
                        if (_exceptCollect.Add(_item))
                            return true;
                    }

                    _index = -1;
                }
                
                return false;
            }
        }
        
        public T Value
        {
            get { return _item; }
        }
        
        public int Size
        {
            get { return 0; }
        }

        public T this[uint index]
        {
            get { return default(T); }
        }

        public ExceptOperator(ref TCollect collect, ref TSecondCollect secondCollect, IEqualityComparer<T> comparer)
        {
            _collect = collect;
            _item = default(T);
            _index = -1;
            _exceptCollect = new HashSet<T>(comparer);

            if (secondCollect.HasIndexOverhead)
            {
                while (secondCollect.HasNext)
                    _exceptCollect.Add(secondCollect.Value);
            }
            else
            {
                var size = secondCollect.Size;
                for (var i = 0u; i < size; ++i)
                    _exceptCollect.Add(secondCollect[i]);
            }
        }
    }
}