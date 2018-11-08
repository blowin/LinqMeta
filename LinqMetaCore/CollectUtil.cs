namespace LinqMetaCore
{
    public static class CollectUtil
    {
        public static class ArrayUtil<T>
        {
            public static readonly T[] Empty = new T[0];
        }

        public static class ListUtil<T>
        {
            public static readonly System.Collections.Generic.List<T> Empty = new System.Collections.Generic.List<T>();
        }
    }
}