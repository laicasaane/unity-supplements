namespace System.Table
{
    public readonly struct Entry<T> where T : IEntry
    {
        public readonly int Id;
        public readonly T Data;

        public Entry(int id, T data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            this.Id = id;
            this.Data = data;
        }

        public static implicit operator T(in Entry<T> entry)
            => entry.Data;
    }
}