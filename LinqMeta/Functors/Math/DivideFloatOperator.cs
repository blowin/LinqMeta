using System.Runtime.CompilerServices;
using LinqMeta.Operators;
using LinqMeta.Operators.Number;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Functors.Math
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