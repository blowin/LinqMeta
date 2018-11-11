namespace LinqMetaCore.Buffers
{
    public interface IBuffer<T>
    {
        uint Size { get; }

        void Add(T item);

        T[] ToArray();
    }
}