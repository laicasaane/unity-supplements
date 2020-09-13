﻿using System;
using System.Grid;

namespace UnityEngine
{
    [Serializable]
    public struct GridVector : IEquatable<GridVector>, IComparable<GridVector>
    {
        [SerializeField, Min(0)]
        private int row;

        public int Row
        {
            get => this.row;
            set => this.row = Mathf.Max(value, 0);
        }

        [SerializeField, Min(0)]
        private int column;

        public int Column
        {
            get => this.column;
            set => this.column = Mathf.Max(value, 0);
        }

        public GridVector(int row, int column)
        {
            this.row = Mathf.Max(row, 0);
            this.column = Mathf.Max(column, 0);
        }

        public void Deconstruct(out int row, out int column)
        {
            row = this.row;
            column = this.column;
        }

        public GridVector With(int? Row = null, int? Column = null)
            => new GridVector(
                Row ?? this.row,
                Column ?? this.column
            );

        public int ToIndex1(int columnCount)
            => columnCount <= 0 ? 0 : this.column + this.row * columnCount;

        public int ToIndex1(in GridVector size)
            => size.column <= 0 ? 0 : this.column + this.row * size.column;

        public override bool Equals(object obj)
            => obj is GridVector other && this.row == other.row && this.column == other.column;

        public bool Equals(GridVector other)
            => this.row == other.row && this.column == other.column;

        public bool Equals(in GridVector other)
            => this.row == other.row && this.column == other.column;

        public int CompareTo(GridVector other)
        {
            var comp = this.row.CompareTo(other.row);
            return comp != 0 ? comp : this.column.CompareTo(other.column);
        }

        public int CompareTo(in GridVector other)
        {
            var comp = this.row.CompareTo(other.row);
            return comp != 0 ? comp : this.column.CompareTo(other.column);
        }

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
        /// Shorthand for writing GridVector(0, 0)
        /// </summary>
        public static GridVector Zero { get; } = new GridVector(0, 0);

        /// <summary>
        /// Shorthand for writing GridVector(1, 1)
        /// </summary>
        public static GridVector One { get; } = new GridVector(1, 1);

        /// <summary>
        /// Shorthand for writing GridVector(0, 1)
        /// </summary>
        public static GridVector Right { get; } = new GridVector(0, 1);

        /// <summary>
        /// Shorthand for writing GridVector(1, 0)
        /// </summary>
        public static GridVector Up { get; } = new GridVector(1, 0);

        public static implicit operator GridVector(in (int row, int column) value)
            => new GridVector(value.row, value.column);

        public static implicit operator GridIndex(in GridVector value)
            => new GridIndex(value.row, value.column);

        public static bool operator ==(in GridVector lhs, in GridVector rhs)
            => lhs.row == rhs.row && lhs.column == rhs.column;

        public static bool operator !=(in GridVector lhs, in GridVector rhs)
            => lhs.row != rhs.row || lhs.column != rhs.column;

        public static bool operator >(in GridVector lhs, in GridVector rhs)
            => lhs.CompareTo(in rhs) > 0;

        public static bool operator <(in GridVector lhs, in GridVector rhs)
            => lhs.CompareTo(in rhs) < 0;

        public static GridVector operator +(in GridVector lhs, in GridVector rhs)
            => new GridVector(lhs.row + rhs.row, lhs.column + rhs.column);

        public static GridVector operator -(in GridVector lhs, in GridVector rhs)
            => new GridVector(lhs.row - rhs.row, lhs.column - rhs.column);

        public static GridVector operator *(in GridVector lhs, int scale)
            => new GridVector(lhs.row * scale, lhs.column * scale);

        public static GridVector operator *(int scale, in GridVector rhs)
            => new GridVector(rhs.row * scale, rhs.column * scale);

        public static GridVector operator /(in GridVector lhs, int scale)
            => new GridVector(lhs.row / scale, lhs.column / scale);

        public static GridVector Clamp(in GridVector value, in GridVector min, in GridVector max)
            => new GridVector(
                value.row < min.row ? min.row : (value.row > max.row ? max.row : value.row),
                value.column < min.column ? min.column : (value.column > max.column ? max.column : value.column)
            );

        public static ReadRange<GridVector> Range(in GridVector pivot, in GridVector extend, in GridVector gridSize)
        {
            var lastIndex = gridSize - One;

            return new ReadRange<GridVector>(
                Clamp(pivot - extend, Zero, lastIndex),
                Clamp(pivot + extend, Zero, lastIndex)
            );
        }

        public static ReadRange<GridVector> Range(in GridVector pivot, bool byRow, in GridVector gridSize)
        {
            var lastIndex = gridSize - One;

            return new ReadRange<GridVector>(
                new GridVector(byRow ? pivot.row : 0, byRow ? 0 : pivot.column),
                new GridVector(byRow ? pivot.row : lastIndex.row, byRow ? lastIndex.column : pivot.column)
            );
        }
    }
}