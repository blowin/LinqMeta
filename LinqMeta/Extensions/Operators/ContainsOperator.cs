using System;
using System.Runtime.CompilerServices;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Extensions.Operators
{
    public static class ContainsOperator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsMeta<TCollect, T>(ref TCollect collect, T val)
            where TCollect : struct, ICollectionWrapper<T>
        {
            if (collect.HasIndexOverhead)
            {
                while (collect.HasNext)
                {
                    if (collect.Value.Equals(val))
                        return true;
                }
            }
            else
            {
                var size = collect.Size;
                for (var i = 0u; i < size; ++i)
                {
                    if (collect[i].Equals(val))
                        return true;
                }   
            }

            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsEqMeta<TCollect, T, T2>(ref TCollect collect, T2 val)
            where TCollect : struct, ICollectionWrapper<T>
            where T2 : IEquatable<T>
        {
            if (collect.HasIndexOverhead)
            {
                while (collect.HasNext)
                {
                    if (val.Equals(collect.Value))
                        return true;
                }
            }
            else
            {
                var size = collect.Size;
                for (var i = 0u; i < size; ++i)
                {
                    if (val.Equals(collect[i]))
                        return true;
                }   
            }

            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsMeta<TCollect, T, TPredicat>(ref TCollect collect, ref TPredicat predicat)
            where TCollect : struct, ICollectionWrapper<T>
            where TPredicat : struct, IFunctor<T, bool>
        {
            if (collect.HasIndexOverhead)
            {
                while (collect.HasNext)
                {
                    if (predicat.Invoke(collect.Value))
                        return true;
                }
            }
            else
            {
                var size = collect.Size;
                for (var i = 0u; i < size; ++i)
                {
                    if (predicat.Invoke(collect[i]))
                        return true;
                }   
            }

            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsMeta<TCollect, T, TPredicat, T2>(ref TCollect collect, ref TPredicat predicat, T2 val)
            where TCollect : struct, ICollectionWrapper<T>
            where TPredicat : struct, IFunctor<T, T2, bool>
        {
            if (collect.HasIndexOverhead)
            {
                while (collect.HasNext)
                {
                    if (predicat.Invoke(collect.Value, val))
                        return true;
                }
            }
            else
            {
                var size = collect.Size;
                for (var i = 0u; i < size; ++i)
                {
                    if (predicat.Invoke(collect[i], val))
                        return true;
                }   
            }

            return false;
        }
    }
}