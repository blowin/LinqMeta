using System;
using LinqMeta.Functors;

namespace LinqMeta.Extensions.Operators.IOperator
{
    public interface IAggregate<T>
    {
        TRes AggregateMeta<TFolder, TRes>(TRes init, TFolder folder)
            where TFolder : struct, IFunctor<TRes, T, TRes>;

        T AggregateMeta<TFolder>(TFolder folder)
            where TFolder : struct, IFunctor<T, T, T>;

        TRes AggregateMeta<TRes>(TRes init, Func<TRes, T, TRes> folder);

        T AggregateMeta(Func<T, T, T> folder);
    }
}