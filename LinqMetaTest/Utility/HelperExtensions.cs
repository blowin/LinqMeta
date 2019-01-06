using System;

namespace LinqMetaTest.Utility
{
    public static class HelperExtensions
    {
        public static bool InEq<T>(this T val, params T[] args)
            where T : IEquatable<T>
        {
            foreach (var arg in args)
            {
                if (arg.Equals(val))
                    return true;
            }

            return false;
        }
        
        public static bool In<T>(this T val, params T[] args)
        {
            foreach (var arg in args)
            {
                if (arg.Equals(val))
                    return true;
            }

            return false;
        }
    }
}