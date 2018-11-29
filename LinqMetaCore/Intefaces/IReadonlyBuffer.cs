namespace LinqMetaCore.Intefaces
{
    public interface IReadonlyBuffer<T>
    {
        uint Size { get; }

        T this[uint index] { get; }
    }
}