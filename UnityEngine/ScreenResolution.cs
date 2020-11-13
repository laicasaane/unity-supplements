using System;
using System.Runtime.Serialization;

namespace UnityEngine
{
    [Serializable]
    public readonly struct ScreenResolution : IEquatableReadOnlyStruct<ScreenResolution>, ISerializable
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

        public ScreenResolution(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public void Deconstruct(out float width, out float height)
        {
            width = this.Width;
            height = this.Height;
        }

        public ScreenResolution With(int? Width = null, int? Height = null)
            => new ScreenResolution(
                Width ?? this.Width,
                Height ?? this.Height
            );

        public override bool Equals(object obj)
            => obj is ScreenResolution other &&
               this.Width == other.Width &&
               this.Height == other.Height;

        public bool Equals(ScreenResolution other)
            => this.Width == other.Width && this.Height == other.Height;

        public bool Equals(in ScreenResolution other)
            => this.Width == other.Width && this.Height == other.Height;

        public override int GetHashCode()
        {
            var hashCode = 859600377;

            hashCode = hashCode * -1521134295 + this.Width.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Height.GetHashCode();

            return hashCode;
        }

        public override string ToString()
            => $"{this.Width} × {this.Height}";

        public void Apply(bool fullscreen = false)
        {
            var mode = fullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;

#if !UNITY_EDITOR
            Screen.SetResolution(this.Width, this.Height, mode);
#endif
        }

        public void Apply(FullScreenMode mode)
        {
#if !UNITY_EDITOR
            Screen.SetResolution(this.Width, this.Height, mode);
#endif
        }

        private ScreenResolution(SerializationInfo info, StreamingContext context)
        {
            this.Width = info.GetInt32OrDefault(nameof(this.Width));
            this.Height = info.GetInt32OrDefault(nameof(this.Height));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Width), this.Width);
            info.AddValue(nameof(this.Height), this.Height);
        }

        public static implicit operator ScreenResolution(Resolution value)
            => new ScreenResolution(value.width, value.height);

        public static implicit operator SizeInt(in ScreenResolution value)
            => new SizeInt(value.Width, value.Height);

        public static implicit operator ScreenResolution(in SizeInt value)
            => new ScreenResolution(value.Width, value.Height);

        public static implicit operator ScreenResolution(Vector2 value)
            => new ScreenResolution((int)value.x, (int)value.y);

        public static implicit operator ScreenResolution(Vector2Int value)
            => new ScreenResolution(value.x, value.y);

        public static implicit operator Vector2(in ScreenResolution value)
            => new Vector2(value.Width, value.Height);

        public static implicit operator Vector2Int(in ScreenResolution value)
            => new Vector2Int(value.Width, value.Height);

        public static bool operator ==(in ScreenResolution lhs, in ScreenResolution rhs)
            => lhs.Width == rhs.Width && lhs.Height == rhs.Height;

        public static bool operator !=(in ScreenResolution lhs, in ScreenResolution rhs)
            => lhs.Width != rhs.Width || lhs.Height != rhs.Height;
    }
}