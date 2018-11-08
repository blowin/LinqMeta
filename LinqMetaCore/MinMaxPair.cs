namespace LinqMetaCore
{
    public struct MinMaxPair<T>
    {
        public T Min;
        public T Max;

        public MinMaxPair(T min, T max)
        {
            Min = min;
            Max = max;
        }
    }
}