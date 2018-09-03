using PicoSystem.Framework.Common;
using PicoSystem.Framework.Graphics;
using PicoSystem.Framework.Input.Mouse;
using PicoSystem.Framework.Platform.SDL2;

namespace PicoSystem.Framework.Platform
{
    internal static partial class PicoPlatform
    {
        internal static int ScrollY;
        private static MouseState _mouseState;

        public static void InitializeMouseModule() { }
        
        public static MouseState GetMouseState()
        {
            int x, y;

            var state = SDL.SDL_GetMouseState(out x, out y);

            if (GetDisplayFlags().HasFlag(DisplayFlags.MouseFocus))
            {

                _mouseState[MouseButton.Left] = (state & SDL.SDL_BUTTON_LMASK) != 0;
                _mouseState[MouseButton.Right] = (state & SDL.SDL_BUTTON_RMASK) != 0;
                _mouseState[MouseButton.Middle] = (state & SDL.SDL_BUTTON_MMASK) != 0;
                _mouseState[MouseButton.X1] = (state & SDL.SDL_BUTTON_X1MASK) != 0;
                _mouseState[MouseButton.X2] = (state & SDL.SDL_BUTTON_X2MASK) != 0;

                _mouseState.ScrollWheelValue = ScrollY;


                Point transformedMousePos = PicoGfx.Instance.TransformMousePosition(x, y);

                _mouseState.X = transformedMousePos.X;
                _mouseState.Y = transformedMousePos.Y;
            }

            return _mouseState;
            
        }
    }
}
