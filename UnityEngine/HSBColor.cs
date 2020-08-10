using System;
using System.Globalization;

namespace UnityEngine
{
    [Serializable]
    public readonly struct HSBColor : IEquatableReadOnlyStruct<HSBColor>, IFormattable
    {
        public static HSBColor Cyan { get; } = Color.cyan;

        public static HSBColor Clear { get; } = Color.clear;

        public static HSBColor Grey { get; } = Color.grey;

        public static HSBColor Gray { get; } = Color.gray;

        public static HSBColor Magenta { get; } = Color.magenta;

        public static HSBColor Red { get; } = Color.red;

        public static HSBColor Yellow { get; } = Color.yellow;

        public static HSBColor Black { get; } = Color.black;

        public static HSBColor White { get; } = Color.white;

        public static HSBColor Green { get; } = Color.green;

        public static HSBColor Blue { get; } = Color.blue;

        /// <summary>
        /// Hue
        /// </summary>
        public readonly float H;

        /// <summary>
        /// Saturation
        /// </summary>
        public readonly float S;

        /// <summary>
        /// Brightness
        /// </summary>
        public readonly float B;

        /// <summary>
        /// Alpha
        /// </summary>
        public readonly float A;

        public HSBColor(float h, float s, float b)
        {
            this.H = h;
            this.S = s;
            this.B = b;
            this.A = 1f;
        }

        public HSBColor(float h, float s, float b, float a)
        {
            this.H = h;
            this.S = s;
            this.B = b;
            this.A = a;
        }

        public void Deconstruct(out float h, out float s, out float b)
        {
            h = this.H;
            s = this.S;
            b = this.B;
        }

        public void Deconstruct(out float h, out float s, out float b, out float a)
        {
            h = this.H;
            s = this.S;
            b = this.B;
            a = this.A;
        }

        public HSBColor(in Color rgb)
        {
            RGBToHSB(rgb, out this.H, out this.S, out this.B, out this.A);
        }

        public override int GetHashCode()
        {
            {
                var hashCode = -1511096998;

                hashCode = hashCode * -1521134295 + this.H.GetHashCode();
                hashCode = hashCode * -1521134295 + this.S.GetHashCode();
                hashCode = hashCode * -1521134295 + this.B.GetHashCode();
                hashCode = hashCode * -1521134295 + this.A.GetHashCode();

                return hashCode;
            }
        }

        public override bool Equals(object obj)
            => obj is HSBColor other && Equals(other);

        public bool Equals(HSBColor other)
            => Mathf.Approximately(this.H, other.H) &&
               Mathf.Approximately(this.S, other.S) &&
               Mathf.Approximately(this.B, other.B) &&
               Mathf.Approximately(this.A, other.A);

        public bool Equals(in HSBColor other)
            => Mathf.Approximately(this.H, other.H) &&
               Mathf.Approximately(this.S, other.S) &&
               Mathf.Approximately(this.B, other.B) &&
               Mathf.Approximately(this.A, other.A);

        public override string ToString()
            => ToString(null, CultureInfo.InvariantCulture.NumberFormat);

        public string ToString(string format)
            => ToString(format, CultureInfo.InvariantCulture.NumberFormat);

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
                format = "F3";

            return string.Format("HSBA({0}, {1}, {2}, {3})",
                                    this.H.ToString(format, formatProvider),
                                    this.S.ToString(format, formatProvider),
                                    this.B.ToString(format, formatProvider),
                                    this.A.ToString(format, formatProvider)
                                );
        }

        public static bool operator ==(in HSBColor lhs, in HSBColor rhs)
            => Mathf.Approximately(lhs.H, rhs.H) &&
               Mathf.Approximately(lhs.S, rhs.S) &&
               Mathf.Approximately(lhs.B, rhs.B) &&
               Mathf.Approximately(lhs.A, rhs.A);

        public static bool operator !=(in HSBColor lhs, in HSBColor rhs)
            => !Mathf.Approximately(lhs.H, rhs.H) ||
               !Mathf.Approximately(lhs.S, rhs.S) ||
               !Mathf.Approximately(lhs.B, rhs.B) ||
               !Mathf.Approximately(lhs.A, rhs.A);

        public static implicit operator Color(in HSBColor hsb)
            => HSBToRGB(hsb);

        public static implicit operator HSBColor(in Color rgb)
            => RGBToHSB(rgb);

        public static HSBColor operator +(in HSBColor lhs, in HSBColor rhs)
            => new HSBColor(lhs.H + rhs.H, lhs.S + rhs.S, lhs.B + rhs.B, lhs.A + rhs.A);

        public static HSBColor operator -(in HSBColor lhs, in HSBColor rhs)
            => new HSBColor(lhs.H - rhs.H, lhs.S - rhs.S, lhs.B - rhs.B, lhs.A - rhs.A);

        public static HSBColor operator *(in HSBColor lhs, in HSBColor rhs)
            => new HSBColor(lhs.H * rhs.H, lhs.S * rhs.S, lhs.B * rhs.B, lhs.A * rhs.A);

        public static HSBColor operator *(in HSBColor lhs, float rhs)
            => new HSBColor(lhs.H * rhs, lhs.S * rhs, lhs.B * rhs, lhs.A * rhs);

        public static HSBColor operator *(float lhs, in HSBColor rhs)
            => new HSBColor(lhs * rhs.H, lhs * rhs.S, lhs * rhs.B, lhs * rhs.A);

        public static HSBColor operator /(in HSBColor lhs, float rhs)
            => new HSBColor(lhs.H / rhs, lhs.S / rhs, lhs.B / rhs, lhs.A / rhs);

        private const float N1_d_N360 = 1f / 360f;

        public static HSBColor RGBToHSB(in Color rgb)
        {
            RGBToHSB(rgb, out var h, out var s, out var b, out var a);
            return new HSBColor(h, s, b, a);
        }

        public static HSBColor RGBToHSB(float r, float g, float b)
        {
            RGBToHSB(new Color(r, g, b, 1f), out var hh, out var ss, out var bb, out var aa);
            return new HSBColor(hh, ss, bb, aa);
        }

        public static HSBColor RGBToHSB(float r, float g, float b, float a)
        {
            RGBToHSB(new Color(r, g, b, a), out var hh, out var ss, out var bb, out var aa);
            return new HSBColor(hh, ss, bb, aa);
        }

        public static void RGBToHSB(in Color rgb, out float h, out float s, out float b)
            => RGBToHSB(rgb, out h, out s, out b, out var _);

        public static void RGBToHSB(in Color rgb, out float h, out float s, out float b, out float a)
        {
            h = 0f;
            s = 0f;
            b = 0f;
            a = rgb.a;

            var red = rgb.r;
            var green = rgb.g;
            var blue = rgb.b;

            var max = Mathf.Max(red, Mathf.Max(green, blue));

            if (max <= 0f)
                return;

            var min = Mathf.Min(red, Mathf.Min(green, blue));
            var dif = max - min;

            if (max <= min)
            {
                h = 0f;
            }
            else
            {
                if (green == max)
                {
                    h = (blue - red) / dif * 60f + 120f;
                }
                else if (blue == max)
                {
                    h = (red - green) / dif * 60f + 240f;
                }
                else if (blue > green)
                {
                    h = (green - blue) / dif * 60f + 360f;
                }
                else
                {
                    h = (green - blue) / dif * 60f;
                }

                if (h < 0f)
                {
                    h += 360f;
                }
            }

            h *= N1_d_N360;
            s = (dif / max) * 1f;
            b = max;
        }

        public static Color HSBToRGB(in HSBColor hsb)
        {
            HSBToRGB(new HSBColor(hsb.H, hsb.S, hsb.B, hsb.A), out var r, out var g, out var b, out var a);
            return new Color(r, g, b, a);
        }

        public static Color HSBToRGB(float h, float s, float b)
        {
            HSBToRGB(new HSBColor(h, s, b, 1f), out var rr, out var gg, out var bb, out var aa);
            return new Color(rr, gg, bb, aa);
        }

        public static Color HSBToRGB(float h, float s, float b, float a)
        {
            HSBToRGB(new HSBColor(h, s, b, a), out var rr, out var gg, out var bb, out var aa);
            return new Color(rr, gg, bb, aa);
        }

        public static void HSBToRGB(in HSBColor hsb, out float r, out float g, out float b)
            => HSBToRGB(hsb, out r, out g, out b, out var _);

        public static void HSBToRGB(in HSBColor hsb, out float r, out float g, out float b, out float a)
        {
            r = hsb.B;
            g = hsb.B;
            b = hsb.B;
            a = hsb.A;

            if (hsb.S != 0)
            {
                var max = hsb.B;
                var dif = hsb.B * hsb.S;
                var min = hsb.B - dif;

                var hue = hsb.H * 360f;

                if (hue < 60f)
                {
                    r = max;
                    g = hue * dif / 60f + min;
                    b = min;
                }
                else if (hue < 120f)
                {
                    r = -(hue - 120f) * dif / 60f + min;
                    g = max;
                    b = min;
                }
                else if (hue < 180f)
                {
                    r = min;
                    g = max;
                    b = (hue - 120f) * dif / 60f + min;
                }
                else if (hue < 240f)
                {
                    r = min;
                    g = -(hue - 240f) * dif / 60f + min;
                    b = max;
                }
                else if (hue < 300f)
                {
                    r = (hue - 240f) * dif / 60f + min;
                    g = min;
                    b = max;
                }
                else if (hue <= 360f)
                {
                    r = max;
                    g = min;
                    b = -(hue - 360f) * dif / 60f + min;
                }
                else
                {
                    r = 0f;
                    g = 0f;
                    b = 0f;
                }
            }
        }

        public static Color Lerp(in Color a, in Color b, float t)
            => Lerp(RGBToHSB(a), RGBToHSB(b), t);

        public static HSBColor Lerp(in HSBColor a, in HSBColor b, float t)
        {
            float h, s;

            // check special case black (hsb.b == 0): interpolate neither hue nor saturation!
            // check special case grey (hsb.s == 0): don't interpolate hue!
            if (a.B == 0f)
            {
                h = b.H;
                s = b.S;
            }
            else if (b.B == 0f)
            {
                h = a.H;
                s = a.S;
            }
            else
            {
                if (a.S == 0)
                {
                    h = b.H;
                }
                else if (b.S == 0f)
                {
                    h = a.H;
                }
                else
                {
                    // works around bug with LerpAngle
                    var angle = Mathf.LerpAngle(a.H * 360f, b.H * 360f, t);

                    while (angle < 0f)
                        angle += 360f;

                    while (angle > 360f)
                        angle -= 360f;

                    h = angle / 360f;
                }

                s = Mathf.Lerp(a.S, b.S, t);
            }

            return new HSBColor(h, s, Mathf.Lerp(a.B, b.B, t), Mathf.Lerp(a.A, b.A, t));
        }
    }
}
