using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System
{
    public readonly struct ReadArray1<T> : IReadOnlyList<T>, IEquatableReadOnlyStruct<ReadArray1<T>>
    {
        private readonly T[] source;
        private readonly int length;

        public int Length => this.length;

        int IReadOnlyCollection<T>.Count => this.length;

        public ref readonly T this[int index]
        {
            get
            {
                if ((uint)index >= (uint)this.length)
                    throw ThrowHelper.GetArgumentOutOfRange_IndexException();

                return ref this.source[index];
            }
        }

        T IReadOnlyList<T>.this[int index]
        {
            get
            {
                if ((uint)index >= (uint)this.length)
                    throw ThrowHelper.GetArgumentOutOfRange_IndexException();

                return this.source[index];
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
            this.length = this.source.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal T[] GetSource()
            => (0 >= (uint)this.length) ? _empty : this.source;

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
            if (0 >= (uint)this.length)
                return Array.Empty<T>();

            var array = new T[this.length];
            Array.Copy(this.source, 0, array, 0, this.length);

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