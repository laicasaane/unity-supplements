using System.Collections;
using System.Collections.Generic;

namespace System.Grid
{
    public partial struct GridRange
    {
        public struct Enumerator : IEnumerator<GridIndex>
        {
            private readonly GridIndex start, end;
            private readonly GridIndex min, max;
            private readonly bool fromEnd, byRow;

            private GridIndex current;
            private sbyte flag;

            public Enumerator(in GridRange range)
            {
                this.fromEnd = range.IsFromEnd;
                this.byRow = range.Direction == GridDirection.Row;
                this.flag = -1;

                var size = (GridSize)range.Size;

                if (range.Clamped)
                {
                    var rowIncreasing = range.Start.Row.CompareTo(range.End.Row) <= 0;
                    var colIncreasing = range.Start.Column.CompareTo(range.End.Column) <= 0;

                    this.start = size.ClampIndex(new GridIndex(
                        rowIncreasing ? range.Start.Row : range.End.Row,
                        colIncreasing ? range.Start.Column : range.End.Column
                    ));

                    this.end = size.ClampIndex(new GridIndex(
                        rowIncreasing ? range.End.Row : range.Start.Row,
                        colIncreasing ? range.End.Column : range.Start.Column
                    ));

                    this.min = this.start;
                    this.max = this.end;
                }
                else
                {
                    var start1 = range.Start.ToIndex1(size);
                    var end1 = range.End.ToIndex1(size);

                    this.start = start1 > end1 ? range.End : range.Start;
                    this.end = start1 > end1 ? range.Start : range.End;

                    this.min = GridIndex.Zero;
                    this.max = range.Size - GridIndex.One;
                }

                this.current = this.fromEnd ? this.end : this.start;
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

                    if (row > this.max.Row)
                    {
                        col += 1;
                        row = this.min.Row;
                    }
                }
                else
                {
                    col = this.current.Column + 1;
                    row = this.current.Row;

                    if (col > this.max.Column)
                    {
                        row += 1;
                        col = this.min.Column;
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

                    if (row < this.min.Row)
                    {
                        col -= 1;
                        row = this.max.Row;
                    }
                }
                else
                {
                    col = this.current.Column - 1;
                    row = this.current.Row;

                    if (col < this.min.Column)
                    {
                        row -= 1;
                        col = this.max.Column;
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
        }
    }
}