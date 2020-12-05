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
            => GetSource()[key];

        public ReadCollection<TKey> Keys
            => new ReadCollection<TKey>(GetSource().Keys);

        public ReadCollection<TValue> Values
            => new ReadCollection<TValue>(GetSource().Values);

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
            => GetSource().Keys;

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
            => GetSource().Values;

        public int Count => GetSource().Count;

        public ReadDictionary(Dictionary<TKey, TValue> source)
        {
            this.source = source ?? _empty;
            this.hasSource = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Dictionary<TKey, TValue> GetSource()
            => this.hasSource ? (this.source ?? _empty) : _empty;

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

        public bool ContainsKey(TKey key)
            => GetSource().ContainsKey(key);

        public bool ContainsValue(TValue value)
            => GetSource().ContainsValue(value);

        public bool TryGetValue(TKey key, out TValue value)
            => GetSource().TryGetValue(key, out value);

        public void GetObjectData(SerializationInfo info, StreamingContext context)
            => GetSource().GetObjectData(info, context);

        public void OnDeserialization(object sender)
            => GetSource().OnDeserialization(sender);

        public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
            => GetSource().GetEnumerator();

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
            => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private static Dictionary<TKey, TValue> _empty { get; } = new Dictionary<TKey, TValue>(0);

        public static ReadDictionary<TKey, TValue> Empty { get; } = new ReadDictionary<TKey, TValue>(_empty);

        public static implicit operator ReadDictionary<TKey, TValue>(Dictionary<TKey, TValue> source)
            => source == null ? Empty : new ReadDictionary<TKey, TValue>(source);

        public static bool operator ==(in ReadDictionary<TKey, TValue> a, in ReadDictionary<TKey, TValue> b)
            => a.Equals(in b);

        public static bool operator !=(in ReadDictionary<TKey, TValue> a, in ReadDictionary<TKey, TValue> b)
            => !a.Equals(in b);
    }
}