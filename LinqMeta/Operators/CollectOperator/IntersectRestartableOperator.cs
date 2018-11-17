using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.CollectOperator
{
    [StructLayout(LayoutKind.Auto)]
    public struct IntersectRestartableOperator<TCollect, TSecondCollect, T> : ICollectionWrapper<T>
        where TCollect : struct, ICollectionWrapper<T>
        where TSecondCollect : struct, ICollectionWrapper<T>
    {
        private HashSet<T> _intersectCollect;
        private TCollect _collect;
        private TSecondCollect _secondCollect;
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
                if (_secondCollect.HasIndexOverhead)
                {
                    while (_secondCollect.HasNext)
                    {
                        _item = _secondCollect.Value;
                        if (_intersectCollect.Remove(_item))
                            return true;
                    }
                }
                else
                {
                    var size = _secondCollect.Size;
                    while (++_index < size)
                    {
                        _item = _secondCollect[(uint) _index];
                        if (_intersectCollect.Remove(_item))
                            return true;
                    }

                    _index = -1;
                }
                
                FillIntersectCollect();
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

        public IntersectRestartableOperator(ref TCollect collect, ref TSecondCollect secondCollect, IEqualityComparer<T> comparer)
        {
            _collect = collect;
            _secondCollect = secondCollect;
            _item = default(T);
            _index = -1;
            _intersectCollect = new HashSet<T>(comparer);
            FillIntersectCollect();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void FillIntersectCollect()
        {
            try
            {
                if (_collect.HasIndexOverhead)
                {
                    while (_collect.HasNext)
                        _intersectCollect.Add(_collect.Value);
                }
                else
                {
                    var size = _collect.Size;
                    for (var i = 0u; i < size; ++i)
                        _intersectCollect.Add(_collect[i]);
                }
            }
            catch (Exception e)
            {
                // Can throw Exception. then collect is range. for example
                // ignored
            }
        }
    }
}