using System;

namespace PicoSystem.Framework.Common
{
    public sealed class Random
    {
        const double REAL_UNIT_INT = 1.0 / (int.MaxValue + 1.0);
        const double REAL_UNIT_UINT = 1.0 / (uint.MaxValue + 1.0);
        const uint Y = 842502087, Z = 3579807591, W = 273326509;

        uint x, y, z, w;

        public Random()
        {
            Reinitialise(Environment.TickCount);
        }

        public Random(int seed)
        {
            Reinitialise(seed);
        }

        public void Reinitialise(int seed)
        {
            x = (uint)seed;
            y = Y;
            z = Z;
            w = W;
        }

        public int NextInt()
        {
            uint t = (x ^ (x << 11));
            x = y; y = z; z = w;
            return (int)(0x7FFFFFFF & (w = (w ^ (w >> 19)) ^ (t ^ (t >> 8))));
        }

        public int NextInt(int upperBound)
        {
            if (upperBound < 0)
                throw new ArgumentOutOfRangeException(nameof(upperBound), upperBound, "upperBound must be >=0");

            uint t = (x ^ (x << 11));
            x = y; y = z; z = w;

            return (int)((REAL_UNIT_INT * (int)(0x7FFFFFFF & (w = (w ^ (w >> 19)) ^ (t ^ (t >> 8))))) * upperBound);
        }

        public int NextInt(int lowerBound, int upperBound)
        {
            if (lowerBound > upperBound)
                throw new ArgumentOutOfRangeException(nameof(upperBound), upperBound, "upperBound must be >=lowerBound");

            uint t = (x ^ (x << 11));
            x = y; y = z; z = w;

            int range = upperBound - lowerBound;
            if (range < 0)
            {
                return lowerBound + (int)((REAL_UNIT_UINT * (w = (w ^ (w >> 19)) ^ (t ^ (t >> 8)))) * (upperBound - (long)lowerBound));
            }

            return lowerBound + (int)((REAL_UNIT_INT * (int)(0x7FFFFFFF & (w = (w ^ (w >> 19)) ^ (t ^ (t >> 8))))) * range);
        }

        public double NextDouble()
        {
            uint t = (x ^ (x << 11));
            x = y; y = z; z = w;

            return (REAL_UNIT_INT * (int)(0x7FFFFFFF & (w = (w ^ (w >> 19)) ^ (t ^ (t >> 8)))));
        }

        public float NextFloat()
        {
            return (float)NextDouble();
        }

        public float NextFloat(float lowerBound, float upperBound)
        {
            return (float)(lowerBound + upperBound * (NextDouble()));
        }

        public uint NextUInt()
        {
            uint t = (x ^ (x << 11));
            x = y; y = z; z = w;
            return (w = (w ^ (w >> 19)) ^ (t ^ (t >> 8)));
        }

        public Color NextColor()
        {
            return new Color(NextFloat(), NextFloat(), NextFloat());
        }

        uint bitBuffer;
        uint bitMask = 1;

        public bool NextBool()
        {
            if (bitMask == 1)
            {
                uint t = (x ^ (x << 11));
                x = y; y = z; z = w;
                bitBuffer = w = (w ^ (w >> 19)) ^ (t ^ (t >> 8));

                bitMask = 0x80000000;
                return (bitBuffer & bitMask) == 0;
            }

            return (bitBuffer & (bitMask >>= 1)) == 0;
        }
    }
}
