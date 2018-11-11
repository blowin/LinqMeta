using System;
using System.Runtime.CompilerServices;
using LinqMetaCore.Intefaces;

namespace LinqMeta.DataTypes
{
    [Flags]
    internal enum TypeIterate : byte
    {
        FirstHasOverhead = 1,
        FirstMayIterate = 1 << 1,
        SecondHasOverhead = 1 << 2,
        SecondMayIterate = 1 << 3,
            
        FirstHasOverHeadSecondHasOverhead = FirstHasOverhead | SecondHasOverhead,
        FirsHasOverheadSecondMayIter = FirstHasOverhead | SecondMayIterate,
        FirstMayIterSecondHasOverhead = FirstMayIterate | SecondHasOverhead,
        FirstMayIterSecondMayIter = FirstMayIterate | SecondMayIterate
    }

    internal static class TypeIterateHelpers
    {
        public static TypeIterate GetTypeIterate<TFirst, TSecond, T>(ref TFirst first, ref TSecond second)
            where TFirst : struct, ICollectionWrapper<T>
            where TSecond : struct, ICollectionWrapper<T>
        {
            return GetTypeIterate<TFirst, TSecond, T, T>(ref first, ref second);
        }
        
        public static TypeIterate GetTypeIterate<TFirst, TSecond, T, T2>(ref TFirst first, ref TSecond second)
            where TFirst : struct, ICollectionWrapper<T>
            where TSecond : struct, ICollectionWrapper<T2>
        {
            var typeIterate = first.HasIndexOverhead
                ? TypeIterate.FirstHasOverhead
                : TypeIterate.FirstMayIterate;

            typeIterate |= second.HasIndexOverhead
                ? TypeIterate.SecondHasOverhead
                : TypeIterate.SecondMayIterate;

            return typeIterate;
        }
    }
}