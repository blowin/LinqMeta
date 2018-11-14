using System;
using System.Runtime.CompilerServices;
using LinqMetaCore;
using LinqMetaCore.Intefaces;
using LinqMetaCore.Utils;

namespace LinqMeta.Functors
{
    public struct FuncFunctor<T, T2, TRes> : IFunctor<T, T2, TRes>
    {
        private Func<T, T2, TRes> _functor;

        public FuncFunctor(Func<T, T2, TRes> functor)
        {
            ErrorUtil.NullCheck(functor, "functor");
            _functor = functor;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TRes Invoke(T param, T2 param2)
        {
            return _functor.Invoke(param, param2);
        }
    }
    
    public struct FuncFunctor<T, TRes> : IFunctor<T, TRes>
    {
        private Func<T, TRes> _functor;

        public FuncFunctor(Func<T, TRes> functor)
        {
            ErrorUtil.NullCheck(functor, "functor");
            _functor = functor;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TRes Invoke(T param)
        {
            return _functor.Invoke(param);
        }
    }
    
    public struct FuncFunctor<TRes> : IFunctor<TRes>
    {
        private Func<TRes> _functor;

        public FuncFunctor(Func<TRes> functor)
        {
            ErrorUtil.NullCheck(functor, "functor");
            _functor = functor;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TRes Invoke()
        {
            return _functor.Invoke();
        }
    }
}