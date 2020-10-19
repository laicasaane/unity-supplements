using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.Grid
{
    [Serializable]
    public readonly partial struct GridRange : IRange<GridIndex, GridRange.Enumerator>,
                                               IEquatableReadOnlyStruct<GridRange>, ISerializable
    {
        public GridIndex Size { get; }

        public bool Clamped { get; }

        public GridIndex Start { get; }

        public GridIndex End { get; }

        public bool IsFromEnd { get; }

        /// <summary>
        /// Whether iterators should increase/decrease <see cref="GridIndex.Row"/> first then <see cref="GridIndex.Column"/>
        /// </summary>
        public bool IsRowFirst { get; }

        public GridRange(in GridIndex size, in GridIndex start, in GridIndex end, bool fromEnd = false, bool rowFirst = false)
        {
            this.Size = size;
            this.Clamped = true;
            this.Start = start;
            this.End = end;
            this.IsFromEnd = fromEnd;
            this.IsRowFirst = rowFirst;
        }

        public GridRange(in GridIndex size, bool clamped, in GridIndex start, in GridIndex end, bool fromEnd = false, bool rowFirst = false)
        {
            this.Size = size;
            this.Clamped = clamped;
            this.Start = start;
            this.End = end;
            this.IsFromEnd = fromEnd;
            this.IsRowFirst = rowFirst;
        }

        private GridRange(SerializationInfo info, StreamingContext context)
        {
            this.Size = info.GetValueOrDefault<GridIndex>(nameof(this.Size));
            this.Clamped = info.GetBooleanOrDefault(nameof(this.Clamped));
            this.Start = info.GetValueOrDefault<GridIndex>(nameof(this.Start));
            this.End = info.GetValueOrDefault<GridIndex>(nameof(this.End));
            this.IsFromEnd = info.GetBooleanOrDefault(nameof(this.IsFromEnd));
            this.IsRowFirst = info.GetBooleanOrDefault(nameof(this.IsRowFirst));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Size), this.Size);
            info.AddValue(nameof(this.Clamped), this.Clamped);
            info.AddValue(nameof(this.Start), this.Start);
            info.AddValue(nameof(this.End), this.End);
            info.AddValue(nameof(this.IsFromEnd), this.IsFromEnd);
            info.AddValue(nameof(this.IsRowFirst), this.IsRowFirst);
        }

        public void Deconstruct(out GridIndex size, out GridIndex start, out GridIndex end)
        {
            size = this.Size;
            start = this.Start;
            end = this.End;
        }

        public void Deconstruct(out GridIndex size, out GridIndex start, out GridIndex end, out bool fromEnd)
        {
            size = this.Size;
            start = this.Start;
            end = this.End;
            fromEnd = this.IsFromEnd;
        }

        public void Deconstruct(out GridIndex size, out GridIndex start, out GridIndex end, out bool fromEnd, bool rowFirst)
        {
            size = this.Size;
            start = this.Start;
            end = this.End;
            fromEnd = this.IsFromEnd;
            rowFirst = this.IsRowFirst;
        }

        public void Deconstruct(out GridIndex size, out bool clamped, out GridIndex start, out GridIndex end)
        {
            size = this.Size;
            clamped = this.Clamped;
            start = this.Start;
            end = this.End;
        }

        public void Deconstruct(out GridIndex size, out bool clamped, out GridIndex start, out GridIndex end, out bool fromEnd)
        {
            size = this.Size;
            clamped = this.Clamped;
            start = this.Start;
            end = this.End;
            fromEnd = this.IsFromEnd;
        }

        public void Deconstruct(out GridIndex size, out bool clamped, out GridIndex start, out GridIndex end, out bool fromEnd, out bool rowFirst)
        {
            size = this.Size;
            clamped = this.Clamped;
            start = this.Start;
            end = this.End;
            fromEnd = this.IsFromEnd;
            rowFirst = this.IsRowFirst;
        }

        public GridRange With(in GridIndex? Size = null, bool? Clamped = null, in GridIndex? Start = null,
                              in GridIndex? End = null, bool? IsFromEnd = null, bool? IsRowFirst = null)
            => new GridRange(
                Size ?? this.Size,
                Clamped ?? this.Clamped,
                Start ?? this.Start,
                End ?? this.End,
                IsFromEnd ?? this.IsFromEnd,
                IsRowFirst ?? this.IsRowFirst
            );

        public GridRange FromStart()
            => new GridRange(this.Size, this.Clamped, this.Start, this.End, false);

        public GridRange FromEnd()
            => new GridRange(this.Size, this.Clamped, this.Start, this.End, true);

        IRange<GridIndex> IRange<GridIndex>.FromStart()
            => FromStart();

        IRange<GridIndex> IRange<GridIndex>.FromEnd()
            => FromEnd();

        public GridRange Clamp()
            => new GridRange(this.Size, true, this.Start, this.End, this.IsFromEnd);

        public GridRange Unclamp()
            => new GridRange(this.Size, false, this.Start, this.End, this.IsFromEnd);

        public GridRange RowFirst()
            => new GridRange(this.Size, this.Clamped, this.Start, this.End, this.IsFromEnd, true);

        public GridRange ColumnFirst()
            => new GridRange(this.Size, this.Clamped, this.Start, this.End, this.IsFromEnd, false);

        public bool Contains(in GridIndex value)
        {
            if (this.Clamped)
            {
                var containsRow = this.Start.Row.CompareTo(this.End.Row) <= 0
                                  ? value.Row >= this.Start.Row && value.Row <= this.End.Row
                                  : value.Row >= this.End.Row && value.Row <= this.Start.Row;

                var containsCol = this.Start.Column.CompareTo(this.End.Column) <= 0
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
                var row = Math.Abs(this.End.Row - this.Start.Row) + 1;
                var col = Math.Abs(this.End.Column - this.Start.Column) + 1;
                return row * col;
            }

            var startIndex = this.Start.ToIndex1(this.Size);
            var endIndex = this.End.ToIndex1(this.Size);
            return Math.Abs(endIndex - startIndex) + 1;
        }

        public override bool Equals(object obj)
            => obj is GridRange other &&
               this.Size == other.Size &&
               this.Clamped == other.Clamped &&
               this.Start == other.Start &&
               this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd &&
               this.IsRowFirst == other.IsRowFirst;

        public bool Equals(in GridRange other)
            => this.Size == other.Size &&
               this.Clamped == other.Clamped &&
               this.Start == other.Start &&
               this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd &&
               this.IsRowFirst == other.IsRowFirst;

        public bool Equals(GridRange other)
            => this.Size == other.Size &&
               this.Clamped == other.Clamped &&
               this.Start == other.Start &&
               this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd &&
               this.IsRowFirst == other.IsRowFirst;

        public override int GetHashCode()
        {
            var hashCode = -535992267;
            hashCode = hashCode * -1521134295 + this.Size.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Clamped.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Start.GetHashCode();
            hashCode = hashCode * -1521134295 + this.End.GetHashCode();
            hashCode = hashCode * -1521134295 + this.IsFromEnd.GetHashCode();
            hashCode = hashCode * -1521134295 + this.IsRowFirst.GetHashCode();
            return hashCode;
        }

        public override string ToString()
            => $"{{ {nameof(this.Size)}={this.Size}, {nameof(this.Clamped)}={this.Clamped}, {nameof(this.Start)}={this.Start}, {nameof(this.End)}={this.End}, {nameof(this.IsFromEnd)}={this.IsFromEnd} }}";

        public Enumerator GetEnumerator()
            => new Enumerator(this);

        public Enumerator Range()
            => GetEnumerator();

        IEnumerator<GridIndex> IRange<GridIndex>.Range()
            => GetEnumerator();

        public GridRange Normalize()
            => Normal(this.Size, this.Clamped, this.Start, this.End);

        public bool Intersects(in GridRange other)
        {
            var nThis = Normalize();
            var nOther = other.Normalize();

            if (nThis.Clamped && nOther.Clamped)
                return (nOther.Start.Row <= nThis.End.Row && nOther.Start.Column <= nThis.End.Column &&
                        nOther.End.Row >= nThis.Start.Row && nOther.End.Column >= nThis.Start.Column);

            if (nOther.IsRowFirst)
                return (nOther.Start.Column <= nThis.End.Column && nOther.Start.Row <= nThis.End.Row) ||
                       (nOther.End.Column >= nThis.Start.Column && nOther.End.Row >= nThis.Start.Row);

            return (nOther.Start.Row <= nThis.End.Row && nOther.Start.Column <= nThis.End.Column) ||
                   (nOther.End.Row >= nThis.Start.Row && nOther.End.Column >= nThis.Start.Column);
        }

        /// <summary>
        /// Create a normal range from (a, b) where <see cref="Start"/> is lesser than or equal to <see cref="End"/>.
        /// </summary>
        public static GridRange Normal(in GridIndex size, bool clamped, in GridIndex a, in GridIndex b)
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

            return new GridRange(size, clamped, start, end);
        }

        public static GridRange From(in GridIndex value, bool fromEnd = false)
            => new GridRange(value, GridIndex.Zero, value - GridIndex.One, fromEnd);

        public static GridRange FromStart(in GridIndex size, in GridIndex start, in GridIndex end)
            => new GridRange(size, start, end, false);

        public static GridRange FromEnd(in GridIndex size, in GridIndex start, in GridIndex end)
            => new GridRange(size, start, end, true);

        public static GridRange FromStart(in GridIndex size, bool clamped, in GridIndex start, in GridIndex end)
            => new GridRange(size, clamped, start, end, false);

        public static GridRange FromEnd(in GridIndex size, bool clamped, in GridIndex start, in GridIndex end)
            => new GridRange(size, clamped, start, end, true);

        public static implicit operator GridRange(in (GridIndex size, GridIndex start, GridIndex end) value)
            => new GridRange(value.size, value.start, value.end);

        public static implicit operator GridRange(in (GridIndex size, GridIndex start, GridIndex end, bool fromEnd) value)
            => new GridRange(value.size, value.start, value.end, value.fromEnd);

        public static implicit operator GridRange(in (GridIndex size, GridIndex start, GridIndex end, bool fromEnd, bool rowFirst) value)
            => new GridRange(value.size, value.start, value.end, value.fromEnd, value.rowFirst);

        public static implicit operator GridRange(in (GridIndex size, bool clamped, GridIndex start, GridIndex end) value)
            => new GridRange(value.size, value.clamped, value.start, value.end);

        public static implicit operator GridRange(in (GridIndex size, bool clamped, GridIndex start, GridIndex end, bool fromEnd) value)
            => new GridRange(value.size, value.clamped, value.start, value.end, value.fromEnd);

        public static implicit operator GridRange(in (GridIndex size, bool clamped, GridIndex start, GridIndex end, bool fromEnd, bool rowFirst) value)
            => new GridRange(value.size, value.clamped, value.start, value.end, value.fromEnd, value.rowFirst);

        public static implicit operator GridIndexRange(in GridRange value)
            => new GridIndexRange(value.Start, value.End, value.IsFromEnd);

        public static bool operator ==(in GridRange lhs, in GridRange rhs)
            => lhs.Equals(in rhs);

        public static bool operator !=(in GridRange lhs, in GridRange rhs)
            => !lhs.Equals(in rhs);

        public static GridRange operator +(in GridRange lhs, in GridIndex rhs)
            => new GridRange(
                lhs.Size + rhs,
                lhs.Clamped,
                lhs.Start + rhs,
                lhs.End + rhs,
                lhs.IsFromEnd
            );

        public static GridRange operator -(in GridRange lhs, in GridIndex rhs)
            => new GridRange(
                lhs.Size - rhs,
                lhs.Clamped,
                lhs.Start - rhs,
                lhs.End - rhs,
                lhs.IsFromEnd
            );
    }
}
