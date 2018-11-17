using System;
using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators
{
    public interface IForEach<T>
    {
        void ForEach<TAction>(TAction action)
            where TAction : struct, IFunctor<T, MetaVoid>;
        
        void ForEach(Action<T> action);
    }
}