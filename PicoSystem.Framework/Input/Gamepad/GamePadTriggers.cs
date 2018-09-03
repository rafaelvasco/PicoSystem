using PicoSystem.Framework.Common;

namespace PicoSystem.Framework.Input.Gamepad
{
    internal struct GamePadTriggers
    {
       
        public float Left { get; private set; }
        public float Right { get; private set; }

        public GamePadTriggers(float left, float right)
        {
            Left = Calculate.Clamp(left, 0f, 1f);
            Right = Calculate.Clamp(right, 0f, 1f);
        }

    }
}
