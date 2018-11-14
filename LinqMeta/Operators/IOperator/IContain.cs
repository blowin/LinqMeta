using System;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator
{
    public interface IContain<T>
    {
        bool Contains(T val);
        
        bool Contains<TPredicat>(TPredicat predicat) 
            where TPredicat : struct, IFunctor<T, bool>;
        
        bool Contains(Func<T, bool> predicat);

        bool Contains<T2, TPredicat>(TPredicat predicat, T2 val)
            where TPredicat : struct, IFunctor<T, T2, bool>;
        
        bool Contains<T2>(Func<T, T2, bool> predicat, T2 val);
        
        bool ContainsEq<T2>(T2 val) where T2 : IEquatable<T>;
    }
}