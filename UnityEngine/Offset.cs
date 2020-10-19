using System;
using System.Runtime.Serialization;

namespace UnityEngine
{
    [Serializable]
    public readonly struct Offset : IEquatableReadOnlyStruct<Offset>, ISerializable
    {
        public readonly float Left;

        public readonly float Right;

        public readonly float Top;

        public readonly float Bottom;

        /// <summary>
        /// Shortcut for <see cref="Left"/> + <see cref="Right"/>.
        /// </summary>
        public float Horizontal
            => this.Left + this.Right;

        /// <summary>
        /// Shortcut for <see cref="Top"/> + <see cref="Bottom"/>.
        /// </summary>
        public float Vertical
            => this.Top + this.Bottom;

        public float this[int index]
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

        public Offset(float left, float right, float top, float bottom)
        {
            this.Left = left;
            this.Right = right;
            this.Top = top;
            this.Bottom = bottom;
        }

        public void Deconstruct(out float left, out float right, out float top, out float bottom)
        {
            left = this.Left;
            right = this.Right;
            top = this.Top;
            bottom = this.Bottom;
        }

        public Offset With(float? Left = null, float? Right = null, float? Top = null, float? Bottom = null)
            => new Offset(
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
            => obj is Offset other &&
               this.Left == other.Left && this.Right == other.Right &&
               this.Top == other.Top && this.Bottom == other.Bottom;

        public bool Equals(Offset other)
            => this.Left == other.Left && this.Right == other.Right &&
               this.Top == other.Top && this.Bottom == other.Bottom;

        public bool Equals(in Offset other)
            => this.Left == other.Left && this.Right == other.Right &&
               this.Top == other.Top && this.Bottom == other.Bottom;

        private Offset(SerializationInfo info, StreamingContext context)
        {
            this.Left = info.GetSingleOrDefault(nameof(this.Left));
            this.Right = info.GetSingleOrDefault(nameof(this.Right));
            this.Top = info.GetSingleOrDefault(nameof(this.Top));
            this.Bottom = info.GetSingleOrDefault(nameof(this.Bottom));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Left), this.Left);
            info.AddValue(nameof(this.Right), this.Right);
            info.AddValue(nameof(this.Top), this.Top);
            info.AddValue(nameof(this.Bottom), this.Bottom);
        }

        /// <summary>
        /// Shorthand for writing <see cref="Offset"/>(0, 0, 0, 0).
        /// </summary>
        public static Offset Zero { get; } = new Offset(0, 0, 0, 0);

        public static implicit operator Offset(in (float, float, float, float) value)
            => new Offset(value.Item1, value.Item2, value.Item3, value.Item4);

        public static implicit operator Offset(RectOffset value)
            => new Offset(value.left, value.right, value.top, value.bottom);

        public static explicit operator RectOffset(in Offset value)
            => new RectOffset((int)value.Left, (int)value.Right, (int)value.Top, (int)value.Bottom);

        public static implicit operator Offset(in Vector4 v)
            => new Offset(v.x, v.y, v.z, v.w);

        public static implicit operator Vector4(in Offset c)
            => new Vector4(c.Left, c.Right, c.Top, c.Bottom);

        public static Offset operator +(in Offset lhs, in Offset rhs)
            => new Offset(lhs.Left + rhs.Left, lhs.Right + rhs.Right, lhs.Top + rhs.Top, lhs.Bottom + rhs.Bottom);

        public static Offset operator -(in Offset lhs, in Offset rhs)
            => new Offset(lhs.Left - rhs.Left, lhs.Right - rhs.Right, lhs.Top - rhs.Top, lhs.Bottom - rhs.Bottom);

        public static Offset operator -(in Offset a)
            => new Offset(-a.Left, -a.Right, -a.Top, -a.Bottom);

        public static Offset operator *(in Offset lhs, int rhs)
            => new Offset(lhs.Left * rhs, lhs.Right * rhs, lhs.Top * rhs, lhs.Bottom * rhs);

        public static Offset operator *(int lhs, in Offset rhs)
            => new Offset(lhs * rhs.Left, lhs * rhs.Right, lhs * rhs.Top, lhs * rhs.Bottom);

        public static Offset operator *(in Offset lhs, in Vector4 rhs)
            => new Offset(lhs.Left * rhs.x, lhs.Right * rhs.y, lhs.Top * rhs.z, lhs.Bottom * rhs.w);

        public static Offset operator *(in Vector4 lhs, in Offset rhs)
            => new Offset(rhs.Left * lhs.x, rhs.Right * lhs.y, rhs.Top * lhs.z, rhs.Bottom * lhs.w);

        public static Offset operator *(in Offset lhs, in Offset rhs)
            => new Offset(lhs.Left * rhs.Left, lhs.Right * rhs.Right, lhs.Top * rhs.Top, lhs.Bottom * rhs.Bottom);

        public static Offset operator /(in Offset lhs, int rhs)
            => new Offset(lhs.Left / rhs, lhs.Right / rhs, lhs.Top / rhs, lhs.Bottom / rhs);

        public static Offset operator /(in Offset lhs, in Vector4 rhs)
            => new Offset(lhs.Left / rhs.x, lhs.Right / rhs.y, lhs.Top / rhs.z, lhs.Bottom / rhs.w);

        public static Offset operator /(in Offset lhs, in Offset rhs)
            => new Offset(lhs.Left / rhs.Left, lhs.Right / rhs.Right, lhs.Top / rhs.Top, lhs.Bottom / rhs.Bottom);

        public static bool operator ==(in Offset lhs, in Offset rhs)
            => Mathf.Approximately(lhs.Left, rhs.Left) &&
               Mathf.Approximately(lhs.Right, rhs.Right) &&
               Mathf.Approximately(lhs.Top, rhs.Top) &&
               Mathf.Approximately(lhs.Bottom, rhs.Bottom);

        public static bool operator !=(in Offset lhs, in Offset rhs)
            => !Mathf.Approximately(lhs.Left, rhs.Left) ||
               !Mathf.Approximately(lhs.Right, rhs.Right) ||
               !Mathf.Approximately(lhs.Top, rhs.Top) ||
               !Mathf.Approximately(lhs.Bottom, rhs.Bottom);
    }
}
