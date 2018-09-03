using System;
using System.IO;
using PicoSystem.Framework.Common;
using PicoSystem.Framework.Platform.SDL2;

namespace PicoSystem.Framework.Platform
{
    internal static partial class PicoPlatform
    {
        public enum OS
        {
            Windows,
            Linux,
            Mac
        }

        internal static IntPtr DisplHandle { get; private set; }

        internal static IntPtr NativeHandle { get; private set; }

        public static event EventHandler TerminateRequested;

        public static event EventHandler<Size> DisplayResized;

        public static OS GetCurrentOS()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Unix:

                    if (Directory.Exists("/Applications") &&
                        Directory.Exists("/System") &&
                        Directory.Exists("/Users") &&
                        Directory.Exists("/Volumes"))
                    {
                        return OS.Mac;
                    }

                    return OS.Linux;

                case PlatformID.MacOSX:
                    return OS.Mac;

                default:
                    return OS.Windows;

            }
        }

        public static void InitializeDisplay()
        {
            SDL.SDL_DisableScreenSaver();

            SDL.SDL_WindowFlags displayFlags =
                (SDL.SDL_WindowFlags)
                    (DisplayFlags.Hidden | DisplayFlags.InputFocus | DisplayFlags.MouseFocus);

            DisplHandle = SDL.SDL_CreateWindow(
                "PicoGame",
                SDL.SDL_WINDOWPOS_CENTERED,
                SDL.SDL_WINDOWPOS_CENTERED,
                800,
                600,
                displayFlags);

            if (DisplHandle == IntPtr.Zero)
            {
                throw new Exception("Error creating SDL2 Display: " + SDL.SDL_GetError());
            }

            var info = new SDL.SDL_SysWMinfo();

            SDL.SDL_GetWindowWMInfo(DisplHandle, ref info);

            NativeHandle = info.info.win.window;

        }

        public static void ShowDisplay()
        {
            SDL.SDL_ShowWindow(DisplHandle);
        }

        public static void HideDisplay()
        {
            SDL.SDL_HideWindow(DisplHandle);
        }

        public static void Present()
        {
            SDL.SDL_GL_SwapWindow(DisplHandle);
        }

        public static bool IsFullscreen()
        {
            return GetDisplayFlags().HasFlag(DisplayFlags.FullscreenDesktop);
        }

        public static void SetDisplayFullscreen(bool fullscreen)
        {
            if (IsFullscreen() != fullscreen)
            {
                SDL.SDL_SetWindowFullscreen(DisplHandle, (uint)(fullscreen ? DisplayFlags.FullscreenDesktop : 0));
            }
        }

        public static void SetDisplaySize(Size size)
        {
            if (IsFullscreen())
            {
                return;
            }

            SDL.SDL_SetWindowSize(DisplHandle, size.Width, size.Height);
            SDL.SDL_SetWindowPosition(DisplHandle, SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED);
        }

        public static Size GetDisplaySize()
        {
            int w, h;

            SDL.SDL_GetWindowSize(DisplHandle, out w, out h);

            return new Size(w, h);
        }

        public static Rect GetDisplayBounds()
        {
            int x, y;
            Size size = GetDisplaySize();
            SDL.SDL_GetWindowPosition(DisplHandle, out x, out y);

            return Rect.FromBox(x, y, size.Width, size.Height);
        }

        public static void SetDisplayPosition(int x, int y)
        {
            SDL.SDL_SetWindowPosition(DisplHandle, x, y);
        }

        public static void SetDisplayBorderless(bool borderless)
        {
            SDL.SDL_SetWindowBordered(DisplHandle, (SDL.SDL_bool) (borderless ? 0 : 1));
        }

        public static void SetTitle(string title)
        {
            SDL.SDL_SetWindowTitle(DisplHandle, title);
        }

        public static string GetTitle()
        {
            return SDL.SDL_GetWindowTitle(DisplHandle);
        }
      
        public static void ShowCursor(bool show)
        {
            SDL.SDL_ShowCursor(show ? 1 : 0);
        }

        internal static DisplayFlags GetDisplayFlags()
        {
            return (DisplayFlags) SDL.SDL_GetWindowFlags(DisplHandle);
        }

        [Flags]
        internal enum DisplayFlags
        {
            Fullscreen = 0x00000001,
            Shown = 0x00000004,
            Hidden = 0x00000008,
            Borderless = 0x00000010,
            Resizable = 0x00000020,
            Minimized = 0x00000040,
            Maximized = 0x00000080,
            InputFocus = 0x00000200,
            MouseFocus = 0x00000400,
            FullscreenDesktop = 0x00001001,
            AllowHighDPI = 0x00002000,
            MouseCapture = 0x00004000
        }

    }
}
