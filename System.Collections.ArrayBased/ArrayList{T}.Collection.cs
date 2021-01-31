using System.Collections.Generic;

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
                get
                {
                    unchecked
                    {
                        return (int)this.source.count;
                    }
                }
            }

            public bool IsReadOnly => false;

            public void Add(T item)
                => this.source.Add(item);

            public void Clear()
                => this.source.Clear();

            public bool Contains(T item)
                => this.source.Contains(item);

            public void CopyTo(T[] array, int arrayIndex)
                => this.source.CopyTo(array, arrayIndex);

            public bool Remove(T item)
                => this.source.Remove(item);

            public Enumerator GetEnumerator()
                => new Enumerator(this.source);

            IEnumerator<T> IEnumerable<T>.GetEnumerator()
                => GetEnumerator();

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
                    get => this.source.Current;
                }

                object IEnumerator.Current
                {
                    get => this.Current;
                }

                public void Dispose()
                    => this.source.Dispose();

                public bool MoveNext()
                    => this.source.MoveNext();

                public void Reset()
                    => this.source.Reset();
            }
        }
    }
}