using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
    public readonly struct ReadDictionary<TKey, TValue> :
        IReadOnlyDictionary<TKey, TValue>,
        IEquatableReadOnlyStruct<ReadDictionary<TKey, TValue>>,
        IDeserializationCallback
    {
        private readonly Dictionary<TKey, TValue> source;
        private readonly bool hasSource;

        public TValue this[TKey key]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetSource()[key];
        }

        public ReadCollection<TKey> Keys
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new ReadCollection<TKey>(GetSource().Keys);
        }

        public ReadCollection<TValue> Values
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new ReadCollection<TValue>(GetSource().Values);
        }

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetSource().Keys;
        }

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetSource().Values;
        }

        public int Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetSource().Count;
        }

        public ReadDictionary(Dictionary<TKey, TValue> source)
        {
            this.source = source ?? _empty;
            this.hasSource = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Dictionary<TKey, TValue> GetSource()
            => this.hasSource ? (this.source ?? _empty) : _empty;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
            => GetSource().GetHashCode();

        public override bool Equals(object obj)
            => obj is ReadDictionary<TKey, TValue> other && Equals(in other);

        public bool Equals(ReadDictionary<TKey, TValue> other)
        {
            var source = GetSource();
            var otherSource = other.GetSource();

            return source == otherSource;
        }

        public bool Equals(in ReadDictionary<TKey, TValue> other)
        {
            var source = GetSource();
            var otherSource = other.GetSource();

            return source == otherSource;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(TKey key)
            => GetSource().ContainsKey(key);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsValue(TValue value)
            => GetSource().ContainsValue(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(TKey key, out TValue value)
            => GetSource().TryGetValue(key, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
            => GetSource().GetObjectData(info, context);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnDeserialization(object sender)
            => GetSource().OnDeserialization(sender);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
            => GetSource().GetEnumerator();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
            => GetEnumerator();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private static Dictionary<TKey, TValue> _empty { get; } = new Dictionary<TKey, TValue>(0);

        public static ReadDictionary<TKey, TValue> Empty { get; } = new ReadDictionary<TKey, TValue>(_empty);

        public static implicit operator ReadDictionary<TKey, TValue>(Dictionary<TKey, TValue> source)
            => source == null ? Empty : new ReadDictionary<TKey, TValue>(source);

        public static implicit operator ReadCollection<KeyValuePair<TKey, TValue>>(in ReadDictionary<TKey, TValue> source)
            => new ReadCollection<KeyValuePair<TKey, TValue>>(source.GetSource());

        public static bool operator ==(in ReadDictionary<TKey, TValue> a, in ReadDictionary<TKey, TValue> b)
            => a.Equals(in b);

        public static bool operator !=(in ReadDictionary<TKey, TValue> a, in ReadDictionary<TKey, TValue> b)
            => !a.Equals(in b);
    }
}