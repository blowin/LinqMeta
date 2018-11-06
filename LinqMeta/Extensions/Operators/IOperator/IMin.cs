using System;
using LinqMeta.Functors;

namespace LinqMeta.Extensions.Operators.IOperator
{
    public interface IMin<T>
    {
        T MinMeta<TComparer>(TComparer firstGreat)
            where TComparer : struct, IFunctor<T, T, bool>;

        T MinMeta(Func<T, T, bool> firstGreat);
        
        // For number collection
        T MinMeta();
    }
}