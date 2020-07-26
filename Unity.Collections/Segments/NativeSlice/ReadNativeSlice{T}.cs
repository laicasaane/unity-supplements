using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
    public readonly struct ReadNativeSlice<T> : IReadOnlyList<T>, IEquatableReadOnlyStruct<ReadNativeSlice<T>>
        where T : struct
    {
        private readonly NativeSlice<T> source;
        private readonly bool hasSource;

        public ReadNativeSlice(in NativeSlice<T> source)
        {
            this.source = source.Stride > 0 ? source : _empty;
            this.Length = this.source.Length;
            this.hasSource = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal NativeSlice<T> GetSource()
            => this.hasSource ? this.source : _empty;

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Length)
                    throw ThrowHelper.GetArgumentOutOfRange_IndexException();

                return this.source[index];
            }
        }

        public int Length { get; }

        int IReadOnlyCollection<T>.Count
            => this.Length;

        public override int GetHashCode()
            => GetSource().GetHashCode();

        public override bool Equals(object obj)
            => obj is ReadNativeSlice<T> other && Equals(in other);

        public bool Equals(ReadNativeSlice<T> other)
        {
            if (this.source == null && other.source == null)
                return true;

            if (this.source == null || other.source == null)
                return false;

            return ReferenceEquals(this.source, other.source);
        }

        public bool Equals(in ReadNativeSlice<T> other)
        {
            if (this.source == null && other.source == null)
                return true;

            if (this.source == null || other.source == null)
                return false;

            return ReferenceEquals(this.source, other.source);
        }

        public bool Equals(ReadNativeSlice<T> x, ReadNativeSlice<T> y)
            => x.Equals(in y);

        public int GetHashCode(ReadNativeSlice<T> obj)
            => obj.GetHashCode();

        public bool Equals(in ReadNativeSlice<T> x, in ReadNativeSlice<T> y)
            => x.Equals(in y);

        public int GetHashCode(in ReadNativeSlice<T> obj)
            => obj.GetHashCode();

        public void CopyTo(T[] array)
            => GetSource().CopyTo(array);

        public void CopyTo(in NativeArray<T> array)
            => GetSource().CopyTo(array);

        public T[] ToArray()
            => this.source.ToArray();

        public Enumerator GetEnumerator()
            => new Enumerator(this.hasSource ? this : Empty);

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
            => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private static NativeSlice<T> _empty { get; } = new NativeSlice<T>();

        public static ReadNativeSlice<T> Empty { get; } = new ReadNativeSlice<T>(_empty);

        public static implicit operator ReadNativeSlice<T>(in NativeSlice<T> source)
            => source.Stride > 0 ? new ReadNativeSlice<T>(source) : Empty;

        public static bool operator ==(in ReadNativeSlice<T> a, in ReadNativeSlice<T> b)
            => a.Equals(in b);

        public static bool operator !=(in ReadNativeSlice<T> a, in ReadNativeSlice<T> b)
            => !a.Equals(in b);

        public struct Enumerator : IEnumerator<T>
        {
            private readonly NativeSlice<T> source;
            private readonly int end;
            private int current;

            internal Enumerator(in ReadNativeSlice<T> array)
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