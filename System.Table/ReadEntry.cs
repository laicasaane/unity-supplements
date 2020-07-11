namespace System.Table
{
    public readonly struct ReadEntry<T> where T : IEntry
    {
        public readonly int Id;
        public readonly T Data;

        public ReadEntry(int id, T data)
        {
            this.Id = id;
            this.Data = data;
        }

        public static implicit operator T(in ReadEntry<T> entry)
            => entry.Data;

        public static implicit operator ReadEntry<T>(in (int, T) entry)
            => new ReadEntry<T>(entry.Item1, entry.Item2);
    }
}