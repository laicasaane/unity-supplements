using System;

namespace UnityEngine
{
    [Serializable]
    public readonly struct Size : IEquatableReadOnlyStruct<Size>, IComparableReadOnlyStruct<Size>
    {
        public readonly float Width;
        public readonly float Height;
        public readonly float Area;

        [Obsolete("This property has been deprecated. Use Area instead.")]
        public float Total => this.Area;

        public Size(float width, float height)
        {
            this.Width = width;
            this.Height = height;
            this.Area = width * height;
        }

        public override int GetHashCode()
        {
            var hashCode = 859600377;
            hashCode = hashCode * -1521134295 + this.Width.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Height.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
            => obj is Size other &&
               Mathf.Approximately(this.Width, other.Width) &&
               Mathf.Approximately(this.Height, other.Height);

        public int CompareTo(Size other)
        {
            var comp = this.Width.CompareTo(other.Width);

            if (comp == 0)
                return this.Height.CompareTo(other.Height);

            return comp;
        }

        public int CompareTo(in Size other)
        {
            var comp = this.Width.CompareTo(other.Width);

            if (comp == 0)
                return this.Height.CompareTo(other.Height);

            return comp;
        }

        public bool Equals(Size other)
            => Mathf.Approximately(this.Width, other.Width) &&
               Mathf.Approximately(this.Height, other.Height);

        public bool Equals(in Size other)
            => Mathf.Approximately(this.Width, other.Width) &&
               Mathf.Approximately(this.Height, other.Height);

        public void Deconstruct(out float width, out float height)
        {
            width = this.Width;
            height = this.Height;
        }

        public override string ToString()
            => $"({this.Width}, {this.Height})";

        /// <summary>
        /// Shorthand for writing <see cref="Size"/>(0, 0).
        /// </summary>
        public static Size Zero { get; } = new Size(0, 0);

        public static implicit operator Size(in (float, float) value)
            => new Size(value.Item1, value.Item2);

        public static implicit operator Size(in Vector2 value)
            => new Size(value.x, value.y);

        public static implicit operator Vector2(in Size value)
            => new Vector2(value.Width, value.Height);

        public static explicit operator Vector2Int(in Size value)
            => new Vector2Int((int)value.Width, (int)value.Height);

        public static implicit operator Size(in Rect value)
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

        public static Size operator /(in Size lhs, float rhs)
            => new Size(lhs.Width / rhs, lhs.Height / rhs);

        public static bool operator ==(in Size lhs, in Size rhs)
            => Mathf.Approximately(lhs.Width, rhs.Width) &&
               Mathf.Approximately(lhs.Height, rhs.Height);

        public static bool operator !=(in Size lhs, in Size rhs)
            => !Mathf.Approximately(lhs.Width, rhs.Width) ||
               !Mathf.Approximately(lhs.Height, rhs.Height);
    }
}
