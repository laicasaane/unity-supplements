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
        public readonly bool IsFromEnd;

        public GridIndexRange(in GridIndex start, in GridIndex end)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = false;
        }

        public GridIndexRange(in GridIndex start, in GridIndex end, bool fromEnd)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = fromEnd;
        }

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

            try
            {
                this.IsFromEnd = info.GetBoolean(nameof(this.IsFromEnd));
            }
            catch
            {
                this.IsFromEnd = default;
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Start), this.Start);
            info.AddValue(nameof(this.End), this.End);
            info.AddValue(nameof(this.IsFromEnd), this.IsFromEnd);
        }

        public void Deconstruct(out GridIndex start, out GridIndex end)
        {
            start = this.Start;
            end = this.End;
        }

        public void Deconstruct(out GridIndex start, out GridIndex end, out bool fromEnd)
        {
            start = this.Start;
            end = this.End;
            fromEnd = this.IsFromEnd;
        }

        public GridIndexRange With(in GridIndex? Start = null, in GridIndex? End = null, in bool? IsFromEnd = null)
            => new GridIndexRange(
                Start ?? this.Start,
                End ?? this.End,
                IsFromEnd ?? this.IsFromEnd
            );

        public override bool Equals(object obj)
            => obj is GridIndexRange other &&
               this.Start.Equals(in other.Start) &&
               this.End.Equals(in other.End) &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(in GridIndexRange other)
            => this.Start.Equals(in other.Start) &&
               this.End.Equals(in other.End) &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(GridIndexRange other)
            => this.Start.Equals(in other.Start) &&
               this.End.Equals(in other.End) &&
               this.IsFromEnd == other.IsFromEnd;

        public override int GetHashCode()
        {
            var hashCode = -1418356749;
            hashCode = hashCode * -1521134295 + this.Start.GetHashCode();
            hashCode = hashCode * -1521134295 + this.End.GetHashCode();
            hashCode = hashCode * -1521134295 + this.IsFromEnd.GetHashCode();
            return hashCode;
        }

        public override string ToString()
            => $"{{ {nameof(this.Start)}={this.Start}, {nameof(this.End)}={this.End}, {nameof(this.IsFromEnd)}={this.IsFromEnd} }}";

        public Enumerator GetEnumerator()
            => new Enumerator(this);

        /// <summary>
        /// Automatically create a range from (a, b).
        /// The components of a and b are compared, the lowest value will be the start, the highest value will be the end.
        /// </summary>
        public static GridIndexRange Auto(in GridIndex a, in GridIndex b)
        {
            var rowIncreasing = a.Row.CompareTo(b.Row) <= 0;
            var colIncreasing = a.Column.CompareTo(b.Column) <= 0;

            var start = new GridIndex(
                rowIncreasing ? a.Row : b.Row,
                colIncreasing ? a.Column : b.Column
            );

            var end = new GridIndex(
                rowIncreasing ? b.Row : a.Row,
                colIncreasing ? b.Column : a.Column
            );

            return new GridIndexRange(start, end);
        }

        public static GridIndexRange Count(in GridIndex value)
            => new GridIndexRange(GridIndex.Zero, value - GridIndex.One);

        public static GridIndexRange FromStart(in GridIndex start, in GridIndex end)
            => new GridIndexRange(start, end, false);

        public static GridIndexRange FromEnd(in GridIndex start, in GridIndex end)
            => new GridIndexRange(start, end, true);

        public static implicit operator GridIndexRange(in (GridIndex start, GridIndex end) value)
            => new GridIndexRange(value.start, value.end);

        public static implicit operator GridIndexRange(in (GridIndex start, GridIndex end, bool fromEnd) value)
            => new GridIndexRange(value.start, value.end, value.fromEnd);

        public static implicit operator ReadRange<GridIndex, Enumerator>(in GridIndexRange value)
            => new ReadRange<GridIndex, Enumerator>(value.Start, value.End, value.IsFromEnd);

        public static implicit operator ReadRange<GridIndex>(in GridIndexRange value)
            => new ReadRange<GridIndex>(value.Start, value.End, value.IsFromEnd, new Enumerator());

        public static implicit operator GridIndexRange(in ReadRange<GridIndex> value)
            => new GridIndexRange(value.Start, value.End, value.IsFromEnd);

        public static implicit operator GridIndexRange(in ReadRange<GridIndex, Enumerator> value)
            => new GridIndexRange(value.Start, value.End, value.IsFromEnd);

        public static bool operator ==(in GridIndexRange lhs, in GridIndexRange rhs)
            => lhs.Equals(in rhs);

        public static bool operator !=(in GridIndexRange lhs, in GridIndexRange rhs)
            => !lhs.Equals(in rhs);

        public struct Enumerator : IEnumerator<GridIndex>, IRangeEnumerator<GridIndex>
        {
            private readonly GridIndex start;
            private readonly GridIndex end;
            private readonly bool fromEnd;

            private GridIndex current;
            private sbyte flag;

            public Enumerator(in GridIndexRange range)
            {
                var rowIsIncreasing = range.Start.Row.CompareTo(range.End.Row) <= 0;
                var colIsIncreasing = range.Start.Column.CompareTo(range.End.Column) <= 0;

                this.start = new GridIndex(
                    rowIsIncreasing ? range.Start.Row : range.End.Row,
                    colIsIncreasing ? range.Start.Column : range.End.Column
                );

                this.end = new GridIndex(
                    rowIsIncreasing ? range.End.Row : range.Start.Row,
                    colIsIncreasing ? range.End.Column : range.Start.Column
                );

                this.fromEnd = range.IsFromEnd;
                this.current = this.fromEnd ? this.end : this.start;
                this.flag = -1;
            }

            public bool MoveNext()
            {
                if (this.flag == 0)
                    return this.fromEnd ? MoveNextFromEnd() : MoveNextFromStart();

                if (this.flag < 0)
                {
                    this.flag = 0;
                    return true;
                }

                return false;
            }

            private bool MoveNextFromStart()
            {
                if (this.current == this.end)
                {
                    this.flag = 1;
                    return false;
                }

                var col = this.current.Column + 1;
                var row = this.current.Row;

                if (col > this.end.Column)
                {
                    row += 1;
                    col = this.start.Column;
                }

                this.current = new GridIndex(row, col);
                return true;
            }

            private bool MoveNextFromEnd()
            {
                if (this.current == this.start)
                {
                    this.flag = 1;
                    return false;
                }

                var col = this.current.Column - 1;
                var row = this.current.Row;

                if (col < this.start.Column)
                {
                    row -= 1;
                    col = this.end.Column;
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
                this.current = this.fromEnd ? this.end : this.start;
                this.flag = -1;
            }

            public IEnumerator<GridIndex> Enumerate(GridIndex start, GridIndex end, bool fromEnd)
            {
                var rowIsIncreasing = start.Row.CompareTo(end.Row) <= 0;
                var colIsIncreasing = start.Column.CompareTo(end.Column) <= 0;

                var newStart = new GridIndex(
                    rowIsIncreasing ? start.Row : end.Row,
                    colIsIncreasing ? start.Column : end.Column
                );

                var newEnd = new GridIndex(
                    rowIsIncreasing ? end.Row : start.Row,
                    colIsIncreasing ? end.Column : start.Column
                );

                return fromEnd ? EnumerateFromEnd(newStart, newEnd) : EnumerateFromStart(newStart, newEnd);
            }

            private IEnumerator<GridIndex> EnumerateFromStart(GridIndex start, GridIndex end)
            {
                for (var r = start.Row; r <= end.Row; r++)
                {
                    for (var c = start.Column; c <= end.Column; c++)
                    {
                        yield return new GridIndex(r, c);
                    }
                }
            }

            private IEnumerator<GridIndex> EnumerateFromEnd(GridIndex start, GridIndex end)
            {
                for (var r = end.Row; r >= start.Row; r--)
                {
                    for (var c = end.Column; c >= start.Column; c--)
                    {
                        yield return new GridIndex(r, c);
                    }
                }
            }
        }
    }
}
