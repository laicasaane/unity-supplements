using System.Collections.ArrayBased;
using System.Collections.Generic;

namespace System.Collections.Pooling
{
    public interface IPoolProvider
    {
        Pool<T> Pool<T>() where T : class, new();

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
    }
}