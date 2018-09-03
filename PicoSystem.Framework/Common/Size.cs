using System;
using System.Runtime.InteropServices;

namespace PicoSystem.Framework.Common
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Size : IEquatable<Size>
    {
        public int Width;
        public int Height;

        public Size(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public bool Equals(Size other)
        {
            return this.Width == other.Width && this.Height == other.Height;
        }

        public static bool Equals(ref Size value1, ref Size value2)
        {
            return value1.Width == value2.Width && value1.Height == value2.Height;
        }

        public static bool operator ==(Size value1, Size value2)
        {
            return Equals(ref value1, ref value2);
        }

        public static bool operator !=(Size value1, Size value2)
        {
            return !Equals(ref value1, ref value2);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Size && Equals((Size)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Width * 397) ^ Height;
            }
        }

        public static implicit operator SizeF(Size rect)
        {
            return new SizeF(rect.Width, rect.Height);
        }
    }
}
