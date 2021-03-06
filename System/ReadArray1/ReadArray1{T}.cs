﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System
{
    public readonly struct ReadArray1<T> : IReadOnlyList<T>, IEquatableReadOnlyStruct<ReadArray1<T>>
    {
        private readonly T[] source;

        public readonly int Length;
        public readonly long LongLength;

        int IReadOnlyCollection<T>.Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.Length;
        }

        public ref readonly T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if ((uint)index >= (uint)this.Length)
                    throw ThrowHelper.GetArgumentOutOfRange_IndexException();

                return ref this.source[index];
            }
        }

        public ref readonly T this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (index >= this.LongLength)
                    throw ThrowHelper.GetArgumentOutOfRange_IndexException();

                return ref this.source[index];
            }
        }

        public ref readonly T this[long index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (index >= this.LongLength)
                    throw ThrowHelper.GetArgumentOutOfRange_IndexException();

                return ref this.source[index];
            }
        }

        T IReadOnlyList<T>.this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if ((uint)index >= (uint)this.Length)
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
            this.Length = source.Length;
            this.LongLength = source.LongLength;
        }

        public ReadArray1(T[] source, long longLength)
        {
            if (source == null)
            {
                this = default;
                return;
            }

            var length = source.Length;

            if (longLength < length)
                length = (int)longLength;

            this.source = source;
            this.Length = length;
            this.LongLength = Math.Min(source.LongLength, longLength);
        }

        public ReadArray1(T[] source, int length, long longLength)
        {
            if (source == null)
            {
                this = default;
                return;
            }

            this.source = source;
            this.Length = Math.Min(source.Length, length);
            this.LongLength = Math.Min(source.LongLength, longLength);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal T[] GetSource()
            => this.source ?? _empty;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(Array array, uint index)
            => GetSource().CopyTo(array, index);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(Array array, int index)
            => GetSource().CopyTo(array, index);

        public T[] ToArray()
        {
            var source = GetSource();
            var length = Math.Min(source.LongLength, this.LongLength);

            if (length == 0)
                return Array.Empty<T>();

            var array = new T[length];
            Array.Copy(source, 0, array, 0, length);

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator GetEnumerator()
            => new Enumerator(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
            => GetEnumerator();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
            private readonly long length;

            private long current;
            private bool first;

            internal Enumerator(in ReadArray1<T> array)
            {
                this.source = array.GetSource();
                this.length = Math.Min(this.source.LongLength, array.LongLength);
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