namespace System.Collections.Generic
{
    public readonly partial struct Segment<T>
    {
        public struct Enumerator : IEnumerator<T>
        {
            private readonly ISegmentSource<T> source;
            private readonly int start;
            private readonly int end; // cache Offset + Count, since it's a little slow
            private int current;

            internal Enumerator(in Segment<T> segment)
            {
                this.source = segment.source;
                this.start = segment.Offset;
                this.end = segment.Offset + segment.Count;
                this.current = this.start - 1;
            }

            public bool MoveNext()
            {
                if (this.current < this.end)
                {
                    this.current++;
                    return (this.current < this.end);
                }

                return false;
            }

            public T Current
            {
                get
                {
                    if (this.current < this.start)
                        throw ThrowHelper.GetInvalidOperationException_InvalidOperation_EnumNotStarted();

                    if (this.current >= this.end)
                        throw ThrowHelper.GetInvalidOperationException_InvalidOperation_EnumEnded();

                    return this.source[this.current];
                }
            }

            object IEnumerator.Current
                => this.Current;

            public void Reset()
            {
                this.current = this.start - 1;
            }

            public void Dispose()
            {
            }
        }
    }
}