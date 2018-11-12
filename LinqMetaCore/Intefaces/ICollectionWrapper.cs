namespace LinqMetaCore.Intefaces
{
    // TODO add check policy
    public interface ICollectionWrapper<T>
    {
        bool HasIndexOverhead { get; }

        bool HasNext { get; }
        
        T Value { get; }
        
        int Size { get; }
        
        T this[uint index] { get; }
    }
}