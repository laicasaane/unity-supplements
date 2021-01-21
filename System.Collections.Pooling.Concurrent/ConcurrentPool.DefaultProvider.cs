using System.Collections.ArrayBased;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace System.Collections.Pooling.Concurrent
{
    public static partial class ConcurrentPool
    {
        private readonly struct DefaultProvider : IConcurrentPoolProvider
        {
            public ConcurrentPool<T> ConcurrentPool<T>() where T : class, new()
            => Concurrent.ConcurrentPool<T>.Default;

            public T[] Array1<T>(int size)
                => Array1ConcurrentPool<T>.Get(size);

            public void Return<T>(T[] item)
                => Array1ConcurrentPool<T>.Return(item);

            public void Return<T>(params T[][] items)
                => Array1ConcurrentPool<T>.Return(items);

            public void Return<T>(IEnumerable<T[]> items)
                => Array1ConcurrentPool<T>.Return(items);

            public List<T> List<T>()
                => ListConcurrentPool<T>.Get();

            public void Return<T>(List<T> item)
                => ListConcurrentPool<T>.Return(item);

            public void Return<T>(params List<T>[] items)
                => ListConcurrentPool<T>.Return(items);

            public void Return<T>(IEnumerable<List<T>> items)
                => ListConcurrentPool<T>.Return(items);

            public ArrayList<T> ArrayList<T>()
                => ArrayListConcurrentPool<T>.Get();

            public void Return<T>(ArrayList<T> item)
                => ArrayListConcurrentPool<T>.Return(item);

            public void Return<T>(params ArrayList<T>[] items)
                => ArrayListConcurrentPool<T>.Return(items);

            public void Return<T>(IEnumerable<ArrayList<T>> items)
                => ArrayListConcurrentPool<T>.Return(items);

            public HashSet<T> HashSet<T>()
                => HashSetConcurrentPool<T>.Get();

            public void Return<T>(HashSet<T> item)
                => HashSetConcurrentPool<T>.Return(item);

            public void Return<T>(params HashSet<T>[] items)
                => HashSetConcurrentPool<T>.Return(items);

            public void Return<T>(IEnumerable<HashSet<T>> items)
                => HashSetConcurrentPool<T>.Return(items);

            public Queue<T> Queue<T>()
                => QueueConcurrentPool<T>.Get();

            public void Return<T>(Queue<T> item)
                => QueueConcurrentPool<T>.Return(item);

            public void Return<T>(params Queue<T>[] items)
                => QueueConcurrentPool<T>.Return(items);

            public void Return<T>(IEnumerable<Queue<T>> items)
                => QueueConcurrentPool<T>.Return(items);

            public Stack<T> Stack<T>()
                => StackConcurrentPool<T>.Get();

            public void Return<T>(Stack<T> item)
                => StackConcurrentPool<T>.Return(item);

            public void Return<T>(params Stack<T>[] items)
                => StackConcurrentPool<T>.Return(items);

            public void Return<T>(IEnumerable<Stack<T>> items)
                => StackConcurrentPool<T>.Return(items);

            public Dictionary<TKey, TValue> Dictionary<TKey, TValue>()
                => DictionaryConcurrentPool<TKey, TValue>.Get();

            public void Return<TKey, TValue>(Dictionary<TKey, TValue> item)
                => DictionaryConcurrentPool<TKey, TValue>.Return(item);

            public void Return<TKey, TValue>(params Dictionary<TKey, TValue>[] items)
                => DictionaryConcurrentPool<TKey, TValue>.Return(items);

            public void Return<TKey, TValue>(IEnumerable<Dictionary<TKey, TValue>> items)
                => DictionaryConcurrentPool<TKey, TValue>.Return(items);

            public ArrayDictionary<TKey, TValue> ArrayDictionary<TKey, TValue>()
                => ArrayDictionaryConcurrentPool<TKey, TValue>.Get();

            public void Return<TKey, TValue>(ArrayDictionary<TKey, TValue> item)
                => ArrayDictionaryConcurrentPool<TKey, TValue>.Return(item);

            public void Return<TKey, TValue>(params ArrayDictionary<TKey, TValue>[] items)
                => ArrayDictionaryConcurrentPool<TKey, TValue>.Return(items);

            public void Return<TKey, TValue>(IEnumerable<ArrayDictionary<TKey, TValue>> items)
                => ArrayDictionaryConcurrentPool<TKey, TValue>.Return(items);

            public ConcurrentBag<T> ConcurrentBag<T>()
                => ConcurrentBagPool<T>.Get();

            public void Return<T>(ConcurrentBag<T> item)
                => ConcurrentBagPool<T>.Return(item);

            public void Return<T>(params ConcurrentBag<T>[] items)
                => ConcurrentBagPool<T>.Return(items);

            public void Return<T>(IEnumerable<ConcurrentBag<T>> items)
                => ConcurrentBagPool<T>.Return(items);

            public ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>()
                => ConcurrentDictionaryPool<TKey, TValue>.Get();

            public void Return<TKey, TValue>(ConcurrentDictionary<TKey, TValue> item)
                => ConcurrentDictionaryPool<TKey, TValue>.Return(item);

            public void Return<TKey, TValue>(params ConcurrentDictionary<TKey, TValue>[] items)
                => ConcurrentDictionaryPool<TKey, TValue>.Return(items);

            public void Return<TKey, TValue>(IEnumerable<ConcurrentDictionary<TKey, TValue>> items)
                => ConcurrentDictionaryPool<TKey, TValue>.Return(items);

            public ConcurrentQueue<T> ConcurrentQueue<T>()
                => ConcurrentQueuePool<T>.Get();

            public void Return<T>(ConcurrentQueue<T> item)
                => ConcurrentQueuePool<T>.Return(item);

            public void Return<T>(params ConcurrentQueue<T>[] items)
                => ConcurrentQueuePool<T>.Return(items);

            public void Return<T>(IEnumerable<ConcurrentQueue<T>> items)
                => ConcurrentQueuePool<T>.Return(items);

            public ConcurrentStack<T> ConcurrentStack<T>()
                => ConcurrentStackPool<T>.Get();

            public void Return<T>(ConcurrentStack<T> item)
                => ConcurrentStackPool<T>.Return(item);

            public void Return<T>(params ConcurrentStack<T>[] items)
                => ConcurrentStackPool<T>.Return(items);

            public void Return<T>(IEnumerable<ConcurrentStack<T>> items)
                => ConcurrentStackPool<T>.Return(items);
        }
    }
}