using System.Runtime.CompilerServices;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.CollectOperator
{
    public struct TypeOfOperator<TOld, TNew> : IFunctor<TOld, TNew> 
        where TNew : class
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TNew Invoke(TOld param)
        {
            return param as TNew;
        }
    }
}