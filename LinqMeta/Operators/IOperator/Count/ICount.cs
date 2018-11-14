using System;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator.Count
{
    public interface ICount<T>
    {
        int Count();
        
        int Count<TPredicat>(TPredicat predicat) where TPredicat : struct, IFunctor<T, bool>;
        
        int Count(Func<T, bool> predicat);
    }
}