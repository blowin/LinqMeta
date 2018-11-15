using System.Collections.Generic;
using System.Runtime.InteropServices;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.CollectOperator
{
    [StructLayout(LayoutKind.Auto)]
    public struct UnionOperator<TCollect, TSecondCollect, T> : ICollectionWrapper<T>
        where TCollect : struct, ICollectionWrapper<T>
        where TSecondCollect : struct, ICollectionWrapper<T>
    {
        private HashSet<T> _unionCollect;
        private TCollect _collect;
        private TSecondCollect _secondCollect;
        private T _item;
        private int _index;
        private bool _endFirstIterate;
        
        public bool HasIndexOverhead
        {
            get { return true; }
        }

        public bool HasNext
        {
            get
            {
                if (_endFirstIterate == false)
                {
                    if (_collect.HasIndexOverhead)
                    {
                        while (_collect.HasNext)
                        {
                            _item = _collect.Value;
                            if (_unionCollect.Add(_item))
                                return true;
                        }
                    }
                    else
                    {
                        var size = _collect.Size;
                        while (++_index < size)
                        {
                            _item = _collect[(uint) _index];
                            if (_unionCollect.Add(_item))
                                return true;
                        }

                        _index = -1;
                    }

                    _endFirstIterate = true;
                }
                
                if (_secondCollect.HasIndexOverhead)
                {
                    while (_secondCollect.HasNext)
                    {
                        _item = _secondCollect.Value;
                        if (_unionCollect.Add(_item))
                            return true;
                    }
                }
                else
                {
                    var size = _secondCollect.Size;
                    while (++_index < size)
                    {
                        _item = _secondCollect[(uint) _index];
                        if (_unionCollect.Add(_item))
                            return true;
                    }

                    _index = -1;
                }

                _endFirstIterate = false;
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

        public UnionOperator(ref TCollect collect, ref TSecondCollect secondCollect, IEqualityComparer<T> comparer)
        {
            _collect = collect;
            _secondCollect = secondCollect;
            _item = default(T);
            _index = -1;
            _endFirstIterate = false;
            _unionCollect = new HashSet<T>(comparer);
        }
    }
}