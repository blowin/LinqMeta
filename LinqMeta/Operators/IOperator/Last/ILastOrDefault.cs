using System;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator.Last
{
    public interface ILastOrDefault<T>
    {
        T LastOrDefault();
        
        T LastOrDefault<TFilter>(TFilter filter) where TFilter : struct, IFunctor<T, bool>;
        
        T LastOrDefault(Func<T, bool> filter);
    }
}