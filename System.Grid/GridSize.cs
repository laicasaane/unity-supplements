using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.Grid
{
    [Serializable]
    public readonly struct GridSize : IEquatableReadOnlyStruct<GridSize>, ISerializable
    {
        public int Row => this.value.Row;

        public int Column => this.value.Column;

        private readonly GridIndex value;

        public GridSize(in GridIndex value)
            => this.value = value;

        private GridSize(SerializationInfo info, StreamingContext context)
        {
            try
            {
                this.value = (GridIndex)info.GetValue(nameof(this.value), typeof(GridIndex));
            }
            catch
            {
                this.value = default;
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.value), this.value);
        }

        public override int GetHashCode()
            => -1937169414 + this.value.GetHashCode();

        public override bool Equals(object obj)
            => obj is GridSize other && this.value.Equals(in other.value);

        public bool Equals(in GridSize other)
            => this.value.Equals(in other.value);

        public bool Equals(GridSize other)
            => this.value.Equals(in other.value);

        public bool ValidateIndex(in GridIndex value)
        => value.Row < this.value.Row && value.Column < this.value.Column;

        public GridIndex LastIndex()
            => this.value - GridIndex.One;

        public GridIndex ClampIndex(in GridIndex value)
            => new GridIndex(
                value.Row >= this.value.Row ? this.value.Row - 1 : value.Row,
                value.Column >= this.value.Column ? this.value.Column - 1 : value.Column
            );

        public GridRange ClampIndexRange(in GridIndex start, in GridIndex end)
            => new GridRange(
                this.value,
                ClampIndex(start),
                ClampIndex(end)
            );

        public GridRange ClampIndexRange(in GridIndexRange range)
            => new GridRange(
                this.value,
                ClampIndex(range.Start),
                ClampIndex(range.End),
                range.IsFromEnd
            );

        public GridRange ClampIndexRange(in GridRange range)
            => new GridRange(
                this.value,
                range.Clamped,
                ClampIndex(range.Start),
                ClampIndex(range.End),
                range.IsFromEnd
            );

        public GridRange IndexRange(in GridIndex pivot, int extend)
            => IndexRange(pivot, GridIndex.One * extend);

        public GridRange IndexRange(in GridIndex pivot, int lowerExtend, int upperExtend)
            => IndexRange(pivot, GridIndex.One * lowerExtend, GridIndex.One * upperExtend);

        public GridRange IndexRange(in GridIndex pivot, in GridIndex extend)
            => new GridRange(
                this.value,
                ClampIndex(pivot - extend),
                ClampIndex(pivot + extend)
            );

        public GridRange IndexRange(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => new GridRange(
                this.value,
                ClampIndex(pivot - lowerExtend),
                ClampIndex(pivot + upperExtend)
            );

        public GridRange IndexRange(in GridIndex pivot, bool row)
            => new GridRange(
                this.value,
                new GridIndex(row ? pivot.Row : 0, row ? 0 : pivot.Column),
                new GridIndex(row ? pivot.Row : this.value.Row - 1, row ? this.value.Column - 1 : pivot.Column)
            );

        public GridRange IndexRange()
            => new GridRange(
                this.value,
                GridIndex.Zero,
                this.value - GridIndex.One
            );

        public static GridSize Zero { get; } = new GridSize(GridIndex.Zero);

        public static implicit operator GridIndex(in GridSize value)
            => value.value;

        public static implicit operator GridSize(in GridIndex value)
            => new GridSize(value);

        public static bool operator ==(in GridSize lhs, in GridSize rhs)
            => lhs.value.Equals(in rhs.value);

        public static bool operator !=(in GridSize lhs, in GridSize rhs)
            => !lhs.value.Equals(in rhs.value);

        public static GridSize operator +(in GridSize lhs, in GridSize rhs)
            => new GridSize(lhs.value + rhs.value);

        public static GridSize operator -(in GridSize lhs, in GridSize rhs)
            => new GridSize(lhs.value - rhs.value);

        public static GridSize operator *(in GridSize lhs, int rhs)
            => new GridSize(lhs.value * rhs);

        public static GridSize operator *(int lhs, in GridSize rhs)
            => new GridSize(rhs.value * lhs);

        public static GridSize operator *(in GridSize lhs, in GridIndex rhs)
            => new GridSize(lhs.value * rhs);

        public static GridSize operator *(in GridIndex lhs, in GridSize rhs)
            => new GridSize(lhs * rhs.value);

        public static GridSize operator /(in GridSize lhs, int rhs)
            => new GridSize(lhs.value / rhs);

        public static GridSize operator /(in GridSize lhs, in GridIndex rhs)
            => new GridSize(lhs.value / rhs);
    }
}