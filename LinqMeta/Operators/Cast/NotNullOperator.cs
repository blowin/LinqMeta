using System.Runtime.CompilerServices;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.Cast
{
    public struct NotNullOperator<T> : IFunctor<T, bool>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Invoke(T param)
        {
            return param != null;
        }
    }
}