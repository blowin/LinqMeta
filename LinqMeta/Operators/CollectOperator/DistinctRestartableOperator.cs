using System.Collections.Generic;
using System.Runtime.InteropServices;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.CollectOperator
{
    [StructLayout(LayoutKind.Auto)]
    public struct DistinctRestartableOperator<TCollect, T> : ICollectionWrapper<T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        private HashSet<T> _distinctCollect;
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
                        if (_distinctCollect.Add(_item))
                            return true;
                    }
                }
                else
                {
                    var size = _collect.Size;
                    while (++_index < size)
                    {
                        _item = _collect[(uint) _index];
                        if (_distinctCollect.Add(_item))
                            return true;
                    }

                    _index = -1;
                }
                
                _distinctCollect.Clear();
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

        public DistinctRestartableOperator(ref TCollect collect, IEqualityComparer<T> comparer)
        {
            _collect = collect;
            _item = default(T);
            _index = -1;
            _distinctCollect = new HashSet<T>(comparer);
        }
    }
}