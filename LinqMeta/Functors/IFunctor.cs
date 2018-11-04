namespace LinqMeta.Functors
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
    
    public interface IFunctor<T, T2, T3, TRes>
    {
        TRes Invoke(T param, T2 param2, T3 param3);
    }
    
    public interface IFunctor<T, T2, T3, T4, TRes>
    {
        TRes Invoke(T param, T2 param2, T3 param3, T4 param4);
    }
    
    public interface IPredicat<T> : IFunctor<T, bool>
    {
    }

    public interface IVoidFunctor<T> : IFunctor<T, Void>
    {
    }
}