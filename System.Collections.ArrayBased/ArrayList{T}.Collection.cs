using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.ArrayBased
{
    public partial class ArrayList<T>
    {
        public Collection AsCollection()
            => new Collection(this);

        public static explicit operator Collection(ArrayList<T> source)
            => new Collection(source);

        public readonly struct Collection : ICollection<T>, IReadOnlyCollection<T>
        {
            private readonly ArrayList<T> source;

            public Collection(ArrayList<T> source)
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
                        return (int)this.source.count;
                    }
                }
            }

            public bool IsReadOnly => false;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Add(T item)
                => this.source.Add(item);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Clear()
                => this.source.Clear();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool Contains(T item)
                => this.source.Contains(item);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void CopyTo(T[] array, int arrayIndex)
                => this.source.CopyTo(array, arrayIndex);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool Remove(T item)
                => this.source.Remove(item);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Enumerator GetEnumerator()
                => new Enumerator(this.source);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            IEnumerator<T> IEnumerable<T>.GetEnumerator()
                => GetEnumerator();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            IEnumerator IEnumerable.GetEnumerator()
                => GetEnumerator();

            public struct Enumerator : IEnumerator<T>
            {
                private readonly ArrayList<T>.Enumerator source;

                public Enumerator(ArrayList<T> source)
                {
                    this.source = (source ?? Empty).GetEnumerator();
                }

                public T Current
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
                    => this.source.Dispose();

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