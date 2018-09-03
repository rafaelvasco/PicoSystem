using System;
using System.Runtime.InteropServices;

namespace PicoSystem.Framework.Common
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector4 : IEquatable<Vector4>
    {
        public float X, Y, Z, W;

        #region Static Constructors

        public static Vector4 Identity => new Vector4(1.0f, 1.0f, 1.0f, 1.0f);

        public static Vector4 Zero => new Vector4(0.0f, 0.0f, 0.0f, 0.0f);

        public static Vector4 UnitX => new Vector4(1.0f, 0.0f, 0.0f, 0.0f);

        public static Vector4 UnitY => new Vector4(0.0, 1.0f, 0.0f, 0.0f);

        public static Vector4 UnitZ => new Vector4(0.0, 0.0f, 1.0f, 0.0f);

        public static Vector4 UnitW => new Vector4(0.0, 0.0f, 0.0f, 1.0f);

        #endregion

        #region Operators

        public static Vector4 operator +(Vector4 v1, Vector4 v2)
        {
            return new Vector4(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
        }

        public static Vector4 operator +(Vector4 v, float s)
        {
            return new Vector4(v.X + s, v.Y + s, v.Z + s, v.W + s);
        }

        public static Vector4 operator +(float s, Vector4 v)
        {
            return new Vector4(v.X + s, v.Y + s, v.Z + s, v.W + s);
        }

        public static Vector4 operator -(Vector4 v1, Vector4 v2)
        {
            return new Vector4(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
        }

        public static Vector4 operator -(Vector4 v, float s)
        {
            return new Vector4(v.X - s, v.Y - s, v.Z - s, v.W - s);
        }

        public static Vector4 operator -(float s, Vector4 v)
        {
            return new Vector4(s - v.X, s - v.Y, s - v.Z, s - v.W);
        }

        public static Vector4 operator -(Vector4 v)
        {
            return new Vector4(-v.X, -v.Y, -v.Z, -v.W);
        }

        public static Vector4 operator *(Vector4 v1, Vector4 v2)
        {
            return new Vector4(v1.X*v2.X, v1.Y*v2.Y, v1.Z*v2.Z, v1.W*v2.W);
        }

        public static Vector4 operator *(float s, Vector4 v)
        {
            return new Vector4(v.X*s, v.Y*s, v.Z*s, v.W*s);
        }

        public static Vector4 operator *(Vector4 v, float s)
        {
            return new Vector4(v.X*s, v.Y*s, v.Z*s, v.W*s);
        }

        public static Vector4 operator /(Vector4 v1, Vector4 v2)
        {
            return new Vector4(v1.X/v2.X, v1.Y/v2.Y, v1.Z/v2.Z, v1.W/v2.W);
        }

        public static Vector4 operator /(float s, Vector4 v)
        {
            return new Vector4(s/v.X, s/v.Y, s/v.Z, s/v.W);
        }

        public static Vector4 operator /(Vector4 v, float s)
        {
            return new Vector4(v.X/s, v.Y/s, v.Z/s, v.W/s);
        }

        public static bool operator ==(Vector4 v1, Vector4 v2)
        {
            return v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z && v1.W == v2.W;
        }

        public static bool operator !=(Vector4 v1, Vector4 v2)
        {
            return v1.X != v2.X || v1.Y != v2.Y || v1.Z != v2.Z || v1.W != v2.W;
        }

        #endregion

        #region Constructors

        /// <summary>Create a Vector4 structure.</summary>
        /// <param name="X">X value</param>
        /// <param name="Y">Y value</param>
        /// <param name="Z">Z value</param>
        /// <param name="W">W value</param>
        public Vector4(float X, float Y, float Z, float W)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.W = W;
        }

        /// <summary>Creates a Vector4 structure.  Casted to floats for OpenGL.</summary>
        /// <param name="X">X value</param>
        /// <param name="Y">Y value</param>
        /// <param name="Z">Z value</param>
        /// <param name="W">W value</param>
        public Vector4(double X, double Y, double Z, double W)
        {
            this.X = (float) X;
            this.Y = (float) Y;
            this.Z = (float) Z;
            this.W = (float) W;
        }

        /// <summary>Creates a Vector4 structure based on a Vector3 and W.</summary>
        /// <param name="v">Vector3 to make up X,Y,Z</param>
        /// <param name="W">Double to make up the W component</param>
        public Vector4(Vector3 v, float W)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
            this.W = W;
        }

        /// <summary>
        ///     Accepts a 24 bit color value and assumes an alpha of 1.0f.
        /// </summary>
        /// <param name="RGBByte">24bit color value</param>
        public Vector4(byte[] RGBByte)
        {
            if (RGBByte.Length < 3) throw new Exception("Color data was not 24bit as expected.");
            X = (float) (RGBByte[0]/256.0);
            Y = (float) (RGBByte[1]/256.0);
            Z = (float) (RGBByte[2]/256.0);
            W = 1.0f;
        }

        /// <summary>Creates a Vector4 tructure from a float array (assuming the float array is of length 4).</summary>
        /// <param name="vector">The float array to convert to a Vector4.</param>
        public Vector4(float[] vector)
        {
            if (vector.Length != 4)
                throw new Exception($"float[] vector was of length {vector.Length}.  Was expecting a length of 4.");
            this.X = vector[0];
            this.Y = vector[1];
            this.Z = vector[2];
            this.W = vector[3];
        }

        #endregion

        #region Overrides

        public override bool Equals(object obj)
        {
            if (!(obj is Vector4)) return false;

            return this.Equals((Vector4) obj);
        }

        public bool Equals(Vector4 other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "{" + X + ", " + Y + ", " + Z + ", " + W + "}";
        }

        /// <summary>
        ///     Parses a JSON stream and produces a Vector4 struct.
        /// </summary>
        public static Vector4 Parse(string text)
        {
            var split = text.Trim('{', '}').Split(',');
            if (split.Length != 4) return Zero;

            return new Vector4(float.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2]),
                float.Parse(split[3]));
        }

        public float this[int a]
        {
            get { return a == 0 ? X : a == 1 ? Y : a == 2 ? Z : W; }
            set
            {
                if (a == 0) X = value;
                else if (a == 1) Y = value;
                else if (a == 2) Z = value;
                else if (a == 3) W = value;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Get the length of the Vector4 structure.
        /// </summary>
        public float Length => (float) Math.Sqrt(X*X + Y*Y + Z*Z + W*W);

        /// <summary>
        ///     Get the squared length of the Vector4 structure.
        /// </summary>
        public float SquaredLength => X*X + Y*Y + Z*Z + W*W;

        /// <summary>
        ///     Gets or sets an OpenGl.Types.Vector2 with the X and Y components of this instance.
        /// </summary>
        public Vector2 Xy
        {
            get { return new Vector2(X, Y); }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        /// <summary>
        ///     Gets or sets an OpenGl.Types.Vector3 with the X, Y and Z components of this instance.
        /// </summary>
        public Vector3 Xyz
        {
            get { return new Vector3(X, Y, Z); }
            set
            {
                X = value.X;
                Y = value.Y;
                Z = value.Z;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Vector4 scalar dot product.
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>Scalar dot product value</returns>
        public static float Dot(Vector4 v1, Vector4 v2)
        {
            return v1.X*v2.X + v1.Y*v2.Y + v1.Z*v2.Z + v1.W*v2.W;
        }

        /// <summary>
        ///     Vector4 scalar dot product.
        /// </summary>
        /// <param name="v">Second vector</param>
        /// <returns>Vector4.Dot(this, v)</returns>
        public float Dot(Vector4 v)
        {
            return Dot(this, v);
        }

        /// <summary>
        ///     Converts a Vector4 to a float array.  Useful for vector commands in GL.
        /// </summary>
        /// <returns>Float array representation of a Vector4</returns>
        public float[] ToFloat()
        {
            return new[] {X, Y, Z, W};
        }

        /// <summary>
        ///     Normalizes the Vector4 structure to have a peak value of one.
        /// </summary>
        /// <returns>if (Length = 0) return Zero; else return Vector4(X,Y,Z,W)/Length</returns>
        public Vector4 Normalize()
        {
            if (Length == 0) return Zero;
            return new Vector4(X, Y, Z, W)/Length;
        }

        /// <summary>
        ///     Checks to see if any value (X, Y, Z, W) are within 0.0001 of 0.
        ///     If so this method truncates that value to zero.
        /// </summary>
        /// <returns>A truncated Vector4</returns>
        public Vector4 Truncate()
        {
            var _x = Math.Abs(X) - 0.0001 < 0 ? 0 : X;
            var _y = Math.Abs(Y) - 0.0001 < 0 ? 0 : Y;
            var _z = Math.Abs(Z) - 0.0001 < 0 ? 0 : Z;
            var _w = Math.Abs(W) - 0.0001 < 0 ? 0 : W;
            return new Vector4(_x, _y, _z, _w);
        }

        /// <summary>
        ///     Store the minimum values of X, Y, Z, and W between the two vectors.
        /// </summary>
        /// <param name="v">Vector to check against</param>
        public void TakeMin(Vector4 v)
        {
            if (v.X < X) X = v.X;
            if (v.Y < Y) Y = v.Y;
            if (v.Z < Z) Z = v.Z;
            if (v.W < W) W = v.W;
        }

        /// <summary>
        ///     Store the maximum values of X, Y, Z, and W  between the two vectors.
        /// </summary>
        /// <param name="v">Vector to check against</param>
        public void TakeMax(Vector4 v)
        {
            if (v.X > X) X = v.X;
            if (v.Y > Y) Y = v.Y;
            if (v.Z > Z) Z = v.Z;
            if (v.W > W) W = v.W;
        }

        /// <summary>
        ///     Linear interpolates between two vectors to get a new vector.
        /// </summary>
        /// <param name="v1">Initial vector (amount = 0).</param>
        /// <param name="v2">Final vector (amount = 1).</param>
        /// <param name="amount">Amount of each vector to consider (0->1).</param>
        /// <returns>A linear interpolated Vector3.</returns>
        public static Vector4 Lerp(Vector4 v1, Vector4 v2, float amount)
        {
            return v1 + (v2 - v1)*amount;
        }

        /// <summary>
        ///     Swaps two Vector4 structures by passing via reference.
        /// </summary>
        /// <param name="v1">The first Vector4 structure.</param>
        /// <param name="v2">The second Vector4 structure.</param>
        public static void Swap(ref Vector4 v1, ref Vector4 v2)
        {
            var t = v1;
            v1 = v2;
            v2 = t;
        }

        #endregion
    }
}