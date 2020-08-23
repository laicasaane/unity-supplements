using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System
{
    public readonly struct ReadArray1<T> : IReadOnlyList<T>, IEquatableReadOnlyStruct<ReadArray1<T>>
    {
        private readonly T[] source;
        private readonly bool hasSource;

        public ReadArray1(T[] source)
        {
            this.source = source ?? _empty;
            this.Length = this.source.Length;
            this.hasSource = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal T[] GetSource()
            => this.hasSource ? (this.source ?? _empty) : _empty;

        public T this[int index]
            => GetSource()[index];

        public int Length { get; }

        int IReadOnlyCollection<T>.Count
            => this.Length;

        public override int GetHashCode()
            => GetSource().GetHashCode();

        public override bool Equals(object obj)
            => obj is ReadArray1<T> other && Equals(in other);

        public bool Equals(ReadArray1<T> other)
        {
            var source = GetSource();
            var otherSource = other.GetSource();

            return source == otherSource;
        }

        public bool Equals(in ReadArray1<T> other)
        {
            var source = GetSource();
            var otherSource = other.GetSource();

            return source == otherSource;
        }

        public void CopyTo(Array array, long index)
            => GetSource().CopyTo(array, index);

        public void CopyTo(Array array, int index)
            => GetSource().CopyTo(array, index);

        public T[] ToArray()
        {
            var source = GetSource();
            var array = new T[source.Length];

            for (var i = 0; i < source.Length; i++)
            {
                array[i] = source[i];
            }

            return array;
        }

        public Enumerator GetEnumerator()
            => new Enumerator(this);

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
            => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private static T[] _empty { get; } = new T[0];

        public static ReadArray1<T> Empty { get; } = new ReadArray1<T>(_empty);

        public static implicit operator ReadArray1<T>(T[] source)
            => source == null ? Empty : new ReadArray1<T>(source);

        public static bool operator ==(in ReadArray1<T> a, in ReadArray1<T> b)
            => a.Equals(in b);

        public static bool operator !=(in ReadArray1<T> a, in ReadArray1<T> b)
            => !a.Equals(in b);

        public struct Enumerator : IEnumerator<T>
        {
            private readonly T[] source;
            private readonly int end;
            private int current;

            internal Enumerator(in ReadArray1<T> array)
            {
                this.source = array.GetSource();
                this.end = this.source.Length;
                this.current = -1;
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
                    if (this.current < 0)
                        throw ThrowHelper.GetInvalidOperationException_InvalidOperation_EnumNotStarted();

                    if (this.current >= this.end)
                        throw ThrowHelper.GetInvalidOperationException_InvalidOperation_EnumEnded();

                    return this.source[this.current];
                }
            }

            object IEnumerator.Current
                => this.Current;

            void IEnumerator.Reset()
            {
                this.current = -1;
            }

            public void Dispose()
            {
            }
        }
    }
}