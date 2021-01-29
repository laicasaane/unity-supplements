namespace System.Collections.Pooling.Concurrent
{
    public interface IConcurrentPoolProviderDecorator : IConcurrentPoolProvider
    {
        IConcurrentPoolProvider Provider { get; }

        void Set(IConcurrentPoolProvider provider);

        T Get<T>() where T : IConcurrentPoolProviderDecorator, new();
    }
}