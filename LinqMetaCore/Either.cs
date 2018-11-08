using System.Runtime.InteropServices;

namespace LinqMetaCore
{
    [StructLayout(LayoutKind.Explicit)]
    public struct Union<TL, TR>
    {
        [FieldOffset(0)] public TL Left;
        [FieldOffset(0)] public TR Right;
    }
}