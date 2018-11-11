using System.Runtime.CompilerServices;
using LinqMeta.Operators.Number;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Functors.Math
{
    public struct LessThanOperator<T> : IFunctor<T, T, bool>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Invoke(T a, T b)
        {
            return NumberOperators<T>.LessThan(a, b);
        }
    }
}