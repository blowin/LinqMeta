using System;
using System.Runtime.CompilerServices;

namespace LinqMeta.Operators.Number
{
    public static partial class NumberOperators<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DivFloat(T a, float b)
        {
            if (typeof(T) == typeof(Byte))
            {
                return ((Byte) (object) a / b);
            }
            else if (typeof(T) == typeof(SByte))
            {
                return ((SByte) (object) a / b);
            }
            else if (typeof(T) == typeof(UInt16))
            {
                return ((UInt16) (object) a / b);
            }
            else if (typeof(T) == typeof(Int16))
            {
                return ((Int16) (object) a / b);
            }
            else if (typeof(T) == typeof(UInt32))
            {
                return ((UInt32) (object) a / b);
            }
            else if (typeof(T) == typeof(Int32))
            {
                return ((Int32) (object) a / b);
            }
            else if (typeof(T) == typeof(UInt64))
            {
                return ((UInt64) (object) a / b);
            }
            else if (typeof(T) == typeof(Int64))
            {
                return ((Int64) (object) a / b);
            }
            else if (typeof(T) == typeof(Single))
            {
                return ((Single) (object) a / b);
            }
            else if (typeof(T) == typeof(Double))
            {
                return (float) (object) ((Double) (object) a / b);
            }
            else
            {
                throw new NotSupportedException("Nope");
            }
        }
    }
}