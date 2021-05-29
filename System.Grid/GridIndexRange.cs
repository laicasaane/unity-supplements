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

        public GridDirection Direction { get; }

        public GridIndexRange(in GridIndex start, in GridIndex end, bool fromEnd = false, GridDirection direction = default)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = fromEnd;
            this.Direction = direction;
        }

        private GridIndexRange(SerializationInfo info, StreamingContext context)
        {
            this.Start = info.GetValueOrDefault<GridIndex>(nameof(this.Start));
            this.End = info.GetValueOrDefault<GridIndex>(nameof(this.End));
            this.IsFromEnd = info.GetBooleanOrDefault(nameof(this.IsFromEnd));
            this.Direction = info.GetValueOrDefault<GridDirection>(nameof(this.Direction));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Start), this.Start);
            info.AddValue(nameof(this.End), this.End);
            info.AddValue(nameof(this.IsFromEnd), this.IsFromEnd);
            info.AddValue(nameof(this.Direction), this.Direction);
        }

        public void Deconstruct(out GridIndex start, out GridIndex end, out bool fromEnd, out GridDirection direction)
        {
            start = this.Start;
            end = this.End;
            fromEnd = this.IsFromEnd;
            direction = this.Direction;
        }

        public GridIndexRange With(in GridIndex? Start = null, in GridIndex? End = null, bool? IsFromEnd = null, GridDirection? Direction = null)
            => new GridIndexRange(
                Start ?? this.Start,
                End ?? this.End,
                IsFromEnd ?? this.IsFromEnd,
                Direction ?? this.Direction
            );

        public GridIndexRange ByRow()
            => new GridIndexRange(this.Start, this.End, this.IsFromEnd, GridDirection.Row);

        public GridIndexRange ByColumn()
            => new GridIndexRange(this.Start, this.End, this.IsFromEnd, GridDirection.Column);

        public GridIndexRange FromStart()
            => new GridIndexRange(this.Start, this.End, false, this.Direction);

        public GridIndexRange FromEnd()
            => new GridIndexRange(this.Start, this.End, true, this.Direction);

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
            var row = Math.Max(this.End.Row - this.Start.Row + 1, 0);
            var col = Math.Max(this.End.Column - this.Start.Column + 1, 0);
            return row * col;
        }

        public override bool Equals(object obj)
            => obj is GridIndexRange other &&
               this.Start == other.Start &&
               this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd &&
               this.Direction == other.Direction;

        public bool Equals(in GridIndexRange other)
            => this.Start == other.Start &&
               this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd &&
               this.Direction == other.Direction;

        public bool Equals(GridIndexRange other)
            => this.Start == other.Start &&
               this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd &&
               this.Direction == other.Direction;

        public override int GetHashCode()
        {
#if USE_SYSTEM_HASHCODE
            return HashCode.Combine(this.Start, this.End, this.IsFromEnd, this.Direction);
#endif

#pragma warning disable CS0162 // Unreachable code detected
            var hashCode = -1418356749;
            hashCode = hashCode * -1521134295 + this.Start.GetHashCode();
            hashCode = hashCode * -1521134295 + this.End.GetHashCode();
            hashCode = hashCode * -1521134295 + this.IsFromEnd.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Direction.GetHashCode();
            return hashCode;
#pragma warning restore CS0162 // Unreachable code detected
        }

        public override string ToString()
            => $"{{ {nameof(this.Start)}={this.Start}, {nameof(this.End)}={this.End}, {nameof(this.IsFromEnd)}={this.IsFromEnd}, {nameof(this.Direction)}={this.Direction} }}";

        public Enumerator GetEnumerator()
            => new Enumerator(this);

        public Enumerator Range()
            => GetEnumerator();

        IEnumerator<GridIndex> IRange<GridIndex>.Range()
            => GetEnumerator();

        public GridIndexRange Normalize()
            => Normal(this.Start, this.End, this.IsFromEnd, this.Direction);

        public bool Intersects(in GridIndexRange other)
        {
            var nThis = Normalize();
            var nOther = other.Normalize();

            return (nOther.Start.Row <= nThis.End.Row && nOther.Start.Column <= nThis.End.Column &&
                    nOther.End.Row >= nThis.Start.Row && nOther.End.Column >= nThis.Start.Column);
        }

        /// <summary>
        /// Create a normal range from (a, b) where <see cref="Start"/> is lesser than <see cref="End"/>.
        /// </summary>
        public static GridIndexRange Normal(in GridIndex a, in GridIndex b, bool fromEnd = false, GridDirection direction = default)
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

            return new GridIndexRange(start, end, fromEnd, direction);
        }

        /// <summary>
        /// Create a range from a size whose row and column are greater than 0
        /// </summary>
        /// <exception cref="InvalidOperationException">Row and column must be greater than 0</exception>
        public static GridIndexRange FromSize(in GridIndex value, bool fromEnd = false)
        {
            if (value.Row <= 0 || value.Column <= 0)
                throw new InvalidOperationException("Row and column must be greater than 0");

            return new GridIndexRange(GridIndex.Zero, value - GridIndex.One, fromEnd);
        }

        public static GridIndexRange FromStart(in GridIndex start, in GridIndex end)
            => new GridIndexRange(start, end, false);

        public static GridIndexRange FromEnd(in GridIndex start, in GridIndex end)
            => new GridIndexRange(start, end, true);

        public static implicit operator GridIndexRange(in (GridIndex start, GridIndex end) value)
            => new GridIndexRange(value.start, value.end);

        public static implicit operator GridIndexRange(in (GridIndex start, GridIndex end, bool fromEnd) value)
            => new GridIndexRange(value.start, value.end, value.fromEnd);

        public static implicit operator GridIndexRange(in (GridIndex start, GridIndex end, bool fromEnd, GridDirection direction) value)
            => new GridIndexRange(value.start, value.end, value.fromEnd, value.direction);

        public static implicit operator GridIndexRange(in ReadRange<GridIndex> value)
            => new GridIndexRange(value.Start, value.End, value.IsFromEnd);

        public static bool operator ==(in GridIndexRange lhs, in GridIndexRange rhs)
            => lhs.Start == rhs.Start &&
               lhs.End == rhs.End &&
               lhs.IsFromEnd == rhs.IsFromEnd &&
               lhs.Direction == rhs.Direction;

        public static bool operator !=(in GridIndexRange lhs, in GridIndexRange rhs)
            => lhs.Start != rhs.Start ||
               lhs.End != rhs.End ||
               lhs.IsFromEnd != rhs.IsFromEnd ||
               lhs.Direction != rhs.Direction;
    }
}
