using System;
using LinqMeta.Functors;

namespace LinqMeta.Extensions.Operators.IOperator
{
    public interface IMax<T>
    {
        T MaxMeta<TComparer>(TComparer firstGreat)
            where TComparer : struct, IFunctor<T, T, bool>;

        T MaxMeta(Func<T, T, bool> firstGreat);
        
        // For number collection
        T MaxMeta();
    }
}