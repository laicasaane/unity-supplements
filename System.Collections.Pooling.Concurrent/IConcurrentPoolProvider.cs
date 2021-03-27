using System.Collections.ArrayBased;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace System.Collections.Pooling.Concurrent
{
    public interface IConcurrentPoolProvider
    {
        ConcurrentPool<T> ConcurrentPool<T>() where T : class, new();

        T[] Array1<T>(int size);

        void Return<T>(T[] item);

        void Return<T>(params T[][] items);

        void Return<T>(IEnumerable<T[]> items);

        List<T> List<T>();

        void Return<T>(List<T> item);

        void Return<T>(params List<T>[] items);

        void Return<T>(IEnumerable<List<T>> items);

        ArrayList<T> ArrayList<T>();

        void Return<T>(ArrayList<T> item);

        void Return<T>(params ArrayList<T>[] items);

        void Return<T>(IEnumerable<ArrayList<T>> items);

        HashSet<T> HashSet<T>();

        void Return<T>(HashSet<T> item);

        void Return<T>(params HashSet<T>[] items);

        void Return<T>(IEnumerable<HashSet<T>> items);

        ArrayHashSet<T> ArrayHashSet<T>();

        void Return<T>(ArrayHashSet<T> item);

        void Return<T>(params ArrayHashSet<T>[] items);

        void Return<T>(IEnumerable<ArrayHashSet<T>> items);

        Queue<T> Queue<T>();

        void Return<T>(Queue<T> item);

        void Return<T>(params Queue<T>[] items);

        void Return<T>(IEnumerable<Queue<T>> items);

        Stack<T> Stack<T>();

        void Return<T>(Stack<T> item);

        void Return<T>(params Stack<T>[] items);

        void Return<T>(IEnumerable<Stack<T>> items);

        Dictionary<TKey, TValue> Dictionary<TKey, TValue>();

        void Return<TKey, TValue>(Dictionary<TKey, TValue> item);

        void Return<TKey, TValue>(params Dictionary<TKey, TValue>[] items);

        void Return<TKey, TValue>(IEnumerable<Dictionary<TKey, TValue>> items);

        ArrayDictionary<TKey, TValue> ArrayDictionary<TKey, TValue>();

        void Return<TKey, TValue>(ArrayDictionary<TKey, TValue> item);

        void Return<TKey, TValue>(params ArrayDictionary<TKey, TValue>[] items);

        void Return<TKey, TValue>(IEnumerable<ArrayDictionary<TKey, TValue>> items);

        ConcurrentBag<T> ConcurrentBag<T>();

        void Return<T>(ConcurrentBag<T> item);

        void Return<T>(params ConcurrentBag<T>[] items);

        void Return<T>(IEnumerable<ConcurrentBag<T>> items);

        ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>();

        void Return<TKey, TValue>(ConcurrentDictionary<TKey, TValue> item);

        void Return<TKey, TValue>(params ConcurrentDictionary<TKey, TValue>[] items);

        void Return<TKey, TValue>(IEnumerable<ConcurrentDictionary<TKey, TValue>> items);

        ConcurrentQueue<T> ConcurrentQueue<T>();

        void Return<T>(ConcurrentQueue<T> item);

        void Return<T>(params ConcurrentQueue<T>[] items);

        void Return<T>(IEnumerable<ConcurrentQueue<T>> items);

        ConcurrentStack<T> ConcurrentStack<T>();

        void Return<T>(ConcurrentStack<T> item);

        void Return<T>(params ConcurrentStack<T>[] items);

        void Return<T>(IEnumerable<ConcurrentStack<T>> items);
    }
}