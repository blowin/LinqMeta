using System;
using System.Runtime.CompilerServices;

namespace LinqMeta.Operators.Number
{
    public static partial class NumberOperators<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Eq(T a, T b)
        {
            if (typeof(T) == typeof(Byte))
            {
                return ((Byte) (object) a == (Byte) (object) b);
            }
            else if (typeof(T) == typeof(SByte))
            {
                return ((SByte) (object) a == (SByte) (object) b);
            }
            else if (typeof(T) == typeof(UInt16))
            {
                return ((UInt16) (object) a == (UInt16) (object) b);
            }
            else if (typeof(T) == typeof(Int16))
            {
                return ((Int16) (object) a == (Int16) (object) b);
            }
            else if (typeof(T) == typeof(UInt32))
            {
                return ((UInt32) (object) a == (UInt32) (object) b);
            }
            else if (typeof(T) == typeof(Int32))
            {
                return ((Int32) (object) a == (Int32) (object) b);
            }
            else if (typeof(T) == typeof(UInt64))
            {
                return ((UInt64) (object) a == (UInt64) (object) b);
            }
            else if (typeof(T) == typeof(Int64))
            {
                return ((Int64) (object) a == (Int64) (object) b);
            }
            else if (typeof(T) == typeof(Single))
            {
                return (Math.Abs((Single) (object) a - (Single) (object) b) < Single.Epsilon);
            }
            else if (typeof(T) == typeof(Double))
            {
                return (Math.Abs((Double) (object) a - (Double) (object) b) < Double.Epsilon);
            }
            else
            {
                throw new NotSupportedException("Nope");
            }
        }
    }
}