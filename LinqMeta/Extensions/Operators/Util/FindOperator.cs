using System.Runtime.CompilerServices;
using LinqMeta.CollectionWrapper;
using LinqMeta.Functors;

namespace LinqMeta.Extensions.Operators.Util
{
    public static class FindOperator
    {
        internal static T FindElementInAllCollection<TCollection, TCond, T>(this TCollection collection, TCond cond)
            where TCollection : struct, ICollectionWrapper<T>
            where TCond : struct, IFunctor<T, T, bool>
        {
            var size = collection.Size;
            if (size > 0)
            {
                var item = collection[0];
                for (var i = 1u; i < size; ++i)
                {
                    var curItem = collection[i];
                    if (cond.Invoke(curItem, item))
                        item = curItem;
                }

                return item;
            }

            return default(T);
        }
    }
}