using System;
using System.IO;
using System.Runtime.InteropServices;
using PicoSystem.Framework.Common;
using Image = PicoSystem.Framework.Platform.STB.Image;
using ImageReader = PicoSystem.Framework.Platform.STB.ImageReader;
using Stb = PicoSystem.Framework.Platform.STB.Stb;

namespace PicoSystem.Framework.Graphics
{
    public class Pixmap : Resource
    {
        public byte[] ByteArray => _pixelData.data;

        internal IntPtr DataPtr => _pixelData.dataptr;

        public int Width { get; }

        public int Height { get; }

        public int Pitch => Width * 4;

        private readonly PinnedByteArray _pixelData;

        internal Pixmap(int width, int height)
        {
            Width = width;
            Height = height;
            _pixelData = new PinnedByteArray(width * height * 4);
        }
      
        internal Pixmap(byte[] data, int width, int height, bool toBgra=true)
        {
            this._pixelData = new PinnedByteArray(data);

            Width = width;
            Height = height;

            if (toBgra)
            {
                var pixelData = this._pixelData.data;

                for (int i = 0; i < Width * Height; ++i)
                {
                    var idx = i * 4;
                    byte r = pixelData[idx];
                    byte g = pixelData[idx + 1];
                    byte b = pixelData[idx + 2];
                    byte a = pixelData[idx + 3];

                    pixelData[idx] = b;
                    pixelData[idx + 1] = g;
                    pixelData[idx + 2] = r;
                    pixelData[idx + 3] = a;
                }
            }
        }
      
        public override void Dispose()
        {
            _pixelData.Dispose();
        }
    }

    internal class PinnedByteArray : IDisposable
    {
        internal byte[] data;
        internal GCHandle gchandle;
        internal IntPtr dataptr;

        private bool destroyed;

        internal PinnedByteArray(int length)
        {
            data = new byte[length];
            gchandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            dataptr = Marshal.UnsafeAddrOfPinnedArrayElement(data, 0);
        }

        internal PinnedByteArray(byte[] sourceData)
        {
            data = new byte[sourceData.Length];
            Buffer.BlockCopy(sourceData, 0, data, 0, sourceData.Length);

            gchandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            dataptr = Marshal.UnsafeAddrOfPinnedArrayElement(data, 0);
        }

        ~PinnedByteArray()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (!destroyed)
            {
                gchandle.Free();
                data = null;
                destroyed = true;
            }
        }
    }
}
