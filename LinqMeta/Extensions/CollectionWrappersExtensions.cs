using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LinqMeta.CollectionWrapper;

namespace LinqMeta.Extensions
{
    public static class CollectionWrappersExtensions
    {
        public static CollectWrapper<TCollect, T> MetaOperators<TCollect, T>(this TCollect collect)
            where TCollect : struct, ICollectionWrapper<T>
        {
            return new CollectWrapper<TCollect, T>(collect);
        }
        
        public static CollectWrapper<ArrayWrapper<T>, T> MetaOperators<T>(this T[] collect)
        {
            return new CollectWrapper<ArrayWrapper<T>, T>(new ArrayWrapper<T>(collect));
        }
        
        public static CollectWrapper<ListWrapper<T>, T> MetaOperators<T>(this List<T> collect)
        {
            return new CollectWrapper<ListWrapper<T>, T>(new ListWrapper<T>(collect));
        }
    }
}