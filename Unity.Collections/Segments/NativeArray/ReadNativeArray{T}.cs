using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
    public readonly struct ReadNativeArray<T> : IReadOnlyList<T>, IEquatableReadOnlyStruct<ReadNativeArray<T>>
        where T : struct
    {
        private readonly NativeArray<T> source;
        private readonly bool hasSource;

        public ReadNativeArray(in NativeArray<T> source)
        {
            this.source = source.IsCreated ? source : _empty;
            this.Length = this.source.Length;
            this.hasSource = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal NativeArray<T> GetSource()
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
            => obj is ReadNativeArray<T> other && Equals(in other);

        public bool Equals(ReadNativeArray<T> other)
        {
            if (this.source == null && other.source == null)
                return true;

            if (this.source == null || other.source == null)
                return false;

            return ReferenceEquals(this.source, other.source);
        }

        public bool Equals(in ReadNativeArray<T> other)
        {
            if (this.source == null && other.source == null)
                return true;

            if (this.source == null || other.source == null)
                return false;

            return ReferenceEquals(this.source, other.source);
        }

        public bool Equals(ReadNativeArray<T> x, ReadNativeArray<T> y)
            => x.Equals(in y);

        public int GetHashCode(ReadNativeArray<T> obj)
            => obj.GetHashCode();

        public bool Equals(in ReadNativeArray<T> x, in ReadNativeArray<T> y)
            => x.Equals(in y);

        public int GetHashCode(in ReadNativeArray<T> obj)
            => obj.GetHashCode();

        public void CopyTo(T[] array)
            => GetSource().CopyTo(array);

        public void CopyTo(NativeArray<T> array)
            => GetSource().CopyTo(array);

        public T[] ToArray()
            => !this.hasSource || this.Length == 0 ? (new T[0]) : this.source.ToArray();

        public Enumerator GetEnumerator()
            => new Enumerator(this.hasSource ? this : Empty);

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
            => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private static NativeArray<T> _empty { get; } = new NativeArray<T>(new T[0], Allocator.Persistent);

        public static ReadNativeArray<T> Empty { get; } = new ReadNativeArray<T>(_empty);

        public static implicit operator ReadNativeArray<T>(in NativeArray<T> source)
            => source == null ? Empty : new ReadNativeArray<T>(source);

        public static bool operator ==(in ReadNativeArray<T> a, in ReadNativeArray<T> b)
            => a.Equals(in b);

        public static bool operator !=(in ReadNativeArray<T> a, in ReadNativeArray<T> b)
            => !a.Equals(in b);

        public struct Enumerator : IEnumerator<T>
        {
            private readonly NativeArray<T> source;
            private readonly int end;
            private int current;

            internal Enumerator(in ReadNativeArray<T> array)
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