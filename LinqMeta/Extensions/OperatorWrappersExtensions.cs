using System.Collections.Generic;
using LinqMeta.CollectionWrapper;

namespace LinqMeta.Extensions
{
    public static class CollectionWrappersExtensions
    {
        public static OperatorWrapper<TCollect, T> MetaOperators<TCollect, T>(this TCollect collect)
            where TCollect : struct, ICollectionWrapper<T>
        {
            return new OperatorWrapper<TCollect, T>(collect);
        }
        
        public static OperatorWrapper<ArrayWrapper<T>, T> MetaOperators<T>(this T[] collect)
        {
            return new OperatorWrapper<ArrayWrapper<T>, T>(new ArrayWrapper<T>(collect));
        }
        
        public static OperatorWrapper<ListWrapper<T>, T> MetaOperators<T>(this List<T> collect)
        {
            return new OperatorWrapper<ListWrapper<T>, T>(new ListWrapper<T>(collect));
        }
    }
}