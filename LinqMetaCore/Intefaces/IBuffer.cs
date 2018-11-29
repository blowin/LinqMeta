namespace LinqMetaCore.Intefaces
{
    public interface IBuffer<T> : IReadonlyBuffer<T>
    {
        void Add(T item);

        T[] ToArray();
    }
}