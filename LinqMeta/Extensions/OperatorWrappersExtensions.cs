using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LinqMeta.CollectionWrapper;
using LinqMetaCore;
using LinqMetaCore.Intefaces;
using LinqMetaCore.Utils;

namespace LinqMeta.Extensions
{
    public static class CollectionWrappersExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OperatorWrapper<TCollect, T> MetaOperators<TCollect, T>(this TCollect collect)
            where TCollect : struct, ICollectionWrapper<T>
        {
            return new OperatorWrapper<TCollect, T>(collect);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OperatorWrapper<ArrayWrapper<T>, T> MetaOperators<T>(this T[] collect)
        {
            return new OperatorWrapper<ArrayWrapper<T>, T>(new ArrayWrapper<T>(collect));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OperatorWrapper<ListWrapper<T>, T> MetaOperators<T>(this List<T> collect)
        {
            return new OperatorWrapper<ListWrapper<T>, T>(new ListWrapper<T>(collect));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OperatorWrapper<EnumeratorWrapper<T>, T> MetaOperators<T>(this IEnumerator<T> collect)
        {
            return new OperatorWrapper<EnumeratorWrapper<T>, T>(new EnumeratorWrapper<T>(collect));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OperatorWrapper<EnumeratorWrapper<T>, T> MetaOperators<T>(this IEnumerable<T> enumerable)
        {
            ErrorUtil.NullCheck(enumerable, "enumerable");
            return new OperatorWrapper<EnumeratorWrapper<T>, T>(new EnumeratorWrapper<T>(enumerable.GetEnumerator()));
        }
    }
}