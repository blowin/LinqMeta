using System;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator.MaxMin
{
    public interface IMin<T>
    {
        T Min<TComparer>(TComparer firstGreat)
            where TComparer : struct, IFunctor<T, T, bool>;

        T Min(Func<T, T, bool> firstGreat);
        
        // For number collection
        T Min();
    }
}