namespace LinqMeta.Extensions.Operators.IOperator
{
    public interface IToArray<T>
    {
        T[] ToMetaArray(uint? capacity);
    }
}