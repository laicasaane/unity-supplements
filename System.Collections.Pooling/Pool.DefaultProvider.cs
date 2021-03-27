using System.Collections.ArrayBased;
using System.Collections.Generic;

namespace System.Collections.Pooling
{
    public static partial class Pool
    {
        private readonly struct DefaultProvider : IPoolProvider
        {
            public Pool<T> Pool<T>() where T : class, new()
            => Pooling.Pool<T>.Default;

            public T[] Array1<T>(int size)
                => Array1Pool<T>.Get(size);

            public void Return<T>(T[] item)
                => Array1Pool<T>.Return(item);

            public void Return<T>(params T[][] items)
                => Array1Pool<T>.Return(items);

            public void Return<T>(IEnumerable<T[]> items)
                => Array1Pool<T>.Return(items);

            public List<T> List<T>()
                => ListPool<T>.Get();

            public void Return<T>(List<T> item)
                => ListPool<T>.Return(item);

            public void Return<T>(params List<T>[] items)
                => ListPool<T>.Return(items);

            public void Return<T>(IEnumerable<List<T>> items)
                => ListPool<T>.Return(items);

            public ArrayList<T> ArrayList<T>()
                => ArrayListPool<T>.Get();

            public void Return<T>(ArrayList<T> item)
                => ArrayListPool<T>.Return(item);

            public void Return<T>(params ArrayList<T>[] items)
                => ArrayListPool<T>.Return(items);

            public void Return<T>(IEnumerable<ArrayList<T>> items)
                => ArrayListPool<T>.Return(items);

            public HashSet<T> HashSet<T>()
                => HashSetPool<T>.Get();

            public void Return<T>(HashSet<T> item)
                => HashSetPool<T>.Return(item);

            public void Return<T>(params HashSet<T>[] items)
                => HashSetPool<T>.Return(items);

            public void Return<T>(IEnumerable<HashSet<T>> items)
                => HashSetPool<T>.Return(items);

            public ArrayHashSet<T> ArrayHashSet<T>()
                => ArrayHashSetPool<T>.Get();

            public void Return<T>(ArrayHashSet<T> item)
                => ArrayHashSetPool<T>.Return(item);

            public void Return<T>(params ArrayHashSet<T>[] items)
                => ArrayHashSetPool<T>.Return(items);

            public void Return<T>(IEnumerable<ArrayHashSet<T>> items)
                => ArrayHashSetPool<T>.Return(items);

            public Queue<T> Queue<T>()
                => QueuePool<T>.Get();

            public void Return<T>(Queue<T> item)
                => QueuePool<T>.Return(item);

            public void Return<T>(params Queue<T>[] items)
                => QueuePool<T>.Return(items);

            public void Return<T>(IEnumerable<Queue<T>> items)
                => QueuePool<T>.Return(items);

            public Stack<T> Stack<T>()
                => StackPool<T>.Get();

            public void Return<T>(Stack<T> item)
                => StackPool<T>.Return(item);

            public void Return<T>(params Stack<T>[] items)
                => StackPool<T>.Return(items);

            public void Return<T>(IEnumerable<Stack<T>> items)
                => StackPool<T>.Return(items);

            public Dictionary<TKey, TValue> Dictionary<TKey, TValue>()
                => DictionaryPool<TKey, TValue>.Get();

            public void Return<TKey, TValue>(Dictionary<TKey, TValue> item)
                => DictionaryPool<TKey, TValue>.Return(item);

            public void Return<TKey, TValue>(params Dictionary<TKey, TValue>[] items)
                => DictionaryPool<TKey, TValue>.Return(items);

            public void Return<TKey, TValue>(IEnumerable<Dictionary<TKey, TValue>> items)
                => DictionaryPool<TKey, TValue>.Return(items);

            public ArrayDictionary<TKey, TValue> ArrayDictionary<TKey, TValue>()
                => ArrayDictionaryPool<TKey, TValue>.Get();

            public void Return<TKey, TValue>(ArrayDictionary<TKey, TValue> item)
                => ArrayDictionaryPool<TKey, TValue>.Return(item);

            public void Return<TKey, TValue>(params ArrayDictionary<TKey, TValue>[] items)
                => ArrayDictionaryPool<TKey, TValue>.Return(items);

            public void Return<TKey, TValue>(IEnumerable<ArrayDictionary<TKey, TValue>> items)
                => ArrayDictionaryPool<TKey, TValue>.Return(items);
        }
    }
}