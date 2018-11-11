using System;
using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator
{
    public interface IFirst<T>
    {
        Option<T> First();

        Option<T> First<TPredicat>(TPredicat predicat)
            where TPredicat : struct, IFunctor<T, bool>;
        
        Option<T> First(Func<T, bool> predicat);
    }
}