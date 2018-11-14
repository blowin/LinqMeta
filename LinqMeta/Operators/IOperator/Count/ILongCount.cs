using System;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator.Count
{
    public interface ILongCount<T>
    {
        long LongCount();
        
        long LongCount<TPredicat>(TPredicat predicat) where TPredicat : struct, IFunctor<T, bool>;
        
        long LongCount(Func<T, bool> predicat);
    }
}