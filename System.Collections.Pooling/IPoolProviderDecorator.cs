namespace System.Collections.Pooling
{
    public interface IPoolProviderDecorator : IPoolProvider
    {
        IPoolProvider Provider { get; }

        void Set(IPoolProvider provider);

        T Get<T>() where T : IPoolProviderDecorator, new();
    }
}