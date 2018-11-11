using System.Collections.Generic;
using LinqMeta.CollectionWrapper;
using LinqMetaCore;
using LinqMetaCore.Intefaces;
using LinqMetaCore.Utils;

namespace LinqMeta.Extensions
{
    public static class CollectionWrapperExtensions
    {
        public static ArrayWrapper<T> MetaWrapper<T>(this T[] collect)
        {
            return new ArrayWrapper<T>(collect);
        }
        
        public static ListWrapper<T> MetaWrapper<T>(this List<T> collect)
        {
            return new ListWrapper<T>(collect);
        }
        
        public static ListInterfaceWrapper<T> MetaWrapper<T>(this IList<T> collect)
        {
            return new ListInterfaceWrapper<T>(collect);
        }
        
        public static EnumeratorWrapper<T> MetaWrapper<T>(this IEnumerator<T> collect)
        {
            return new EnumeratorWrapper<T>(collect);
        }
        
        public static EnumeratorWrapper<T> MetaWrapper<T>(this IEnumerable<T> enumerable)
        {
            ErrorUtil.NullCheck(enumerable, "enumerable");
            return new EnumeratorWrapper<T>(enumerable.GetEnumerator());
        }
    }
}