using System;
using System.Runtime.CompilerServices;

namespace LinqMeta.Operators.Number
{
    public static partial class NumberOperators<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Product(T a, T b)
        {
            if (typeof(T) == typeof(Byte))
            {
                return (T) (object) ((Byte) (object) a * (Byte) (object) b);
            }
            else if (typeof(T) == typeof(SByte))
            {
                return (T) (object) ((SByte) (object) a * (SByte) (object) b);
            }
            else if (typeof(T) == typeof(UInt16))
            {
                return (T) (object) ((UInt16) (object) a * (UInt16) (object) b);
            }
            else if (typeof(T) == typeof(Int16))
            {
                return (T) (object) ((Int16) (object) a * (Int16) (object) b);
            }
            else if (typeof(T) == typeof(UInt32))
            {
                return (T) (object) ((UInt32) (object) a * (UInt32) (object) b);
            }
            else if (typeof(T) == typeof(Int32))
            {
                return (T) (object) ((Int32) (object) a * (Int32) (object) b);
            }
            else if (typeof(T) == typeof(UInt64))
            {
                return (T) (object) ((UInt64) (object) a * (UInt64) (object) b);
            }
            else if (typeof(T) == typeof(Int64))
            {
                return (T) (object) ((Int64) (object) a * (Int64) (object) b);
            }
            else if (typeof(T) == typeof(Single))
            {
                return (T) (object) ((Single) (object) a * (Single) (object) b);
            }
            else if (typeof(T) == typeof(Double))
            {
                return (T) (object) ((Double) (object) a * (Double) (object) b);
            }
            else
            {
                throw new NotSupportedException("Nope");
            }
        }
    }
}