// This script provides a LEB (Lab) color space in addition to Unity's built in Red/Green/Blue colors.
// LEB is based on CIE XYZ and is a color-opponent space with L* for lightness and e* and b* for the color-opponent dimensions.
// LEB color is designed to approximate human vision and so it aspires to perceptual uniformity.
// The L component closely matches human perception of lightness.

using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace UnityEngine
{
    [Serializable]
    public readonly struct LEBColor : IEquatableReadOnlyStruct<LEBColor>, IFormattable, ISerializable
    {
        public static LEBColor Cyan { get; } = Color.cyan;

        public static LEBColor Clear { get; } = Color.clear;

        public static LEBColor Grey { get; } = Color.grey;

        public static LEBColor Gray { get; } = Color.gray;

        public static LEBColor Magenta { get; } = Color.magenta;

        public static LEBColor Red { get; } = Color.red;

        public static LEBColor Yellow { get; } = Color.yellow;

        public static LEBColor Black { get; } = Color.black;

        public static LEBColor White { get; } = Color.white;

        public static LEBColor Green { get; } = Color.green;

        public static LEBColor Blue { get; } = Color.blue;

        /// <summary>
        /// Lightness
        /// </summary>
        public readonly float L;

        /// <summary>
        /// E* (or A*) color-opponent from green to red
        /// </summary>
        public readonly float E;

        /// <summary>
        /// B* color-opponent from blue to yellow
        /// </summary>
        public readonly float B;

        /// <summary>
        /// Alpha
        /// </summary>
        public readonly float A;

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return this.L;
                    case 1: return this.E;
                    case 2: return this.B;
                    case 3: return this.A;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        public LEBColor(float l, float e, float b)
        {
            this.L = l;
            this.E = e;
            this.B = b;
            this.A = 1f;
        }

        public LEBColor(float l, float e, float b, float a)
        {
            this.L = l;
            this.E = e;
            this.B = b;
            this.A = a;
        }

        public LEBColor(in Color rgb)
        {
            RGBToLEB(rgb, out this.L, out this.E, out this.B, out this.A);
        }

        public void Deconstruct(out float l, out float e, out float b)
        {
            l = this.L;
            e = this.E;
            b = this.B;
        }

        public void Deconstruct(out float l, out float e, out float b, out float a)
        {
            l = this.L;
            e = this.E;
            b = this.B;
            a = this.A;
        }

        public LEBColor With(float? L = null, float? E = null, float? B = null, float? A = null)
            => new LEBColor(
                L ?? this.L,
                E ?? this.E,
                B ?? this.B,
                A ?? this.A
            );

        public override string ToString()
            => ToString(null, CultureInfo.InvariantCulture.NumberFormat);

        public string ToString(string format)
            => ToString(format, CultureInfo.InvariantCulture.NumberFormat);

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
                format = "F3";

            return string.Format("LEBA({0}, {1}, {2}, {3})",
                                    this.L.ToString(format, formatProvider),
                                    this.E.ToString(format, formatProvider),
                                    this.B.ToString(format, formatProvider),
                                    this.A.ToString(format, formatProvider)
                                );
        }

        public override int GetHashCode()
        {
#if USE_SYSTEM_HASHCODE
            return HashCode.Combine(this.L, this.E, this.B, this.A);
#endif

#pragma warning disable CS0162 // Unreachable code detected
            unchecked
            {
                var hashCode = -374917833;

                hashCode = hashCode * -1521134295 + this.L.GetHashCode();
                hashCode = hashCode * -1521134295 + this.E.GetHashCode();
                hashCode = hashCode * -1521134295 + this.B.GetHashCode();
                hashCode = hashCode * -1521134295 + this.A.GetHashCode();

                return hashCode;
            }
#pragma warning restore CS0162 // Unreachable code detected
        }

        public override bool Equals(object obj)
            => obj is LEBColor other && Equals(other);

        public bool Equals(LEBColor other)
            => Mathf.Approximately(this.L, other.L) &&
               Mathf.Approximately(this.E, other.E) &&
               Mathf.Approximately(this.B, other.B) &&
               Mathf.Approximately(this.A, other.A);

        public bool Equals(in LEBColor other)
            => Mathf.Approximately(this.L, other.L) &&
               Mathf.Approximately(this.E, other.E) &&
               Mathf.Approximately(this.B, other.B) &&
               Mathf.Approximately(this.A, other.A);

        private LEBColor(SerializationInfo info, StreamingContext context)
        {
            this.L = info.GetSingleOrDefault(nameof(this.L));
            this.E = info.GetSingleOrDefault(nameof(this.E));
            this.B = info.GetSingleOrDefault(nameof(this.B));
            this.A = info.GetSingleOrDefault(nameof(this.A));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.L), this.L);
            info.AddValue(nameof(this.E), this.E);
            info.AddValue(nameof(this.B), this.B);
            info.AddValue(nameof(this.A), this.A);
        }

        public static bool operator ==(in LEBColor lhs, in LEBColor rhs)
            => Mathf.Approximately(lhs.L, rhs.L) &&
               Mathf.Approximately(lhs.E, rhs.E) &&
               Mathf.Approximately(lhs.B, rhs.B) &&
               Mathf.Approximately(lhs.A, rhs.A);

        public static bool operator !=(in LEBColor lhs, in LEBColor rhs)
            => !Mathf.Approximately(lhs.L, rhs.L) ||
               !Mathf.Approximately(lhs.E, rhs.E) ||
               !Mathf.Approximately(lhs.B, rhs.B) ||
               !Mathf.Approximately(lhs.A, rhs.A);

        public static implicit operator Color(in LEBColor lab)
            => LEBToRGB(lab);

        public static implicit operator LEBColor(in Color rgb)
            => RGBToLEB(rgb);

        public static implicit operator LEBColor(in Vector4 v)
            => new LEBColor(v.x, v.y, v.z, v.w);

        public static implicit operator Vector4(in LEBColor c)
            => new Vector4(c.L, c.E, c.B, c.A);

        public static LEBColor operator +(in LEBColor lhs, in LEBColor rhs)
            => new LEBColor(lhs.L + rhs.L, lhs.E + rhs.E, lhs.B + rhs.B, lhs.A + rhs.A);

        public static LEBColor operator -(in LEBColor lhs, in LEBColor rhs)
            => new LEBColor(lhs.L - rhs.L, lhs.E - rhs.E, lhs.B - rhs.B, lhs.A - rhs.A);

        public static LEBColor operator *(in LEBColor lhs, in LEBColor rhs)
            => new LEBColor(lhs.L * rhs.L, lhs.E * rhs.E, lhs.B * rhs.B, lhs.A * rhs.A);

        public static LEBColor operator *(in LEBColor lhs, float rhs)
            => new LEBColor(lhs.L * rhs, lhs.E * rhs, lhs.B * rhs, lhs.A * rhs);

        public static LEBColor operator *(float lhs, in LEBColor rhs)
            => new LEBColor(lhs * rhs.L, lhs * rhs.E, lhs * rhs.B, lhs * rhs.A);

        public static LEBColor operator *(in LEBColor lhs, in Vector4 rhs)
            => new LEBColor(lhs.L * rhs.x, lhs.E * rhs.y, lhs.B * rhs.z, lhs.A * rhs.w);

        public static LEBColor operator *(in Vector4 lhs, in LEBColor rhs)
            => new LEBColor(rhs.L * lhs.x, rhs.E * lhs.y, rhs.B * lhs.z, rhs.A * lhs.w);

        public static LEBColor operator /(in LEBColor lhs, float rhs)
            => new LEBColor(lhs.L / rhs, lhs.E / rhs, lhs.B / rhs, lhs.A / rhs);

        public static LEBColor operator /(in LEBColor lhs, in Vector4 rhs)
            => new LEBColor(lhs.L / rhs.x, lhs.E / rhs.y, lhs.B / rhs.z, lhs.A / rhs.w);

        public static LEBColor operator /(in LEBColor lhs, in LEBColor rhs)
            => new LEBColor(lhs.L / rhs.L, lhs.E / rhs.E, lhs.B / rhs.B, lhs.A / rhs.A);

        public static LEBColor Lerp(in LEBColor a, in LEBColor b, float t)
            => new LEBColor(Mathf.Lerp(a.L, b.L, t),
                            Mathf.Lerp(a.E, b.E, t),
                            Mathf.Lerp(a.B, b.B, t),
                            Mathf.Lerp(a.A, b.A, t));

        public static Color Lerp(in Color a, in Color b, float t)
            => Lerp(RGBToLEB(a), RGBToLEB(b), t);

        public static float Distance(in LEBColor a, in LEBColor b)
            => Mathf.Sqrt(Mathf.Pow(a.L - b.L, 2f) +
                          Mathf.Pow(a.E - b.E, 2f) +
                          Mathf.Pow(a.B - b.B, 2f) +
                          Mathf.Pow(a.A - b.A, 2f));

        private const float D65x = 0.950489f;
        private const float D65y = 1f;
        private const float D65z = 1.088840f;
        private const float Delta = 6f / 29f;

        private const float D_x_D = Delta * Delta;
        private const float N16_d_N116 = 16f / 116f;
        private const float N1_d_N3 = 1f / 3f;
        private const float N1_d_N2_4 = 1f / 2.4f;
        private const float N1_p_N0_05 = 1f + 0.055f;

        public static LEBColor RGBToLEB(in Color rgb)
        {
            RGBToLEB(rgb, out var l, out var e, out var b, out var a);
            return new LEBColor(l, e, b, a);
        }

        public static LEBColor RGBToLEB(float r, float g, float b)
        {
            RGBToLEB(new Color(r, g, b, 1f), out var ll, out var ee, out var bb, out var aa);
            return new LEBColor(ll, ee, bb, aa);
        }

        public static LEBColor RGBToLEB(float r, float g, float b, float a)
        {
            RGBToLEB(new Color(r, g, b, a), out var ll, out var ee, out var bb, out var aa);
            return new LEBColor(ll, ee, bb, aa);
        }

        public static void RGBToLEB(in Color rgb, out float l, out float e, out float b)
            => RGBToLEB(rgb, out l, out e, out b, out var _);

        public static void RGBToLEB(in Color rgb, out float l, out float e, out float b, out float a)
        {
            var rLinear = rgb.r;
            var gLinear = rgb.g;
            var bLinear = rgb.b;
            a = rgb.a;

            var rr = (rLinear > 0.04045f) ? Mathf.Pow((rLinear + 0.055f) / N1_p_N0_05, 2.2f) : (rLinear / 12.92f);
            var gg = (gLinear > 0.04045f) ? Mathf.Pow((gLinear + 0.055f) / N1_p_N0_05, 2.2f) : (gLinear / 12.92f);
            var bb = (bLinear > 0.04045f) ? Mathf.Pow((bLinear + 0.055f) / N1_p_N0_05, 2.2f) : (bLinear / 12.92f);

            var x = rr * 0.4124f + gg * 0.3576f + bb * 0.1805f;
            var y = rr * 0.2126f + gg * 0.7152f + bb * 0.0722f;
            var z = rr * 0.0193f + gg * 0.1192f + bb * 0.9505f;

            x = (x > 0.9505f) ? 0.9505f : ((x < 0f) ? 0f : x);
            y = (y > 1.0f) ? 1.0f : ((y < 0f) ? 0f : y);
            z = (z > 1.089f) ? 1.089f : ((z < 0f) ? 0f : z);

            var fx = x / D65x;
            var fy = y / D65y;
            var fz = z / D65z;

            fx = (fx > 0.008856f) ? Mathf.Pow(fx, N1_d_N3) : (7.787f * fx + N16_d_N116);
            fy = (fy > 0.008856f) ? Mathf.Pow(fy, N1_d_N3) : (7.787f * fy + N16_d_N116);
            fz = (fz > 0.008856f) ? Mathf.Pow(fz, N1_d_N3) : (7.787f * fz + N16_d_N116);

            l = 116.0f * fy - 16f;
            e = 500.0f * (fx - fy);
            b = 200.0f * (fy - fz);
        }

        public static Color LEBToRGB(in LEBColor leb)
        {
            LEBToRGB(new LEBColor(leb.L, leb.E, leb.B, leb.A), out var r, out var g, out var b, out var a);
            return new Color(r, g, b, a);
        }

        public static Color LEBToRGB(float l, float e, float b)
        {
            LEBToRGB(new LEBColor(l, e, b, 1f), out var rr, out var gg, out var bb, out var aa);
            return new Color(rr, gg, bb, aa);
        }

        public static Color LEBToRGB(float l, float e, float b, float a)
        {
            LEBToRGB(new LEBColor(l, e, b, a), out var rr, out var gg, out var bb, out var aa);
            return new Color(rr, gg, bb, aa);
        }

        public static void LEBToRGB(in LEBColor leb, out float r, out float g, out float b)
            => LEBToRGB(leb, out r, out g, out b, out var _);

        public static void LEBToRGB(in LEBColor leb, out float r, out  float g, out float b, out float a)
        {
            var fy = (leb.L + 16f) / 116.0f;
            var fx = fy + (leb.E / 500.0f);
            var fz = fy - (leb.B / 200.0f);

            var x = (fx > Delta) ? D65x * (fx * fx * fx) : (fx - N16_d_N116) * 3f * D_x_D * D65x;
            var y = (fy > Delta) ? D65y * (fy * fy * fy) : (fy - N16_d_N116) * 3f * D_x_D * D65y;
            var z = (fz > Delta) ? D65z * (fz * fz * fz) : (fz - N16_d_N116) * 3f * D_x_D * D65z;

            a = leb.A;

            r = x * 3.2410f - y * 1.5374f - z * 0.4986f;
            g = -x * 0.9692f + y * 1.8760f - z * 0.0416f;
            b = x * 0.0556f - y * 0.2040f + z * 1.0570f;

            r = (r <= 0.0031308f) ? 12.92f * r : N1_p_N0_05 * Mathf.Pow(r, N1_d_N2_4) - 0.055f;
            g = (g <= 0.0031308f) ? 12.92f * g : N1_p_N0_05 * Mathf.Pow(g, N1_d_N2_4) - 0.055f;
            b = (b <= 0.0031308f) ? 12.92f * b : N1_p_N0_05 * Mathf.Pow(b, N1_d_N2_4) - 0.055f;

            r = (r < 0f) ? 0f : r;
            g = (g < 0f) ? 0f : g;
            b = (b < 0f) ? 0f : b;
        }
    }
}