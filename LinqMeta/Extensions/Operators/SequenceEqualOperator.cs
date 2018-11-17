using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LinqMeta.CollectionWrapper;
using LinqMeta.DataTypes;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Extensions.Operators
{
    public static class SequenceEqualOperator
    {
        internal struct DefaultEqual<T> : IFunctor<T, T, bool>
        {
            private IEqualityComparer<T> _comparer;

            public DefaultEqual(IEqualityComparer<T> comparer)
            {
                _comparer = comparer;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool Invoke(T param, T param2)
            {
                return _comparer.Equals(param, param2);
            }
        }
        
        internal struct OverrideEq<T, T2> : IFunctor<T, T2, bool>
            where T2 : IEquatable<T>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool Invoke(T param, T2 param2)
            {
                return param2.Equals(param);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SequenceEqual<TCollect, TSecond, T>(ref TCollect collect, ref TSecond second)
            where TCollect : struct, ICollectionWrapper<T>
            where TSecond : struct, ICollectionWrapper<T>
        {
            var predicat = new DefaultEqual<T>(EqualityComparer<T>.Default);
            return SequenceEqual<TCollect, TSecond, DefaultEqual<T>, T, T>(ref collect, ref second, ref predicat);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SequenceEqual<TCollect, TSecond, T>(ref TCollect collect, ref TSecond second, IEqualityComparer<T> comparer)
            where TCollect : struct, ICollectionWrapper<T>
            where TSecond : struct, ICollectionWrapper<T>
        {
            var predicat = new DefaultEqual<T>(comparer);
            return SequenceEqual<TCollect, TSecond, DefaultEqual<T>, T, T>(ref collect, ref second, ref predicat);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SequenceEqualEq<TCollect, TSecond, T, T2>(ref TCollect collect, ref TSecond second)
            where TCollect : struct, ICollectionWrapper<T>
            where TSecond : ICollectionWrapper<T2>
            where T2 : IEquatable<T>
        {
            var predicat = new OverrideEq<T, T2>();
            return SequenceEqual<TCollect, TSecond, OverrideEq<T, T2>, T, T2>(ref collect, ref second, ref predicat);
        }
        
        public static bool SequenceEqual<TCollect, TSecond, TPredicat, T, T2>(ref TCollect collect, ref TSecond second, ref TPredicat predicat)
            where TCollect : struct, ICollectionWrapper<T>
            where TSecond : ICollectionWrapper<T2>
            where TPredicat : struct, IFunctor<T, T2, bool>
        {
            var wrap = new StructMaybeBoxCollectionWrapper<TSecond, T2>(second);
            var typeIter = TypeIterateHelpers.GetTypeIterate<TCollect, StructMaybeBoxCollectionWrapper<TSecond, T2>, T, T2>(ref collect, ref wrap);
            
            switch (typeIter)
            {
                case TypeIterate.FirstHasOverHeadSecondHasOverhead:
                    while (collect.HasNext)
                    {
                        if (!second.HasNext || !predicat.Invoke(collect.Value, second.Value))
                            return false;
                    }

                    if (second.HasNext)
                        return false;

                    return true;
                case TypeIterate.FirsHasOverheadSecondMayIter:
                    var sizeSecond = second.Size;
                    for (var i = 0u; i < sizeSecond; ++i)
                    {
                        if (!collect.HasNext || !predicat.Invoke(collect.Value, second[i]))
                            return false;
                    }

                    if (collect.HasNext)
                        return false;

                    return true;
                case TypeIterate.FirstMayIterSecondHasOverhead:
                    var size = collect.Size;
                    for (var i = 0u; i < size; ++i)
                    {
                        if (!second.HasNext || !predicat.Invoke(collect[i], second.Value))
                            return false;
                    }

                    if (second.HasNext)
                        return false;

                    return true;
                case TypeIterate.FirstMayIterSecondMayIter:
                    var sizeFirst = collect.Size;
                    if (sizeFirst != second.Size)
                        return false;

                    for (var i = 0u; i < sizeFirst; ++i)
                    {
                        var firstItem = collect[i];
                        var secondItem = second[i];
                        if (!predicat.Invoke(firstItem, secondItem))
                            return false;
                    }
                    
                    return true;
                default: return false;
            }
        }
    }
}