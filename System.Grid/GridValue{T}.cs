using System.Collections.Generic;
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

        public override string ToString()
            => $"[{this.Index}] = {this.Value}";

        private GridValue(SerializationInfo info, StreamingContext context)
        {
            try
            {
                this.Index = (GridIndex)info.GetValue(nameof(this.Index), typeof(GridIndex));
            }
            catch
            {
                this.Index = default;
            }

            try
            {
                this.Value = (T)info.GetValue(nameof(this.Value), typeof(T));
            }
            catch
            {
                this.Value = default;
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Index), this.Index);
            info.AddValue(nameof(this.Value), this.Value);
        }

        public static implicit operator GridValue<T>(in KeyValuePair<GridIndex, T> kvp)
            => new GridValue<T>(kvp.Key, kvp.Value);
    }
}