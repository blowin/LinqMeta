using System;
using System.Runtime.CompilerServices;
using LinqMeta.Functors;

namespace LinqMeta.Operators.Numbers
{
    public struct GreaterThan<T> : IFunctor<T, T, bool>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Invoke(T a, T b)
        {
            return NumberOperators<T>.GreatThan(a, b);
        }
    }
}