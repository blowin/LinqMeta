using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Extensions.Operators
{
    public static class ForEachOperator
    {
        public static void ForEachMeta<TCollect, TAction, T>(ref TCollect collect, ref TAction action)
            where TCollect : struct, ICollectionWrapper<T>
            where TAction : struct, IFunctor<T, MetaVoid>
        {
            if (collect.HasIndexOverhead)
            {
                while (collect.HasNext)
                    action.Invoke(collect.Value);
            }
            else
            {
                var size = collect.Size;
                for (var i = 0u; i < size; ++i)
                    action.Invoke(collect[i]);
            }
        }
    }
}