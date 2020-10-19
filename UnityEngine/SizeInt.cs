using System;
using System.Runtime.Serialization;

namespace UnityEngine
{
    [Serializable]
    public readonly struct SizeInt : IEquatableReadOnlyStruct<SizeInt>, ISerializable
    {
        public readonly int Width;
        public readonly int Height;

        public int this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return this.Width;
                    case 1: return this.Height;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        public SizeInt(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public void Deconstruct(out int width, out int height)
        {
            width = this.Width;
            height = this.Height;
        }

        public Size With(int? Width = null, int? Height = null)
            => new Size(
                Width ?? this.Width,
                Height ?? this.Height
            );

        public override int GetHashCode()
        {
            var hashCode = 859600377;
            hashCode = hashCode * -1521134295 + this.Width;
            hashCode = hashCode * -1521134295 + this.Height;
            return hashCode;
        }

        public override bool Equals(object obj)
            => obj is SizeInt other &&
               this.Width == other.Width &&
               this.Height == other.Height;

        public bool Equals(SizeInt other)
            => this.Width == other.Width && this.Height == other.Height;

        public bool Equals(in SizeInt other)
            => this.Width == other.Width && this.Height == other.Height;

        public override string ToString()
            => $"({this.Width}, {this.Height})";

        private SizeInt(SerializationInfo info, StreamingContext context)
        {
            this.Width = info.GetInt32OrDefault(nameof(this.Width));
            this.Height = info.GetInt32OrDefault(nameof(this.Height));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Width), this.Width);
            info.AddValue(nameof(this.Height), this.Height);
        }

        /// <summary>
        /// Shorthand for writing <see cref="SizeInt"/>(0, 0).
        /// </summary>
        public static SizeInt Zero { get; } = new SizeInt(0, 0);

        public static implicit operator SizeInt(in (int, int) value)
            => new SizeInt(value.Item1, value.Item2);

        public static implicit operator SizeInt(Vector2Int value)
            => new SizeInt(value.x, value.y);

        public static implicit operator Vector2Int(in SizeInt value)
            => new Vector2Int(value.Width, value.Height);

        public static implicit operator Vector2(in SizeInt value)
            => new Vector2(value.Width, value.Height);

        public static implicit operator SizeInt(RectInt value)
            => new SizeInt(value.width, value.height);

        public static explicit operator RectInt(in SizeInt value)
            => new RectInt(0, 0, value.Width, value.Height);

        public static explicit operator Rect(in SizeInt value)
            => new Rect(0, 0, value.Width, value.Height);

        public static SizeInt operator +(in SizeInt lhs, in SizeInt rhs)
            => new SizeInt(lhs.Width + rhs.Width, lhs.Height + rhs.Height);

        public static SizeInt operator -(in SizeInt lhs, in SizeInt rhs)
            => new SizeInt(lhs.Width - rhs.Width, lhs.Height - rhs.Height);

        public static SizeInt operator -(in SizeInt a)
            => new SizeInt(-a.Width, -a.Height);

        public static SizeInt operator *(in SizeInt lhs, int rhs)
            => new SizeInt(lhs.Width * rhs, lhs.Height * rhs);

        public static SizeInt operator *(int lhs, in SizeInt rhs)
            => new SizeInt(lhs * rhs.Width, lhs * rhs.Height);

        public static SizeInt operator *(in SizeInt lhs, in Vector2Int rhs)
            => new SizeInt(lhs.Width * rhs.x, lhs.Height * rhs.y);

        public static SizeInt operator *(in Vector2Int lhs, in SizeInt rhs)
            => new SizeInt(rhs.Width * lhs.x, rhs.Height * lhs.y);

        public static SizeInt operator *(in SizeInt lhs, in SizeInt rhs)
            => new SizeInt(lhs.Width * rhs.Width, lhs.Height * rhs.Height);

        public static SizeInt operator /(in SizeInt lhs, int rhs)
            => new SizeInt(lhs.Width / rhs, lhs.Height / rhs);

        public static SizeInt operator /(in SizeInt lhs, in Vector2Int rhs)
            => new SizeInt(lhs.Width / rhs.x, lhs.Height / rhs.y);

        public static SizeInt operator /(in SizeInt lhs, in SizeInt rhs)
            => new SizeInt(lhs.Width / rhs.Width, lhs.Height / rhs.Height);

        public static bool operator ==(in SizeInt lhs, in SizeInt rhs)
            => lhs.Width == rhs.Width && lhs.Height == rhs.Height;

        public static bool operator !=(in SizeInt lhs, in SizeInt rhs)
            => lhs.Width != rhs.Width || lhs.Height != rhs.Height;
    }
}
