using System;
using System.Runtime.InteropServices;

namespace PicoSystem.Framework.Platform.SDL2
{
    public static partial class SDLImage
    {
        public static SDL.SDL_Surface IMG_LoadEx(string file)
        {
            IntPtr surfacePtr = SDLImage.IMG_Load(file);

            var imageSurface = (SDL.SDL_Surface)Marshal.PtrToStructure(
               surfacePtr,
               typeof(SDL.SDL_Surface)
               );



            return imageSurface;
        }

        public static SDL.SDL_PixelFormat GetPixelFormat(SDL.SDL_Surface surface)
        {
            IntPtr dataPtr = surface.format;

            var pixelFormat = (SDL.SDL_PixelFormat)Marshal.PtrToStructure(
              dataPtr,
              typeof(SDL.SDL_PixelFormat)
              );

            return pixelFormat;
        }
    }
}
