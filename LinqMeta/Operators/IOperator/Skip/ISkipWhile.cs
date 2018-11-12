using System;
using LinqMeta.CollectionWrapper;
using LinqMeta.Functors;
using LinqMeta.Operators.CollectOperator;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator.Skip
{
    public interface ISkipWhile<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        OperatorWrapper<SkipWhileOperator<TCollect, TFilter, T>, T> SkipWhile<TFilter>(TFilter filter)
                where TFilter : struct, IFunctor<T, bool>;
        
        OperatorWrapper<SkipWhileOperator<TCollect, FuncFunctor<T, bool>, T>, T> SkipWhile(Func<T, bool> filter);
    }
}