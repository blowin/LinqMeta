using System;
using LinqMeta.Functors;
using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator
{
    public interface IAggregate<T>
    {
        TRes Aggregate<TFolder, TRes>(TRes init, TFolder folder)
            where TFolder : struct, IFunctor<TRes, T, TRes>;

        T Aggregate<TFolder>(TFolder folder)
            where TFolder : struct, IFunctor<T, T, T>;

        TRes Aggregate<TRes>(TRes init, Func<TRes, T, TRes> folder);

        T Aggregate(Func<T, T, T> folder);
    }
}