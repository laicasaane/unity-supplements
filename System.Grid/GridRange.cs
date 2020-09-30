using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.Grid
{
    [Serializable]
    public readonly struct GridRange : IRange<GridIndex, GridRange.Enumerator>,
                                       IEquatableReadOnlyStruct<GridRange>, ISerializable
    {
        public GridIndex Size { get; }

        public bool Clamped { get; }

        public GridIndex Start { get; }

        public GridIndex End { get; }

        public bool IsFromEnd { get; }

        public GridRange(in GridIndex size, in GridIndex start, in GridIndex end)
        {
            this.Size = size;
            this.Clamped = true;
            this.Start = start;
            this.End = end;
            this.IsFromEnd = false;
        }

        public GridRange(in GridIndex size, in GridIndex start, in GridIndex end, bool fromEnd)
        {
            this.Size = size;
            this.Clamped = true;
            this.Start = start;
            this.End = end;
            this.IsFromEnd = fromEnd;
        }

        public GridRange(in GridIndex size, bool clamped, in GridIndex start, in GridIndex end)
        {
            this.Size = size;
            this.Clamped = clamped;
            this.Start = start;
            this.End = end;
            this.IsFromEnd = false;
        }

        public GridRange(in GridIndex size, bool clamped, in GridIndex start, in GridIndex end, bool fromEnd)
        {
            this.Size = size;
            this.Clamped = clamped;
            this.Start = start;
            this.End = end;
            this.IsFromEnd = fromEnd;
        }

        private GridRange(SerializationInfo info, StreamingContext context)
        {
            try
            {
                this.Size = (GridIndex)info.GetValue(nameof(this.Size), typeof(GridIndex));
            }
            catch
            {
                this.Size = default;
            }

            try
            {
                this.Clamped = info.GetBoolean(nameof(this.Clamped));
            }
            catch
            {
                this.Clamped = default;
            }

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
            info.AddValue(nameof(this.Size), this.Size);
            info.AddValue(nameof(this.Clamped), this.Clamped);
            info.AddValue(nameof(this.Start), this.Start);
            info.AddValue(nameof(this.End), this.End);
            info.AddValue(nameof(this.IsFromEnd), this.IsFromEnd);
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

        public GridRange With(in GridIndex? Size = null, bool? Clamped = null, in GridIndex? Start = null,
                              in GridIndex? End = null, bool? IsFromEnd = null)
            => new GridRange(
                Size ?? this.Size,
                Clamped ?? this.Clamped,
                Start ?? this.Start,
                End ?? this.End,
                IsFromEnd ?? this.IsFromEnd
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

        public override bool Equals(object obj)
            => obj is GridRange other &&
               this.Size == other.Size &&
               this.Clamped == other.Clamped &&
               this.Start == other.Start &&
               this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(in GridRange other)
            => this.Size == other.Size &&
               this.Clamped == other.Clamped &&
               this.Start == other.Start &&
               this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(GridRange other)
            => this.Size == other.Size &&
               this.Clamped == other.Clamped &&
               this.Start == other.Start &&
               this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd;

        public override int GetHashCode()
        {
            var hashCode = -535992267;
            hashCode = hashCode * -1521134295 + this.Size.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Clamped.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Start.GetHashCode();
            hashCode = hashCode * -1521134295 + this.End.GetHashCode();
            hashCode = hashCode * -1521134295 + this.IsFromEnd.GetHashCode();
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

        /// <summary>
        /// Create a normal range from (a, b).
        /// If a &lt;= b, then a is the <see cref="Start"/> value, and b is the <see cref="End"/> value.
        /// Otherwise, they are swapped.
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

        public static GridRange Count(in GridIndex value, bool fromEnd = false)
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

        public static implicit operator GridRange(in (GridIndex size, bool clamped, GridIndex start, GridIndex end) value)
            => new GridRange(value.size, value.clamped, value.start, value.end);

        public static implicit operator GridRange(in (GridIndex size, bool clamped, GridIndex start, GridIndex end, bool fromEnd) value)
            => new GridRange(value.size, value.clamped, value.start, value.end, value.fromEnd);

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

        public struct Enumerator : IEnumerator<GridIndex>
        {
            private readonly GridIndex size;
            private readonly bool clamped;
            private readonly GridIndex start;
            private readonly GridIndex end;
            private readonly int start1;
            private readonly int end1;
            private readonly bool fromEnd;

            private GridIndex current;
            private int current1;
            private sbyte flag;

            public Enumerator(in GridRange range)
            {
                this.size = range.Size;
                this.clamped = range.Clamped;
                this.fromEnd = range.IsFromEnd;
                this.flag = -1;

                if (this.clamped)
                {
                    this.start1 = default;
                    this.end1 = default;
                    this.current1 = default;

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

                    this.current = this.fromEnd ? this.end : this.start;
                }
                else
                {
                    this.start = default;
                    this.end = default;
                    this.current = default;

                    var start1 = range.Start.ToIndex1(this.size);
                    var end1 = range.End.ToIndex1(this.size);
                    var increasing = start1 <= end1;

                    this.start1 = increasing ? start1 : end1;
                    this.end1 = increasing ? end1 : start1;
                    this.current1 = this.fromEnd ? this.end1 : this.start1;
                }
            }

            public bool MoveNext()
            {
                if (this.flag == 0)
                {
                    return this.clamped
                        ? this.fromEnd ? MoveNextFromEnd() : MoveNextFromStart()
                        : this.fromEnd ? MoveNextFromEnd1() : MoveNextFromStart1();
                }

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

            private bool MoveNextFromStart1()
            {
                if (this.current1 == this.end1)
                {
                    this.flag = 1;
                    return false;
                }

                this.current1++;
                return true;
            }

            private bool MoveNextFromEnd1()
            {
                if (this.current1 == this.start1)
                {
                    this.flag = 1;
                    return false;
                }

                this.current1--;
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

                    return this.clamped ? this.current : GridIndex.Convert(this.current1, this.size);
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
                this.current1 = this.fromEnd ? this.end1 : this.start1;
                this.flag = -1;
            }
        }
    }
}
