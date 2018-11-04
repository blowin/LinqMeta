namespace LinqMeta.CollectionWrapper
{
    public interface ICollectionWrapper<T>
    {
        int Size { get; }

        Option<T> ElementAt(uint index);
        
        T this[uint index] { get; }
    }
}