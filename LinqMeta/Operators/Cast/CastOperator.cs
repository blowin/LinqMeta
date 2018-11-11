using System.Runtime.CompilerServices;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.Cast
{
    public struct CastOperator<TOld, TNew> : IFunctor<TOld, TNew>
        where TNew : TOld
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TNew Invoke(TOld param)
        {
            return (TNew)param;
        }
    }
}