using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.CollectOperator
{
    [StructLayout(LayoutKind.Auto)]
    public struct SelectManyOperator<TFirst, T, TSecond, T2, TSelector> : ICollectionWrapper<T2>
        where TFirst : struct, ICollectionWrapper<T>
        where TSecond : ICollectionWrapper<T2>
        where TSelector : struct, IFunctor<T, TSecond>
    {
        private TFirst _first;
        private TSecond _second;
        
        private TSelector _selector;
        
        private int _index;
        private int _indexSecond;
        
        private T2 _item;

        public bool HasIndexOverhead
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return true; }
        }

        public bool HasNext
        {
            get
            {
                if (_first.HasIndexOverhead)
                {
                    if (_indexSecond != -1)
                        return HasOverheadNext();
                    
                    if (_first.HasNext)
                    {
                        _second = _selector.Invoke(_first.Value);
                        return HasOverheadNext();
                    }
                }
                else
                {
                    if (_indexSecond != -1)
                        return HasIndexNext();
                    
                    if (++_index < _first.Size)
                    {
                        _second = _selector.Invoke(_first[(uint) _index]);
                        return HasIndexNext();
                    }

                    _index = -1;
                }

                return false;
            }
        }

        public T2 Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _item; }
        }

        public int Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return 0; }
        }

        public T2 this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return default(T2); }
        }

        private bool HasIndexNext()
        {
            while (true)
            {
                if (_second.HasIndexOverhead)
                {
                    if (_second.HasNext)
                    {
                        _indexSecond += 1;
                        _item = _second.Value;
                        return true;
                    }
                    
                    _indexSecond = -1;
                    if (++_index < _first.Size)
                    {
                        _second = _selector.Invoke(_first[(uint) _index]);
                        continue;
                    }
                }
                else
                {
                    if (++_indexSecond < _second.Size)
                    {
                        _item = _second[(uint) _indexSecond];
                        return true;
                    }
                    
                    _indexSecond = -1;
                    if (++_index < _first.Size)
                    {
                        _second = _selector.Invoke(_first[(uint) _index]);
                        continue;
                    }
                }

                _index = -1;
                return false;
            }
        }

        private bool HasOverheadNext()
        {
            while (true)
            {
                if (_second.HasIndexOverhead)
                {
                    if (_second.HasNext)
                    {
                        _item = _second.Value;
                        return true;
                    }
                    
                    _indexSecond = -1;
                    if (_first.HasNext)
                    {
                        _second = _selector.Invoke(_first.Value);
                        continue;
                    }
                }
                else
                {
                    if (++_indexSecond < _second.Size)
                    {
                        _item = _second[(uint) _indexSecond];
                        return true;
                    }
                    
                    _indexSecond = -1;
                    if (_first.HasNext)
                    {
                        _second = _selector.Invoke(_first.Value);
                        continue;
                    }
                }

                return false;
            }
        }

        public SelectManyOperator(ref TFirst first, ref TSelector selector)
        {
            _first = first;
            _second = default(TSecond);
            
            _selector = selector;
            
            _index = -1;
            _indexSecond = -1;
            
            _item = default(T2);
        }
    }
}