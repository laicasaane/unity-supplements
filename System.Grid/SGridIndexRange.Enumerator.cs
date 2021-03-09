using System.Collections;
using System.Collections.Generic;

namespace System.Grid
{
    public partial struct SGridIndexRange
    {

        public static implicit operator ReadRange<SGridIndex, Enumerator.ColumnFirst>(in SGridIndexRange value)
            => new ReadRange<SGridIndex, Enumerator.ColumnFirst>(value.Start, value.End, value.IsFromEnd);

        public static implicit operator ReadRange<SGridIndex, Enumerator.RowFirst>(in SGridIndexRange value)
            => new ReadRange<SGridIndex, Enumerator.RowFirst>(value.Start, value.End, value.IsFromEnd);

        public static implicit operator ReadRange<SGridIndex>(in SGridIndexRange value)
        {
            if (value.Direction == GridDirection.Row)
                return new ReadRange<SGridIndex>(value.Start, value.End, value.IsFromEnd, new Enumerator.RowFirst());
            else
                return new ReadRange<SGridIndex>(value.Start, value.End, value.IsFromEnd, new Enumerator.ColumnFirst());
        }

        public static implicit operator SGridIndexRange(in ReadRange<SGridIndex, Enumerator.ColumnFirst> value)
            => new SGridIndexRange(value.Start, value.End, value.IsFromEnd, GridDirection.Column);

        public static implicit operator SGridIndexRange(in ReadRange<SGridIndex, Enumerator.RowFirst> value)
            => new SGridIndexRange(value.Start, value.End, value.IsFromEnd, GridDirection.Row);

        public struct Enumerator : IEnumerator<SGridIndex>
        {
            private readonly SGridIndex start, end;
            private readonly int startValue, endValue;
            private readonly sbyte rowSign, colSign, compare;
            private readonly bool byRow;

            private SGridIndex current;
            private sbyte flag;

            public Enumerator(in SGridIndexRange range)
                : this(range.Start, range.End, range.IsFromEnd, range.Direction)
            { }

            public Enumerator(in SGridIndex start, in SGridIndex end, bool fromEnd, GridDirection direction)
            {
                this.byRow = direction == GridDirection.Row;
                this.colSign = default;

                var minCol = Math.Min(start.Column, end.Column);
                var maxCol = Math.Max(start.Column, end.Column);
                var colCount = Math.Abs(maxCol - minCol) + 1;

                if (fromEnd)
                {
                    this.start = end;
                    this.end = start;
                }
                else
                {
                    this.start = start;
                    this.end = end;
                }

                var startIndex = start.ToIndex1(colCount);
                var endIndex = end.ToIndex1(colCount);
                var increasing = startIndex <= endIndex;

                if (increasing)
                {
                    if (fromEnd)
                    {
                        if (this.byRow)
                        {
                            this.rowSign = -1;
                            this.colSign = (sbyte)(this.start.Column.CompareTo(this.end.Column) * -1);
                            this.startValue = Math.Max(this.start.Row, this.end.Row);
                            this.endValue = Math.Min(this.start.Row, this.end.Row);

                            if (this.start.Column.CompareTo(this.end.Column) < 0)
                            {
                                this.startValue -= 1;
                                this.compare = 0;
                            }
                            else
                            {
                                this.compare = -1;
                            }
                        }
                        else
                        {
                            this.rowSign = this.colSign = this.compare = -1;
                            this.startValue = Math.Max(this.start.Column, this.end.Column);
                            this.endValue = Math.Min(this.start.Column, this.end.Column);
                        }
                    }
                    else
                    {
                        if (this.byRow)
                        {
                            this.rowSign = 1;
                            this.colSign = (sbyte)(this.start.Column.CompareTo(this.end.Column) * -1);

                            this.startValue = Math.Min(this.start.Row, this.end.Row);
                            this.endValue = Math.Max(this.start.Row, this.end.Row);

                            if (this.start.Column.CompareTo(this.end.Column) > 0)
                            {
                                this.startValue += 1;
                                this.compare = 0;
                            }
                            else
                            {
                                this.compare = 1;
                            }
                        }
                        else
                        {
                            this.rowSign = this.colSign = this.compare = 1;
                            this.startValue = Math.Min(this.start.Column, this.end.Column);
                            this.endValue = Math.Max(this.start.Column, this.end.Column);
                        }
                    }
                }
                else
                {
                    if (fromEnd)
                    {
                        if (this.byRow)
                        {
                            this.rowSign = 1;
                            this.colSign = (sbyte)(this.start.Column.CompareTo(this.end.Column) * -1);
                            this.startValue = Math.Min(this.start.Row, this.end.Row);
                            this.endValue = Math.Max(this.start.Row, this.end.Row);

                            if (this.start.Column.CompareTo(this.end.Column) > 0)
                            {
                                this.startValue += 1;
                                this.compare = 0;
                            }
                            else
                            {
                                this.compare = 1;
                            }
                        }
                        else
                        {
                            this.rowSign = this.colSign = this.compare = 1;
                            this.startValue = Math.Min(this.start.Column, this.end.Column);
                            this.endValue = Math.Max(this.start.Column, this.end.Column);
                        }
                    }
                    else
                    {
                        if (this.byRow)
                        {
                            this.rowSign = -1;
                            this.colSign = (sbyte)(this.start.Column.CompareTo(this.end.Column) * -1);
                            this.startValue = Math.Max(this.start.Row, this.end.Row);
                            this.endValue = Math.Min(this.start.Row, this.end.Row);

                            if (this.start.Column.CompareTo(this.end.Column) < 0)
                            {
                                this.startValue -= 1;
                                this.compare = 0;
                            }
                            else
                            {
                                this.compare = -1;
                            }
                        }
                        else
                        {
                            this.rowSign = this.colSign = this.compare = -1;
                            this.startValue = Math.Max(this.start.Column, this.end.Column);
                            this.endValue = Math.Min(this.start.Column, this.end.Column);
                        }
                    }
                }

                this.current = this.start;
                this.flag = -1;
            }

            public Enumerator(GridDirection direction)
            {
                this.byRow = direction == GridDirection.Row;
                this.current = this.start = this.end = default;
                this.startValue = this.endValue = default;
                this.rowSign = this.colSign = this.compare = this.flag = default;
            }

            public bool MoveNext()
            {
                if (this.flag == 0)
                {
                    if (this.current == this.end)
                    {
                        this.flag = 1;
                        return false;
                    }

                    int row, col;

                    if (this.byRow)
                    {
                        row = this.current.Row + this.rowSign;
                        col = this.current.Column;

                        if (row.CompareTo(this.endValue) == this.compare && col != this.end.Column)
                        {
                            col += this.colSign;
                            row = this.startValue;
                        }
                    }
                    else
                    {
                        col = this.current.Column + this.colSign;
                        row = this.current.Row;

                        if (col.CompareTo(this.endValue) == this.compare)
                        {
                            row += this.rowSign;
                            col = this.startValue;
                        }
                    }

                    this.current = new SGridIndex(row, col);
                    return true;
                }

                if (this.flag < 0)
                {
                    this.flag = 0;
                    return true;
                }

                return false;
            }

            public SGridIndex Current
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

            object IEnumerator.Current
                => this.Current;

            public void Reset()
            {
                this.current = this.start;
                this.flag = -1;
            }

            public void Dispose()
            {
            }

            public readonly struct ColumnFirst : IRangeEnumerator<SGridIndex>
            {
                public IEnumerator<SGridIndex> Enumerate(SGridIndex start, SGridIndex end, bool fromEnd)
                    => new Enumerator(start, end, fromEnd, GridDirection.Column);
            }

            public readonly struct RowFirst : IRangeEnumerator<SGridIndex>
            {
                public IEnumerator<SGridIndex> Enumerate(SGridIndex start, SGridIndex end, bool fromEnd)
                    => new Enumerator(start, end, fromEnd, GridDirection.Row);
            }
        }
    }
}