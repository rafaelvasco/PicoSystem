using System;
using System.Diagnostics;

namespace PicoSystem.Framework.Common
{
    [Serializable]
    [DebuggerDisplay("{ToString()}")]
    public struct Color : IEquatable<Color>
    {
        public byte R => (byte) ((Rgba & 0xFF000000) >> 24);
        public byte G => (byte) ((Rgba & 0x00FF0000) >> 16);
        public byte B => (byte) ((Rgba & 0x0000FF00) >> 8);
        public byte A => (byte) ((Rgba & 0x000000FF));

        public float Rf => R/255.0f;
        public float Gf => G/255.0f;
        public float Bf => B/255.0f;
        public float Af => A/255.0f;

        public uint Rgba { get; private set; }
        public uint Argb { get; private set; }

        public Color(uint rgba)
        {
            Rgba = rgba;

            Argb = ((Rgba & 0x000000FF) << 24) +
                             ((Rgba & 0x0000FF00) << 8) +
                             ((Rgba & 0x00FF0000) >> 8) +
                             ((Rgba & 0xFF000000) >> 24);


        }

        public Color(byte r, byte g, byte b, byte a = 255)
        {
            Rgba = ((uint) r << 24) | ((uint) g << 16) | ((uint) b << 8) | a;

            Argb = ((Rgba & 0x000000FF) << 24) +
                   ((Rgba & 0x0000FF00) << 8) +
                   ((Rgba & 0x00FF0000) >> 8) +
                   ((Rgba & 0xFF000000) >> 24);
        }

        public Color(float r, float g, float b, float a = 1.0f)
        {
            byte ri = (byte) (r*255);
            byte gi = (byte) (g*255);
            byte bi = (byte) (b*255);
            byte alphai = (byte) (a*255);
            Rgba = ((uint) ri << 24) | ((uint) gi << 16) | ((uint) bi << 8) | alphai;

            Argb = ((Rgba & 0x000000FF) << 24) +
                   ((Rgba & 0x0000FF00) << 8) +
                   ((Rgba & 0x00FF0000) >> 8) +
                   ((Rgba & 0xFF000000) >> 24);
        }

        public Color(Color color, byte alpha)
        {
            Rgba = color.Rgba & 0xFFFFFF00;
            Rgba += alpha;

            Argb = ((Rgba & 0x000000FF) << 24) +
                   ((Rgba & 0x0000FF00) << 8) +
                   ((Rgba & 0x00FF0000) >> 8) +
                   ((Rgba & 0xFF000000) >> 24);
        }

        public Color(Color color, float alpha)
        {
            Rgba = color.Rgba & 0xFFFFFF00;
            Rgba += (byte) (alpha*255);

            Argb = ((Rgba & 0x000000FF) << 24) +
                   ((Rgba & 0x0000FF00) << 8) +
                   ((Rgba & 0x00FF0000) >> 8) +
                   ((Rgba & 0xFF000000) >> 24);
        }

        public static Color operator *(Color color, float val)
        {
            if (val < 0.0f)
            {
                val = 0.0f;
            }

            uint r = Math.Min((uint) Math.Round(color.R*val), 255);
            uint g = Math.Min((uint) Math.Round(color.G*val), 255);
            uint b = Math.Min((uint) Math.Round(color.B*val), 255);
            uint alpha = Math.Min((uint) Math.Round(color.A*val), 255);

            color.Rgba = (r << 24) + (g << 16) + (b << 8) + (alpha);
            return color;
        }

        public static Color operator *(Color color1, Color color2)
        {
            float mr = color1.Rf*color2.Rf;
            float mg = color1.Gf*color2.Gf;
            float mb = color1.Bf*color2.Bf;
            float ma = color1.Af*color2.Af;

            return new Color(mr, mg, mb, ma);
        }

        public Color AsARGBPremultiplied()
        {
            if (this == White || this == Transparent)
            {
                return this;
            }

            uint a = A;

            float f = a/255.0f;

            uint r = ((uint) Math.Round(this.R*f));
            uint g = ((uint) Math.Round(this.G*f));
            uint b = ((uint) Math.Round(this.B*f));

            return new Color((a << 24) + (b << 16) + (g << 8) + r);
        }

        public static bool operator ==(Color color1, Color color2)
        {
            return color1.Rgba == color2.Rgba;
        }

        public static bool operator !=(Color color1, Color color2)
        {
            return !(color1 == color2);
        }

        public bool Equals(Color other)
        {
            return this.Rgba == other.Rgba;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Color && Equals((Color) obj);
        }

        public override int GetHashCode()
        {
            return (int) Rgba;
        }

        public override string ToString()
        {
            return $"{R},{G},{B},{A}";
        }

        

        public static readonly Color Transparent = new Color(0);
        public static readonly Color AliceBlue = new Color(0xf0f8ffff);
        public static readonly Color AntiqueWhite = new Color(0xfaebd7ff);
        public static readonly Color Aqua = new Color(0x00ffffff);
        public static readonly Color Aquamarine = new Color(0x7fffd4ff);
        public static readonly Color Azure = new Color(0xf0ffffff);
        public static readonly Color Beige = new Color(0xf5f5dcff);
        public static readonly Color Bisque = new Color(0xffe4c4ff);
        public static readonly Color Black = new Color(0x000000ff);
        public static readonly Color BlanchedAlmond = new Color(0xffebcdff);
        public static readonly Color Blue = new Color(0x0000ffff);
        public static readonly Color BlueViolet = new Color(0x8a2be2ff);
        public static readonly Color Brown = new Color(0xa52a2aff);
        public static readonly Color BurlyWood = new Color(0xdeb887ff);
        public static readonly Color CadetBlue = new Color(0x5f9ea0ff);
        public static readonly Color Chartreuse = new Color(0x7fff00ff);
        public static readonly Color Chocolate = new Color(0xff1e69d2);
        public static readonly Color Coral = new Color(0xd2691eff);
        public static readonly Color CornflowerBlue = new Color(0x6495edff);
        public static readonly Color Cornsilk = new Color(0xfff8dcff);
        public static readonly Color Crimson = new Color(0xdc143cff);
        public static readonly Color Cyan = new Color(0x00ffffff);
        public static readonly Color DarkBlue = new Color(0x00008bff);
        public static readonly Color DarkCyan = new Color(0x008b8bff);
        public static readonly Color DarkGoldenrod = new Color(0xb8860bff);
        public static readonly Color DarkGray = new Color(0xa9a9a9ff);
        public static readonly Color DarkGreen = new Color(0x006400ff);
        public static readonly Color DarkKhaki = new Color(0xbdb76bff);
        public static readonly Color DarkMagenta = new Color(0x8b008bff);
        public static readonly Color DarkOliveGreen = new Color(0x556b2fff);
        public static readonly Color DarkOrange = new Color(0xff8c00ff);
        public static readonly Color DarkOrchid = new Color(0x9932ccff);
        public static readonly Color DarkRed = new Color(0x8b0000ff);
        public static readonly Color DarkSalmon = new Color(0xe9967aff);
        public static readonly Color DarkSeaGreen = new Color(0x8fbc8bff);
        public static readonly Color DarkSlateBlue = new Color(0x483d8bff);
        public static readonly Color DarkSlateGray = new Color(0x2f4f4fff);
        public static readonly Color DarkTurquoise = new Color(0x00ced1ff);
        public static readonly Color DarkViolet = new Color(0x9400d3ff);
        public static readonly Color DeepPink = new Color(0xff1493ff);
        public static readonly Color DeepSkyBlue = new Color(0x00bfffff);
        public static readonly Color DimGray = new Color(0x696969ff);
        public static readonly Color DodgerBlue = new Color(0x1e90ffff);
        public static readonly Color Firebrick = new Color(0xb22222ff);
        public static readonly Color FloralWhite = new Color(0xfffaf0ff);
        public static readonly Color ForestGreen = new Color(0x228b22ff);
        public static readonly Color Fuchsia = new Color(0xff00ffff);
        public static readonly Color Gainsboro = new Color(0xdcdcdcff);
        public static readonly Color GhostWhite = new Color(0xf8f8ffff);
        public static readonly Color Gold = new Color(0xffd700ff);
        public static readonly Color Goldenrod = new Color(0xdaa520ff);
        public static readonly Color Gray = new Color(0x808080ff);
        public static readonly Color Green = new Color(0x008000ff);
        public static readonly Color GreenYellow = new Color(0xadff2fff);
        public static readonly Color Honeydew = new Color(0xf0fff0ff);
        public static readonly Color HotPink = new Color(0xff69b4ff);
        public static readonly Color IndianRed = new Color(0xcd5c5cff);
        public static readonly Color Indigo = new Color(0x4b0082ff);
        public static readonly Color Ivory = new Color(0xfffff0ff);
        public static readonly Color Khaki = new Color(0xf0e68cff);
        public static readonly Color Lavender = new Color(0xe6e6faff);
        public static readonly Color LavenderBlush = new Color(0xfff0f5ff);
        public static readonly Color LawnGreen = new Color(0x7cfc00ff);
        public static readonly Color LemonChiffon = new Color(0xfffacdff);
        public static readonly Color LightBlue = new Color(0xadd8e6ff);
        public static readonly Color LightCoral = new Color(0xf08080ff);
        public static readonly Color LightCyan = new Color(0xe0ffffff);
        public static readonly Color LightGoldenrodYellow = new Color(0xfafad2ff);
        public static readonly Color LightGray = new Color(0xd3d3d3ff);
        public static readonly Color LightGreen = new Color(0x90ee90ff);
        public static readonly Color LightPink = new Color(0xffb6c1ff);
        public static readonly Color LightSalmon = new Color(0xffa07aff);
        public static readonly Color LightSeaGreen = new Color(0x20b2aaff);
        public static readonly Color LightSkyBlue = new Color(0x87cefaff);
        public static readonly Color LightSlateGray = new Color(0x778899ff);
        public static readonly Color LightSteelBlue = new Color(0xb0c4deff);
        public static readonly Color LightYellow = new Color(0xffffe0ff);
        public static readonly Color Lime = new Color(0x00ff00ff);
        public static readonly Color LimeGreen = new Color(0x32cd32ff);
        public static readonly Color Linen = new Color(0xfaf0e6ff);
        public static readonly Color Magenta = new Color(0xff00ffff);
        public static readonly Color Maroon = new Color(0x800000ff);
        public static readonly Color MediumAquamarine = new Color(0x66cdaaff);
        public static readonly Color MediumBlue = new Color(0x0000cdff);
        public static readonly Color MediumOrchid = new Color(0xba55d3ff);
        public static readonly Color MediumPurple = new Color(0x9370dbff);
        public static readonly Color MediumSeaGreen = new Color(0x3cb371ff);
        public static readonly Color MediumSlateBlue = new Color(0x7b68eeff);
        public static readonly Color MediumSpringGreen = new Color(0x00fa9aff);
        public static readonly Color MediumTurquoise = new Color(0x48d1ccff);
        public static readonly Color MediumVioletRed = new Color(0xc71585ff);
        public static readonly Color MidnightBlue = new Color(0x191970ff);
        public static readonly Color MintCream = new Color(0xf5fffaff);
        public static readonly Color MistyRose = new Color(0xffe4e1ff);
        public static readonly Color Moccasin = new Color(0xffe4b5ff);
        public static readonly Color MonoGameOrange = new Color(0xe73c00ff);
        public static readonly Color NavajoWhite = new Color(0xffdeadff);
        public static readonly Color Navy = new Color(0x000080ff);
        public static readonly Color OldLace = new Color(0xfdf5e6ff);
        public static readonly Color Olive = new Color(0x808000ff);
        public static readonly Color OliveDrab = new Color(0x6b8e23ff);
        public static readonly Color Orange = new Color(0xffa500ff);
        public static readonly Color OrangeRed = new Color(0xff4500ff);
        public static readonly Color Orchid = new Color(0xda70d6ff);
        public static readonly Color PaleGoldenrod = new Color(0xeee8aaff);
        public static readonly Color PaleGreen = new Color(0x98fb98ff);
        public static readonly Color PaleTurquoise = new Color(0xafeeeeff);
        public static readonly Color PaleVioletRed = new Color(0xdb7093ff);
        public static readonly Color PapayaWhip = new Color(0xffefd5ff);
        public static readonly Color PeachPuff = new Color(0xffdab9ff);
        public static readonly Color Peru = new Color(0xcd853fff);
        public static readonly Color Pink = new Color(0xffc0cbff);
        public static readonly Color Plum = new Color(0xdda0ddff);
        public static readonly Color PowderBlue = new Color(0xb0e0e6ff);
        public static readonly Color Purple = new Color(0x800080ff);
        public static readonly Color Red = new Color(0xff0000ff);
        public static readonly Color RosyBrown = new Color(0xbc8f8fff);
        public static readonly Color RoyalBlue = new Color(0x4169e1ff);
        public static readonly Color SaddleBrown = new Color(0x8b4513ff);
        public static readonly Color Salmon = new Color(0xfa8072ff);
        public static readonly Color SandyBrown = new Color(0xf4a460ff);
        public static readonly Color SeaGreen = new Color(0x2e8b57ff);
        public static readonly Color SeaShell = new Color(0xfff5eeff);
        public static readonly Color Sienna = new Color(0xa0522dff);
        public static readonly Color Silver = new Color(0xc0c0c0ff);
        public static readonly Color SkyBlue = new Color(0x87ceebff);
        public static readonly Color SlateBlue = new Color(0x6a5acdff);
        public static readonly Color SlateGray = new Color(0x708090ff);
        public static readonly Color Snow = new Color(0xfffafaff);
        public static readonly Color SpringGreen = new Color(0x00ff7fff);
        public static readonly Color SteelBlue = new Color(0x4682b4ff);
        public static readonly Color Tan = new Color(0xd2b48cff);
        public static readonly Color Teal = new Color(0x008080ff);
        public static readonly Color Thistle = new Color(0xd8bfd8ff);
        public static readonly Color Tomato = new Color(0xff6347ff);
        public static readonly Color Turquoise = new Color(0x40e0d0ff);
        public static readonly Color Violet = new Color(0xee82eeff);
        public static readonly Color Wheat = new Color(0xf5deb3ff);
        public static readonly Color WhiteSmoke = new Color(0xf5f5f5ff);
        public static readonly Color Yellow = new Color(0xffff00ff);
        public static readonly Color YellowGreen = new Color(0x9acd32ff);
        public static readonly Color White = new Color(uint.MaxValue);
    }
}