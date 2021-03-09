using System;
using System.Grid;

namespace UnityEngine
{
    /// <summary>
    /// Represent the signed coordinates of the 2D grid.
    /// </summary>
    [Serializable]
    public struct SGridVector : IEquatable<SGridVector>
    {
        [SerializeField]
        private int row;

        public int Row
        {
            get => this.row;
            set => this.row = value;
        }

        [SerializeField]
        private int column;

        public int Column
        {
            get => this.column;
            set => this.column = value;
        }

        public int this[int index]
        {
            get
            {
                if (index == 0) return this.row;
                if (index == 1) return this.column;
                throw new IndexOutOfRangeException();
            }
        }

        public SGridVector(int row, int column)
        {
            this.row = row;
            this.column = column;
        }

        public void Deconstruct(out int row, out int column)
        {
            row = this.row;
            column = this.column;
        }

        public SGridVector With(int? Row = null, int? Column = null)
            => new SGridVector(
                Row ?? this.row,
                Column ?? this.column
            );

        public override bool Equals(object obj)
            => obj is SGridVector other && this.row == other.row && this.column == other.column;

        public bool Equals(SGridVector other)
            => this.row == other.row && this.column == other.column;

        public bool Equals(in SGridVector other)
            => this.row == other.row && this.column == other.column;

        public override int GetHashCode()
        {
            var hashCode = -1663278630;
            hashCode = hashCode * -1521134295 + this.row.GetHashCode();
            hashCode = hashCode * -1521134295 + this.column.GetHashCode();
            return hashCode;
        }

        public override string ToString()
            => $"({this.row}, {this.column})";

        /// <summary>
        /// Shorthand for writing SGridVector(0, 0)
        /// </summary>
        public static SGridVector Zero { get; } = new SGridVector(0, 0);

        /// <summary>
        /// Shorthand for writing SGridVector(1, 1)
        /// </summary>
        public static SGridVector One { get; } = new SGridVector(1, 1);

        /// <summary>
        /// Shorthand for writing SGridVector(0, -1)
        /// </summary>
        public static SGridVector Left { get; } = new SGridVector(0, -1);

        /// <summary>
        /// Shorthand for writing SGridVector(0, 1)
        /// </summary>
        public static SGridVector Right { get; } = new SGridVector(0, 1);

        /// <summary>
        /// Shorthand for writing SGridVector(1, 0)
        /// </summary>
        public static SGridVector Up { get; } = new SGridVector(-1, 0);

        /// <summary>
        /// Shorthand for writing SGridVector(-1, 0)
        /// </summary>
        public static SGridVector Down { get; } = new SGridVector(1, 0);

        public static implicit operator SGridVector(in (int row, int column) value)
            => new SGridVector(value.row, value.column);

        public static implicit operator SGridVector(in SGridIndex value)
            => new SGridVector(value.Row, value.Column);

        public static implicit operator SGridIndex(in SGridVector value)
            => new SGridIndex(value.row, value.column);

        public static implicit operator SGridVector(in GridIndex value)
            => new SGridVector(value.Row, value.Column);

        public static implicit operator SGridVector(GridVector value)
            => new SGridVector(value.Row, value.Column);

        public static implicit operator GridVector(in SGridVector value)
            => new GridVector(value.row, value.column);

        public static implicit operator GridIndex(in SGridVector value)
            => new GridIndex(value.row, value.column);

        public static bool operator ==(in SGridVector lhs, in SGridVector rhs)
            => lhs.row == rhs.row && lhs.column == rhs.column;

        public static bool operator !=(in SGridVector lhs, in SGridVector rhs)
            => lhs.row != rhs.row || lhs.column != rhs.column;

        public static SGridVector operator +(in SGridVector lhs, in SGridVector rhs)
            => new SGridVector(lhs.row + rhs.row, lhs.column + rhs.column);

        public static SGridVector operator -(in SGridVector lhs, in SGridVector rhs)
            => new SGridVector(lhs.row - rhs.row, lhs.column - rhs.column);

        public static SGridVector operator *(in SGridVector lhs, int rhs)
            => new SGridVector(lhs.row * rhs, lhs.column * rhs);

        public static SGridVector operator *(int lhs, in SGridVector rhs)
            => new SGridVector(rhs.row * lhs, rhs.column * lhs);

        public static SGridVector operator *(in SGridVector lhs, in SGridVector rhs)
            => new SGridVector(lhs.row * rhs.Row, lhs.column * rhs.Column);

        public static SGridVector operator /(in SGridVector lhs, int rhs)
            => new SGridVector(lhs.row / rhs, lhs.column / rhs);

        public static SGridVector operator /(in SGridVector lhs, in SGridVector rhs)
            => new SGridVector(lhs.row / rhs.Row, lhs.column / rhs.Column);

        public static SGridVector operator %(in SGridVector lhs, int rhs)
            => new SGridVector(lhs.row % rhs, lhs.column % rhs);

        public static SGridVector operator %(in SGridVector lhs, in SGridVector rhs)
            => new SGridVector(lhs.row % rhs.Row, lhs.column % rhs.Column);

        public static SGridVector operator +(in SGridVector lhs, in SGridIndex rhs)
            => new SGridVector(lhs.row + rhs.Row, lhs.column + rhs.Column);

        public static SGridVector operator -(in SGridVector lhs, in SGridIndex rhs)
            => new SGridVector(lhs.row - rhs.Row, lhs.column - rhs.Column);

        public static SGridVector operator *(in SGridVector lhs, in SGridIndex rhs)
            => new SGridVector(lhs.row * rhs.Row, lhs.column * rhs.Column);

        public static SGridVector operator /(in SGridVector lhs, in SGridIndex rhs)
            => new SGridVector(lhs.row / rhs.Row, lhs.column / rhs.Column);

        public static SGridVector operator %(in SGridVector lhs, in SGridIndex rhs)
            => new SGridVector(lhs.row % rhs.Row, lhs.column % rhs.Column);

        public static SGridVector operator +(in SGridVector lhs, GridVector rhs)
            => new SGridVector(lhs.row + rhs.Row, lhs.column + rhs.Column);

        public static SGridVector operator -(in SGridVector lhs, GridVector rhs)
            => new SGridVector(lhs.row - rhs.Row, lhs.column - rhs.Column);

        public static SGridVector operator *(in SGridVector lhs, GridVector rhs)
            => new SGridVector(lhs.row * rhs.Row, lhs.column * rhs.Column);

        public static SGridVector operator /(in SGridVector lhs, GridVector rhs)
            => new SGridVector(lhs.row / rhs.Row, lhs.column / rhs.Column);

        public static SGridVector operator %(in SGridVector lhs, GridVector rhs)
            => new SGridVector(lhs.row % rhs.Row, lhs.column % rhs.Column);

        public static SGridVector operator +(in SGridVector lhs, in GridIndex rhs)
            => new SGridVector(lhs.row + rhs.Row, lhs.column + rhs.Column);

        public static SGridVector operator -(in SGridVector lhs, in GridIndex rhs)
            => new SGridVector(lhs.row - rhs.Row, lhs.column - rhs.Column);

        public static SGridVector operator *(in SGridVector lhs, in GridIndex rhs)
            => new SGridVector(lhs.row * rhs.Row, lhs.column * rhs.Column);

        public static SGridVector operator /(in SGridVector lhs, in GridIndex rhs)
            => new SGridVector(lhs.row / rhs.Row, lhs.column / rhs.Column);

        public static SGridVector operator %(in SGridVector lhs, in GridIndex rhs)
            => new SGridVector(lhs.row % rhs.Row, lhs.column % rhs.Column);

        public static SGridVector operator +(in SGridIndex lhs, in SGridVector rhs)
            => new SGridVector(lhs.Row + rhs.row, lhs.Column + rhs.column);

        public static SGridVector operator -(in SGridIndex lhs, in SGridVector rhs)
            => new SGridVector(lhs.Row - rhs.row, lhs.Column - rhs.column);

        public static SGridVector operator *(in SGridIndex lhs, in SGridVector rhs)
            => new SGridVector(lhs.Row * rhs.row, lhs.Column * rhs.column);

        public static SGridVector operator /(in SGridIndex lhs, in SGridVector rhs)
            => new SGridVector(lhs.Row / rhs.row, lhs.Column / rhs.column);

        public static SGridVector operator %(in SGridIndex lhs, in SGridVector rhs)
            => new SGridVector(lhs.Row % rhs.Row, lhs.Column % rhs.Column);

        public static SGridVector operator +(GridVector lhs, in SGridVector rhs)
            => new SGridVector(lhs.Row + rhs.row, lhs.Column + rhs.column);

        public static SGridVector operator -(GridVector lhs, in SGridVector rhs)
            => new SGridVector(lhs.Row - rhs.row, lhs.Column - rhs.column);

        public static SGridVector operator *(GridVector lhs, in SGridVector rhs)
            => new SGridVector(lhs.Row * rhs.row, lhs.Column * rhs.column);

        public static SGridVector operator /(GridVector lhs, in SGridVector rhs)
            => new SGridVector(lhs.Row / rhs.row, lhs.Column / rhs.column);

        public static SGridVector operator %(GridVector lhs, in SGridVector rhs)
            => new SGridVector(lhs.Row % rhs.Row, lhs.Column % rhs.Column);

        public static SGridVector operator +(in GridIndex lhs, in SGridVector rhs)
            => new SGridVector(lhs.Row + rhs.row, lhs.Column + rhs.column);

        public static SGridVector operator -(in GridIndex lhs, in SGridVector rhs)
            => new SGridVector(lhs.Row - rhs.row, lhs.Column - rhs.column);

        public static SGridVector operator *(in GridIndex lhs, in SGridVector rhs)
            => new SGridVector(lhs.Row * rhs.row, lhs.Column * rhs.column);

        public static SGridVector operator /(in GridIndex lhs, in SGridVector rhs)
            => new SGridVector(lhs.Row / rhs.row, lhs.Column / rhs.column);

        public static SGridVector operator %(in GridIndex lhs, in SGridVector rhs)
            => new SGridVector(lhs.Row % rhs.Row, lhs.Column % rhs.Column);
    }
}