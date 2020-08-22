using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Table
{
    public readonly struct ReadTable<T> : IReadTable<T> where T : IEntry
    {
        public int Count
            => GetSource().Count;

        public IEnumerable<T> Entries
            => GetSource().Entries;

        private readonly Table<T> source;
        private readonly bool hasSource;

        public ReadTable(Table<T> table)
        {
            this.source = table ?? _empty;
            this.hasSource = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Table<T> GetSource()
            => this.hasSource ? this.source : _empty;

        public bool ContainsId(int id)
            => GetSource().ContainsId(id);

        public T GetEntry(int id = 0)
            => GetSource().GetEntry(id);

        public bool TryGetEntry(int id, out T entry)
            => GetSource().TryGetEntry(id, out entry);

        public Enumerator GetEnumerator()
            => new Enumerator(this.hasSource ? this : _empty);

        IEnumerator<ReadEntry<T>> IEnumerable<ReadEntry<T>>.GetEnumerator()
            => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public static implicit operator ReadTable<T>(Table<T> table)
            => new ReadTable<T>(table);

        private static Table<T> _empty { get; } = new Table<T>(0);

        public static ReadTable<T> Empty { get; } = new ReadTable<T>(_empty);

        public struct Enumerator : IEnumerator<ReadEntry<T>>
        {
            private readonly IEnumerator<ReadEntry<T>> source;

            internal Enumerator(in ReadTable<T> table)
            {
                this.source = table.GetSource().GetEnumerator();
            }

            public bool MoveNext()
                => this.source.MoveNext();

            public ReadEntry<T> Current
                => this.source.Current;

            object IEnumerator.Current
                => this.source.Current;

            void IEnumerator.Reset()
                => this.source.Reset();

            public void Dispose()
                => this.source.Dispose();
        }
    }
}