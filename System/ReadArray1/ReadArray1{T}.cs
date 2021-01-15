using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System
{
    public readonly struct ReadArray1<T> : IReadOnlyList<T>, IEquatableReadOnlyStruct<ReadArray1<T>>
    {
        private readonly T[] source;

        public int Length => GetSource().Length;

        int IReadOnlyCollection<T>.Count => GetSource().Length;

        public ref readonly T this[int index]
        {
            get
            {
                var source = GetSource();

                if ((uint)index >= (uint)source.Length)
                    throw ThrowHelper.GetArgumentOutOfRange_IndexException();

                return ref source[index];
            }
        }

        T IReadOnlyList<T>.this[int index]
        {
            get
            {
                var source = GetSource();

                if ((uint)index >= (uint)source.Length)
                    throw ThrowHelper.GetArgumentOutOfRange_IndexException();

                return source[index];
            }
        }

        public ReadArray1(T[] source)
        {
            if (source == null)
            {
                this = default;
                return;
            }

            this.source = source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal T[] GetSource()
            => this.source ?? _empty;

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

            if (source.Length == 0)
                return Array.Empty<T>();

            var array = new T[source.Length];
            Array.Copy(source, 0, array, 0, source.Length);

            return array;
        }

        public Enumerator GetEnumerator()
            => new Enumerator(this);

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
            => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private static readonly T[] _empty = Array.Empty<T>();

        public static ReadArray1<T> Empty { get; } = new ReadArray1<T>(_empty);

        public static implicit operator ReadArray1<T>(T[] source)
            => source == null ? Empty : new ReadArray1<T>(source);

        public static implicit operator ReadCollection<T>(in ReadArray1<T> source)
            => new ReadCollection<T>(source.GetSource());

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