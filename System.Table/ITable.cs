using System.Collections.Generic;

namespace System.Table
{
    public interface IReadTable<T> : IEnumerable<ReadEntry<T>>
        where T : IEntry
    {
        int Count { get; }

        IEnumerable<T> Entries { get; }

        bool TryGetEntry(int id, out T entry);

        T GetEntry(int id = 0);

        bool ContainsId(int id);
    }

    public interface ITable<T> : IReadTable<T>
        where T : IEntry
    {
        void Clear();

        void Add(int id, T entry);

        void Add(T entry);

        void Add(T entry, bool autoIncrement);

        void Add(T entry, IGetId<T> idGetter);

        void Add(int id, in ReadEntry<T> entry);

        void Add(in ReadEntry<T> entry);

        void Add(in ReadEntry<T> entry, bool autoIncrement);

        void Add(in ReadEntry<T> entry, IGetId<T> idGetter);

        bool TryAdd(int id, T entry);

        bool TryAdd(T entry);

        bool TryAdd(T entry, IGetId<T> idGetter);

        bool TryAdd(int id, in ReadEntry<T> entry);

        bool TryAdd(in ReadEntry<T> entry);

        bool TryAdd(in ReadEntry<T> entry, IGetId<T> idGetter);

        void AddRange(T[] entries);

        void AddRange(T[] entries, bool autoIncrement);

        void AddRange(T[] entries, IGetId<T> idGetter);

        void AddRange(IEnumerable<T> entries);

        void AddRange(IEnumerable<T> entries, bool autoIncrement);

        void AddRange(IEnumerable<T> entries, IGetId<T> idGetter);

        void AddRange(ReadEntry<T>[] entries);

        void AddRange(ReadEntry<T>[] entries, bool autoIncrement);

        void AddRange(ReadEntry<T>[] entries, IGetId<T> idGetter);

        void AddRange(IEnumerable<ReadEntry<T>> entries);

        void AddRange(IEnumerable<ReadEntry<T>> entries, bool autoIncrement);

        void AddRange(IEnumerable<ReadEntry<T>> entries, IGetId<T> idGetter);

        void Remove(int id);

        void Remove(T entry);

        void Remove(in ReadEntry<T> entry);

        bool TryRemove(int id);

        bool TryRemove(T entry);

        bool TryRemove(in ReadEntry<T> entry);
    }
}