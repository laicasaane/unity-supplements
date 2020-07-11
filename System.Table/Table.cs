using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Table
{
    public class Table<T> : ITable<T> where T : IEntry
    {
        private readonly Dictionary<int, T> table;

        public Table()
        {
            this.table = new Dictionary<int, T>();
        }

        public Table(int capacity)
        {
            this.table = new Dictionary<int, T>(capacity);
        }

        public Table(IDictionary<int, T> dictionary)
        {
            if (dictionary == null)
                throw new ArgumentNullException();

            this.table = new Dictionary<int, T>(dictionary);
        }

        public int Count
            => this.table.Count;

        public T this[int key]
            => this.table[key];

        public IEnumerable<T> Entries
            => this.table.Values;

        public void Clear()
            => this.table.Clear();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddInternal(int id, T entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            if (this.table.ContainsKey(id))
                throw new ArgumentException($"An entry with id={id} has already existed.");

            if (entry.Id != id)
                entry.SetId(id);

            this.table.Add(id, entry);
        }

        public void Add(int id, T entry)
            => AddInternal(id, entry);

        public void Add(T entry)
            => AddInternal(entry?.Id ?? 0, entry);

        public void Add(T entry, bool autoIncrement)
            => AddInternal(autoIncrement ? this.table.Count : entry.Id, entry);

        public void Add(T entry, IGetId<T> idGetter)
        {
            if (idGetter == null)
                throw new ArgumentNullException(nameof(idGetter));

            AddInternal(idGetter.GetId(entry), entry);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddInternal(int id, in ReadEntry<T> entry)
        {
            if (this.table.ContainsKey(id))
                throw new InvalidOperationException($"An entry with id={id} has already existed.");

            if (entry.Id != id)
                entry.Data.SetId(id);

            this.table.Add(id, entry.Data);
        }

        public void Add(int id, in ReadEntry<T> entry)
            => AddInternal(id, entry);

        public void Add(in ReadEntry<T> entry)
            => AddInternal(entry.Id, entry);

        public void Add(in ReadEntry<T> entry, bool autoIncrement)
            => AddInternal(autoIncrement ? this.table.Count : entry.Id, entry);

        public void Add(in ReadEntry<T> entry, IGetId<T> idGetter)
        {
            if (idGetter == null)
                throw new ArgumentNullException(nameof(idGetter));

            AddInternal(idGetter.GetId(entry), entry);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool TryAddInternal(int id, T entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            if (this.table.ContainsKey(id))
                return false;

            if (entry.Id != id)
                entry.SetId(id);

            this.table.Add(id, entry);
            return true;
        }

        public bool TryAdd(int id, T entry)
            => TryAddInternal(id, entry);

        public bool TryAdd(T entry)
            => TryAddInternal(entry?.Id ?? 0, entry);

        public bool TryAdd(T entry, IGetId<T> idGetter)
        {
            if (idGetter == null)
                throw new ArgumentNullException(nameof(idGetter));

            return TryAddInternal(idGetter.GetId(entry), entry);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool TryAddInternal(int id, in ReadEntry<T> entry)
        {
            if (this.table.ContainsKey(id))
                return false;

            if (entry.Id != id)
                entry.Data.SetId(id);

            this.table.Add(id, entry.Data);
            return true;
        }

        public bool TryAdd(int id, in ReadEntry<T> entry)
            => TryAddInternal(id, entry);

        public bool TryAdd(in ReadEntry<T> entry)
            => TryAddInternal(entry.Id, entry);

        public bool TryAdd(in ReadEntry<T> entry, IGetId<T> idGetter)
        {
            if (idGetter == null)
                throw new ArgumentNullException(nameof(idGetter));

            return TryAddInternal(idGetter.GetId(entry), entry);
        }

        public void AddRange(T[] entries)
            => AddRange(entries, false);

        public void AddRange(T[] entries, bool autoIncrement)
        {
            if (entries == null)
                throw new ArgumentNullException(nameof(entries));

            for (var i = 0; i < entries.Length; i++)
            {
                var entry = entries[i];
                AddInternal(autoIncrement ? this.table.Count : entry.Id, entry);
            }
        }

        public void AddRange(T[] entries, IGetId<T> idGetter)
        {
            if (entries == null)
                throw new ArgumentNullException(nameof(entries));

            if (idGetter == null)
                throw new ArgumentNullException(nameof(idGetter));

            for (var i = 0; i < entries.Length; i++)
            {
                var entry = entries[i];
                AddInternal(idGetter.GetId(entry), entry);
            }
        }

        public void AddRange(IEnumerable<T> entries)
            => AddRange(entries, false);

        public void AddRange(IEnumerable<T> entries, bool autoIncrement)
        {
            if (entries == null)
                throw new ArgumentNullException(nameof(entries));

            foreach (var entry in entries)
            {
                AddInternal(autoIncrement ? this.table.Count : entry.Id, entry);
            }
        }

        public void AddRange(IEnumerable<T> entries, IGetId<T> idGetter)
        {
            if (entries == null)
                throw new ArgumentNullException(nameof(entries));

            if (idGetter == null)
                throw new ArgumentNullException(nameof(idGetter));

            foreach (var entry in entries)
            {
                AddInternal(idGetter.GetId(entry), entry);
            }
        }

        public void AddRange(ReadEntry<T>[] entries)
            => AddRange(entries, false);

        public void AddRange(ReadEntry<T>[] entries, bool autoIncrement)
        {
            if (entries == null)
                throw new ArgumentNullException(nameof(entries));

            for (var i = 0; i < entries.Length; i++)
            {
                var entry = entries[i];
                AddInternal(autoIncrement ? this.table.Count : entry.Id, entry);
            }
        }

        public void AddRange(ReadEntry<T>[] entries, IGetId<T> idGetter)
        {
            if (entries == null)
                throw new ArgumentNullException(nameof(entries));

            if (idGetter == null)
                throw new ArgumentNullException(nameof(idGetter));

            for (var i = 0; i < entries.Length; i++)
            {
                var entry = entries[i];
                AddInternal(idGetter.GetId(entry), entry);
            }
        }

        public void AddRange(IEnumerable<ReadEntry<T>> entries)
            => AddRange(entries, false);

        public void AddRange(IEnumerable<ReadEntry<T>> entries, bool autoIncrement)
        {
            if (entries == null)
                throw new ArgumentNullException(nameof(entries));

            foreach (var entry in entries)
            {
                AddInternal(autoIncrement ? this.table.Count : entry.Id, entry);
            }
        }

        public void AddRange(IEnumerable<ReadEntry<T>> entries, IGetId<T> idGetter)
        {
            if (entries == null)
                throw new ArgumentNullException(nameof(entries));

            if (idGetter == null)
                throw new ArgumentNullException(nameof(idGetter));

            foreach (var entry in entries)
            {
                AddInternal(idGetter.GetId(entry), entry);
            }
        }

        public void Remove(int id)
        {
            if (!this.table.ContainsKey(id))
                throw new KeyNotFoundException($"The entry with id={id} was not present in the table.");

            this.table.Remove(id);
        }

        public void Remove(T entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            if (!this.table.ContainsKey(entry.Id))
                throw new KeyNotFoundException($"The entry with id={entry.Id} was not present in the table.");

            this.table.Remove(entry.Id);
        }

        public void Remove(in ReadEntry<T> entry)
        {
            if (!this.table.ContainsKey(entry.Id))
                throw new KeyNotFoundException($"The entry with id={entry.Id} was not present in the table.");

            this.table.Remove(entry.Id);
        }

        public bool TryRemove(int id)
        {
            if (!this.table.ContainsKey(id))
                return false;

            this.table.Remove(id);
            return true;
        }

        public bool TryRemove(T entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            if (!this.table.ContainsKey(entry.Id))
                return false;

            this.table.Remove(entry.Id);
            return true;
        }

        public bool TryRemove(in ReadEntry<T> entry)
        {
            if (!this.table.ContainsKey(entry.Id))
                return false;

            this.table.Remove(entry.Id);
            return true;
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

        public IEnumerator<ReadEntry<T>> GetEnumerator()
        {
            foreach (var kv in this.table)
            {
                yield return new ReadEntry<T>(kv.Key, kv.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}
