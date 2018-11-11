using System;
using LinqMeta.Functors;
using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator
{
    public interface IAll<T>
    {
        bool All<TFilter>(TFilter filter)
            where TFilter : struct, IFunctor<T, bool>;
        
        bool All(Func<T, bool> filter);
    }
}