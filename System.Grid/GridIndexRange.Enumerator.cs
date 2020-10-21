using System.Collections;
using System.Collections.Generic;

namespace System.Grid
{
    public partial struct GridIndexRange
    {
        public struct Enumerator : IEnumerator<GridIndex>, IRangeEnumerator<GridIndex>
        {
            private readonly GridIndex start, end;
            private readonly bool fromEnd, byRow;

            private GridIndex current;
            private sbyte flag;

            public Enumerator(in GridIndexRange range)
            {
                var rowIncreasing = range.Start.Row.CompareTo(range.End.Row) <= 0;
                var colIncreasing = range.Start.Column.CompareTo(range.End.Column) <= 0;

                this.start = new GridIndex(
                    rowIncreasing ? range.Start.Row : range.End.Row,
                    colIncreasing ? range.Start.Column : range.End.Column
                );

                this.end = new GridIndex(
                    rowIncreasing ? range.End.Row : range.Start.Row,
                    colIncreasing ? range.End.Column : range.Start.Column
                );

                this.fromEnd = range.IsFromEnd;
                this.byRow = range.Direction == GridDirection.Row;
                this.current = this.fromEnd ? this.end : this.start;
                this.flag = -1;
            }

            public Enumerator(bool rowFirst)
            {
                this.byRow = rowFirst;
                this.start = this.end = default;
                this.fromEnd = default;
                this.current = default;
                this.flag = default;
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

                int row, col;

                if (this.byRow)
                {
                    row = this.current.Row + 1;
                    col = this.current.Column;

                    if (row > this.end.Row)
                    {
                        col += 1;
                        row = this.start.Row;
                    }
                }
                else
                {
                    col = this.current.Column + 1;
                    row = this.current.Row;

                    if (col > this.end.Column)
                    {
                        row += 1;
                        col = this.start.Column;
                    }
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

                int row, col;

                if (this.byRow)
                {
                    row = this.current.Row - 1;
                    col = this.current.Column;

                    if (row < this.start.Row)
                    {
                        col -= 1;
                        row = this.end.Row;
                    }
                }
                else
                {
                    col = this.current.Column - 1;
                    row = this.current.Row;

                    if (col < this.start.Column)
                    {
                        row -= 1;
                        col = this.end.Column;
                    }
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
                var rowIncreasing = start.Row.CompareTo(end.Row) <= 0;
                var colIncreasing = start.Column.CompareTo(end.Column) <= 0;

                var newStart = new GridIndex(
                    rowIncreasing ? start.Row : end.Row,
                    colIncreasing ? start.Column : end.Column
                );

                var newEnd = new GridIndex(
                    rowIncreasing ? end.Row : start.Row,
                    colIncreasing ? end.Column : start.Column
                );

                return fromEnd ? EnumerateFromEnd(newStart, newEnd) : EnumerateFromStart(newStart, newEnd);
            }

            private IEnumerator<GridIndex> EnumerateFromStart(GridIndex start, GridIndex end)
            {
                if (this.byRow)
                {
                    for (var c = start.Column; c <= end.Column; c++)
                    {
                        for (var r = start.Row; r <= end.Row; r++)
                        {
                            yield return new GridIndex(r, c);
                        }
                    }
                }
                else
                {
                    for (var r = start.Row; r <= end.Row; r++)
                    {
                        for (var c = start.Column; c <= end.Column; c++)
                        {
                            yield return new GridIndex(r, c);
                        }
                    }
                }
            }

            private IEnumerator<GridIndex> EnumerateFromEnd(GridIndex start, GridIndex end)
            {
                if (this.byRow)
                {
                    for (var c = end.Column; c >= start.Column; c--)
                    {
                        for (var r = end.Row; r >= start.Row; r--)
                        {
                            yield return new GridIndex(r, c);
                        }
                    }
                }
                else
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
}