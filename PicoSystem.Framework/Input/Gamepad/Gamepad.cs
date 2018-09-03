using PicoSystem.Framework.Common;

namespace PicoSystem.Framework.Input.Gamepad
{
    public class Gamepad
    {
        private GamepadState _previousState;
        private GamepadState _currentState;

        internal static readonly Gamepad Default = new Gamepad(0, "Dummy:)");

        public Gamepad(int slot, string name)
        {
            this.Slot = slot;
            this.Name = name;
        }

        public int Slot { get; private set; }
        public string Name { get; private set; }

        public bool ButtonDown(GamepadButton button)
        {
            return _currentState[button];
        }

        public bool ButtonUp(GamepadButton button)
        {
            return !_currentState[button];
        }

        public bool ButtonPressed(GamepadButton button)
        {
            return _currentState[button] && !_previousState[button];
        }

        public bool ButtonReleased(GamepadButton button)
        {
            return !_currentState[button] && _previousState[button];
        }

        public Vector2 LeftThumbstickVector => _currentState.Thumbsticks.Left;
        public Vector2 RightThumbstickVector => _currentState.Thumbsticks.Right;

        public float LeftTriggerValue => _currentState.Triggers.Left;
        public float RightTriggerValue => _currentState.Triggers.Right;

        internal void Update(GamepadState state)
        {
            _previousState = _currentState;

            _currentState = state;
        }
    }
}
