using System;
using LinqMeta.Functors;
using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator
{
    public interface IMaxMin<T>
    {
        MinMaxPair<T>? MaxMin<TMaxComparer, TMinComparer>(TMaxComparer maxComparer, TMinComparer minComparer)
            where TMaxComparer : struct, IFunctor<T, T, bool>
            where TMinComparer : struct, IFunctor<T, T, bool>;

        MinMaxPair<T>? MaxMin(Func<T, T, bool> maxComparer, Func<T, T, bool> minComparer);
        
        MinMaxPair<T>? MaxMin();
    }
}