using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.Grid
{
    [Serializable]
    public readonly struct GridIndexRange : IRange<GridIndex, GridIndexRange.Enumerator>,
                                            IEquatableReadOnlyStruct<GridIndexRange>, ISerializable
    {
        public GridIndex Start => this.start;

        public GridIndex End => this.end;

        public bool IsFromEnd => this.isFromEnd;

        private readonly GridIndex start;
        private readonly GridIndex end;
        private readonly bool isFromEnd;

        public GridIndexRange(in GridIndex start, in GridIndex end)
        {
            this.start = start;
            this.end = end;
            this.isFromEnd = false;
        }

        public GridIndexRange(in GridIndex start, in GridIndex end, bool fromEnd)
        {
            this.start = start;
            this.end = end;
            this.isFromEnd = fromEnd;
        }

        private GridIndexRange(SerializationInfo info, StreamingContext context)
        {
            try
            {
                this.start = (GridIndex)info.GetValue(nameof(this.Start), typeof(GridIndex));
            }
            catch
            {
                this.start = default;
            }

            try
            {
                this.end = (GridIndex)info.GetValue(nameof(this.End), typeof(GridIndex));
            }
            catch
            {
                this.end = default;
            }

            try
            {
                this.isFromEnd = info.GetBoolean(nameof(this.IsFromEnd));
            }
            catch
            {
                this.isFromEnd = default;
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Start), this.start);
            info.AddValue(nameof(this.End), this.end);
            info.AddValue(nameof(this.IsFromEnd), this.isFromEnd);
        }

        public void Deconstruct(out GridIndex start, out GridIndex end)
        {
            start = this.start;
            end = this.end;
        }

        public void Deconstruct(out GridIndex start, out GridIndex end, out bool fromEnd)
        {
            start = this.start;
            end = this.end;
            fromEnd = this.isFromEnd;
        }

        public GridIndexRange With(in GridIndex? Start = null, in GridIndex? End = null, in bool? IsFromEnd = null)
            => new GridIndexRange(
                Start ?? this.start,
                End ?? this.end,
                IsFromEnd ?? this.isFromEnd
            );

        public GridIndexRange FromStart()
            => new GridIndexRange(this.start, this.end, false);

        public GridIndexRange FromEnd()
            => new GridIndexRange(this.start, this.end, true);

        IRange<GridIndex> IRange<GridIndex>.FromStart()
            => FromStart();

        IRange<GridIndex> IRange<GridIndex>.FromEnd()
            => FromEnd();

        public override bool Equals(object obj)
            => obj is GridIndexRange other &&
               this.start.Equals(in other.start) &&
               this.end.Equals(in other.end) &&
               this.isFromEnd == other.isFromEnd;

        public bool Equals(in GridIndexRange other)
            => this.start.Equals(in other.start) &&
               this.end.Equals(in other.end) &&
               this.isFromEnd == other.isFromEnd;

        public bool Equals(GridIndexRange other)
            => this.start.Equals(in other.start) &&
               this.end.Equals(in other.end) &&
               this.isFromEnd == other.isFromEnd;

        public override int GetHashCode()
        {
            var hashCode = -1418356749;
            hashCode = hashCode * -1521134295 + this.start.GetHashCode();
            hashCode = hashCode * -1521134295 + this.end.GetHashCode();
            hashCode = hashCode * -1521134295 + this.isFromEnd.GetHashCode();
            return hashCode;
        }

        public override string ToString()
            => $"{{ {nameof(this.Start)}={this.start}, {nameof(this.End)}={this.end}, {nameof(this.IsFromEnd)}={this.isFromEnd} }}";

        public Enumerator GetEnumerator()
            => new Enumerator(this);

        public Enumerator Range()
            => GetEnumerator();

        IEnumerator<GridIndex> IRange<GridIndex>.Range()
            => GetEnumerator();

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

        public static GridIndexRange Count(in GridIndex value, bool fromEnd = false)
            => new GridIndexRange(GridIndex.Zero, value - GridIndex.One, fromEnd);

        public static GridIndexRange FromStart(in GridIndex start, in GridIndex end)
            => new GridIndexRange(start, end, false);

        public static GridIndexRange FromEnd(in GridIndex start, in GridIndex end)
            => new GridIndexRange(start, end, true);

        public static implicit operator GridIndexRange(in (GridIndex start, GridIndex end) value)
            => new GridIndexRange(value.start, value.end);

        public static implicit operator GridIndexRange(in (GridIndex start, GridIndex end, bool fromEnd) value)
            => new GridIndexRange(value.start, value.end, value.fromEnd);

        public static implicit operator ReadRange<GridIndex, Enumerator>(in GridIndexRange value)
            => new ReadRange<GridIndex, Enumerator>(value.start, value.end, value.isFromEnd);

        public static implicit operator ReadRange<GridIndex>(in GridIndexRange value)
            => new ReadRange<GridIndex>(value.start, value.end, value.isFromEnd, new Enumerator());

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
                var rowIsIncreasing = range.start.Row.CompareTo(range.end.Row) <= 0;
                var colIsIncreasing = range.start.Column.CompareTo(range.end.Column) <= 0;

                this.start = new GridIndex(
                    rowIsIncreasing ? range.start.Row : range.end.Row,
                    colIsIncreasing ? range.start.Column : range.end.Column
                );

                this.end = new GridIndex(
                    rowIsIncreasing ? range.end.Row : range.start.Row,
                    colIsIncreasing ? range.end.Column : range.start.Column
                );

                this.fromEnd = range.isFromEnd;
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
