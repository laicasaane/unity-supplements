using System.Collections;
using System.Collections.Generic;

namespace System.Grid
{
    public partial struct GridRange
    {
        public struct Enumerator : IEnumerator<GridIndex>
        {
            private readonly GridSize size;
            private readonly GridIndex start, end;
            private readonly int startValue, endValue;
            private readonly sbyte rowSign, colSign, compare;
            private readonly bool byRow, clamped;

            private GridIndex current;
            private sbyte flag;

            public Enumerator(in GridRange range)
                : this(range.Size, range.Clamped, range.Start, range.End, range.IsFromEnd, range.Direction)
            { }

            public Enumerator(in GridSize size, bool clamped, in GridIndex start, in GridIndex end, bool fromEnd, GridDirection direction)
            {
                var cStart = size.ClampIndex(start);
                var cEnd = size.ClampIndex(end);

                this.size = size;
                this.byRow = direction == GridDirection.Row;
                this.clamped = clamped;

                var startIndex = cStart.ToIndex1(this.size.Column);
                var endIndex = cEnd.ToIndex1(this.size.Column);
                var increasing = startIndex <= endIndex;

                if (fromEnd)
                {
                    this.start = cEnd;
                    this.end = cStart;
                }
                else
                {
                    this.start = cStart;
                    this.end = cEnd;
                }

                this.current = this.start;

                if (clamped)
                {
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
                }
                else
                {
                    this.startValue = this.endValue = default;
                    this.compare = default;
                    this.rowSign = this.colSign = (sbyte)(increasing
                                                          ? (fromEnd ? -1 : 1)
                                                          : (fromEnd ?  1 : -1));
                }

                this.flag = -1;
            }

            public Enumerator(GridDirection direction)
            {
                this.size = default;
                this.byRow = direction == GridDirection.Row;
                this.clamped = default;
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

                    if (this.clamped)
                        MoveNextClamped();
                    else
                        MoveNextUnclamped();

                    return true;
                }

                if (this.flag < 0)
                {
                    this.flag = 0;
                    return true;
                }

                return false;
            }

            private void MoveNextClamped()
            {
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

                this.current = new GridIndex(row, col);
            }

            private void MoveNextUnclamped()
            {
                var index1 = this.size.Index1Of(this.current);
                this.current = GridIndex.Convert(index1 + this.colSign, this.size.Column);
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
        }
    }
}