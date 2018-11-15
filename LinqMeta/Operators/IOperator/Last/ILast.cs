using System;
using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator.Last
{
    public interface ILast<T>
    {
        Option<T> Last();
        
        Option<T> Last<TFilter>(TFilter filter) where TFilter : struct, IFunctor<T, bool>;
        
        Option<T> Last(Func<T, bool> filter);
    }
}