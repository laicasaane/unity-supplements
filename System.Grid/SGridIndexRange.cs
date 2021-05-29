using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.Grid
{
    [Serializable]
    public readonly partial struct SGridIndexRange : IRange<SGridIndex, SGridIndexRange.Enumerator>,
                                                    IEquatableReadOnlyStruct<SGridIndexRange>, ISerializable
    {
        public SGridIndex Start { get; }

        public SGridIndex End { get; }

        public bool IsFromEnd { get; }

        public GridDirection Direction { get; }

        public SGridIndexRange(in SGridIndex start, in SGridIndex end, bool fromEnd = false, GridDirection direction = default)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = fromEnd;
            this.Direction = direction;
        }

        private SGridIndexRange(SerializationInfo info, StreamingContext context)
        {
            this.Start = info.GetValueOrDefault<SGridIndex>(nameof(this.Start));
            this.End = info.GetValueOrDefault<SGridIndex>(nameof(this.End));
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

        public void Deconstruct(out SGridIndex start, out SGridIndex end, out bool fromEnd, out GridDirection direction)
        {
            start = this.Start;
            end = this.End;
            fromEnd = this.IsFromEnd;
            direction = this.Direction;
        }

        public SGridIndexRange With(in SGridIndex? Start = null, in SGridIndex? End = null, bool? IsFromEnd = null, GridDirection? Direction = null)
            => new SGridIndexRange(
                Start ?? this.Start,
                End ?? this.End,
                IsFromEnd ?? this.IsFromEnd,
                Direction ?? this.Direction
            );

        public SGridIndexRange ByRow()
            => new SGridIndexRange(this.Start, this.End, this.IsFromEnd, GridDirection.Row);

        public SGridIndexRange ByColumn()
            => new SGridIndexRange(this.Start, this.End, this.IsFromEnd, GridDirection.Column);

        public SGridIndexRange FromStart()
            => new SGridIndexRange(this.Start, this.End, false, this.Direction);

        public SGridIndexRange FromEnd()
            => new SGridIndexRange(this.Start, this.End, true, this.Direction);

        IRange<SGridIndex> IRange<SGridIndex>.FromStart()
            => FromStart();

        IRange<SGridIndex> IRange<SGridIndex>.FromEnd()
            => FromEnd();

        public bool Contains(in SGridIndex value)
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
            => obj is SGridIndexRange other &&
               this.Start == other.Start &&
               this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd &&
               this.Direction == other.Direction;

        public bool Equals(in SGridIndexRange other)
            => this.Start == other.Start &&
               this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd &&
               this.Direction == other.Direction;

        public bool Equals(SGridIndexRange other)
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

        IEnumerator<SGridIndex> IRange<SGridIndex>.Range()
            => GetEnumerator();

        public SGridIndexRange Normalize()
            => Normal(this.Start, this.End, this.IsFromEnd, this.Direction);

        public bool Intersects(in SGridIndexRange other)
        {
            var nThis = Normalize();
            var nOther = other.Normalize();

            return (nOther.Start.Row <= nThis.End.Row && nOther.Start.Column <= nThis.End.Column &&
                    nOther.End.Row >= nThis.Start.Row && nOther.End.Column >= nThis.Start.Column);
        }

        /// <summary>
        /// Create a normal range from (a, b) where <see cref="Start"/> is lesser than <see cref="End"/>.
        /// </summary>
        public static SGridIndexRange Normal(in SGridIndex a, in SGridIndex b, bool fromEnd = false, GridDirection direction = default)
        {
            var rowIncreasing = a.Row.CompareTo(b.Row) <= 0;
            var colIncreasing = a.Column.CompareTo(b.Column) <= 0;

            var start = new SGridIndex(
                rowIncreasing ? a.Row : b.Row,
                colIncreasing ? a.Column : b.Column
            );

            var end = new SGridIndex(
                rowIncreasing ? b.Row : a.Row,
                colIncreasing ? b.Column : a.Column
            );

            return new SGridIndexRange(start, end, fromEnd, direction);
        }

        /// <summary>
        /// Create a range from a size whose row and column are greater than 0
        /// </summary>
        /// <exception cref="InvalidOperationException">Row and column must be greater than 0</exception>
        public static SGridIndexRange FromSize(in SGridIndex value, bool fromEnd = false)
        {
            if (value.Row <= 0 || value.Column <= 0)
                throw new InvalidOperationException("Row and column must be greater than 0");

            return new SGridIndexRange(SGridIndex.Zero, value - SGridIndex.One, fromEnd);
        }

        public static SGridIndexRange FromStart(in SGridIndex start, in SGridIndex end)
            => new SGridIndexRange(start, end, false);

        public static SGridIndexRange FromEnd(in SGridIndex start, in SGridIndex end)
            => new SGridIndexRange(start, end, true);

        public static implicit operator SGridIndexRange(in (SGridIndex start, SGridIndex end) value)
            => new SGridIndexRange(value.start, value.end);

        public static implicit operator SGridIndexRange(in (SGridIndex start, SGridIndex end, bool fromEnd) value)
            => new SGridIndexRange(value.start, value.end, value.fromEnd);

        public static implicit operator SGridIndexRange(in (SGridIndex start, SGridIndex end, bool fromEnd, GridDirection direction) value)
            => new SGridIndexRange(value.start, value.end, value.fromEnd, value.direction);

        public static implicit operator SGridIndexRange(in ReadRange<SGridIndex> value)
            => new SGridIndexRange(value.Start, value.End, value.IsFromEnd);

        public static bool operator ==(in SGridIndexRange lhs, in SGridIndexRange rhs)
            => lhs.Start == rhs.Start &&
               lhs.End == rhs.End &&
               lhs.IsFromEnd == rhs.IsFromEnd &&
               lhs.Direction == rhs.Direction;

        public static bool operator !=(in SGridIndexRange lhs, in SGridIndexRange rhs)
            => lhs.Start != rhs.Start ||
               lhs.End != rhs.End ||
               lhs.IsFromEnd != rhs.IsFromEnd ||
               lhs.Direction != rhs.Direction;
    }
}
