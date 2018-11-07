namespace LinqMeta.Operators.IOperator
{
    public interface IToArray<T>
    {
        T[] ToArray(uint? capacity);
    }
}