using System;
using LinqMeta.Functors;
using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator
{
    public interface IAny<T>
    {
        bool Any();

        bool Any<TFilter>(TFilter filter)
            where TFilter : struct, IFunctor<T, bool>;
        
        bool Any(Func<T, bool> filter);
    }
}