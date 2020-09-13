﻿using System.Collections.Generic;

namespace System.Grid
{
    public readonly struct GridValue<T> : IEquatableReadOnlyStruct<GridValue<T>>
    {
        public readonly GridIndex Index;
        public readonly T Value;

        public GridValue(in GridIndex index, T value)
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
            => new GridValue<T>(Index, this.Value);

        public GridValue<T> With(T Value)
            => new GridValue<T>(this.Index, Value);

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
            var hashCode = 1774931160;
            hashCode = hashCode * -1521134295 + this.Index.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(this.Value);
            return hashCode;
        }

        public static implicit operator GridValue<T>(in KeyValuePair<GridIndex, T> kvp)
            => new GridValue<T>(kvp.Key, kvp.Value);
    }
}