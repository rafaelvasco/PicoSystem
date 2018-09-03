using System;
using System.Runtime.InteropServices;

namespace PicoSystem.Framework.Common
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Matrix4 : IEquatable<Matrix4>
    {
        public static readonly Matrix4 Identity = new Matrix4(
            1f, 0f, 0f, 0f,
            0f, 1f, 0f, 0f,
            0f, 0f, 1f, 0f,
            0f, 0f, 0f, 1f
            );

        public float M11, M12, M13, M14;
        public float M21, M22, M23, M24;
        public float M31, M32, M33, M34;
        public float M41, M42, M43, M44;

        public Matrix4(
            float m11, float m12, float m13, float m14,
            float m21, float m22, float m23, float m24,
            float m31, float m32, float m33, float m34,
            float m41, float m42, float m43, float m44
            )
        {
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M14 = m14;

            M21 = m21;
            M22 = m22;
            M23 = m23;
            M24 = m24;

            M31 = m31;
            M32 = m32;
            M33 = m33;
            M34 = m34;

            M41 = m41;
            M42 = m42;
            M43 = m43;
            M44 = m44;
        }


        public void Translate(float dx, float dy)
        {
            M41 += dx;
            M42 += dy;
        }

        /// <summary>
        ///     Builds a rotation matrix for a rotation around the x-axis.
        /// </summary>
        /// <param name="angle">The counter-clockwise angle in radians.</param>
        /// <param name="result">The resulting Matrix4 instance.</param>
        public static void CreateRotationX(float angle, out Matrix4 result)
        {
            var cos = (float) Math.Cos(angle);
            var sin = (float) Math.Sin(angle);

            result = Identity;
            result.M22 = cos;
            result.M23 = sin;
            result.M32 = -sin;
            result.M33 = cos;
        }


        /// <summary>
        ///     Builds a rotation matrix for a rotation around the x-axis.
        /// </summary>
        /// <param name="angle">The counter-clockwise angle in radians.</param>
        /// <returns>The resulting Matrix4 instance.</returns>
        public static Matrix4 CreateRotationX(float angle)
        {
            Matrix4 result;
            CreateRotationX(angle, out result);
            return result;
        }


        /// <summary>
        ///     Builds a rotation matrix for a rotation around the y-axis.
        /// </summary>
        /// <param name="angle">The counter-clockwise angle in radians.</param>
        /// <param name="result">The resulting Matrix4 instance.</param>
        public static void CreateRotationY(float angle, out Matrix4 result)
        {
            var cos = (float) Math.Cos(angle);
            var sin = (float) Math.Sin(angle);

            result = Identity;
            result.M11 = cos;
            result.M13 = -sin;
            result.M31 = sin;
            result.M33 = cos;
        }


        /// <summary>
        ///     Builds a rotation matrix for a rotation around the y-axis.
        /// </summary>
        /// <param name="angle">The counter-clockwise angle in radians.</param>
        /// <returns>The resulting Matrix4 instance.</returns>
        public static Matrix4 CreateRotationY(float angle)
        {
            Matrix4 result;
            CreateRotationY(angle, out result);
            return result;
        }


        /// <summary>
        ///     Builds a rotation matrix for a rotation around the z-axis.
        /// </summary>
        /// <param name="angle">The counter-clockwise angle in radians.</param>
        /// <param name="result">The resulting Matrix4 instance.</param>
        public static void CreateRotationZ(float angle, out Matrix4 result)
        {
            var cos = (float) Math.Cos(angle);
            var sin = (float) Math.Sin(angle);

            result = Identity;
            result.M11 = cos;
            result.M12 = sin;
            result.M21 = -sin;
            result.M22 = cos;
        }

        /// <summary>
        ///     Builds a rotation matrix for a rotation around the z-axis.
        /// </summary>
        /// <param name="angle">The counter-clockwise angle in radians.</param>
        /// <returns>The resulting Matrix4 instance.</returns>
        public static Matrix4 CreateRotationZ(float angle)
        {
            Matrix4 result;
            CreateRotationZ(angle, out result);
            return result;
        }

        /// <summary>
        ///     Creates a translation matrix.
        /// </summary>
        /// <param name="x">X translation.</param>
        /// <param name="y">Y translation.</param>
        /// <param name="z">Z translation.</param>
        /// <param name="result">The resulting Matrix4 instance.</param>
        public static void CreateTranslation(float x, float y, float z, out Matrix4 result)
        {
            result = Identity;
            result.M41 = x;
            result.M42 = y;
            result.M43 = z;
        }

        /// <summary>
        ///     Creates a translation matrix.
        /// </summary>
        /// <param name="vector">The translation vector.</param>
        /// <param name="result">The resulting Matrix4 instance.</param>
        public static void CreateTranslation(ref Vector3 vector, out Matrix4 result)
        {
            result = Identity;
            result.M41 = vector.X;
            result.M42 = vector.Y;
            result.M43 = vector.Z;
        }

        /// <summary>
        ///     Creates a translation matrix.
        /// </summary>
        /// <param name="x">X translation.</param>
        /// <param name="y">Y translation.</param>
        /// <param name="z">Z translation.</param>
        /// <returns>The resulting Matrix4 instance.</returns>
        public static Matrix4 CreateTranslation(float x, float y, float z)
        {
            Matrix4 result;
            CreateTranslation(x, y, z, out result);
            return result;
        }

        /// <summary>
        ///     Creates a translation matrix.
        /// </summary>
        /// <param name="vector">The translation vector.</param>
        /// <returns>The resulting Matrix4 instance.</returns>
        public static Matrix4 CreateTranslation(Vector3 vector)
        {
            Matrix4 result;
            CreateTranslation(vector.X, vector.Y, vector.Z, out result);
            return result;
        }

        /// <summary>
        ///     Creates a scale matrix.
        /// </summary>
        /// <param name="scale">Single scale factor for the x, y, and z axes.</param>
        /// <returns>A scale matrix.</returns>
        public static Matrix4 CreateScale(float scale)
        {
            Matrix4 result;
            CreateScale(scale, out result);
            return result;
        }

        /// <summary>
        ///     Creates a scale matrix.
        /// </summary>
        /// <param name="scale">Scale factors for the x, y, and z axes.</param>
        /// <returns>A scale matrix.</returns>
        public static Matrix4 CreateScale(Vector3 scale)
        {
            Matrix4 result;
            CreateScale(ref scale, out result);
            return result;
        }

        /// <summary>
        ///     Creates a scale matrix.
        /// </summary>
        /// <param name="x">Scale factor for the x axis.</param>
        /// <param name="y">Scale factor for the y axis.</param>
        /// <param name="z">Scale factor for the z axis.</param>
        /// <returns>A scale matrix.</returns>
        public static Matrix4 CreateScale(float x, float y, float z)
        {
            Matrix4 result;
            CreateScale(x, y, z, out result);
            return result;
        }

        /// <summary>
        ///     Creates a scale matrix.
        /// </summary>
        /// <param name="scale">Single scale factor for the x, y, and z axes.</param>
        /// <param name="result">A scale matrix.</param>
        public static void CreateScale(float scale, out Matrix4 result)
        {
            result = Identity;
            result.M11 = scale;
            result.M22 = scale;
            result.M33 = scale;
        }

        /// <summary>
        ///     Creates a scale matrix.
        /// </summary>
        /// <param name="scale">Scale factors for the x, y, and z axes.</param>
        /// <param name="result">A scale matrix.</param>
        public static void CreateScale(ref Vector3 scale, out Matrix4 result)
        {
            result = Identity;
            result.M11 = scale.X;
            result.M22 = scale.Y;
            result.M33 = scale.Z;
        }

        /// <summary>
        ///     Creates a scale matrix.
        /// </summary>
        /// <param name="x">Scale factor for the x axis.</param>
        /// <param name="y">Scale factor for the y axis.</param>
        /// <param name="z">Scale factor for the z axis.</param>
        /// <param name="result">A scale matrix.</param>
        public static void CreateScale(float x, float y, float z, out Matrix4 result)
        {
            result = Identity;
            result.M11 = x;
            result.M22 = y;
            result.M33 = z;
        }

        /// <summary>
        ///     Creates an orthographic projection matrix.
        /// </summary>
        /// <param name="width">The width of the projection volume.</param>
        /// <param name="height">The height of the projection volume.</param>
        /// <param name="zNear">The near edge of the projection volume.</param>
        /// <param name="zFar">The far edge of the projection volume.</param>
        /// <param name="result">The resulting Matrix4 instance.</param>
        public static void CreateOrthographic(float width, float height, float zNear, float zFar, out Matrix4 result)
        {
            CreateOrthographicOffCenter(-width/2, width/2, -height/2, height/2, zNear, zFar, out result);
        }

        /// <summary>
        ///     Creates an orthographic projection matrix.
        /// </summary>
        /// <param name="width">The width of the projection volume.</param>
        /// <param name="height">The height of the projection volume.</param>
        /// <param name="zNear">The near edge of the projection volume.</param>
        /// <param name="zFar">The far edge of the projection volume.</param>
        /// <rereturns>The resulting Matrix4 instance.</rereturns>
        public static Matrix4 CreateOrthographic(float width, float height, float zNear, float zFar)
        {
            Matrix4 result;
            CreateOrthographicOffCenter(-width/2, width/2, -height/2, height/2, zNear, zFar, out result);
            return result;
        }


        /// <summary>
        ///     Creates an orthographic projection matrix.
        /// </summary>
        /// <param name="left">The left edge of the projection volume.</param>
        /// <param name="right">The right edge of the projection volume.</param>
        /// <param name="bottom">The bottom edge of the projection volume.</param>
        /// <param name="top">The top edge of the projection volume.</param>
        /// <param name="zNear">The near edge of the projection volume.</param>
        /// <param name="zFar">The far edge of the projection volume.</param>
        /// <param name="result">The resulting Matrix4 instance.</param>
        public static void CreateOrthographicOffCenter(float left, float right, float bottom, float top, float zNear,
            float zFar, out Matrix4 result)
        {
            result = Identity;

            result.M11 = 2.0f/(right - left);
            result.M12 = result.M13 = result.M14 = 0.0f;

            result.M22 = 2.0f/(top - bottom);
            result.M21 = result.M23 = result.M24 = 0.0f;

            result.M33 = 1.0f/(zNear - zFar);
            result.M31 = result.M32 = result.M34 = 0.0f;

            result.M41 = (left + right)/(left - right);
            result.M42 = (top + bottom)/(bottom - top);
            result.M43 = zNear/(zNear - zFar);
            result.M44 = 1.0f;
        }

        /// <summary>
        ///     Creates an orthographic projection matrix.
        /// </summary>
        /// <param name="left">The left edge of the projection volume.</param>
        /// <param name="right">The right edge of the projection volume.</param>
        /// <param name="bottom">The bottom edge of the projection volume.</param>
        /// <param name="top">The top edge of the projection volume.</param>
        /// <param name="zNear">The near edge of the projection volume.</param>
        /// <param name="zFar">The far edge of the projection volume.</param>
        /// <returns>The resulting Matrix4 instance.</returns>
        public static Matrix4 CreateOrthographicOffCenter(float left, float right, float bottom, float top, float zNear,
            float zFar)
        {
            Matrix4 result;
            CreateOrthographicOffCenter(left, right, bottom, top, zNear, zFar, out result);
            return result;
        }

        /// <summary>
        ///     Adds two instances.
        /// </summary>
        /// <param name="left">The left operand of the addition.</param>
        /// <param name="right">The right operand of the addition.</param>
        /// <returns>A new instance that is the result of the addition.</returns>
        public static Matrix4 Add(Matrix4 left, Matrix4 right)
        {
            Matrix4 result;
            Add(ref left, ref right, out result);
            return result;
        }

        /// <summary>
        ///     Adds two instances.
        /// </summary>
        /// <param name="left">The left operand of the addition.</param>
        /// <param name="right">The right operand of the addition.</param>
        /// <param name="result">A new instance that is the result of the addition.</param>
        public static void Add(ref Matrix4 left, ref Matrix4 right, out Matrix4 result)
        {
            result.M11 = left.M11 + right.M11;
            result.M12 = left.M12 + right.M12;
            result.M13 = left.M13 + right.M13;
            result.M14 = left.M14 + right.M14;

            result.M21 = left.M21 + right.M21;
            result.M22 = left.M22 + right.M22;
            result.M23 = left.M23 + right.M23;
            result.M24 = left.M24 + right.M24;

            result.M31 = left.M31 + right.M31;
            result.M32 = left.M32 + right.M32;
            result.M33 = left.M33 + right.M33;
            result.M34 = left.M34 + right.M34;

            result.M41 = left.M41 + right.M41;
            result.M42 = left.M42 + right.M42;
            result.M43 = left.M43 + right.M43;
            result.M44 = left.M44 + right.M44;
        }

        /// <summary>
        ///     Subtracts one instance from another.
        /// </summary>
        /// <param name="left">The left operand of the subraction.</param>
        /// <param name="right">The right operand of the subraction.</param>
        /// <returns>A new instance that is the result of the subraction.</returns>
        public static Matrix4 Subtract(Matrix4 left, Matrix4 right)
        {
            Matrix4 result;
            Subtract(ref left, ref right, out result);
            return result;
        }

        /// <summary>
        ///     Subtracts one instance from another.
        /// </summary>
        /// <param name="left">The left operand of the subraction.</param>
        /// <param name="right">The right operand of the subraction.</param>
        /// <param name="result">A new instance that is the result of the subraction.</param>
        public static void Subtract(ref Matrix4 left, ref Matrix4 right, out Matrix4 result)
        {
            result.M11 = left.M11 - right.M11;
            result.M12 = left.M12 - right.M12;
            result.M13 = left.M13 - right.M13;
            result.M14 = left.M14 - right.M14;

            result.M21 = left.M21 - right.M21;
            result.M22 = left.M22 - right.M22;
            result.M23 = left.M23 - right.M23;
            result.M24 = left.M24 - right.M24;

            result.M31 = left.M31 - right.M31;
            result.M32 = left.M32 - right.M32;
            result.M33 = left.M33 - right.M33;
            result.M34 = left.M34 - right.M34;

            result.M41 = left.M41 - right.M41;
            result.M42 = left.M42 - right.M42;
            result.M43 = left.M43 - right.M43;
            result.M44 = left.M44 - right.M44;
        }

        /// <summary>
        ///     Multiplies two instances.
        /// </summary>
        /// <param name="left">The left operand of the multiplication.</param>
        /// <param name="right">The right operand of the multiplication.</param>
        /// <returns>A new instance that is the result of the multiplication.</returns>
        public static Matrix4 Mult(Matrix4 left, Matrix4 right)
        {
            Matrix4 result;
            Mult(ref left, ref right, out result);
            return result;
        }

        /// <summary>
        ///     Multiplies two instances.
        /// </summary>
        /// <param name="left">The left operand of the multiplication.</param>
        /// <param name="right">The right operand of the multiplication.</param>
        /// <param name="result">A new instance that is the result of the multiplication.</param>
        public static void Mult(ref Matrix4 left, ref Matrix4 right, out Matrix4 result)
        {
            float lM11 = left.M11,
                lM12 = left.M12,
                lM13 = left.M13,
                lM14 = left.M14,
                lM21 = left.M21,
                lM22 = left.M22,
                lM23 = left.M23,
                lM24 = left.M24,
                lM31 = left.M31,
                lM32 = left.M32,
                lM33 = left.M33,
                lM34 = left.M34,
                lM41 = left.M41,
                lM42 = left.M42,
                lM43 = left.M43,
                lM44 = left.M44,
                rM11 = right.M11,
                rM12 = right.M12,
                rM13 = right.M13,
                rM14 = right.M14,
                rM21 = right.M21,
                rM22 = right.M22,
                rM23 = right.M23,
                rM24 = right.M24,
                rM31 = right.M31,
                rM32 = right.M32,
                rM33 = right.M33,
                rM34 = right.M34,
                rM41 = right.M41,
                rM42 = right.M42,
                rM43 = right.M43,
                rM44 = right.M44;

            result.M11 = lM11*rM11 + lM12*rM21 + lM13*rM31 + lM14*rM41;
            result.M12 = lM11*rM12 + lM12*rM22 + lM13*rM32 + lM14*rM42;
            result.M13 = lM11*rM13 + lM12*rM23 + lM13*rM33 + lM14*rM43;
            result.M14 = lM11*rM14 + lM12*rM24 + lM13*rM34 + lM14*rM44;
            result.M21 = lM21*rM11 + lM22*rM21 + lM23*rM31 + lM24*rM41;
            result.M22 = lM21*rM12 + lM22*rM22 + lM23*rM32 + lM24*rM42;
            result.M23 = lM21*rM13 + lM22*rM23 + lM23*rM33 + lM24*rM43;
            result.M24 = lM21*rM14 + lM22*rM24 + lM23*rM34 + lM24*rM44;
            result.M31 = lM31*rM11 + lM32*rM21 + lM33*rM31 + lM34*rM41;
            result.M32 = lM31*rM12 + lM32*rM22 + lM33*rM32 + lM34*rM42;
            result.M33 = lM31*rM13 + lM32*rM23 + lM33*rM33 + lM34*rM43;
            result.M34 = lM31*rM14 + lM32*rM24 + lM33*rM34 + lM34*rM44;
            result.M41 = lM41*rM11 + lM42*rM21 + lM43*rM31 + lM44*rM41;
            result.M42 = lM41*rM12 + lM42*rM22 + lM43*rM32 + lM44*rM42;
            result.M43 = lM41*rM13 + lM42*rM23 + lM43*rM33 + lM44*rM43;
            result.M44 = lM41*rM14 + lM42*rM24 + lM43*rM34 + lM44*rM44;
        }

        /// <summary>
        ///     Calculate the inverse of the given matrix
        /// </summary>
        /// <param name="mat">The matrix to invert</param>
        /// <param name="result">The inverse of the given matrix if it has one, or the input if it is singular</param>
        /// <exception cref="InvalidOperationException">Thrown if the Matrix4 is singular.</exception>
        public static void Invert(ref Matrix4 mat, out Matrix4 result)
        {
            int[] colIdx = {0, 0, 0, 0};
            int[] rowIdx = {0, 0, 0, 0};
            int[] pivotIdx = {-1, -1, -1, -1};

            // convert the matrix to an array for easy looping
            float[,] inverse =
            {
                {mat.M11, mat.M12, mat.M13, mat.M14},
                {mat.M21, mat.M22, mat.M23, mat.M24},
                {mat.M31, mat.M32, mat.M33, mat.M34},
                {mat.M41, mat.M42, mat.M43, mat.M44}
            };
            var icol = 0;
            var irow = 0;
            for (var i = 0; i < 4; i++)
            {
                // Find the largest pivot value
                var maxPivot = 0.0f;
                for (var j = 0; j < 4; j++)
                {
                    if (pivotIdx[j] != 0)
                    {
                        for (var k = 0; k < 4; ++k)
                        {
                            if (pivotIdx[k] == -1)
                            {
                                var absVal = Math.Abs(inverse[j, k]);
                                if (absVal > maxPivot)
                                {
                                    maxPivot = absVal;
                                    irow = j;
                                    icol = k;
                                }
                            }
                            else if (pivotIdx[k] > 0)
                            {
                                result = mat;
                                return;
                            }
                        }
                    }
                }

                ++pivotIdx[icol];

                // Swap rows over so pivot is on diagonal
                if (irow != icol)
                {
                    for (var k = 0; k < 4; ++k)
                    {
                        var f = inverse[irow, k];
                        inverse[irow, k] = inverse[icol, k];
                        inverse[icol, k] = f;
                    }
                }

                rowIdx[i] = irow;
                colIdx[i] = icol;

                var pivot = inverse[icol, icol];
                // check for singular matrix
                if (pivot == 0.0f)
                {
                    throw new InvalidOperationException("Matrix is singular and cannot be inverted.");
                }

                // Scale row so it has a unit diagonal
                var oneOverPivot = 1.0f/pivot;
                inverse[icol, icol] = 1.0f;
                for (var k = 0; k < 4; ++k)
                    inverse[icol, k] *= oneOverPivot;

                // Do elimination of non-diagonal elements
                for (var j = 0; j < 4; ++j)
                {
                    // check this isn't on the diagonal
                    if (icol != j)
                    {
                        var f = inverse[j, icol];
                        inverse[j, icol] = 0.0f;
                        for (var k = 0; k < 4; ++k)
                            inverse[j, k] -= inverse[icol, k]*f;
                    }
                }
            }

            for (var j = 3; j >= 0; --j)
            {
                var ir = rowIdx[j];
                var ic = colIdx[j];
                for (var k = 0; k < 4; ++k)
                {
                    var f = inverse[k, ir];
                    inverse[k, ir] = inverse[k, ic];
                    inverse[k, ic] = f;
                }
            }

            result.M11 = inverse[0, 0];
            result.M12 = inverse[0, 1];
            result.M13 = inverse[0, 2];
            result.M14 = inverse[0, 3];
            result.M21 = inverse[1, 0];
            result.M22 = inverse[1, 1];
            result.M23 = inverse[1, 2];
            result.M24 = inverse[1, 3];
            result.M31 = inverse[2, 0];
            result.M32 = inverse[2, 1];
            result.M33 = inverse[2, 2];
            result.M34 = inverse[2, 3];
            result.M41 = inverse[3, 0];
            result.M42 = inverse[3, 1];
            result.M43 = inverse[3, 2];
            result.M44 = inverse[3, 3];
        }

        /// <summary>
        ///     Calculate the inverse of the given matrix
        /// </summary>
        /// <param name="mat">The matrix to invert</param>
        /// <returns>The inverse of the given matrix if it has one, or the input if it is singular</returns>
        /// <exception cref="InvalidOperationException">Thrown if the Matrix4 is singular.</exception>
        public static Matrix4 Invert(Matrix4 mat)
        {
            Matrix4 result;
            Invert(ref mat, out result);
            return result;
        }

        /// <summary>
        ///     Matrix multiplication
        /// </summary>
        /// <param name="left">left-hand operand</param>
        /// <param name="right">right-hand operand</param>
        /// <returns>A new Matrix4 which holds the result of the multiplication</returns>
        public static Matrix4 operator *(Matrix4 left, Matrix4 right)
        {
            return Mult(left, right);
        }

        /// <summary>
        ///     Matrix addition
        /// </summary>
        /// <param name="left">left-hand operand</param>
        /// <param name="right">right-hand operand</param>
        /// <returns>A new Matrix4 which holds the result of the addition</returns>
        public static Matrix4 operator +(Matrix4 left, Matrix4 right)
        {
            return Add(left, right);
        }

        /// <summary>
        ///     Matrix subtraction
        /// </summary>
        /// <param name="left">left-hand operand</param>
        /// <param name="right">right-hand operand</param>
        /// <returns>A new Matrix4 which holds the result of the subtraction</returns>
        public static Matrix4 operator -(Matrix4 left, Matrix4 right)
        {
            return Subtract(left, right);
        }

        /// <summary>
        ///     Compares two instances for equality.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <returns>True, if left equals right; false otherwise.</returns>
        public static bool operator ==(Matrix4 left, Matrix4 right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Compares two instances for inequality.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <returns>True, if left does not equal right; false otherwise.</returns>
        public static bool operator !=(Matrix4 left, Matrix4 right)
        {
            return !left.Equals(right);
        }


        /// <summary>
        ///     Returns the hashcode for this instance.
        /// </summary>
        /// <returns>A System.Int32 containing the unique hashcode for this instance.</returns>
        public override int GetHashCode()
        {
            return M11.GetHashCode() ^
                   M12.GetHashCode() ^
                   M13.GetHashCode() ^
                   M14.GetHashCode() ^
                   M21.GetHashCode() ^
                   M22.GetHashCode() ^
                   M23.GetHashCode() ^
                   M24.GetHashCode() ^
                   M31.GetHashCode() ^
                   M32.GetHashCode() ^
                   M33.GetHashCode() ^
                   M34.GetHashCode() ^
                   M41.GetHashCode() ^
                   M42.GetHashCode() ^
                   M43.GetHashCode() ^
                   M44.GetHashCode();
        }

        /// <summary>
        ///     Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare tresult.</param>
        /// <returns>True if the instances are equal; false otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Matrix4))
                return false;

            return this.Equals((Matrix4) obj);
        }

        /// <summary>Indicates whether the current matrix is equal to another matrix.</summary>
        /// <param name="other">An matrix to compare with this matrix.</param>
        /// <returns>true if the current matrix is equal to the matrix parameter; otherwise, false.</returns>
        public bool Equals(Matrix4 other)
        {
            return M11 == other.M11 &&
                   M12 == other.M12 &&
                   M13 == other.M13 &&
                   M14 == other.M14 &&
                   M21 == other.M21 &&
                   M22 == other.M22 &&
                   M23 == other.M23 &&
                   M24 == other.M24 &&
                   M31 == other.M31 &&
                   M32 == other.M32 &&
                   M33 == other.M33 &&
                   M34 == other.M34 &&
                   M41 == other.M41 &&
                   M42 == other.M42 &&
                   M43 == other.M43 &&
                   M44 == other.M44;
        }
    }
}