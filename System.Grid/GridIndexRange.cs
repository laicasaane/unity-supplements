using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.Grid
{
    [Serializable]
    public readonly struct GridIndexRange : IEquatableReadOnlyStruct<GridIndexRange>, ISerializable
    {
        public readonly GridIndex Start;
        public readonly GridIndex End;

        public GridIndexRange(in GridIndex start, in GridIndex end)
        {
            this.Start = start;
            this.End = end;
        }

        public void Deconstruct(out GridIndex start, out GridIndex end)
        {
            start = this.Start;
            end = this.End;
        }

        public GridIndexRange With(in GridIndex? Start = null, in GridIndex? End = null)
            => new GridIndexRange(
                Start ?? this.Start,
                End ?? this.End
            );

        public override bool Equals(object obj)
            => obj is GridIndexRange other &&
               this.Start.Equals(other.Start) &&
               this.End.Equals(other.End);

        public bool Equals(in GridIndexRange other)
            => this.Start.Equals(other.Start) &&
               this.End.Equals(other.End);

        public bool Equals(GridIndexRange other)
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

        public static implicit operator GridIndexRange(in (GridIndex start, GridIndex end) value)
            => new GridIndexRange(value.start, value.end);

        public static implicit operator ReadRange<GridIndex, Enumerator>(in GridIndexRange value)
            => new ReadRange<GridIndex, Enumerator>(value.Start, value.End);

        public static implicit operator ReadRange<GridIndex>(in GridIndexRange value)
            => new ReadRange<GridIndex>(value.Start, value.End, new Enumerator());

        public static implicit operator GridIndexRange(in ReadRange<GridIndex> value)
            => new GridIndexRange(value.Start, value.End);

        public static implicit operator GridIndexRange(in ReadRange<GridIndex, Enumerator> value)
            => new GridIndexRange(value.Start, value.End);

        public static bool operator ==(in GridIndexRange lhs, in GridIndexRange rhs)
            => lhs.Equals(in rhs);

        public static bool operator !=(in GridIndexRange lhs, in GridIndexRange rhs)
            => !lhs.Equals(in rhs);

        private GridIndexRange(SerializationInfo info, StreamingContext context)
        {
            try
            {
                this.Start = (GridIndex)info.GetValue(nameof(this.Start), typeof(GridIndex));
            }
            catch
            {
                this.Start = default;
            }

            try
            {
                this.End = (GridIndex)info.GetValue(nameof(this.End), typeof(GridIndex));
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

        public struct Enumerator : IEnumerator<GridIndex>, IRangeEnumerator<GridIndex>
        {
            private readonly GridIndexRange range;

            private GridIndex current;
            private sbyte flag;

            public Enumerator(in GridIndexRange range)
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

                var col = this.current.Column + 1;
                var row = this.current.Row;

                if (col > this.range.End.Column)
                {
                    row += 1;
                    col = this.range.Start.Column;
                }

                this.current = new GridIndex(row, col);
                return true;
            }

            public GridIndex Current
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

            public IEnumerator<GridIndex> Enumerate(GridIndex start, GridIndex end)
            {
                for (var r = start.Row; r <= end.Row; r++)
                {
                    for (var c = start.Column; c <= end.Column; c++)
                    {
                        yield return new GridIndex(r, c);
                    }
                }
            }
        }
    }
}
