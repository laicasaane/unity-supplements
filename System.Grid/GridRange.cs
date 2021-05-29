using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.Grid
{
    [Serializable]
    public readonly partial struct GridRange : IRange<GridIndex, GridRange.Enumerator>,
                                               IEquatableReadOnlyStruct<GridRange>, ISerializable
    {
        public GridSize Size { get; }

        /// <summary>
        /// Should iterate only within the boundary of <see cref="Start"/> and <see cref="End"/>.
        /// </summary>
        public bool Clamped { get; }

        public GridIndex Start { get; }

        public GridIndex End { get; }

        public bool IsFromEnd { get; }

        /// <summary>
        /// <para>Iterate by <see cref="GridDirection.Column"/> or <see cref="GridDirection.Row"/>.</para>
        /// <para>If <see cref="Clamped"/> == false, the direction will always be <see cref="GridDirection.Column"/>.</para>
        /// </summary>
        public GridDirection Direction { get; }

        public GridRange(in GridSize size, in GridIndex start, in GridIndex end, bool fromEnd = false, GridDirection direction = default)
        {
            this.Size = size;
            this.Clamped = true;
            this.Start = start;
            this.End = end;
            this.IsFromEnd = fromEnd;
            this.Direction = direction;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="size"></param>
        /// <param name="clamped">Should iterate only within the boundary of <see cref="Start"/> and <see cref="End"/>.</param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="fromEnd"></param>
        /// <param name="direction">Iterate by <see cref="GridDirection.Column"/> or <see cref="GridDirection.Row"/>.</param>
        public GridRange(in GridSize size, bool clamped, in GridIndex start, in GridIndex end, bool fromEnd = false, GridDirection direction = default)
        {
            this.Size = size;
            this.Clamped = clamped;
            this.Start = start;
            this.End = end;
            this.IsFromEnd = fromEnd;
            this.Direction = direction;
        }

        private GridRange(SerializationInfo info, StreamingContext context)
        {
            this.Size = info.GetValueOrDefault<GridSize>(nameof(this.Size));
            this.Clamped = info.GetBooleanOrDefault(nameof(this.Clamped));
            this.Start = info.GetValueOrDefault<GridIndex>(nameof(this.Start));
            this.End = info.GetValueOrDefault<GridIndex>(nameof(this.End));
            this.IsFromEnd = info.GetBooleanOrDefault(nameof(this.IsFromEnd));
            this.Direction = info.GetValueOrDefault<GridDirection>(nameof(this.Direction));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Size), this.Size);
            info.AddValue(nameof(this.Clamped), this.Clamped);
            info.AddValue(nameof(this.Start), this.Start);
            info.AddValue(nameof(this.End), this.End);
            info.AddValue(nameof(this.IsFromEnd), this.IsFromEnd);
            info.AddValue(nameof(this.Direction), this.Direction);
        }

        public void Deconstruct(out GridSize size, out bool clamped, out GridIndex start, out GridIndex end, out bool fromEnd, out GridDirection direction)
        {
            size = this.Size;
            clamped = this.Clamped;
            start = this.Start;
            end = this.End;
            fromEnd = this.IsFromEnd;
            direction = this.Direction;
        }

        public GridRange With(in GridSize? Size = null, bool? Clamped = null, in GridIndex? Start = null,
                              in GridIndex? End = null, bool? IsFromEnd = null, GridDirection? Direction = null)
            => new GridRange(
                Size ?? this.Size,
                Clamped ?? this.Clamped,
                Start ?? this.Start,
                End ?? this.End,
                IsFromEnd ?? this.IsFromEnd,
                Direction ?? this.Direction
            );

        public GridRange ByRow()
            => new GridRange(this.Size, this.Clamped, this.Start, this.End, this.IsFromEnd, GridDirection.Row);

        public GridRange ByColumn()
            => new GridRange(this.Size, this.Clamped, this.Start, this.End, this.IsFromEnd, GridDirection.Column);

        public GridRange FromStart()
            => new GridRange(this.Size, this.Clamped, this.Start, this.End, false, this.Direction);

        public GridRange FromEnd()
            => new GridRange(this.Size, this.Clamped, this.Start, this.End, true, this.Direction);

        IRange<GridIndex> IRange<GridIndex>.FromStart()
            => FromStart();

        IRange<GridIndex> IRange<GridIndex>.FromEnd()
            => FromEnd();

        public GridRange Clamp()
            => new GridRange(this.Size, true, this.Start, this.End, this.IsFromEnd, this.Direction);

        public GridRange Unclamp()
            => new GridRange(this.Size, false, this.Start, this.End, this.IsFromEnd, this.Direction);

        public bool Contains(in GridIndex value)
        {
            if (this.Clamped)
            {
                var containsRow = this.Start.Row <= this.End.Row
                                  ? value.Row >= this.Start.Row && value.Row <= this.End.Row
                                  : value.Row >= this.End.Row && value.Row <= this.Start.Row;

                var containsCol = this.Start.Column <= this.End.Column
                                  ? value.Column >= this.Start.Column && value.Column <= this.End.Column
                                  : value.Column >= this.End.Column && value.Column <= this.Start.Column;

                return containsRow && containsCol;
            }

            var index = value.ToIndex1(this.Size);
            var startIndex = value.ToIndex1(this.Size);
            var endIndex = value.ToIndex1(this.Size);

            return startIndex <= endIndex
                   ? index >= startIndex && index <= endIndex
                   : index >= endIndex && index <= startIndex;
        }
        public int Count()
        {
            if (this.Clamped)
            {
                var row = Math.Max(this.End.Row - this.Start.Row + 1, 0);
                var col = Math.Max(this.End.Column - this.Start.Column + 1, 0);
                return row * col;
            }

            var startIndex = this.Start.ToIndex1(this.Size);
            var endIndex = this.End.ToIndex1(this.Size);
            return Math.Max(endIndex - startIndex + 1, 0);
        }

        public override bool Equals(object obj)
            => obj is GridRange other &&
               this.Size == other.Size &&
               this.Clamped == other.Clamped &&
               this.Start == other.Start &&
               this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd &&
               this.Direction == other.Direction;

        public bool Equals(in GridRange other)
            => this.Size == other.Size &&
               this.Clamped == other.Clamped &&
               this.Start == other.Start &&
               this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd &&
               this.Direction == other.Direction;

        public bool Equals(GridRange other)
            => this.Size == other.Size &&
               this.Clamped == other.Clamped &&
               this.Start == other.Start &&
               this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd &&
               this.Direction == other.Direction;

        public override int GetHashCode()
        {
#if USE_SYSTEM_HASHCODE
            return HashCode.Combine(this.Size, this.Clamped, this.Start, this.End, this.IsFromEnd, this.Direction);
#endif

#pragma warning disable CS0162 // Unreachable code detected
            var hashCode = -535992267;
            hashCode = hashCode * -1521134295 + this.Size.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Clamped.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Start.GetHashCode();
            hashCode = hashCode * -1521134295 + this.End.GetHashCode();
            hashCode = hashCode * -1521134295 + this.IsFromEnd.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Direction.GetHashCode();
            return hashCode;
#pragma warning restore CS0162 // Unreachable code detected
        }

        public override string ToString()
            => $"{{ {nameof(this.Size)}={this.Size}, {nameof(this.Clamped)}={this.Clamped}, {nameof(this.Start)}={this.Start}, {nameof(this.End)}={this.End}, {nameof(this.IsFromEnd)}={this.IsFromEnd}, {nameof(this.Direction)}={this.Direction} }}";

        public Enumerator GetEnumerator()
            => new Enumerator(this);

        public Enumerator Range()
            => GetEnumerator();

        IEnumerator<GridIndex> IRange<GridIndex>.Range()
            => GetEnumerator();

        public GridRange Normalize()
            => Normal(this.Size, this.Clamped, this.Start, this.End, this.IsFromEnd, this.Direction);

        public bool Intersects(in GridRange other)
        {
            var nThis = Normalize();
            var nOther = other.Normalize();

            if (nThis.Clamped && nOther.Clamped)
                return (nOther.Start.Column <= nThis.End.Column && nOther.Start.Row <= nThis.End.Row &&
                        nOther.End.Column >= nThis.Start.Column && nOther.End.Row >= nThis.Start.Row);

            if (nThis.Clamped)
                return Intersects(nThis, nOther);

            if (nOther.Clamped)
                return Intersects(nOther, nThis);

            if (nThis.Direction == nOther.Direction)
                return Intersects(nThis, nOther);

            if (nThis.Start.Row == nThis.End.Row ||
                nThis.Start.Column == nThis.End.Column)
                return Intersects(nThis, nOther);

            if (nOther.Start.Row == nOther.End.Row ||
                nOther.Start.Column == nOther.End.Column)
                return Intersects(nOther, nThis);

            return true;
        }

        private bool Intersects(in GridRange clamped, in GridRange unclamped)
        {
            switch (unclamped.Direction)
            {
                case GridDirection.Column:
                    {
                        if (unclamped.Start.Row > clamped.End.Row ||
                            unclamped.End.Row < clamped.Start.Row)
                            return false;

                        if (unclamped.Start.Row == clamped.End.Row)
                            return unclamped.Start.Column <= clamped.End.Column;

                        if (unclamped.End.Row == clamped.Start.Row)
                            return unclamped.End.Column >= clamped.Start.Column;

                        return true;
                    }

                case GridDirection.Row:
                    {
                        if (unclamped.Start.Column > clamped.End.Column ||
                            unclamped.End.Column < clamped.Start.Column)
                            return false;

                        if (unclamped.Start.Column == clamped.End.Column)
                            return unclamped.Start.Row <= clamped.End.Row;

                        if (unclamped.End.Column == clamped.Start.Column)
                            return unclamped.End.Row >= clamped.Start.Row;

                        return true;
                    }

                default:
                    return false;
            }
        }

        /// <summary>
        /// Create a normal range from (a, b) where <see cref="Start"/> is lesser than <see cref="End"/>.
        /// </summary>
        public static GridRange Normal(in GridSize size, bool clamped, in GridIndex a, in GridIndex b, bool fromEnd = false, GridDirection direction = default)
        {
            GridIndex start, end;

            if (clamped)
            {
                var rowIncreasing = a.Row.CompareTo(b.Row) <= 0;
                var colIncreasing = a.Column.CompareTo(b.Column) <= 0;

                start = new GridIndex(
                    rowIncreasing ? a.Row : b.Row,
                    colIncreasing ? a.Column : b.Column
                );

                end = new GridIndex(
                    rowIncreasing ? b.Row : a.Row,
                    colIncreasing ? b.Column : a.Column
                );
            }
            else
            {
                var a1 = a.ToIndex1(size);
                var b1 = b.ToIndex1(size);
                var increasing = a1 < b1;

                start = increasing ? a : b;
                end = increasing ? b : a;
            }

            return new GridRange(size, clamped, start, end, fromEnd, direction);
        }

        /// <summary>
        /// Create a range from a size whose row and column are greater than 0
        /// </summary>
        /// <exception cref="InvalidOperationException">Row and column must be greater than 0</exception>
        public static GridRange FromSize(in GridIndex value, bool fromEnd = false)
        {
            if (value.Row <= 0 || value.Column <= 0)
                throw new InvalidOperationException("Row and column must be greater than 0");

            return new GridRange(value, GridIndex.Zero, value - GridIndex.One, fromEnd);
        }

        public static GridRange FromStart(in GridSize size, in GridIndex start, in GridIndex end)
            => new GridRange(size, start, end, false);

        public static GridRange FromEnd(in GridSize size, in GridIndex start, in GridIndex end)
            => new GridRange(size, start, end, true);

        public static GridRange FromSize(in GridIndex value, bool clamped, bool fromEnd = false)
            => new GridRange(value, clamped, GridIndex.Zero, value - GridIndex.One, fromEnd);

        public static GridRange FromStart(in GridSize size, bool clamped, in GridIndex start, in GridIndex end)
            => new GridRange(size, clamped, start, end, false);

        public static GridRange FromEnd(in GridSize size, bool clamped, in GridIndex start, in GridIndex end)
            => new GridRange(size, clamped, start, end, true);

        public static implicit operator GridRange(in (GridSize size, GridIndex start, GridIndex end) value)
            => new GridRange(value.size, value.start, value.end);

        public static implicit operator GridRange(in (GridSize size, GridIndex start, GridIndex end, bool fromEnd) value)
            => new GridRange(value.size, value.start, value.end, value.fromEnd);

        public static implicit operator GridRange(in (GridSize size, GridIndex start, GridIndex end, bool fromEnd, GridDirection direction) value)
            => new GridRange(value.size, value.start, value.end, value.fromEnd, value.direction);

        public static implicit operator GridRange(in (GridSize size, bool clamped, GridIndex start, GridIndex end) value)
            => new GridRange(value.size, value.clamped, value.start, value.end);

        public static implicit operator GridRange(in (GridSize size, bool clamped, GridIndex start, GridIndex end, bool fromEnd) value)
            => new GridRange(value.size, value.clamped, value.start, value.end, value.fromEnd);

        public static implicit operator GridRange(in (GridSize size, bool clamped, GridIndex start, GridIndex end, bool fromEnd, GridDirection direction) value)
            => new GridRange(value.size, value.clamped, value.start, value.end, value.fromEnd, value.direction);

        public static implicit operator GridIndexRange(in GridRange value)
            => new GridIndexRange(value.Start, value.End, value.IsFromEnd, value.Direction);

        public static bool operator ==(in GridRange lhs, in GridRange rhs)
            => lhs.Size == rhs.Size &&
               lhs.Clamped == rhs.Clamped &&
               lhs.Start == rhs.Start &&
               lhs.End == rhs.End &&
               lhs.IsFromEnd == rhs.IsFromEnd &&
               lhs.Direction == rhs.Direction;

        public static bool operator !=(in GridRange lhs, in GridRange rhs)
            => lhs.Size != rhs.Size ||
               lhs.Clamped != rhs.Clamped ||
               lhs.Start != rhs.Start ||
               lhs.End != rhs.End ||
               lhs.IsFromEnd != rhs.IsFromEnd ||
               lhs.Direction != rhs.Direction;
    }
}
