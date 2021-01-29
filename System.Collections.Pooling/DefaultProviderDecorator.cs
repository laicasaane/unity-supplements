using System.Collections.ArrayBased;
using System.Collections.Generic;

namespace System.Collections.Pooling
{
    public struct DefaultProviderDecorator : IPoolProviderDecorator
    {
        public IPoolProvider Provider { get; private set; }

        public void Set(IPoolProvider provider)
            => this.Provider = provider ?? throw new ArgumentNullException(nameof(provider));

        public T Get<T>() where T : IPoolProviderDecorator, new()
        {
            var decorator = new T();
            decorator.Set(this);

            return decorator;
        }

        public T[] Array1<T>(int size)
            => this.Provider.Array1<T>(size);

        public T[] Array1<T>(long size)
            => Array1Pool<T>.Get(size);

        public ArrayDictionary<TKey, TValue> ArrayDictionary<TKey, TValue>()
            => this.Provider.ArrayDictionary<TKey, TValue>();

        public ArrayList<T> ArrayList<T>()
            => this.Provider.ArrayList<T>();

        public Dictionary<TKey, TValue> Dictionary<TKey, TValue>()
            => this.Provider.Dictionary<TKey, TValue>();

        public HashSet<T> HashSet<T>()
            => this.Provider.HashSet<T>();

        public List<T> List<T>()
            => this.Provider.List<T>();

        public Pool<T> Pool<T>() where T : class, new()
            => this.Provider.Pool<T>();

        public Queue<T> Queue<T>()
            => this.Provider.Queue<T>();

        public Stack<T> Stack<T>()
            => this.Provider.Stack<T>();

        public void Return<T>(T[] item)
            => this.Provider.Return(item);

        public void Return<T>(params T[][] items)
            => this.Provider.Return(items);

        public void Return<T>(IEnumerable<T[]> items)
            => this.Provider.Return(items);

        public void Return<T>(List<T> item)
            => this.Provider.Return(item);

        public void Return<T>(params List<T>[] items)
            => this.Provider.Return(items);

        public void Return<T>(IEnumerable<List<T>> items)
            => this.Provider.Return(items);

        public void Return<T>(ArrayList<T> item)
            => this.Provider.Return(item);

        public void Return<T>(params ArrayList<T>[] items)
            => this.Provider.Return(items);

        public void Return<T>(IEnumerable<ArrayList<T>> items)
            => this.Provider.Return(items);

        public void Return<T>(bool shallowClear, ArrayList<T> item)
            => ArrayListPool<T>.Return(shallowClear, item);

        public void Return<T>(bool shallowClear, params ArrayList<T>[] items)
            => ArrayListPool<T>.Return(shallowClear, items);

        public void Return<T>(bool shallowClear, IEnumerable<ArrayList<T>> items)
            => ArrayListPool<T>.Return(shallowClear, items);

        public void Return<T>(HashSet<T> item)
            => this.Provider.Return(item);

        public void Return<T>(params HashSet<T>[] items)
            => this.Provider.Return(items);

        public void Return<T>(IEnumerable<HashSet<T>> items)
            => this.Provider.Return(items);

        public void Return<T>(Queue<T> item)
            => this.Provider.Return(item);

        public void Return<T>(params Queue<T>[] items)
            => this.Provider.Return(items);

        public void Return<T>(IEnumerable<Queue<T>> items)
            => this.Provider.Return(items);

        public void Return<T>(Stack<T> item)
            => this.Provider.Return(item);

        public void Return<T>(params Stack<T>[] items)
            => this.Provider.Return(items);

        public void Return<T>(IEnumerable<Stack<T>> items)
            => this.Provider.Return(items);

        public void Return<TKey, TValue>(Dictionary<TKey, TValue> item)
            => this.Provider.Return(item);

        public void Return<TKey, TValue>(params Dictionary<TKey, TValue>[] items)
            => this.Provider.Return(items);

        public void Return<TKey, TValue>(IEnumerable<Dictionary<TKey, TValue>> items)
            => this.Provider.Return(items);

        public void Return<TKey, TValue>(ArrayDictionary<TKey, TValue> item)
            => this.Provider.Return(item);

        public void Return<TKey, TValue>(params ArrayDictionary<TKey, TValue>[] items)
            => this.Provider.Return(items);

        public void Return<TKey, TValue>(IEnumerable<ArrayDictionary<TKey, TValue>> items)
            => this.Provider.Return(items);

        public void Return<TKey, TValue>(bool shallowClear, ArrayDictionary<TKey, TValue> item)
            => ArrayDictionaryPool<TKey, TValue>.Return(shallowClear, item);

        public void Return<TKey, TValue>(bool shallowClear, params ArrayDictionary<TKey, TValue>[] items)
            => ArrayDictionaryPool<TKey, TValue>.Return(shallowClear, items);

        public void Return<TKey, TValue>(bool shallowClear, IEnumerable<ArrayDictionary<TKey, TValue>> items)
            => ArrayDictionaryPool<TKey, TValue>.Return(shallowClear, items);
    }
}