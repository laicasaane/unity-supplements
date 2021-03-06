using System;
using System.Runtime.Serialization;

namespace UnityEngine
{
    [Serializable]
    public readonly struct Size : IEquatableReadOnlyStruct<Size>, ISerializable
    {
        public readonly float Width;
        public readonly float Height;

        public float this[int index]
        {
            get
            {
                if (index == 0) return this.Width;
                if (index == 1) return this.Height;
                throw new IndexOutOfRangeException();
            }
        }

        public Size(float width, float height)
        {
            this.Width = width;
            this.Height = height;
        }

        public void Deconstruct(out float width, out float height)
        {
            width = this.Width;
            height = this.Height;
        }

        public Size With(float? Width = null, float? Height = null)
            => new Size(
                Width ?? this.Width,
                Height ?? this.Height
            );

        public override int GetHashCode()
        {
#if USE_SYSTEM_HASHCODE
            return HashCode.Combine(this.Width, this.Height);
#endif

#pragma warning disable CS0162 // Unreachable code detected
            var hashCode = 859600377;
            hashCode = hashCode * -1521134295 + this.Width.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Height.GetHashCode();
            return hashCode;
#pragma warning restore CS0162 // Unreachable code detected
        }

        public override bool Equals(object obj)
            => obj is Size other &&
               Mathf.Approximately(this.Width, other.Width) &&
               Mathf.Approximately(this.Height, other.Height);

        public bool Equals(Size other)
            => Mathf.Approximately(this.Width, other.Width) &&
               Mathf.Approximately(this.Height, other.Height);

        public bool Equals(in Size other)
            => Mathf.Approximately(this.Width, other.Width) &&
               Mathf.Approximately(this.Height, other.Height);

        public override string ToString()
            => $"({this.Width}, {this.Height})";

        private Size(SerializationInfo info, StreamingContext context)
        {
            this.Width = info.GetSingleOrDefault(nameof(this.Width));
            this.Height = info.GetSingleOrDefault(nameof(this.Height));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Width), this.Width);
            info.AddValue(nameof(this.Height), this.Height);
        }

        /// <summary>
        /// Shorthand for writing <see cref="Size"/>(0, 0).
        /// </summary>
        public static Size Zero { get; } = new Size(0, 0);

        public static implicit operator Size(in (float, float) value)
            => new Size(value.Item1, value.Item2);

        public static implicit operator Size(Vector2 value)
            => new Size(value.x, value.y);

        public static implicit operator Size(Vector2Int value)
            => new Size(value.x, value.y);

        public static implicit operator Vector2(in Size value)
            => new Vector2(value.Width, value.Height);

        public static explicit operator Vector2Int(in Size value)
            => new Vector2Int((int)value.Width, (int)value.Height);

        public static implicit operator Size(Rect value)
            => new Size(value.width, value.height);

        public static explicit operator Rect(in Size value)
            => new Rect(0, 0, value.Width, value.Height);

        public static explicit operator RectInt(in Size value)
            => new RectInt(0, 0, (int)value.Width, (int)value.Height);

        public static Size operator +(in Size lhs, in Size rhs)
            => new Size(lhs.Width + rhs.Width, lhs.Height + rhs.Height);

        public static Size operator -(in Size lhs, in Size rhs)
            => new Size(lhs.Width - rhs.Width, lhs.Height - rhs.Height);

        public static Size operator -(in Size a)
            => new Size(-a.Width, -a.Height);

        public static Size operator *(in Size lhs, float rhs)
            => new Size(lhs.Width * rhs, lhs.Height * rhs);

        public static Size operator *(float lhs, in Size rhs)
            => new Size(lhs * rhs.Width, lhs * rhs.Height);

        public static Size operator *(in Size lhs, in Vector2 rhs)
            => new Size(lhs.Width * rhs.x, lhs.Height * rhs.y);

        public static Size operator *(in Vector2 lhs, in Size rhs)
            => new Size(rhs.Width * lhs.x, rhs.Height * lhs.y);

        public static Size operator *(in Size lhs, in Size rhs)
            => new Size(lhs.Width * rhs.Width, lhs.Height * rhs.Height);

        public static Size operator /(in Size lhs, float rhs)
            => new Size(lhs.Width / rhs, lhs.Height / rhs);

        public static Size operator /(in Size lhs, in Vector2 rhs)
            => new Size(lhs.Width / rhs.x, lhs.Height / rhs.y);

        public static Size operator /(in Size lhs, in Size rhs)
            => new Size(lhs.Width / rhs.Width, lhs.Height / rhs.Height);

        public static bool operator ==(in Size lhs, in Size rhs)
            => Mathf.Approximately(lhs.Width, rhs.Width) &&
               Mathf.Approximately(lhs.Height, rhs.Height);

        public static bool operator !=(in Size lhs, in Size rhs)
            => !Mathf.Approximately(lhs.Width, rhs.Width) ||
               !Mathf.Approximately(lhs.Height, rhs.Height);
    }
}
