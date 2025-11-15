namespace CommonUtilitiesCSharp.Core
{
    public interface ISingleton<T> where T: class
    {
        static abstract T Instance { get; }
    }
}
