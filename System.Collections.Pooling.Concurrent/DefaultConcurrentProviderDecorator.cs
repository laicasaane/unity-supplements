using System.Collections.ArrayBased;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace System.Collections.Pooling.Concurrent
{
    public struct DefaultConcurrentProviderDecorator : IConcurrentPoolProviderDecorator
    {
        public IConcurrentPoolProvider Provider { get; private set; }

        public void Set(IConcurrentPoolProvider provider)
            => this.Provider = provider ?? throw new ArgumentNullException(nameof(provider));

        public T Get<T>() where T : IConcurrentPoolProviderDecorator, new()
        {
            var decorator = new T();
            decorator.Set(this);

            return decorator;
        }

        public T[] Array1<T>(int size)
            => this.Provider.Array1<T>(size);

        public T[] Array1<T>(long size)
            => Array1ConcurrentPool<T>.Get(size);

        public ArrayDictionary<TKey, TValue> ArrayDictionary<TKey, TValue>()
            => this.Provider.ArrayDictionary<TKey, TValue>();

        public ArrayList<T> ArrayList<T>()
            => this.Provider.ArrayList<T>();

        public ArrayHashSet<T> ArrayHashSet<T>()
            => this.Provider.ArrayHashSet<T>();

        public ConcurrentBag<T> ConcurrentBag<T>()
            => this.Provider.ConcurrentBag<T>();

        public ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>()
            => this.Provider.ConcurrentDictionary<TKey, TValue>();

        public ConcurrentPool<T> ConcurrentPool<T>() where T : class, new()
            => this.Provider.ConcurrentPool<T>();

        public ConcurrentQueue<T> ConcurrentQueue<T>()
            => this.Provider.ConcurrentQueue<T>();

        public ConcurrentStack<T> ConcurrentStack<T>()
            => this.Provider.ConcurrentStack<T>();

        public Dictionary<TKey, TValue> Dictionary<TKey, TValue>()
            => this.Provider.Dictionary<TKey, TValue>();

        public HashSet<T> HashSet<T>()
            => this.Provider.HashSet<T>();

        public List<T> List<T>()
            => this.Provider.List<T>();

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
            => ArrayListConcurrentPool<T>.Return(shallowClear, item);

        public void Return<T>(bool shallowClear, params ArrayList<T>[] items)
            => ArrayListConcurrentPool<T>.Return(shallowClear, items);

        public void Return<T>(bool shallowClear, IEnumerable<ArrayList<T>> items)
            => ArrayListConcurrentPool<T>.Return(shallowClear, items);

        public void Return<T>(HashSet<T> item)
            => this.Provider.Return(item);

        public void Return<T>(params HashSet<T>[] items)
            => this.Provider.Return(items);

        public void Return<T>(IEnumerable<HashSet<T>> items)
            => this.Provider.Return(items);

        public void Return<T>(ArrayHashSet<T> item)
            => this.Provider.Return(item);

        public void Return<T>(params ArrayHashSet<T>[] items)
            => this.Provider.Return(items);

        public void Return<T>(IEnumerable<ArrayHashSet<T>> items)
            => this.Provider.Return(items);

        public void Return<T>(bool shallowClear, ArrayHashSet<T> item)
            => ArrayHashSetConcurrentPool<T>.Return(shallowClear, item);

        public void Return<T>(bool shallowClear, params ArrayHashSet<T>[] items)
            => ArrayHashSetConcurrentPool<T>.Return(shallowClear, items);

        public void Return<T>(bool shallowClear, IEnumerable<ArrayHashSet<T>> items)
            => ArrayHashSetConcurrentPool<T>.Return(shallowClear, items);

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
            => ArrayDictionaryConcurrentPool<TKey, TValue>.Return(shallowClear, item);

        public void Return<TKey, TValue>(bool shallowClear, params ArrayDictionary<TKey, TValue>[] items)
            => ArrayDictionaryConcurrentPool<TKey, TValue>.Return(shallowClear, items);

        public void Return<TKey, TValue>(bool shallowClear, IEnumerable<ArrayDictionary<TKey, TValue>> items)
            => ArrayDictionaryConcurrentPool<TKey, TValue>.Return(shallowClear, items);

        public void Return<T>(ConcurrentBag<T> item)
            => this.Provider.Return(item);

        public void Return<T>(params ConcurrentBag<T>[] items)
            => this.Provider.Return(items);

        public void Return<T>(IEnumerable<ConcurrentBag<T>> items)
            => this.Provider.Return(items);

        public void Return<TKey, TValue>(ConcurrentDictionary<TKey, TValue> item)
            => this.Provider.Return(item);

        public void Return<TKey, TValue>(params ConcurrentDictionary<TKey, TValue>[] items)
            => this.Provider.Return(items);

        public void Return<TKey, TValue>(IEnumerable<ConcurrentDictionary<TKey, TValue>> items)
            => this.Provider.Return(items);

        public void Return<T>(ConcurrentQueue<T> item)
            => this.Provider.Return(item);

        public void Return<T>(params ConcurrentQueue<T>[] items)
            => this.Provider.Return(items);

        public void Return<T>(IEnumerable<ConcurrentQueue<T>> items)
            => this.Provider.Return(items);

        public void Return<T>(ConcurrentStack<T> item)
            => this.Provider.Return(item);

        public void Return<T>(params ConcurrentStack<T>[] items)
            => this.Provider.Return(items);

        public void Return<T>(IEnumerable<ConcurrentStack<T>> items)
            => this.Provider.Return(items);
    }
}