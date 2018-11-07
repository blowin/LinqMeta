using System;
using System.Runtime.CompilerServices;
using LinqMeta.Functors;

namespace LinqMeta.Operators.Numbers
{
    public struct SumOperator<T> : IFunctor<T, T, T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Invoke(T a, T b)
        {
            return NumberOperators<T>.Sum(a, b);
        }
    }
}