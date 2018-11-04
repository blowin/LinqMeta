using System;
using System.Runtime.CompilerServices;

namespace LinqMeta.Functors
{
    public struct PredicatFuncFunctor<T> : IPredicat<T>
    {
        private Func<T, bool> _predicat;
        
        public PredicatFuncFunctor(Func<T, bool> predicat)
        {
            if(predicat == null)
                throw new ArgumentNullException("predicat");

            _predicat = predicat;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Invoke(T param)
        {
            return _predicat(param);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator PredicatFuncFunctor<T>(Func<T, bool> predicat)
        {
            return new PredicatFuncFunctor<T>(predicat);
        }
    }
}