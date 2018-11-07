using System;
using System.Runtime.CompilerServices;
using LinqMeta.Functors;

namespace LinqMeta.Operators.Numbers
{
    public struct DivideOperator<T> : IFunctor<T, double, double>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Invoke(T a, double b)
        {
            return NumberOperators<T>.DivDouble(a, b);
        }
    }
}