using System;
using System.Runtime.InteropServices;

namespace PicoSystem.Framework.Common
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3 : IEquatable<Vector3>
    {
        public float X, Y, Z;

        #region Static Constructors

        public static Vector3 Identity => new Vector3(1.0f, 1.0f, 1.0f);

        public static Vector3 Zero => new Vector3(0.0f, 0.0f, 0.0f);

        public static Vector3 Up => new Vector3(0.0f, 1.0f, 0.0f);

        public static Vector3 Down => new Vector3(0.0f, -1.0f, 0.0f);

        public static Vector3 Forward => new Vector3(0.0f, 0.0f, -1.0f);

        public static Vector3 Backward => new Vector3(0.0f, 0.0f, 1.0f);

        public static Vector3 Left => new Vector3(-1.0f, 0.0f, 0.0f);

        public static Vector3 Right => new Vector3(1.0f, 0.0f, 0.0f);

        public static Vector3 UnitX => new Vector3(1.0f, 0.0f, 0.0f);

        public static Vector3 UnitY => new Vector3(0.0f, 1.0f, 0.0f);

        public static Vector3 UnitZ => new Vector3(0.0f, 0.0f, 1.0f);

        public static Vector3 UnitScale => new Vector3(1.0f, 1.0f, 1.0f);

        public static readonly int SizeInBytes = Marshal.SizeOf(new Vector3());

        #endregion

        #region Operators

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector3 operator +(Vector3 v, float s)
        {
            return new Vector3(v.X + s, v.Y + s, v.Z + s);
        }

        public static Vector3 operator +(float s, Vector3 v)
        {
            return new Vector3(v.X + s, v.Y + s, v.Z + s);
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static Vector3 operator -(Vector3 v, float s)
        {
            return new Vector3(v.X - s, v.Y - s, v.Z - s);
        }

        public static Vector3 operator -(float s, Vector3 v)
        {
            return new Vector3(s - v.X, s - v.Y, s - v.Z);
        }

        public static Vector3 operator -(Vector3 v)
        {
            return new Vector3(-v.X, -v.Y, -v.Z);
        }

        public static Vector3 operator *(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X*v2.X, v1.Y*v2.Y, v1.Z*v2.Z);
        }

        public static Vector3 operator *(float s, Vector3 v)
        {
            return new Vector3(v.X*s, v.Y*s, v.Z*s);
        }

        public static Vector3 operator *(Vector3 v, float s)
        {
            return new Vector3(v.X*s, v.Y*s, v.Z*s);
        }

        public static Vector3 operator /(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X/v2.X, v1.Y/v2.Y, v1.Z/v2.Z);
        }

        public static Vector3 operator /(float s, Vector3 v)
        {
            return new Vector3(s/v.X, s/v.Y, s/v.Z);
        }

        public static Vector3 operator /(Vector3 v, float s)
        {
            return new Vector3(v.X/s, v.Y/s, v.Z/s);
        }

        public static bool operator ==(Vector3 v1, Vector3 v2)
        {
            return v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z;
        }

        public static bool operator !=(Vector3 v1, Vector3 v2)
        {
            return v1.X != v2.X || v1.Y != v2.Y || v1.Z != v2.Z;
        }

        #endregion

        #region Constructors

        /// <summary>Create a Vector3 structure, normally used to store Vertex positions.</summary>
        /// <param name="X">X value</param>
        /// <param name="Y">Y value</param>
        /// <param name="Z">Z value</param>
        public Vector3(float X, float Y, float Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        /// <summary>Creates a Vector3 structure, normally used to store Vertex positions.  Casted to floats for OpenGL.</summary>
        /// <param name="X">X value</param>
        /// <param name="Y">Y value</param>
        /// <param name="Z">Z value</param>
        public Vector3(double X, double Y, double Z)
        {
            this.X = (float) X;
            this.Y = (float) Y;
            this.Z = (float) Z;
        }

        /// <summary>Creates a Vector3 tructure from a float array (assuming the float array is of length 3).</summary>
        /// <param name="vector">The float array to convert to a Vector3.</param>
        public Vector3(float[] vector)
        {
            if (vector.Length != 3)
                throw new Exception($"float[] vector was of length {vector.Length}.  Was expecting a length of 3.");
            this.X = vector[0];
            this.Y = vector[1];
            this.Z = vector[2];
        }

        #endregion

        #region Overrides

        public override bool Equals(object obj)
        {
            if (!(obj is Vector3)) return false;

            return this.Equals((Vector3) obj);
        }

        public bool Equals(Vector3 other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "{" + X + ", " + Y + ", " + Z + "}";
        }

        /// <summary>
        ///     Parses a JSON stream and produces a Vector3 struct.
        /// </summary>
        public static Vector3 Parse(string text)
        {
            var split = text.Trim('{', '}').Split(',');
            if (split.Length != 3) return Zero;

            return new Vector3(float.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2]));
        }

        public float this[int a] => a == 0 ? X : a == 1 ? Y : Z;

        #endregion

        #region Methods

        /// <summary>
        ///     Converts a Vector3 to a float array.  Useful for vector commands in GL.
        /// </summary>
        /// <returns>Float array representation of a Vector3</returns>
        public float[] ToFloat()
        {
            return new[] {X, Y, Z};
        }

        /// <summary>
        ///     Get the length of the Vector3 structure.
        /// </summary>
        public float Length => (float) Math.Sqrt(X*X + Y*Y + Z*Z);

        /// <summary>
        ///     Performs the Vector3 scalar dot product.
        /// </summary>
        /// <param name="v1">The left Vector3.</param>
        /// <param name="v2">The right Vector3.</param>
        /// <returns>Scalar dot product value</returns>
        public static float Dot(Vector3 v1, Vector3 v2)
        {
            return v1.X*v2.X + v1.Y*v2.Y + v1.Z*v2.Z;
        }

        /// <summary>
        ///     Performs the Vector3 scalar dot product.
        /// </summary>
        /// <param name="v">Second dot product term</param>
        /// <returns>Vector3.Dot(this, v)</returns>
        public float Dot(Vector3 v)
        {
            return Dot(this, v);
        }

        /// <summary>
        ///     Returns the squared length of the Vector3 structure.
        /// </summary>
        public float SquaredLength => X*X + Y*Y + Z*Z;

        /// <summary>
        ///     Vector3 cross product
        /// </summary>
        /// <param name="v1">Vector1</param>
        /// <param name="v2">Vector2</param>
        /// <returns>Vector3 cross product value</returns>
        public static Vector3 Cross(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.Y*v2.Z - v1.Z*v2.Y, v1.Z*v2.X - v1.X*v2.Z, v1.X*v2.Y - v1.Y*v2.X);
        }

        /// <summary>
        ///     Vector3 cross product
        /// </summary>
        /// <param name="v">Second cross product term</param>
        /// <returns>this X v</returns>
        public Vector3 Cross(Vector3 v)
        {
            return Cross(this, v);
        }

        /// <summary>
        ///     Normalizes the Vector3 structure to have a peak value of one.
        /// </summary>
        /// <returns>if (Length = 0) return Zero; else return Vector3(X,Y,Z)/Length</returns>
        public Vector3 Normalize()
        {
            if (Length == 0) return Zero;
            return new Vector3(X, Y, Z)/Length;
        }

        /// <summary>
        ///     Checks to see if any value (X, Y, Z) are within 0.0001 of 0.
        ///     If so this method truncates that value to zero.
        /// </summary>
        /// <returns>A truncated Vector3</returns>
        public Vector3 Truncate()
        {
            var _x = Math.Abs(X) - 0.0001 < 0 ? 0 : X;
            var _y = Math.Abs(Y) - 0.0001 < 0 ? 0 : Y;
            var _z = Math.Abs(Z) - 0.0001 < 0 ? 0 : Z;
            return new Vector3(_x, _y, _z);
        }

        /// <summary>
        ///     Store the minimum values of X, Y, and Z between the two vectors.
        /// </summary>
        /// <param name="v">Vector to check against</param>
        public void TakeMin(Vector3 v)
        {
            if (v.X < X) X = v.X;
            if (v.Y < Y) Y = v.Y;
            if (v.Z < Z) Z = v.Z;
        }

        /// <summary>
        ///     Store the maximum values of X, Y, and Z between the two vectors.
        /// </summary>
        /// <param name="v">Vector to check against</param>
        public void TakeMax(Vector3 v)
        {
            if (v.X > X) X = v.X;
            if (v.Y > Y) Y = v.Y;
            if (v.Z > Z) Z = v.Z;
        }

        /// <summary>
        ///     Returns the maximum component of the Vector3.
        /// </summary>
        /// <returns>The maximum component of the Vector3</returns>
        public float Max()
        {
            return X >= Y && X >= Z ? X : Y >= Z ? Y : Z;
        }

        /// <summary>
        ///     Returns the minimum component of the Vector3.
        /// </summary>
        /// <returns>The minimum component of the Vector3</returns>
        public float Min()
        {
            return X <= Y && X <= Z ? X : Y <= Z ? Y : Z;
        }

        /// <summary>
        ///     Linear interpolates between two vectors to get a new vector.
        /// </summary>
        /// <param name="v1">Initial vector (amount = 0).</param>
        /// <param name="v2">Final vector (amount = 1).</param>
        /// <param name="amount">Amount of each vector to consider (0->1).</param>
        /// <returns>A linear interpolated Vector3.</returns>
        public static Vector3 Lerp(Vector3 v1, Vector3 v2, float amount)
        {
            return v1 + (v2 - v1)*amount;
        }

        /// <summary>
        ///     Calculates the angle (in radians) between two vectors.
        /// </summary>
        /// <param name="first">The first vector.</param>
        /// <param name="second">The second vector.</param>
        /// <returns>Angle (in radians) between the vectors.</returns>
        /// <remarks>Note that the returned angle is never bigger than the constant Pi.</remarks>
        public static float CalculateAngle(Vector3 first, Vector3 second)
        {
            return (float) Math.Acos(Dot(first, second)/(first.Length*second.Length));
        }

        /// <summary>
        ///     Calculates the angle (in radians) between two vectors.
        /// </summary>
        /// <param name="v">The second vector.</param>
        /// <returns>Angle (in radians) between the vectors.</returns>
        /// <remarks>Note that the returned angle is never bigger than the constant Pi.</remarks>
        public float CalculateAngle(Vector3 v)
        {
            return CalculateAngle(this, v);
        }

        /// <summary>
        ///     Swaps two Vector3 structures by passing via reference.
        /// </summary>
        /// <param name="v1">The first Vector3 structure.</param>
        /// <param name="v2">The second Vector3 structure.</param>
        public static void Swap(ref Vector3 v1, ref Vector3 v2)
        {
            var t = v1;
            v1 = v2;
            v2 = t;
        }

        #endregion
    }
}