namespace LinqMeta.Operators.IOperator.ConvertToCollect
{
    public interface IToArray<T>
    {
        T[] ToArray(uint? capacity);
    }
}