using System;
using System.Collections.Generic;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator
{
    public interface ISequenceEqual<T>
    {
        bool SequenceEqual<TSecond>(TSecond second)
            where TSecond : struct, ICollectionWrapper<T>;

        bool SequenceEqual<TSecond>(TSecond second, IEqualityComparer<T> comparer)
            where TSecond : struct, ICollectionWrapper<T>;

        bool SequenceEqual<TSecond, TPredicat>(TSecond second, TPredicat predicat)
            where TSecond : struct, ICollectionWrapper<T>
            where TPredicat : struct, IFunctor<T, T, bool>;
        
        bool SequenceEqual<TSecond>(TSecond second, Func<T, T, bool> predicat)
            where TSecond : struct, ICollectionWrapper<T>;
        
        bool SequenceEqualEq<TSecond, T2>(TSecond second)
            where TSecond : struct, ICollectionWrapper<T2>
            where T2 : IEquatable<T>;
        
        bool SequenceEqualEqBox<T2>(ICollectionWrapper<T2> second)
            where T2 : IEquatable<T>;
    }
}