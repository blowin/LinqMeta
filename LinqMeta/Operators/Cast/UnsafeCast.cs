using System.Runtime.CompilerServices;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.Cast
{
    public struct UnsafeCast<TOld, TNew> : IFunctor<TOld, TNew>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TNew Invoke(TOld param)
        {
            return (TNew)(object)param;
        }
    }
}