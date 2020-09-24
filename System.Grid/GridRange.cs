using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.Grid
{
    [Serializable]
    public readonly struct GridRange : IRange<GridIndex, GridRange.Enumerator>,
                                       IEquatableReadOnlyStruct<GridRange>, ISerializable
    {
        public GridIndex Start => this.start;

        public GridIndex End => this.end;

        public bool IsFromEnd => this.isFromEnd;

        public readonly GridIndex Size;
        public readonly bool Clamped;

        private readonly GridIndex start;
        private readonly GridIndex end;
        private readonly bool isFromEnd;

        public GridRange(in GridIndex size, in GridIndex start, in GridIndex end)
        {
            this.Size = size;
            this.Clamped = true;
            this.start = start;
            this.end = end;
            this.isFromEnd = false;
        }

        public GridRange(in GridIndex size, in GridIndex start, in GridIndex end, bool fromEnd)
        {
            this.Size = size;
            this.Clamped = true;
            this.start = start;
            this.end = end;
            this.isFromEnd = fromEnd;
        }

        public GridRange(in GridIndex size, bool clamped, in GridIndex start, in GridIndex end)
        {
            this.Size = size;
            this.Clamped = clamped;
            this.start = start;
            this.end = end;
            this.isFromEnd = false;
        }

        public GridRange(in GridIndex size, bool clamped, in GridIndex start, in GridIndex end, bool fromEnd)
        {
            this.Size = size;
            this.Clamped = clamped;
            this.start = start;
            this.end = end;
            this.isFromEnd = fromEnd;
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
            info.AddValue(nameof(this.Size), this.Size);
            info.AddValue(nameof(this.Clamped), this.Clamped);
            info.AddValue(nameof(this.Start), this.start);
            info.AddValue(nameof(this.End), this.end);
            info.AddValue(nameof(this.IsFromEnd), this.isFromEnd);
        }

        public void Deconstruct(out GridIndex size, out GridIndex start, out GridIndex end)
        {
            size = this.Size;
            start = this.start;
            end = this.end;
        }

        public void Deconstruct(out GridIndex size, out GridIndex start, out GridIndex end, out bool fromEnd)
        {
            size = this.Size;
            start = this.start;
            end = this.end;
            fromEnd = this.isFromEnd;
        }

        public void Deconstruct(out GridIndex size, out bool clamped, out GridIndex start, out GridIndex end)
        {
            size = this.Size;
            clamped = this.Clamped;
            start = this.start;
            end = this.end;
        }

        public void Deconstruct(out GridIndex size, out bool clamped, out GridIndex start, out GridIndex end, out bool fromEnd)
        {
            size = this.Size;
            clamped = this.Clamped;
            start = this.start;
            end = this.end;
            fromEnd = this.isFromEnd;
        }

        public GridRange With(in GridIndex? Size = null, in bool? Clamped = null, in GridIndex? Start = null,
                              in GridIndex? End = null, in bool? IsFromEnd = null)
            => new GridRange(
                Size ?? this.Size,
                Clamped ?? this.Clamped,
                Start ?? this.start,
                End ?? this.end,
                IsFromEnd ?? this.isFromEnd
            );

        public GridRange FromStart()
            => new GridRange(this.Size, this.Clamped, this.start, this.end, false);

        public GridRange FromEnd()
            => new GridRange(this.Size, this.Clamped, this.start, this.end, true);

        IRange<GridIndex> IRange<GridIndex>.FromStart()
            => FromStart();

        IRange<GridIndex> IRange<GridIndex>.FromEnd()
            => FromEnd();

        public override bool Equals(object obj)
            => obj is GridRange other &&
               this.Size.Equals(in other.Size) &&
               this.Clamped == other.Clamped &&
               this.start.Equals(in other.start) &&
               this.end.Equals(in other.end) &&
               this.isFromEnd == other.isFromEnd;

        public bool Equals(in GridRange other)
            => this.Size.Equals(in other.Size) &&
               this.Clamped == other.Clamped &&
               this.start.Equals(in other.start) &&
               this.end.Equals(in other.end) &&
               this.isFromEnd == other.isFromEnd;

        public bool Equals(GridRange other)
            => this.Size.Equals(in other.Size) &&
               this.Clamped == other.Clamped &&
               this.start.Equals(in other.start) &&
               this.end.Equals(in other.end) &&
               this.isFromEnd == other.isFromEnd;

        public override int GetHashCode()
        {
            var hashCode = -535992267;
            hashCode = hashCode * -1521134295 + this.Size.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Clamped.GetHashCode();
            hashCode = hashCode * -1521134295 + this.start.GetHashCode();
            hashCode = hashCode * -1521134295 + this.end.GetHashCode();
            hashCode = hashCode * -1521134295 + this.isFromEnd.GetHashCode();
            return hashCode;
        }

        public override string ToString()
            => $"{{ {nameof(this.Size)}={this.Size}, {nameof(this.Clamped)}={this.Clamped}, {nameof(this.Start)}={this.start}, {nameof(this.End)}={this.end}, {nameof(this.IsFromEnd)}={this.isFromEnd} }}";

        public Enumerator GetEnumerator()
            => new Enumerator(this);

        public Enumerator Range()
            => GetEnumerator();

        IEnumerator<GridIndex> IRange<GridIndex>.Range()
            => GetEnumerator();

        /// <summary>
        /// Automatically create a range from (a, b).
        /// If a <= b, then a is the start value, and b is the end value.
        /// Otherwise, they are swapped.
        /// </summary>
        public static GridRange Auto(in GridIndex size, bool clamped, in GridIndex a, in GridIndex b)
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
            => new GridIndexRange(value.start, value.end, value.isFromEnd);

        public static bool operator ==(in GridRange lhs, in GridRange rhs)
            => lhs.Equals(in rhs);

        public static bool operator !=(in GridRange lhs, in GridRange rhs)
            => !lhs.Equals(in rhs);

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
                this.fromEnd = range.isFromEnd;
                this.flag = -1;

                if (this.clamped)
                {
                    this.start1 = default;
                    this.end1 = default;
                    this.current1 = default;

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

                    this.current = this.fromEnd ? this.end : this.start;
                }
                else
                {
                    this.start = default;
                    this.end = default;
                    this.current = default;

                    var start1 = range.start.ToIndex1(this.size);
                    var end1 = range.end.ToIndex1(this.size);
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
