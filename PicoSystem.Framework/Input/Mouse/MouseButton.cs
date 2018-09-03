using System;

namespace PicoSystem.Framework.Input.Mouse
{
    [Flags]
    public enum MouseButton
    {
        Left = 1 << 0,
        Right = 1 << 1,
        Middle = 1 << 2,
        X1 = 1 << 3,
        X2 = 1 << 4
    }
}
