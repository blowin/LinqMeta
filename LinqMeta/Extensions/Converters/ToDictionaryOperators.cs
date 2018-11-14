using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Extensions.Converters
{
    public static class ToDictionaryOperators
    {
        internal struct ThisValueSelector<T> : IFunctor<T, T>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public T Invoke(T param)
            {
                return param;
            }
        }
        
        public static Dictionary<TKey, T> ToDictionaryMeta<TCollect, T, TKey, TKeySelector>(ref TCollect collect, TKeySelector keySelector,
            IEqualityComparer<TKey> comparer, uint? capacity)
            where TCollect : struct, ICollectionWrapper<T>
            where TKeySelector : struct, IFunctor<T, TKey>
        {
            return ToDictionaryMeta<TCollect, T, TKey, T, TKeySelector, ThisValueSelector<T>>(ref collect, keySelector,
                default(ThisValueSelector<T>), comparer, capacity);
        }

        public static Dictionary<TKey, T> ToDictionaryMeta<TCollect, T, TKey, TKeySelector>(ref TCollect collect, TKeySelector keySelector,
            uint? capacity)
            where TCollect : struct, ICollectionWrapper<T>
            where TKeySelector : struct, IFunctor<T, TKey>
        {
            return ToDictionaryMeta<TCollect, T, TKey, T, TKeySelector, ThisValueSelector<T>>(ref collect, keySelector,
                default(ThisValueSelector<T>), null, capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<TKey, TElem> ToDictionaryMeta<TCollect, T, TKey, TElem, TKeySelector, TSelect>(ref TCollect collect, TKeySelector keySelector, TSelect elementSelector,
            uint? capacity) 
            where TCollect : struct, ICollectionWrapper<T>
            where TKeySelector : struct, IFunctor<T, TKey> 
            where TSelect : struct, IFunctor<T, TElem>
        {
            return ToDictionaryMeta<TCollect, T, TKey, TElem, TKeySelector, TSelect>(ref collect, keySelector,
                elementSelector, null, capacity);
        }
        
        public static Dictionary<TKey, TElem>
            ToDictionaryMeta<TCollect, T, TKey, TElem, TKeySelector, TElemSelector>(ref TCollect collect, TKeySelector keySelector,
                TElemSelector elementSelector, IEqualityComparer<TKey> comparer, uint? capacity)
            where TCollect : struct, ICollectionWrapper<T>
            where TKeySelector : struct, IFunctor<T, TKey>
            where TElemSelector : struct, IFunctor<T, TElem>
        {
            if (collect.HasIndexOverhead)
            {
                var buff = new Dictionary<TKey, TElem>((int) capacity.GetValueOrDefault(), comparer);
                while (collect.HasNext)
                {
                    var val = collect.Value;
                    buff.Add(keySelector.Invoke(val), elementSelector.Invoke(val));
                }

                return buff;
            }
            else
            {
                var size = collect.Size;
                if (size > 0)
                {
                    var buff = new Dictionary<TKey, TElem>(size, comparer);
                    for (var i = 0u; i < size; ++i)
                    {
                        var val = collect[i];
                        buff.Add(keySelector.Invoke(val), elementSelector.Invoke(val));
                    }

                    return buff;
                }
            }
            
            return new Dictionary<TKey, TElem>(comparer);
        }
    }
}