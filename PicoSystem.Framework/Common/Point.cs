using System;

namespace PicoSystem.Framework.Common
{
    [Serializable]
    public struct Point : IEquatable<Point>
    {
        public int X;
        public int Y;

        public static Point Empty => zeroPoint;

        private static readonly Point zeroPoint = new Point();

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Point && Equals((Point)obj);
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ")";
        }

        public static Point operator +(Point value1, Point value2)
        {
            return new Point(value1.X + value2.X, value1.Y + value2.Y);
        }

        public static Point operator +(Point value1, Size value2)
        {
            return new Point(value1.X + value2.Width, value1.Y + value2.Height);
        }

        public static Point operator -(Point value1, Point value2)
        {
            return new Point(value1.X - value2.X, value1.Y - value2.Y);
        }

        public static Point operator *(Point value1, Point value2)
        {
            return new Point(value1.X * value2.X, value1.Y * value2.Y);
        }

        public static Point operator /(Point value1, Point value2)
        {
            return new Point(value1.X / value2.X, value1.Y / value2.Y);
        }

        public static bool operator ==(Point a, Point b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Point a, Point b)
        {
            return !a.Equals(b);
        }
    }
}
