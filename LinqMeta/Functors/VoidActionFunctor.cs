using System;
using System.Runtime.CompilerServices;

namespace LinqMeta.Functors
{
    public struct VoidActionFunctor<T> : IVoidFunctor<T>
    {
        private Action<T> _functor;
        
        public VoidActionFunctor(Action<T> functor)
        {
            if(functor == null)
                throw new ArgumentNullException("functor");

            _functor = functor;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Void Invoke(T param)
        {
            _functor(param);
            return new Void();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator VoidActionFunctor<T>(Action<T> functor)
        {
            return new VoidActionFunctor<T>(functor);
        }
    }
}