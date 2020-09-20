using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.Grid
{
    [Serializable]
    public readonly struct UIntRange : IEquatableReadOnlyStruct<UIntRange>, ISerializable
    {
        public readonly uint Start;
        public readonly uint End;

        public UIntRange(uint start, uint end)
        {
            if (end < start)
                throw new InvalidOperationException($"{nameof(end)} must be greater than or equal to {nameof(start)}");

            this.Start = start;
            this.End = end;
        }

        private UIntRange(SerializationInfo info, StreamingContext context)
        {
            try
            {
                this.Start = info.GetUInt32(nameof(this.Start));
            }
            catch
            {
                this.Start = default;
            }

            try
            {
                this.End = info.GetUInt32(nameof(this.End));
            }
            catch
            {
                this.End = default;
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Start), this.Start);
            info.AddValue(nameof(this.End), this.End);
        }

        public void Deconstruct(out uint start, out uint end)
        {
            start = this.Start;
            end = this.End;
        }

        public UIntRange With(in uint? Start = null, in uint? End = null)
            => new UIntRange(
                Start ?? this.Start,
                End ?? this.End
            );

        public override bool Equals(object obj)
            => obj is UIntRange other &&
               this.Start.Equals(other.Start) &&
               this.End.Equals(other.End);

        public bool Equals(in UIntRange other)
            => this.Start.Equals(other.Start) &&
               this.End.Equals(other.End);

        public bool Equals(UIntRange other)
            => this.Start.Equals(other.Start) &&
               this.End.Equals(other.End);

        public override int GetHashCode()
        {
            var hashCode = -1676728671;
            hashCode = hashCode * -1521134295 + this.Start.GetHashCode();
            hashCode = hashCode * -1521134295 + this.End.GetHashCode();
            return hashCode;
        }

        public Enumerator GetEnumerator()
            => new Enumerator(this);

        /// <summary>
        /// Automatically create a range from (a, b).
        /// If a <= b, then a is the start value, and b is the end value.
        /// Otherwise, they are swapped.
        /// </summary>
        public static UIntRange Auto(uint a, uint b)
            => a > b ? new UIntRange(b, a) : new UIntRange(a, b);

        public static UIntRange Count(uint size)
            => new UIntRange(0, size - 1);

        public static implicit operator UIntRange(in (uint start, uint end) value)
            => new UIntRange(value.start, value.end);

        public static implicit operator ReadRange<uint, Enumerator>(in UIntRange value)
            => new ReadRange<uint, Enumerator>(value.Start, value.End);

        public static implicit operator ReadRange<uint>(in UIntRange value)
            => new ReadRange<uint>(value.Start, value.End, new Enumerator());

        public static implicit operator UIntRange(in ReadRange<uint> value)
            => new UIntRange(value.Start, value.End);

        public static implicit operator UIntRange(in ReadRange<uint, Enumerator> value)
            => new UIntRange(value.Start, value.End);

        public static bool operator ==(in UIntRange lhs, in UIntRange rhs)
            => lhs.Equals(in rhs);

        public static bool operator !=(in UIntRange lhs, in UIntRange rhs)
            => !lhs.Equals(in rhs);

        public struct Enumerator : IEnumerator<uint>, IRangeEnumerator<uint>
        {
            private readonly UIntRange range;

            private uint current;
            private sbyte flag;

            public Enumerator(in UIntRange range)
            {
                this.range = range;
                this.current = range.Start;
                this.flag = -1;
            }

            public bool MoveNext()
            {
                if (this.flag > 0)
                    return false;

                if (this.flag < 0)
                {
                    this.flag = 0;
                    return true;
                }

                if (this.current == this.range.End)
                {
                    this.flag = 1;
                    return false;
                }

                this.current += 1;
                return true;
            }

            public uint Current
            {
                get
                {
                    if (this.flag < 0)
                        throw ThrowHelper.GetInvalidOperationException_InvalidOperation_EnumNotStarted();

                    if (this.flag > 0)
                        throw ThrowHelper.GetInvalidOperationException_InvalidOperation_EnumEnded();

                    return this.current;
                }
            }

            public void Dispose()
            {
            }

            object IEnumerator.Current
                => this.Current;

            void IEnumerator.Reset()
            {
                this.current = this.range.Start;
                this.flag = -1;
            }

            public IEnumerator<uint> Enumerate(uint start, uint end)
            {
                for (var i = start; i <= end; i++)
                {
                    yield return i;
                }
            }
        }
    }
}
