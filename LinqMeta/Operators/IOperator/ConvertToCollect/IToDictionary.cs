using System;
using System.Collections.Generic;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator.ConvertToCollect
{
    public interface IToDictionary<T>
    {
        Dictionary<TKey, T> ToDictionary<TKey>(Func<T, TKey> keySelector, uint? capacity);
        Dictionary<TKey, T> ToDictionary<TKey>(Func<T, TKey> keySelector, IEqualityComparer<TKey> comparer, uint? capacity);
        Dictionary<TKey, TElem> ToDictionary<TKey, TElem>(Func<T, TKey> keySelector, Func<T, TElem> elementSelector, uint? capacity);
        Dictionary<TKey, TElem> ToDictionary<TKey, TElem>(Func<T, TKey> keySelector, Func<T, TElem> elementSelector, IEqualityComparer<TKey> comparer, uint? capacity);
        
        
        Dictionary<TKey, T> ToDictionary<TSelect, TKey>(TSelect keySelector, uint? capacity) where TSelect : struct, IFunctor<T, TKey>;
        
        Dictionary<TKey, T> ToDictionary<TKeySelector, TKey>(TKeySelector keySelector, IEqualityComparer<TKey> comparer, uint? capacity)
            where TKeySelector : struct, IFunctor<T, TKey>;
        
        Dictionary<TKey, TElem> ToDictionary<TKeySelector, TSelect, TKey, TElem>(TKeySelector keySelector, TSelect elementSelector, uint? capacity) 
            where TSelect : struct, IFunctor<T, TElem>
            where TKeySelector : struct, IFunctor<T, TKey>;
        
        Dictionary<TKey, TElem> ToDictionary<TKeySelector, TElemSelector, TKey, TElem>(TKeySelector keySelector, TElemSelector elementSelector, IEqualityComparer<TKey> comparer, uint? capacity)
           where TKeySelector : struct, IFunctor<T, TKey>
           where TElemSelector : struct, IFunctor<T, TElem>;
    }
}