﻿using System.Collections.ArrayBased;
using System.Collections.Generic;

namespace System.Collections.Pooling
{
    public static class ArrayDictionaryPool<TKey, TValue>
    {
        private static readonly Pool<ArrayDictionary<TKey, TValue>> _pool = new Pool<ArrayDictionary<TKey, TValue>>();

        public static ArrayDictionary<TKey, TValue> Get()
            => _pool.Get();

        public static void Return(ArrayDictionary<TKey, TValue> item)
        {
            if (item == null)
                return;

            item.Clear();
            _pool.Return(item);
        }

        public static void Return(params ArrayDictionary<TKey, TValue>[] items)
        {
            if (items == null)
                return;

            foreach (var item in items)
            {
                if (item == null)
                    continue;

                item.Clear();
                _pool.Return(item);
            }
        }

        public static void Return(IEnumerable<ArrayDictionary<TKey, TValue>> items)
        {
            if (items == null)
                return;

            foreach (var item in items)
            {
                if (item == null)
                    continue;

                item.Clear();
                _pool.Return(item);
            }
        }
    }
}