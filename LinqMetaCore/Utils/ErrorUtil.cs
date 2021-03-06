using System;
using System.Runtime.CompilerServices;

namespace LinqMetaCore.Utils
{
    public static class ErrorUtil
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NullCheck<T>(T v, string msg)
            where T : class
        {
            if(v == null)
                throw new ArgumentNullException(msg);
        }
    }
}