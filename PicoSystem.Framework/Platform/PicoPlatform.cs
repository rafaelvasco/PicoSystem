using System;
using PicoSystem.Framework.Common;
using PicoSystem.Framework.Platform.SDL2;

namespace PicoSystem.Framework.Platform
{
    internal static partial class PicoPlatform
    {
        public static void Initialize()
        {
            SDL.SDL_SetHint(SDL.SDL_HINT_WINDOWS_DISABLE_THREAD_NAMING, "1");

            if (SDL.SDL_Init(
                SDL.SDL_INIT_VIDEO |
                SDL.SDL_INIT_JOYSTICK |
                SDL.SDL_INIT_GAMECONTROLLER |
                SDL.SDL_INIT_HAPTIC) < 0)
            {
                SDL.SDL_Quit();
                throw new Exception("Failed to initialize SDL2");
            }

            InitializeDisplay();
            InitializeKeyboardModule();
            InitializeMouseModule();
            InitializeGamepadModule();
        }

        public static void Shutdown()
        {
            Console.WriteLine("PicoPlatform: Shutdown");

            SDL.SDL_DestroyWindow(DisplHandle);
            SDL.SDL_Quit();
        }

        public static void ProcessEvents()
        {
            SDL.SDL_Event ev;

            while (SDL.SDL_PollEvent(out ev) == 1)
            {

                switch (ev.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT:
                        TerminateRequested?.Invoke(null, EventArgs.Empty);
                        break;

                    case SDL.SDL_EventType.SDL_KEYDOWN:

                        if (ev.key.repeat == 0)
                        {
                            AddKey((int)ev.key.keysym.sym);
                        }

                        break;

                    case SDL.SDL_EventType.SDL_KEYUP:

                        RemoveKey((int) ev.key.keysym.sym);

                        break;

                    case SDL.SDL_EventType.SDL_WINDOWEVENT:

                        switch (ev.window.windowEvent)
                        {
                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_SIZE_CHANGED:

                                var newW = ev.window.data1;
                                var newH = ev.window.data2;

                                DisplayResized?.Invoke(null, new Size(newW, newH));

                                break;
                        }

                        break;

                    case SDL.SDL_EventType.SDL_MOUSEWHEEL:

                        ScrollY += ev.wheel.y*120;

                        break;


                    case SDL.SDL_EventType.SDL_CONTROLLERDEVICEADDED:

                        AddGamepadDevice(ev.cdevice.which);

                        break;

                    case SDL.SDL_EventType.SDL_CONTROLLERDEVICEREMOVED:

                        RemoveGamepadDevice(ev.cdevice.which);

                        break;

                   
                }
            }
        }
    }
}
