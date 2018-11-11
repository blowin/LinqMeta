using System.Runtime.InteropServices;

namespace LinqMeta.DataTypes
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct IteratePack
    {
        [FieldOffset(0)] public HasOverheadWithIndex OverHeadWithIndex;
        [FieldOffset(0)] public HasOverheadTwoCollect OverheadTwoCollect;
        [FieldOffset(0)] public int Size;
        
        public static IteratePack CreateOverheadWithIndex()
        {
            return new IteratePack {OverHeadWithIndex = new HasOverheadWithIndex
            {
                Index = 0,
                EndOverheadIter = false
            }};
        }
        
        public static IteratePack CreateOverheadTwoCollect()
        {
            return new IteratePack 
            {
                OverheadTwoCollect = new HasOverheadTwoCollect
                {
                    EndFirst = false,
                    EndSecond = false
                }
            };
        }
        
        public static IteratePack CreateSize(int size)
        {
            return new IteratePack {Size = size};
        }

        internal struct HasOverheadWithIndex
        {
            public int Index;
            public bool EndOverheadIter;
        }

        internal struct HasOverheadTwoCollect
        {
            public bool EndFirst;
            public bool EndSecond;
        }
    }
}