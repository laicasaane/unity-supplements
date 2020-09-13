using System;
using System.Runtime.Serialization;

namespace UnityEngine
{
    [Serializable]
    public readonly struct OffsetInt : IEquatableReadOnlyStruct<OffsetInt>, ISerializable
    {
        public readonly int Left;

        public readonly int Right;

        public readonly int Top;

        public readonly int Bottom;

        /// <summary>
        /// Shortcut for <see cref="Left"/> + <see cref="Right"/>.
        /// </summary>
        public int Horizontal
            => this.Left + this.Right;

        /// <summary>
        /// Shortcut for <see cref="Top"/> + <see cref="Bottom"/>.
        /// </summary>
        public int Vertical
            => this.Top + this.Bottom;

        public int this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return this.Left;
                    case 1: return this.Right;
                    case 2: return this.Top;
                    case 3: return this.Bottom;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        public OffsetInt(int left, int right, int top, int bottom)
        {
            this.Left = left;
            this.Right = right;
            this.Top = top;
            this.Bottom = bottom;
        }

        public void Deconstruct(out int left, out int right, out int top, out int bottom)
        {
            left = this.Left;
            right = this.Right;
            top = this.Top;
            bottom = this.Bottom;
        }

        public OffsetInt With(int? Left = null, int? Right = null, int? Top = null, int? Bottom = null)
            => new OffsetInt(
                Left ?? this.Left,
                Right ?? this.Right,
                Top ?? this.Top,
                Bottom ?? this.Bottom
            );

        public override string ToString()
            => $"({this.Left}, {this.Right}, {this.Top}, {this.Bottom})";

        public override int GetHashCode()
        {
            var hashCode = 551583723;
            hashCode = hashCode * -1521134295 + this.Left.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Right.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Top.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Bottom.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
            => obj is OffsetInt other &&
               this.Left == other.Left && this.Right == other.Right &&
               this.Top == other.Top && this.Bottom == other.Bottom;

        public bool Equals(OffsetInt other)
            => this.Left == other.Left && this.Right == other.Right &&
               this.Top == other.Top && this.Bottom == other.Bottom;

        public bool Equals(in OffsetInt other)
            => this.Left == other.Left && this.Right == other.Right &&
               this.Top == other.Top && this.Bottom == other.Bottom;

        private OffsetInt(SerializationInfo info, StreamingContext context)
        {
            try
            {
                this.Left = info.GetInt32(nameof(this.Left));
            }
            catch
            {
                this.Left = default;
            }

            try
            {
                this.Right = info.GetInt32(nameof(this.Right));
            }
            catch
            {
                this.Right = default;
            }

            try
            {
                this.Top = info.GetInt32(nameof(this.Top));
            }
            catch
            {
                this.Top = default;
            }

            try
            {
                this.Bottom = info.GetInt32(nameof(this.Bottom));
            }
            catch
            {
                this.Bottom = default;
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Left), this.Left);
            info.AddValue(nameof(this.Right), this.Right);
            info.AddValue(nameof(this.Top), this.Top);
            info.AddValue(nameof(this.Bottom), this.Bottom);
        }

        /// <summary>
        /// Shorthand for writing <see cref="OffsetInt"/>(0, 0, 0, 0).
        /// </summary>
        public static OffsetInt Zero { get; } = new OffsetInt(0, 0, 0, 0);

        public static implicit operator OffsetInt(in (int, int, int, int) value)
            => new OffsetInt(value.Item1, value.Item2, value.Item3, value.Item4);

        public static implicit operator OffsetInt(RectOffset value)
            => new OffsetInt(value.left, value.right, value.top, value.bottom);

        public static explicit operator RectOffset(in OffsetInt value)
            => new RectOffset(value.Left, value.Right, value.Top, value.Bottom);

        public static OffsetInt operator +(in OffsetInt lhs, in OffsetInt rhs)
            => new OffsetInt(lhs.Left + rhs.Left, lhs.Right + rhs.Right, lhs.Top + rhs.Top, lhs.Bottom + rhs.Bottom);

        public static OffsetInt operator -(in OffsetInt lhs, in OffsetInt rhs)
            => new OffsetInt(lhs.Left - rhs.Left, lhs.Right - rhs.Right, lhs.Top - rhs.Top, lhs.Bottom - rhs.Bottom);

        public static OffsetInt operator -(in OffsetInt a)
            => new OffsetInt(-a.Left, -a.Right, -a.Top, -a.Bottom);

        public static OffsetInt operator *(in OffsetInt lhs, int rhs)
            => new OffsetInt(lhs.Left * rhs, lhs.Right * rhs, lhs.Top * rhs, lhs.Bottom * rhs);

        public static OffsetInt operator *(int lhs, in OffsetInt rhs)
            => new OffsetInt(lhs * rhs.Left, lhs * rhs.Right, lhs * rhs.Top, lhs * rhs.Bottom);

        public static OffsetInt operator /(in OffsetInt lhs, int rhs)
            => new OffsetInt(lhs.Left / rhs, lhs.Right / rhs, lhs.Top / rhs, lhs.Bottom / rhs);
    }
}
