using System;
using System.Collections.Generic;

namespace PicoSystem.Framework.Common
{
    public static class Calculate
    {
        public const float E = (float)Math.E;
        public const float PI = (float)Math.PI;
        public const float PI_OVER2 = (PI / 2.0f);
        public const float PI_OVER3 = (PI / 3.0f);
        public const float PI_OVER4 = (PI / 4.0f);
        public const float PI_OVER6 = (PI / 6.0f);
        public const float TWO_PI = (PI * 2.0f);

        public const float RAD_ANGLE30 = PI_OVER6;
        public const float RAD_ANGLE45 = PI_OVER4;
        public const float RAD_ANGLE90 = PI_OVER2;
        public const float RAD_ANGLE180 = PI;
        public const float RAD_ANGLE360 = TWO_PI;

        private const float RADIANS_TO_DEGREES_FACTOR = 180f / PI;
        private const float DEGREES_TO_RADIANS_FACTOR = PI / 180f;
        private const int SIN_BITS = 13;
        private const int SIN_MASK = ~(-1 << SIN_BITS);
        private const int SIN_COUNT = SIN_MASK + 1;
        private const float RAD_FULL = PI * 2;
        private const float DEG_FULL = 360;
        private const float RAD_TO_INDEX = SIN_COUNT / RAD_FULL;
        private const float DEG_TO_INDEX = SIN_COUNT / DEG_FULL;

        private static readonly float[] sinBuffer = new float[SIN_COUNT];
        private static readonly float[] cosBuffer = new float[SIN_COUNT];

        static Calculate()
        {
            for (int i = 0; i < SIN_COUNT; i++)
            {
                float angle = (i + 0.5f) / SIN_COUNT * RAD_FULL;
                sinBuffer[i] = (float)Math.Sin(angle);
                cosBuffer[i] = (float)Math.Cos(angle);
            }
            for (int i = 0; i < 360; i += 90)
            {
                sinBuffer[(int)(i * DEG_TO_INDEX) & SIN_MASK] = (float)Math.Sin(i * DEGREES_TO_RADIANS_FACTOR);
                cosBuffer[(int)(i * DEG_TO_INDEX) & SIN_MASK] = (float)Math.Cos(i * DEGREES_TO_RADIANS_FACTOR);
            }
        }

        public static void Round(ref Vector2 vector)
        {
            vector.X = (float)Math.Round(vector.X);
            vector.Y = (float)Math.Round(vector.Y);
        }

        public static int RoundToInt(float v)
        {
            return (int)Math.Round(v);
        }

        public static int RoundToInt(float v, MidpointRounding mode)
        {
            return (int)Math.Round(v, mode);
        }

        public static float Sign(float v)
        {
            return Math.Sign(v);
        }

        public static int Sign(int v)
        {
            return Math.Sign(v);
        }

        public static float Sqrt(float v)
        {
            return (float)Math.Sqrt(v);
        }

        public static int Factorial(int n)
        {
            int r = 1;
            for (; n > 1; n--) r *= n;
            return r;
        }

        public static float Floor(float value)
        {
            return (float)Math.Floor(value);
        }

        public static float Ceiling(float value)
        {
            return (float)Math.Ceiling(value);
        }

        public static float Min(float v1, float v2)
        {
            return Math.Min(v1, v2);
        }

        public static float Min(float v1, float v2, float v3)
        {
            return Math.Min(Math.Min(v1, v2), v3);
        }

        public static float Min(float v1, float v2, float v3, float v4)
        {
            return Math.Min(Math.Min(Math.Min(v1, v2), v3), v4);
        }

        public static float Min(params float[] v)
        {
            float m = Single.MaxValue;
            foreach (float val in v) m = Math.Min(m, val);
            return m;
        }

        public static int Min(int v1, int v2)
        {
            return Math.Min(v1, v2);
        }

        public static int Min(int v1, int v2, int v3)
        {
            return Math.Min(Math.Min(v1, v2), v3);
        }

        public static int Min(int v1, int v2, int v3, int v4)
        {
            return Math.Min(Math.Min(Math.Min(v1, v2), v3), v4);
        }

        public static int Min(params int[] v)
        {
            int m = Int32.MaxValue;
            foreach (int val in v) m = Math.Min(m, val);
            return m;
        }

        public static float Max(float v1, float v2)
        {
            return Math.Max(v1, v2);
        }

        public static float Max(float v1, float v2, float v3)
        {
            return Math.Max(Math.Max(v1, v2), v3);
        }

        public static float Max(float v1, float v2, float v3, float v4)
        {
            return Math.Max(Math.Max(Math.Max(v1, v2), v3), v4);
        }

        public static float Max(params float[] v)
        {
            float m = Single.MinValue;
            foreach (float val in v) m = Math.Max(m, val);
            return m;
        }

        public static int Max(int v1, int v2)
        {
            return Math.Max(v1, v2);
        }

        public static int Max(int v1, int v2, int v3)
        {
            return Math.Max(Math.Max(v1, v2), v3);
        }

        public static int Max(int v1, int v2, int v3, int v4)
        {
            return Math.Max(Math.Max(Math.Max(v1, v2), v3), v4);
        }

        public static int Max(params int[] v)
        {
            int m = Int32.MinValue;
            foreach (int val in v) m = Math.Max(m, val);
            return m;
        }

        public static float Sin(float rad)
        {
            return sinBuffer[(int)(rad * RAD_TO_INDEX) & SIN_MASK];
        }

        public static float Cos(float rad)
        {
            return cosBuffer[(int)(rad * RAD_TO_INDEX) & SIN_MASK];
        }

        public static float SinDeg(float deg)
        {
            return sinBuffer[(int)(deg * DEG_TO_INDEX) & SIN_MASK];
        }

        public static float CosDeg(float deg)
        {
            return cosBuffer[(int)(deg * DEG_TO_INDEX) & SIN_MASK];
        }

        public static float ToRadians(float degree)
        {
            return degree * DEGREES_TO_RADIANS_FACTOR;
        }

        public static float ToDegrees(float radian)
        {
            return radian * RADIANS_TO_DEGREES_FACTOR;
        }

        public static float Clamp(float value, float min, float max)
        {
            return (value > max) ? max : ((value < min) ? min : value);
        }

        public static int Clamp(int value, int min, int max)
        {
            return (value > max) ? max : ((value < min) ? min : value);
        }

        public static int NextPowerOfTwo(int value)
        {
            if (value == 0)
            {
                return 1;
            }

            value -= 1;
            value |= value >> 1;
            value |= value >> 2;
            value |= value >> 4;
            value |= value >> 8;
            value |= value >> 16;
            return value + 1;
        }

        public static bool IsPowerOfTwo(int value)
        {
            return value != 0 && (value & value - 1) == 0;
        }

        public static float NormalizeVar(float var, float min, float max)
        {
            if (var >= min && var < max) return var;

            if (var < min)
                var = max + ((var - min) % max);
            else
                var = min + var % (max - min);

            return var;
        }

        public static int NormalizeVar(int var, int min, int max)
        {
            if (var >= min && var < max) return var;

            if (var < min)
                var = max + ((var - min) % max);
            else
                var = min + var % (max - min);

            return var;
        }

        public static float NormalizeAngle(float var)
        {
            if (var >= 0.0f && var < RAD_ANGLE360) return var;

            if (var < 0.0f)
                var = RAD_ANGLE360 + (var % RAD_ANGLE360);
            else
                var = var % RAD_ANGLE360;

            return var;
        }

        public static bool IsZero(float value, float tolerance = float.Epsilon)
        {
            return Math.Abs(value) < tolerance;
        }

        public static bool IsEqual(float a, float b, float tolerance = float.Epsilon)
        {
            return Math.Abs(a - b) < tolerance;
        }

        /// <summary>
        /// Interpolates one value to another by amount linearly
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static float Lerp(float value1, float value2, float amount)
        {
            return value1 + (value2 - value1) * amount;
        }

        public static float LerpPrecise(float value1, float value2, float amount)
        {
            return ((1 - amount) * value1) + (value2 * amount);
        }

        /// <summary>
        /// Interpolates one value to another by amount quadractically
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static float Qerp(float value1, float value2, float amount)
        {
            float result = Clamp(amount, 0f, 1f);
            result = Hermite(value1, 0f, value2, 0f, result);
            return result;
        }


        public static float Barycentric(float value1, float value2, float value3, float amount1, float amount2)
        {
            return value1 + (value2 - value1) * amount1 + (value3 - value1) * amount2;
        }

        /// <summary>
        /// Performs a CatmullRom interpolation
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="value3"></param>
        /// <param name="value4"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static float CatmullRom(float value1, float value2, float value3, float value4, float amount)
        {
            // Using formula from http://www.mvps.org/directx/articles/catmull/
            // Internally using doubles not to lose precission
            double amountSquared = amount * amount;
            double amountCubed = amountSquared * amount;
            return (float)(0.5 * (2.0 * value2 +
                                 (value3 - value1) * amount +
                                 (2.0 * value1 - 5.0 * value2 + 4.0 * value3 - value4) * amountSquared +
                                 (3.0 * value2 - value1 - 3.0 * value3 + value4) * amountCubed));
        }

        /// <summary>
        /// Performs an Hermite Interpolation
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="tangent1"></param>
        /// <param name="value2"></param>
        /// <param name="tangent2"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static float Hermite(float value1, float tangent1, float value2, float tangent2, float amount)
        {
            float a2 = amount * amount;
            float asqr3 = amount * a2;
            float a3 = a2 + a2 + a2;

            return (value1 * (((asqr3 + asqr3) - a3) + 1f)) +
                   (value2 * ((-2f * asqr3) + a3)) +
                   (tangent1 * ((asqr3 - (a2 + a2)) + amount)) +
                   (tangent2 * (asqr3 - a2));
        }

        public static float SmoothStep(float value1, float value2, float amount)
        {

            float result = Clamp(amount, 0f, 1f);
            result = Hermite(value1, 0f, value2, 0f, result);
            return result;
        }

        public static void GenerateCircle(int cx, int cy, int r, ref List<Point> points)
        {
            int x = -r,
                y = 0,
                error = 2 - 2 * r;

            do
            {
                points.Add(new Point(cx - x, cy + y));
                points.Add(new Point(cx - y, cy - x));
                points.Add(new Point(cx + x, cy - y));
                points.Add(new Point(cx + y, cy + x));

                r = error;

                if (r <= y)
                {
                    error += ++y * 2 + 1;
                }

                if (r > x || error > y)
                {
                    error += ++x * 2 + 1;
                }


            } while (x < 0);
        }

        public static void GenerateEllipse(int x0, int y0, int x1, int y1, ref List<Point> points)
        {
            int a = Math.Abs(x1 - x0),
                b = Math.Abs(y1 - y0),
                b1 = b & 1;
            long dx = 4 * (1 - a) * b * b,
                 dy = 4 * (b1 + 1) * a * a;
            long error = dx + dy + b1 * a * a,
                 e2;

            if (x0 > x1)
            {
                x0 = x1;
                x1 += a;
            }
            if (y0 > y1)
            {
                y0 = y1;
            }

            y0 += (b + 1) / 2;
            y1 = y0 - b1;

            a *= 8 * a;
            b1 = 8 * b * b;

            do
            {
                points.Add(new Point(x1, y0));
                points.Add(new Point(x0, y0));
                points.Add(new Point(x0, y1));
                points.Add(new Point(x1, y1));

                e2 = 2 * error;
                if (e2 <= dy)
                {
                    y0++;
                    y1--;
                    error += dy += a;
                }
                if (e2 >= dx || 2 * error > dy)
                {
                    x0++;
                    x1--;
                    error += dx += b1;
                }

            } while (x0 <= x1);

            while (y0 - y1 < b)
            {
                points.Add(new Point(x0 - 1, y0));
                points.Add(new Point(x1 + 1, y0++));
                points.Add(new Point(x0 - 1, y1));
                points.Add(new Point(x1 + 1, y1--));
            }
        }

        public static float Distance(float x, float y)
        {
            return ((float)Math.Sqrt(x * x + y * y));
        }

        public static float PointDistance(float x1, float y1, float x2, float y2)
        {
            return ((float)Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1)));
        }

        public static int PointDistance(int x1, int y1, int x2, int y2)
        {
            return (int)(Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1)));
        }

        public static float DistanceQuad(float x1, float y1, float x2, float y2)
        {
            return (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1);
        }

        public static float Angle(float x1, float y1, float x2, float y2)
        {
            return (float)((Math.Atan2((y2 - y1), (x2 - x1)) + PI_OVER2 + TWO_PI) % TWO_PI);
        }

        public static float NormalizeAngle(float angle, float angleMin, float angleMax)
        {
            if (angle >= angleMin && angle < angleMax)
            {
                return angle;
            }

            if (angle < angleMin)
            {
                angle = angleMax + ((angle - angleMin) % angleMax);
            }
            else
            {
                angle = angleMin + angle % (angleMax - angleMin);
            }

            return angle;
        }

        public static int NormalizeAngle(int angle, int angleMin, int angleMax)
        {
            if (angle >= angleMin && angle < angleMax)
            {
                return angle;
            }

            if (angle < angleMin)
            {
                angle = angleMax + ((angle - angleMin) % angleMax);
            }
            else
            {
                angle = angleMin + angle % (angleMax - angleMin);
            }

            return angle;
        }

        public static Vector2 AngleToVector(float angle, float length)
        {
            return new Vector2(Cos(angle) * length, Sin(angle) * length);
        }

        public static float Approach(float val, float target, float maxMove)
        {
            return val > target ? Max(val - maxMove, target) : Min(val + maxMove, target);
        }

        public static float Snap(float value, float increment)
        {
            return Floor(value / increment) * increment;
        }

        public static int Snap(int value, int increment)
        {
            return (int) (Floor((float)value / increment) * increment);
        }

        public static void Snap(ref Vector2 point, float increment)
        {
            point.X = Floor(point.X / increment) * increment;
            point.Y = Floor(point.Y / increment) * increment;
        }

    }
}
