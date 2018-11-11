using System.Runtime.CompilerServices;
using LinqMetaCore.Intefaces;

namespace LinqMeta.DataTypes
{
    internal struct StateInfo
    {
        public IteratePack IteratePack;
        public TypeIterate TypeIterate;

        public bool HasOverhead
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return TypeIterate != TypeIterate.FirstMayIterSecondMayIter; }
        }
        
        public static StateInfo Create<TFirst, TSecond, T>(ref TFirst first, ref TSecond second) 
            where TFirst : struct, ICollectionWrapper<T>
            where TSecond : struct, ICollectionWrapper<T>
        {
            return Create<TFirst, TSecond, T, T>(ref first, ref second);
        }
        
        public static StateInfo Create<TFirst, TSecond, T1, T2>(ref TFirst first, ref TSecond second) 
            where TFirst : struct, ICollectionWrapper<T1>
            where TSecond : struct, ICollectionWrapper<T2>
        {
            var stateInfo = new StateInfo
            {
                TypeIterate = TypeIterateHelpers.GetTypeIterate<TFirst, TSecond, T1, T2>(ref first, ref second)
            };

            switch (stateInfo.TypeIterate)
            {
                case TypeIterate.FirstHasOverHeadSecondHasOverhead:
                    stateInfo.IteratePack = IteratePack.CreateOverheadTwoCollect();
                    break;
                case TypeIterate.FirsHasOverheadSecondMayIter:
                case TypeIterate.FirstMayIterSecondHasOverhead:
                    stateInfo.IteratePack = IteratePack.CreateOverheadWithIndex();
                    break;
                case TypeIterate.FirstMayIterSecondMayIter:
                default:
                    stateInfo.IteratePack = IteratePack.CreateSize(first.Size);
                    break;
            }

            return stateInfo;
        }
    }
}