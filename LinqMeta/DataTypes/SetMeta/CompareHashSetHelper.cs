using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LinqMeta.Operators.Number;

namespace LinqMeta.DataTypes.SetMeta
{
    public struct CompareHashSetHelper<T> : IEqualityComparer<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(T x, T y)
        {
            return NumberOperators<T>.Eq(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetHashCode(T a)
        {    
            if (typeof(T) == typeof(Byte))
            {
                return ((Byte) (object) a) & int.MaxValue;
            }
            else if (typeof(T) == typeof(SByte))
            {
                return ((SByte) (object) a) & int.MaxValue;
            }
            else if (typeof(T) == typeof(UInt16))
            {
                return ((UInt16) (object) a) & int.MaxValue;
            }
            else if (typeof(T) == typeof(Int16))
            {
                return ((Int16) (object) a) & int.MaxValue;
            }
            else if (typeof(T) == typeof(UInt32))
            {
                return ((Int32)(UInt32) (object) a) & int.MaxValue;
            }
            else if (typeof(T) == typeof(Int32))
            {
                return ((Int32) (object) a) & int.MaxValue;
            }
            else if (typeof(T) == typeof(UInt64))
            {
                return ((Int32) ((UInt64) (object) a) ^ (int) (((UInt64) (object) a) >> 32)) & int.MaxValue;
            }
            else if (typeof(T) == typeof(Int64))
            {
                return ((Int32) ((Int64) (object) a) ^ (int) (((Int64) (object) a) >> 32)) & int.MaxValue;
            }
            else if (typeof(T) == typeof(Single))
            {
                unsafe
                {
                    var num = (Single) (object) a + 1;
                    return (*(Int32*)&num) & int.MaxValue;
                }
            }
            else if (typeof(T) == typeof(Double))
            {
                unsafe
                {
                    var num1 = (Double) (object) a + 1;
                    var num2 = *(Int64*) &num1;
                    return ((Int32) num2 ^ (Int32) (num2 >> 32)) & int.MaxValue;
                }
            }
            else
            {
                return a != null ? 
                    ComparerCash<T>.Value.GetHashCode(a) & int.MaxValue :
                    0;
            }
        }
    }
}