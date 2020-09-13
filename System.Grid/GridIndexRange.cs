using System.Collections.Generic;

namespace System.Grid
{
    public readonly struct GridIndexRange
    {
        public static ReadRange<GridIndex> Get(in GridIndex start, in GridIndex end)
            => new ReadRange<GridIndex>(start, end, GetEnumerator());

        public static ReadRange<GridIndex> Get(in ReadRange<GridIndex> range)
            => new ReadRange<GridIndex>(range.Start, range.End, GetEnumerator());

        public static IRangeEnumerator<GridIndex> GetEnumerator()
            => new Enumerator();

        private readonly struct Enumerator : IRangeEnumerator<GridIndex>
        {
            public IEnumerator<GridIndex> Enumerate(GridIndex start, GridIndex end)
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
    }
}
