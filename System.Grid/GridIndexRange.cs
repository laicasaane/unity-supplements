using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.Grid
{
    [Serializable]
    public readonly partial struct GridIndexRange : IRange<GridIndex, GridIndexRange.Enumerator>,
                                                    IEquatableReadOnlyStruct<GridIndexRange>, ISerializable
    {
        public GridIndex Start { get; }

        public GridIndex End { get; }

        public bool IsFromEnd { get; }

        /// <summary>
        /// Whether iterators should increase/decrease <see cref="GridIndex.Row"/> first then <see cref="GridIndex.Column"/>
        /// </summary>
        public bool IsRowFirst { get; }

        public GridIndexRange(in GridIndex start, in GridIndex end, bool fromEnd = false, bool rowFirst = false)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = fromEnd;
            this.IsRowFirst = rowFirst;
        }

        private GridIndexRange(SerializationInfo info, StreamingContext context)
        {
            this.Start = (GridIndex)info.GetValue(nameof(this.Start), typeof(GridIndex));
            this.End = (GridIndex)info.GetValue(nameof(this.End), typeof(GridIndex));
            this.IsFromEnd = info.GetBoolean(nameof(this.IsFromEnd));
            this.IsRowFirst = info.GetBooleanOrDefault(nameof(this.IsRowFirst));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Start), this.Start);
            info.AddValue(nameof(this.End), this.End);
            info.AddValue(nameof(this.IsFromEnd), this.IsFromEnd);
            info.AddValue(nameof(this.IsRowFirst), this.IsRowFirst);
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

        public void Deconstruct(out GridIndex start, out GridIndex end, out bool fromEnd, out bool rowFirst)
        {
            start = this.Start;
            end = this.End;
            fromEnd = this.IsFromEnd;
            rowFirst = this.IsRowFirst;
        }

        public GridIndexRange With(in GridIndex? Start = null, in GridIndex? End = null, bool? IsFromEnd = null, bool? IsRowFirst = null)
            => new GridIndexRange(
                Start ?? this.Start,
                End ?? this.End,
                IsFromEnd ?? this.IsFromEnd,
                IsRowFirst ?? this.IsRowFirst
            );

        public GridIndexRange RowFirst()
            => new GridIndexRange(this.Start, this.End, this.IsFromEnd, true);

        public GridIndexRange ColumnFirst()
            => new GridIndexRange(this.Start, this.End, this.IsFromEnd, false);

        public GridIndexRange FromStart()
            => new GridIndexRange(this.Start, this.End, false);

        public GridIndexRange FromEnd()
            => new GridIndexRange(this.Start, this.End, true);

        IRange<GridIndex> IRange<GridIndex>.FromStart()
            => FromStart();

        IRange<GridIndex> IRange<GridIndex>.FromEnd()
            => FromEnd();

        public bool Contains(in GridIndex value)
        {
            var containsRow = this.Start.Row.CompareTo(this.End.Row) <= 0
                              ? value.Row >= this.Start.Row && value.Row <= this.End.Row
                              : value.Row >= this.End.Row && value.Row <= this.Start.Row;

            var containsCol = this.Start.Column.CompareTo(this.End.Column) <= 0
                              ? value.Column >= this.Start.Column && value.Column <= this.End.Column
                              : value.Column >= this.End.Column && value.Column <= this.Start.Column;

            return containsRow && containsCol;
        }
        public int Count()
        {
            var row = Math.Abs(this.End.Row - this.Start.Row) + 1;
            var col = Math.Abs(this.End.Column - this.Start.Column) + 1;
            return row * col;
        }

        public override bool Equals(object obj)
            => obj is GridIndexRange other &&
               this.Start == other.Start &&
               this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd &&
               this.IsRowFirst == other.IsRowFirst;

        public bool Equals(in GridIndexRange other)
            => this.Start == other.Start &&
               this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd &&
               this.IsRowFirst == other.IsRowFirst;

        public bool Equals(GridIndexRange other)
            => this.Start == other.Start &&
               this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd &&
               this.IsRowFirst == other.IsRowFirst;

        public override int GetHashCode()
        {
            var hashCode = -1418356749;
            hashCode = hashCode * -1521134295 + this.Start.GetHashCode();
            hashCode = hashCode * -1521134295 + this.End.GetHashCode();
            hashCode = hashCode * -1521134295 + this.IsFromEnd.GetHashCode();
            hashCode = hashCode * -1521134295 + this.IsRowFirst.GetHashCode();
            return hashCode;
        }

        public override string ToString()
            => $"{{ {nameof(this.Start)}={this.Start}, {nameof(this.End)}={this.End}, {nameof(this.IsFromEnd)}={this.IsFromEnd} }}";

        public Enumerator GetEnumerator()
            => new Enumerator(this);

        public Enumerator Range()
            => GetEnumerator();

        IEnumerator<GridIndex> IRange<GridIndex>.Range()
            => GetEnumerator();

        public GridIndexRange Normalize()
            => Normal(this.Start, this.End);

        public bool Intersects(in GridIndexRange other)
        {
            var nThis = Normalize();
            var nOther = other.Normalize();

            return (nOther.Start.Row <= nThis.End.Row && nOther.Start.Column <= nThis.End.Column &&
                    nOther.End.Row >= nThis.Start.Row && nOther.End.Column >= nThis.Start.Column);
        }

        /// <summary>
        /// Create a normal range from (a, b) where <see cref="Start"/> is lesser than or equal to <see cref="End"/>.
        /// </summary>
        public static GridIndexRange Normal(in GridIndex a, in GridIndex b)
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

        public static GridIndexRange FromSize(in GridIndex value, bool fromEnd = false)
            => new GridIndexRange(GridIndex.Zero, value - GridIndex.One, fromEnd);

        public static GridIndexRange FromStart(in GridIndex start, in GridIndex end)
            => new GridIndexRange(start, end, false);

        public static GridIndexRange FromEnd(in GridIndex start, in GridIndex end)
            => new GridIndexRange(start, end, true);

        public static implicit operator GridIndexRange(in (GridIndex start, GridIndex end) value)
            => new GridIndexRange(value.start, value.end);

        public static implicit operator GridIndexRange(in (GridIndex start, GridIndex end, bool fromEnd) value)
            => new GridIndexRange(value.start, value.end, value.fromEnd);

        public static implicit operator GridIndexRange(in (GridIndex start, GridIndex end, bool fromEnd, bool rowFirst) value)
            => new GridIndexRange(value.start, value.end, value.fromEnd, value.rowFirst);

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

        public static GridIndexRange operator +(in GridIndexRange lhs, in GridIndex rhs)
            => new GridIndexRange(
                lhs.Start + rhs,
                lhs.End + rhs,
                lhs.IsFromEnd
            );

        public static GridIndexRange operator -(in GridIndexRange lhs, in GridIndex rhs)
            => new GridIndexRange(
                lhs.Start - rhs,
                lhs.End - rhs,
                lhs.IsFromEnd
            );
    }
}
