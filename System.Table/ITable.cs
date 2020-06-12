using System.Collections.Generic;

namespace System.Table
{
    public interface IReadTable<T> : IEnumerable<Entry<T>>
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

        void Insert(T entry);

        void Insert(T entry, bool autoIncrement);

        void Insert(T entry, IGetId<T> idGetter);

        void Insert(in Entry<T> entry);

        void Insert(in Entry<T> entry, bool autoIncrement);

        void Insert(in Entry<T> entry, IGetId<T> idGetter);

        void Insert(T[] entries);

        void Insert(T[] entries, bool autoIncrement);

        void Insert(T[] entries, IGetId<T> idGetter);

        void Insert(IEnumerable<T> entries);

        void Insert(IEnumerable<T> entries, bool autoIncrement);

        void Insert(IEnumerable<T> entries, IGetId<T> idGetter);

        void Insert(Entry<T>[] entries);

        void Insert(Entry<T>[] entries, bool autoIncrement);

        void Insert(Entry<T>[] entries, IGetId<T> idGetter);

        void Insert(IEnumerable<Entry<T>> entries);

        void Insert(IEnumerable<Entry<T>> entries, bool autoIncrement);

        void Insert(IEnumerable<Entry<T>> entries, IGetId<T> idGetter);

        void Remove(int id);

        void Remove(T entry);

        void Remove(in Entry<T> entry);
    }
}