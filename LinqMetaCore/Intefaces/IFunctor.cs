namespace LinqMetaCore.Intefaces
{
    public interface IFunctor<TRes>
    {
        TRes Invoke();
    }
    
    public interface IFunctor<T, TRes>
    {
        TRes Invoke(T param);
    }
    
    public interface IFunctor<T, T2, TRes>
    {
        TRes Invoke(T param, T2 param2);
    }
}