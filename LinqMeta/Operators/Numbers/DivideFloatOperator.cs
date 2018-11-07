using System;
using System.Runtime.CompilerServices;
using LinqMeta.Functors;

namespace LinqMeta.Operators.Numbers
{
    public struct DivideFloatOperator<T> : IFunctor<T, float, float>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Invoke(T a, float b)
        {
            return NumberOperators<T>.DivFloat(a, b);
        }
    }
}