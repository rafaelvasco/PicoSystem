using System;
using System.Runtime.InteropServices;

namespace PicoSystem.Framework.Common
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2 : IEquatable<Vector2>
    {
        public float X, Y;

        public static Vector2 One => new Vector2(1.0f, 1.0f);

        public static Vector2 Zero => new Vector2(0.0f, 0.0f);

        public static Vector2 Half => new Vector2(0.5f, 0.5f);

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2 operator +(Vector2 v, float s)
        {
            return new Vector2(v.X + s, v.Y + s);
        }

        public static Vector2 operator +(float s, Vector2 v)
        {
            return new Vector2(v.X + s, v.Y + s);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector2 operator -(Vector2 v, float s)
        {
            return new Vector2(v.X - s, v.Y - s);
        }

        public static Vector2 operator -(float s, Vector2 v)
        {
            return new Vector2(s - v.X, s - v.Y);
        }

        public static Vector2 operator -(Vector2 v)
        {
            return new Vector2(-v.X, -v.Y);
        }

        public static Vector2 operator *(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X * v2.X, v1.Y * v2.Y);
        }

        public static Vector2 operator *(float s, Vector2 v)
        {
            return new Vector2(v.X * s, v.Y * s);
        }

        public static Vector2 operator *(Vector2 v, float s)
        {
            return new Vector2(v.X * s, v.Y * s);
        }

        public static Vector2 operator /(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X / v2.X, v1.Y / v2.Y);
        }

        public static Vector2 operator /(float s, Vector2 v)
        {
            return new Vector2(s / v.X, s / v.Y);
        }

        public static Vector2 operator /(Vector2 v, float s)
        {
            return new Vector2(v.X / s, v.Y / s);
        }

        public static bool operator ==(Vector2 v1, Vector2 v2)
        {
            return (v1.X == v2.X && v1.Y == v2.Y);
        }

        public static bool operator !=(Vector2 v1, Vector2 v2)
        {
            return (v1.X != v2.X || v1.Y != v2.Y);
        }

        /// <summary>Create a Vector2 structure, normally used to store Vertex positions.</summary>
        /// <param name="X">X value</param>
        /// <param name="Y">Y value</param>
        public Vector2(float X, float Y)
        {
            this.X = X; this.Y = Y;
        }

        /// <summary>Create a Vector2 structure, normally used to store Vertex positions.</summary>
        /// <param name="X">X value</param>
        /// <param name="Y">Y value</param>
        public Vector2(double X, double Y)
        {
            this.X = (float)X; this.Y = (float)Y;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector2)) return false;

            return Equals((Vector2)obj);
        }

        public bool Equals(Vector2 other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "{" + X + ", " + Y + "}";
        }

        /// <summary>
        /// Parses a JSON stream and produces a Vector2 struct.
        /// </summary>
        public static Vector2 Parse(string text)
        {
            string[] split = text.Trim('{', '}').Split(',');
            if (split.Length != 2) return Zero;

            return new Vector2(float.Parse(split[0]), float.Parse(split[1]));
        }

        public float this[int a] => (a == 0) ? X : Y;

        /// <summary>
        /// Get the length of the Vector2 structure.
        /// </summary>
        public float Length => (float)Math.Sqrt(X * X + Y * Y);

        /// <summary>
        /// Get the squared length of the Vector2 structure.
        /// </summary>
        public float SquaredLength => X * X + Y * Y;

        /// <summary>
        /// Gets the perpendicular vector on the right side of this vector.
        /// </summary>
        public Vector2 PerpendicularRight => new Vector2(Y, -X);

        /// <summary>
        /// Gets the perpendicular vector on the left side of this vector.
        /// </summary>
        public Vector2 PerpendicularLeft => new Vector2(-Y, X);

        /// <summary>
        /// Converts a Vector2 to a float array.  Useful for vector commands in GL.
        /// </summary>
        /// <returns>Float array representation of a Vector2</returns>
        public float[] ToFloat()
        {
            return new[] { X, Y };
        }

        /// <summary>
        /// Performs the Vector2 scalar dot product.
        /// </summary>
        /// <param name="v1">The left Vector2.</param>
        /// <param name="v2">The right Vector2.</param>
        /// <returns>v1.X * v2.X + v1.Y * v2.Y</returns>
        public static float Dot(Vector2 v1, Vector2 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y;
        }



        /// <summary>
        /// Normalizes the Vector2 structure to have a peak value of one.
        /// </summary>
        /// <returns>if (Length = 0) return Zero; else return Vector2(X,Y)/Length</returns>
        public Vector2 Normalize()
        {
            if (Length == 0) return Zero;
            return new Vector2(X, Y) / Length;
        }

        /// <summary>
        /// Checks to see if any value (X, Y, z) are within 0.0001 of 0.
        /// If so this method truncates that value to zero.
        /// </summary>
        /// <returns>A truncated Vector2</returns>
        public Vector2 Truncate()
        {
            float _x = (Math.Abs(X) - 0.0001 < 0) ? 0 : X;
            float _y = (Math.Abs(Y) - 0.0001 < 0) ? 0 : Y;
            return new Vector2(_x, _y);
        }

        /// <summary>
        /// Store the minimum values of X, and Y between the two vectors.
        /// </summary>
        /// <param name="v">Vector to check against</param>
        public void TakeMin(Vector2 v)
        {
            if (v.X < X) X = v.X;
            if (v.Y < Y) Y = v.Y;
        }

        /// <summary>
        /// Store the maximum values of X, and Y between the two vectors.
        /// </summary>
        /// <param name="v">Vector to check against</param>
        public void TakeMax(Vector2 v)
        {
            if (v.X > X) X = v.X;
            if (v.Y > Y) Y = v.Y;
        }

        /// <summary>
        /// Linear interpolates between two vectors to get a new vector.
        /// </summary>
        /// <param name="v1">Initial vector (amount = 0).</param>
        /// <param name="v2">Final vector (amount = 1).</param>
        /// <param name="amount">Amount of each vector to consider (0->1).</param>
        /// <returns>A linear interpolated Vector3.</returns>
        public static Vector2 Lerp(Vector2 v1, Vector2 v2, float amount)
        {
            return v1 + (v2 - v1) * amount;
        }

        /// <summary>
        /// Swaps two Vector2 structures by passing via reference.
        /// </summary>
        /// <param name="v1">The first Vector2 structure.</param>
        /// <param name="v2">The second Vector2 structure.</param>
        public static void Swap(ref Vector2 v1, ref Vector2 v2)
        {
            Vector2 t = v1;
            v1 = v2;
            v2 = t;
        }
    }
}
