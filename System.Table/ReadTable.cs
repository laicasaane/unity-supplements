using System.Collections;
using System.Collections.Generic;

namespace System.Table
{
    public readonly struct ReadTable<T> : IReadTable<T> where T : IEntry
    {
        public int Count
            => this.table.Count;

        public IEnumerable<T> Entries
            => this.table.Entries;

        private readonly Table<T> table;

        public ReadTable(Table<T> table)
        {
            this.table = table ?? throw new ArgumentNullException(nameof(table));
        }

        public bool ContainsId(int id)
            => this.table.ContainsId(id);

        public IEnumerator<Entry<T>> GetEnumerator()
            => this.table.GetEnumerator();

        public T GetEntry(int id = 0)
            => this.table.GetEntry(id);

        public bool TryGetEntry(int id, out T entry)
            => this.table.TryGetEntry(id, out entry);

        IEnumerator IEnumerable.GetEnumerator()
            => this.table.GetEnumerator();

        public static implicit operator ReadTable<T>(Table<T> table)
            => new ReadTable<T>(table);
    }
}