using System.Runtime.CompilerServices;
using LinqMeta.Operators.Number;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Functors.Math
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