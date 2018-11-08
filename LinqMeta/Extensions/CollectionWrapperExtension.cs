using System.Collections.Generic;
using LinqMeta.CollectionWrapper;

namespace LinqMeta.Extensions
{
    public static class CollectionWrapperExtension
    {
        public static ArrayWrapper<T> GetMetaIter<T>(this T[] collect)
        {
            return new ArrayWrapper<T>(collect);
        }
        
        public static ListWrapper<T> GetMetaIter<T>(this List<T> collect)
        {
            return new ListWrapper<T>(collect);
        }
    }
}