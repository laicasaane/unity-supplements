using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
    public readonly struct Values<T> : IEnumerable<T>
    {
        private readonly T[] values;

        public Values(T[] values)
        {
            this.values = values;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator GetEnumerator()
            => new Enumerator(this.values);

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
            => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public static implicit operator Values<T>(T[] values)
            => new Values<T>(values);

        public struct Enumerator : IEnumerator<T>
        {
            private readonly T[] source;
            private readonly long length;

            private long current;
            private bool first;

            internal Enumerator(T[] array)
            {
                this.source = array ?? ReadArray1<T>.Empty.GetSource();
                this.length = this.source.LongLength;
                this.current = 0;
                this.first = true;
            }

            public bool MoveNext()
            {
                if (this.length == 0)
                    return false;

                if (this.first)
                {
                    this.first = false;
                    return true;
                }

                if (this.current < this.length - 1)
                {
                    this.current++;
                    return true;
                }

                return false;
            }

            public T Current
            {
                get
                {
                    if (this.current >= this.length)
                        throw ThrowHelper.GetInvalidOperationException_InvalidOperation_EnumEnded();

                    return this.source[this.current];
                }
            }

            object IEnumerator.Current
                => this.Current;

            public void Reset()
            {
                this.current = 0;
                this.first = true;
            }

            public void Dispose()
            {
            }
        }
    }
}