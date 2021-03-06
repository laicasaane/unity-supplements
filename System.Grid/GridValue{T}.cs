﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.Grid
{
    [Serializable]
    public readonly struct GridValue<T> : IEquatableReadOnlyStruct<GridValue<T>>, ISerializable
    {
        public readonly GridIndex Index;
        public readonly T Value;

        public GridValue(in GridIndex index, T value)
        {
            this.Index = index;
            this.Value = value;
        }

        public GridValue(in GridIndex index, in T value)
        {
            this.Index = index;
            this.Value = value;
        }

        public void Deconstruct(out GridIndex index, out T value)
        {
            index = this.Index;
            value = this.Value;
        }

        public GridValue<T> With(in GridIndex Index)
            => new GridValue<T>(in Index, this.Value);

        public GridValue<T> With(T Value)
            => new GridValue<T>(in this.Index, Value);

        public GridValue<T> With(in T Value)
            => new GridValue<T>(this.Index, in Value);

        public override bool Equals(object obj)
            => obj is GridValue<T> other &&
               this.Index.Equals(in other.Index) &&
               EqualityComparer<T>.Default.Equals(this.Value, other.Value);

        public bool Equals(in GridValue<T> other)
            => this.Index.Equals(in other.Index) &&
               EqualityComparer<T>.Default.Equals(this.Value, other.Value);

        public bool Equals(GridValue<T> other)
            => this.Index.Equals(in other.Index) &&
               EqualityComparer<T>.Default.Equals(this.Value, other.Value);

        public override int GetHashCode()
        {
#if USE_SYSTEM_HASHCODE
            return HashCode.Combine(this.Index, this.Value);
#endif

#pragma warning disable CS0162 // Unreachable code detected
            var hashCode = 1774931160;
            hashCode = hashCode * -1521134295 + this.Index.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(this.Value);
            return hashCode;
#pragma warning restore CS0162 // Unreachable code detected
        }

        public override string ToString()
            => $"[{this.Index}] = {this.Value}";

        private GridValue(SerializationInfo info, StreamingContext context)
        {
            this.Index = info.GetValueOrDefault<GridIndex>(nameof(this.Index));
            this.Value = info.GetValueOrDefault<T>(nameof(this.Value));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Index), this.Index);
            info.AddValue(nameof(this.Value), this.Value);
        }

        public static implicit operator GridValue<T>(in KeyValuePair<GridIndex, T> kvp)
        {
            var gridIndex = kvp.Key;
            return new GridValue<T>(in gridIndex, kvp.Value);
        }

        public static implicit operator GridValue<T>(in (GridIndex Index, T Value) kvp)
            => new GridValue<T>(in kvp.Index, kvp.Value);
    }
}