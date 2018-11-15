using System;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator.First
{
    public interface IFirstOrDefault<T>
    {
        T FirstOrDefault();

        T FirstOrDefault<TPredicat>(TPredicat predicat)
            where TPredicat : struct, IFunctor<T, bool>;
        
        T FirstOrDefault(Func<T, bool> predicat);
    }
}