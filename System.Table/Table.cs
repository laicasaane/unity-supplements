using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Table
{
    public class Table<T> : ITable<T> where T : IEntry
    {
        private readonly Dictionary<int, T> table
            = new Dictionary<int, T>();

        public int Count
            => this.table.Count;

        public T this[int key]
            => this.table[key];

        public IEnumerable<T> Entries
            => this.table.Values;

        public void Clear()
            => this.table.Clear();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Add(int id, T entry)
        {
            if (this.table.ContainsKey(id))
                throw new ArgumentException($"An entry with id={id} has already existed.");

            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            if (entry.Id != id)
                entry.SetId(id);

            this.table.Add(id, entry);
        }

        public void Insert(T entry)
            => Insert(entry, false);

        public void Insert(T entry, bool autoIncrement)
            => Add(autoIncrement ? this.table.Count : entry.Id, entry);

        public void Insert(T entry, IGetId<T> idGetter)
        {
            if (idGetter == null)
                throw new ArgumentNullException(nameof(idGetter));

            Add(idGetter.GetId(entry), entry);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Add(int id, in Entry<T> entry)
        {
            if (this.table.ContainsKey(id))
                throw new InvalidOperationException($"An entry with id={id} has already existed.");

            if (entry.Id != id)
                entry.Data.SetId(id);

            this.table.Add(id, entry.Data);
        }

        public void Insert(in Entry<T> entry)
            => Insert(entry, false);

        public void Insert(in Entry<T> entry, bool autoIncrement)
            => Add(autoIncrement ? this.table.Count : entry.Id, entry);

        public void Insert(in Entry<T> entry, IGetId<T> idGetter)
        {
            if (idGetter == null)
                throw new ArgumentNullException(nameof(idGetter));

            Add(idGetter.GetId(entry), entry);
        }

        public void Insert(T[] entries)
            => Insert(entries, false);

        public void Insert(T[] entries, bool autoIncrement)
        {
            if (entries == null)
                throw new ArgumentNullException(nameof(entries));

            for (var i = 0; i < entries.Length; i++)
            {
                var entry = entries[i];
                Add(autoIncrement ? this.table.Count : entry.Id, entry);
            }
        }

        public void Insert(T[] entries, IGetId<T> idGetter)
        {
            if (entries == null)
                throw new ArgumentNullException(nameof(entries));

            if (idGetter == null)
                throw new ArgumentNullException(nameof(idGetter));

            for (var i = 0; i < entries.Length; i++)
            {
                var entry = entries[i];
                Add(idGetter.GetId(entry), entry);
            }
        }

        public void Insert(IEnumerable<T> entries)
            => Insert(entries, false);

        public void Insert(IEnumerable<T> entries, bool autoIncrement)
        {
            if (entries == null)
                throw new ArgumentNullException(nameof(entries));

            foreach (var entry in entries)
            {
                Add(autoIncrement ? this.table.Count : entry.Id, entry);
            }
        }

        public void Insert(IEnumerable<T> entries, IGetId<T> idGetter)
        {
            if (entries == null)
                throw new ArgumentNullException(nameof(entries));

            if (idGetter == null)
                throw new ArgumentNullException(nameof(idGetter));

            foreach (var entry in entries)
            {
                Add(idGetter.GetId(entry), entry);
            }
        }

        public void Insert(Entry<T>[] entries)
            => Insert(entries, false);

        public void Insert(Entry<T>[] entries, bool autoIncrement)
        {
            if (entries == null)
                throw new ArgumentNullException(nameof(entries));

            for (var i = 0; i < entries.Length; i++)
            {
                var entry = entries[i];
                Add(autoIncrement ? this.table.Count : entry.Id, entry);
            }
        }

        public void Insert(Entry<T>[] entries, IGetId<T> idGetter)
        {
            if (entries == null)
                throw new ArgumentNullException(nameof(entries));

            if (idGetter == null)
                throw new ArgumentNullException(nameof(idGetter));

            for (var i = 0; i < entries.Length; i++)
            {
                var entry = entries[i];
                Add(idGetter.GetId(entry), entry);
            }
        }

        public void Insert(IEnumerable<Entry<T>> entries)
            => Insert(entries, false);

        public void Insert(IEnumerable<Entry<T>> entries, bool autoIncrement)
        {
            if (entries == null)
                throw new ArgumentNullException(nameof(entries));

            foreach (var entry in entries)
            {
                Add(autoIncrement ? this.table.Count : entry.Id, entry);
            }
        }

        public void Insert(IEnumerable<Entry<T>> entries, IGetId<T> idGetter)
        {
            if (entries == null)
                throw new ArgumentNullException(nameof(entries));

            if (idGetter == null)
                throw new ArgumentNullException(nameof(idGetter));

            foreach (var entry in entries)
            {
                Add(idGetter.GetId(entry), entry);
            }
        }

        public void Remove(int id)
        {
            if (this.table.ContainsKey(id))
                this.table.Remove(id);
        }

        public void Remove(T entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            if (this.table.ContainsKey(entry.Id))
                this.table.Remove(entry.Id);
        }

        public void Remove(in Entry<T> entry)
        {
            if (this.table.ContainsKey(entry.Id))
                this.table.Remove(entry.Id);
        }

        public T GetEntry(int id = 0)
        {
            if (this.table.ContainsKey(id))
                return this.table[id];

            throw new KeyNotFoundException($"The entry with id={id} was not present in the table.");
        }

        public bool TryGetEntry(int id, out T entry)
            => this.table.TryGetValue(id, out entry);

        public bool ContainsId(int id)
            => this.table.ContainsKey(id);

        public IEnumerator<Entry<T>> GetEnumerator()
        {
            foreach (var kv in this.table)
            {
                yield return new Entry<T>(kv.Key, kv.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}
