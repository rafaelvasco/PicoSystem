using System;
using PicoSystem.Framework.Platform.SDL2;

namespace PicoSystem.Framework.Graphics
{
    public class PicoSurface : Resource
    {
        public enum AccessType
        {
            Static,
            Dynamic,
            Target
        }

        public Pixmap Pixmap => _pixmap;
        internal IntPtr Texture => _texture;

        public int Width => _pixmap.Width;
        public int Height => _pixmap.Height;

        private readonly Pixmap _pixmap;
        private readonly IntPtr _texture;

        internal PicoSurface(PicoGfx gfx, Pixmap pixmap, AccessType accessType)
        {
            _pixmap = pixmap;

            SDL.SDL_TextureAccess textureAccess;

            switch (accessType)
            {

                case AccessType.Static: textureAccess = SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STATIC; break;
                case AccessType.Dynamic: textureAccess = SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STREAMING; break;
                case AccessType.Target: textureAccess = SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET; break;
                default: textureAccess = SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STATIC; break;
            }

            _texture = SDL.SDL_CreateTexture(gfx.Context, SDL.SDL_PIXELFORMAT_ARGB8888,
                (int)textureAccess, pixmap.Width, pixmap.Height);

            SDL.SDL_SetTextureBlendMode(_texture, SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND);

            SDL.SDL_UpdateTexture(_texture, IntPtr.Zero, _pixmap.DataPtr, _pixmap.Pitch);
        }

        internal PicoSurface(PicoGfx gfx, int width, int height, AccessType accessType)
        {
           
            _pixmap = new Pixmap(width, height);

            SDL.SDL_TextureAccess textureAccess;

            switch (accessType)
            {

                case AccessType.Static: textureAccess = SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STATIC; break;
                case AccessType.Dynamic: textureAccess = SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STREAMING; break;
                case AccessType.Target: textureAccess = SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET; break;
                default: textureAccess = SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STATIC; break;
            }

            _texture = SDL.SDL_CreateTexture(gfx.Context, SDL.SDL_PIXELFORMAT_ARGB8888,
                (int)textureAccess, width, height);

            SDL.SDL_UpdateTexture(_texture, IntPtr.Zero, _pixmap.DataPtr, _pixmap.Pitch);

        }

        public void BlitPixmap(Pixmap pixmap, int x, int y)
        {
            var w = pixmap.Width;
            var h = pixmap.Height;
            var srcData = pixmap.ByteArray;
            var targetData = _pixmap.ByteArray;

            for (var i = 0; i < w; ++i)
            {
                for (var j = 0; j < h; ++j)
                {
                    var idx = (i + j * w) * 4;

                    var a = srcData[idx + 3];

                    if (a != 255)
                    {
                        continue;
                    }

                    var r = srcData[idx + 2];
                    var g = srcData[idx + 1];
                    var b = srcData[idx];

                    var targetIdx = ((i+x) + (j+y) * _pixmap.Width) * 4;

                    targetData[targetIdx] = b;
                    targetData[targetIdx + 1] = g;
                    targetData[targetIdx + 2] = r;
                }
            }

            SDL.SDL_UpdateTexture(_texture, IntPtr.Zero, _pixmap.DataPtr, _pixmap.Pitch);

        }

        public override void Dispose()
        {
            _pixmap?.Dispose();
            SDL.SDL_DestroyTexture(_texture);
        }
       
    }
}
