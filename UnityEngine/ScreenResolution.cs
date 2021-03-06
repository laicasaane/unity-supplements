﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace UnityEngine
{
    [Serializable]
    public readonly partial struct ScreenResolution : IEquatableReadOnlyStruct<ScreenResolution>, ISerializable
    {
        public readonly int Width;
        public readonly int Height;

        public int this[int index]
        {
            get
            {
                if (index == 0) return this.Width;
                if (index == 1) return this.Height;
                throw new IndexOutOfRangeException();
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

        /// <summary>
        /// Returns a new resolution where <see cref="Width"/> and <see cref="Height"/> are swapped
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScreenResolution ChangeOrientation()
            => new ScreenResolution(this.Height, this.Width);

        public ScreenResolution ToLandscape()
            => IsLandscape() ? this : ChangeOrientation();

        public ScreenResolution ToPortrait()
            => IsPortrait() ? this : ChangeOrientation();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsLandscape()
            => this.Width > this.Height;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsPortrait()
            => this.Height > this.Width;

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