using System.Runtime.Serialization;

namespace System.Grid
{
    /// <summary>
    /// Represent the signed coordinates of the 2D grid.
    /// </summary>
    [Serializable]
    public readonly struct SGridIndex : IEquatableReadOnlyStruct<SGridIndex>, ISerializable
    {
        public readonly int Row;
        public readonly int Column;

        public int this[int index]
        {
            get
            {
                if (index == 0) return this.Row;
                if (index == 1) return this.Column;
                throw new IndexOutOfRangeException();
            }
        }

        public SGridIndex(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        private SGridIndex(SerializationInfo info, StreamingContext context)
        {
            this.Row = info.GetInt32OrDefault(nameof(this.Row));
            this.Column = info.GetInt32OrDefault(nameof(this.Column));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Row), this.Row);
            info.AddValue(nameof(this.Column), this.Column);
        }

        public void Deconstruct(out int row, out int column)
        {
            row = this.Row;
            column = this.Column;
        }

        public SGridIndex With(int? Row = null, int? Column = null)
            => new SGridIndex(
                Row ?? this.Row,
                Column ?? this.Column
            );

        public override bool Equals(object obj)
            => obj is SGridIndex other && this.Row == other.Row && this.Column == other.Column;

        public bool Equals(SGridIndex other)
            => this.Row == other.Row && this.Column == other.Column;

        public bool Equals(in SGridIndex other)
            => this.Row == other.Row && this.Column == other.Column;

        public override int GetHashCode()
        {
            var hashCode = 240067226;
            hashCode = hashCode * -1521134295 + this.Row.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Column.GetHashCode();
            return hashCode;
        }

        public override string ToString()
            => $"({this.Row}, {this.Column})";

        /// <summary>
        /// Shorthand for writing SGridIndex(0, 0)
        /// </summary>
        public static SGridIndex Zero { get; } = new SGridIndex(0, 0);

        /// <summary>
        /// Shorthand for writing SGridIndex(1, 1)
        /// </summary>
        public static SGridIndex One { get; } = new SGridIndex(1, 1);

        /// <summary>
        /// Shorthand for writing SGridIndex(0, -1)
        /// </summary>
        public static SGridIndex Left { get; } = new SGridIndex(0, -1);

        /// <summary>
        /// Shorthand for writing SGridIndex(0, 1)
        /// </summary>
        public static SGridIndex Right { get; } = new SGridIndex(0, 1);

        /// <summary>
        /// Shorthand for writing SGridIndex(1, 0)
        /// </summary>
        public static SGridIndex Up { get; } = new SGridIndex(1, 0);

        /// <summary>
        /// Shorthand for writing SGridIndex(-1, 0)
        /// </summary>
        public static SGridIndex Down { get; } = new SGridIndex(-1, 0);

        public static implicit operator SGridIndex(in (int row, int column) value)
            => new SGridIndex(value.row, value.column);

        public static implicit operator SGridIndex(in GridIndex value)
            => new SGridIndex(value.Row, value.Column);

        public static implicit operator GridIndex(in SGridIndex value)
            => new GridIndex(value.Row, value.Column);

        public static bool operator ==(in SGridIndex lhs, in SGridIndex rhs)
            => lhs.Row == rhs.Row && lhs.Column == rhs.Column;

        public static bool operator !=(in SGridIndex lhs, in SGridIndex rhs)
            => lhs.Row != rhs.Row || lhs.Column != rhs.Column;

        public static SGridIndex operator +(in SGridIndex lhs, in SGridIndex rhs)
            => new SGridIndex(lhs.Row + rhs.Row, lhs.Column + rhs.Column);

        public static SGridIndex operator -(in SGridIndex lhs, in SGridIndex rhs)
            => new SGridIndex(lhs.Row - rhs.Row, lhs.Column - rhs.Column);

        public static SGridIndex operator *(in SGridIndex lhs, int rhs)
            => new SGridIndex(lhs.Row * rhs, lhs.Column * rhs);

        public static SGridIndex operator *(int lhs, in SGridIndex rhs)
            => new SGridIndex(rhs.Row * lhs, rhs.Column * lhs);

        public static SGridIndex operator *(in SGridIndex lhs, in SGridIndex rhs)
            => new SGridIndex(lhs.Row * rhs.Row, lhs.Column * rhs.Column);

        public static SGridIndex operator /(in SGridIndex lhs, int rhs)
            => new SGridIndex(lhs.Row / rhs, lhs.Column / rhs);

        public static SGridIndex operator /(in SGridIndex lhs, in SGridIndex rhs)
            => new SGridIndex(lhs.Row / rhs.Row, lhs.Column / rhs.Column);

        public static SGridIndex operator %(in SGridIndex lhs, int rhs)
            => new SGridIndex(lhs.Row % rhs, lhs.Column % rhs);

        public static SGridIndex operator %(in SGridIndex lhs, in SGridIndex rhs)
            => new SGridIndex(lhs.Row % rhs.Row, lhs.Column % rhs.Column);

        public static SGridIndex operator +(in SGridIndex lhs, in GridIndex rhs)
            => new SGridIndex(lhs.Row + rhs.Row, lhs.Column + rhs.Column);

        public static SGridIndex operator -(in SGridIndex lhs, in GridIndex rhs)
            => new SGridIndex(lhs.Row - rhs.Row, lhs.Column - rhs.Column);

        public static SGridIndex operator *(in SGridIndex lhs, in GridIndex rhs)
            => new SGridIndex(lhs.Row * rhs.Row, lhs.Column * rhs.Column);

        public static SGridIndex operator /(in SGridIndex lhs, in GridIndex rhs)
            => new SGridIndex(lhs.Row / rhs.Row, lhs.Column / rhs.Column);

        public static SGridIndex operator %(in SGridIndex lhs, in GridIndex rhs)
            => new SGridIndex(lhs.Row % rhs.Row, lhs.Column % rhs.Column);

        public static SGridIndex operator +(in GridIndex lhs, in SGridIndex rhs)
            => new SGridIndex(lhs.Row + rhs.Row, lhs.Column + rhs.Column);

        public static SGridIndex operator -(in GridIndex lhs, in SGridIndex rhs)
            => new SGridIndex(lhs.Row - rhs.Row, lhs.Column - rhs.Column);

        public static SGridIndex operator *(in GridIndex lhs, in SGridIndex rhs)
            => new SGridIndex(lhs.Row * rhs.Row, lhs.Column * rhs.Column);

        public static SGridIndex operator /(in GridIndex lhs, in SGridIndex rhs)
            => new SGridIndex(lhs.Row / rhs.Row, lhs.Column / rhs.Column);

        public static SGridIndex operator %(in GridIndex lhs, in SGridIndex rhs)
            => new SGridIndex(lhs.Row % rhs.Row, lhs.Column % rhs.Column);
    }
}