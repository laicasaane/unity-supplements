using System.Linq;
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

        public ReadDictionary(Dictionary<TKey, TValue> source)
        {
            this.source = source ?? _empty;
            this.Count = this.source.Count;
            this.hasSource = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Dictionary<TKey, TValue> GetSource()
            => this.hasSource ? this.source : _empty;

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

        public int Count { get; }

        public override int GetHashCode()
            => GetSource().GetHashCode();

        public override bool Equals(object obj)
            => obj is ReadDictionary<TKey, TValue> other && Equals(in other);

        public bool Equals(ReadDictionary<TKey, TValue> other)
        {
            if (this.source == null && other.source == null)
                return true;

            if (this.source == null || other.source == null)
                return false;

            return ReferenceEquals(this.source, other.source);
        }

        public bool Equals(in ReadDictionary<TKey, TValue> other)
        {
            if (this.source == null && other.source == null)
                return true;

            if (this.source == null || other.source == null)
                return false;

            return ReferenceEquals(this.source, other.source);
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

        public Enumerator GetEnumerator()
            => new Enumerator(GetSource());

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

        public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDictionaryEnumerator
        {
            private readonly TKey[] keys;
            private readonly TValue[] values;
            private readonly int count;

            private int index;
            private KeyValuePair<TKey, TValue> current;

            internal Enumerator(in ReadDictionary<TKey, TValue> dictionary)
            {
                var dict = dictionary.GetSource();
                this.keys = dict.Keys.ToArray();
                this.values = dict.Values.ToArray();
                this.count = dict.Count;

                this.index = 0;
                this.current = new KeyValuePair<TKey, TValue>();
            }

            public bool MoveNext()
            {
                // Use unsigned comparison since we set index to this.count+1 when the enumeration ends.
                // this.count+1 could be negative if this.count is Int32.MaxValue
                while ((uint)this.index < (uint)this.count)
                {
                    this.current = new KeyValuePair<TKey, TValue>(this.keys[this.index], this.values[this.index]);
                    this.index++;
                    return true;
                }

                this.index = this.count + 1;
                this.current = new KeyValuePair<TKey, TValue>();
                return false;
            }

            public KeyValuePair<TKey, TValue> Current
            {
                get { return this.current; }
            }

            public void Dispose() { }

            object IEnumerator.Current
            {
                get
                {
                    if (this.index == 0 || (this.index == this.count + 1))
                    {
                        throw ThrowHelper.GetInvalidOperationException_InvalidOperation_EnumOpCantHappen();
                    }

                    return new KeyValuePair<TKey, TValue>(this.current.Key, this.current.Value);
                }
            }

            void IEnumerator.Reset()
            {
                this.index = 0;
                this.current = new KeyValuePair<TKey, TValue>();
            }

            DictionaryEntry IDictionaryEnumerator.Entry
            {
                get
                {
                    if (this.index == 0 || (this.index == this.count + 1))
                    {
                        throw ThrowHelper.GetInvalidOperationException_InvalidOperation_EnumOpCantHappen();
                    }

                    return new DictionaryEntry(this.current.Key, this.current.Value);
                }
            }

            object IDictionaryEnumerator.Key
            {
                get
                {
                    if (this.index == 0 || (this.index == this.count + 1))
                    {
                        throw ThrowHelper.GetInvalidOperationException_InvalidOperation_EnumOpCantHappen();
                    }

                    return this.current.Key;
                }
            }

            object IDictionaryEnumerator.Value
            {
                get
                {
                    if (this.index == 0 || (this.index == this.count + 1))
                    {
                        throw ThrowHelper.GetInvalidOperationException_InvalidOperation_EnumOpCantHappen();
                    }

                    return this.current.Value;
                }
            }
        }
    }
}