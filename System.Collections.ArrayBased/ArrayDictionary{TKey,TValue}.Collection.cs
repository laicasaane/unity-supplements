using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.ArrayBased
{
    public partial class ArrayDictionary<TKey, TValue>
    {
        public Collection AsCollection()
            => new Collection(this);

        public static explicit operator Collection(ArrayDictionary<TKey, TValue> source)
            => new Collection(source);

        public readonly struct Collection : ICollection<KeyValuePair<TKey, TValue>>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
        {
            private readonly ArrayDictionary<TKey, TValue> source;

            public Collection(ArrayDictionary<TKey, TValue> source)
            {
                this.source = source ?? throw new ArgumentNullException(nameof(source));
            }

            public int Count
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    unchecked
                    {
                        return (int)this.source.Count;
                    }
                }
            }

            public bool IsReadOnly => false;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Add(KeyValuePair<TKey, TValue> item)
                => this.source.Add(item.Key, item.Value);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Clear()
                => this.source.Clear();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool Contains(KeyValuePair<TKey, TValue> item)
            {
                if (!this.source.TryGetValue(item.Key, out var value))
                    return false;

                return EqualityComparer<TValue>.Default.Equals(value, item.Value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
                => this.source.CopyTo(array, arrayIndex);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool Remove(KeyValuePair<TKey, TValue> item)
            {
                if (!this.source.TryGetValue(item.Key, out var value))
                    return false;

                if (!EqualityComparer<TValue>.Default.Equals(value, item.Value))
                    return false;

                this.source.Remove(item.Key);
                return true;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Enumerator GetEnumerator()
                => new Enumerator(this.source);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
                => GetEnumerator();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            IEnumerator IEnumerable.GetEnumerator()
                => GetEnumerator();

            public readonly struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>
            {
                private readonly ArrayDictionary<TKey, TValue>.Enumerator source;

                public Enumerator(ArrayDictionary<TKey, TValue> source)
                {
                    this.source = (source ?? Empty).GetEnumerator();
                }

                public KeyValuePair<TKey, TValue> Current
                {
                    [MethodImpl(MethodImplOptions.AggressiveInlining)]
                    get => this.source.Current;
                }

                object IEnumerator.Current
                {
                    [MethodImpl(MethodImplOptions.AggressiveInlining)]
                    get => this.Current;
                }

                public void Dispose()
                {
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool MoveNext()
                    => this.source.MoveNext();

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public void Reset()
                    => this.source.Reset();
            }
        }
    }
}