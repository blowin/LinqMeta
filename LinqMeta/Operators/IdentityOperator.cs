using System.Runtime.CompilerServices;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators
{
    public struct IdentityOperator<T> : IFunctor<T, T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Invoke(T param)
        {
            return param;
        }
    }
}