namespace LinqMeta.CollectionWrapper
{
    public interface ICollectionWrapper<T>
    {
        bool HasIndexOverhead { get; }

        bool HasNext { get; }
        
        T Value { get; }
        
        int Size { get; }
        
        T this[uint index] { get; }
    }
}