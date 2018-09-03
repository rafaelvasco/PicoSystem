using System;
using System.Collections.Generic;
using PicoSystem.Framework.Input.Gamepad;
using PicoSystem.Framework.Input.Keyboard;
using PicoSystem.Framework.Input.Mouse;
using PicoSystem.Framework.Platform;

namespace PicoSystem.Framework.Input
{
    public class PicoInput
    {
        private readonly Dictionary<int, Gamepad.Gamepad> _gamepads = new Dictionary<int, Gamepad.Gamepad>();

        private int _gamepadCount;

        private KeyboardState _prevKeyboardState;
        private KeyboardState _curKeyboardState;

        private MouseState _prevMouseState;
        private MouseState _curMouseState;
        
        public int MouseX => _curMouseState.X;
        public int MouseY => _curMouseState.Y;

        public int ScrollValue => _curMouseState.ScrollWheelValue;

        public int ScrollDelta => _curMouseState.ScrollWheelValue - _prevMouseState.ScrollWheelValue;

        public GamepadDeadZoneMode GamepadDeadzoneMode
        {
            get { return PicoPlatform.GamepadDeadzoneMode; }
            set { PicoPlatform.GamepadDeadzoneMode = value; }
        }

        internal void AddGamePad(Tuple<int, string> gamepadDesc )
        {
            _gamepads.Add(gamepadDesc.Item1, new Gamepad.Gamepad(gamepadDesc.Item1, gamepadDesc.Item2));

            _gamepadCount ++;
        }

        internal void RemoveGamePad(int slot)
        {
            _gamepads.Remove(slot);

            _gamepadCount--;
        }

        public Gamepad.Gamepad Gamepad(int slot)
        {
            if (_gamepadCount <= 0) return Input.Gamepad.Gamepad.Default;

            Gamepad.Gamepad gamepad;

            return _gamepads.TryGetValue(slot, out gamepad) ? gamepad : Input.Gamepad.Gamepad.Default;
        }

        public bool KeyDown(Key key)
        {
            return _curKeyboardState[key];
        }

        public bool KeyUp(Key key)
        {
            return !_curKeyboardState[key];
        }

        public bool KeyPressed(Key key)
        {
            return _curKeyboardState[key] && !_prevKeyboardState[key];
        }

        public bool KeyReleased(Key key)
        {
            return !_curKeyboardState[key] && _prevKeyboardState[key];
        }

        public bool MouseDown(MouseButton button)
        {
            return _curMouseState[button];
        }

        public bool MouseUp(MouseButton button)
        {
            return !_curMouseState[button];
        }

        public bool MousePressed(MouseButton button)
        {
            var pressed = _curMouseState[button] && !_prevMouseState[button];

            if (pressed)
            {
                Console.WriteLine("");
            }

            return _curMouseState[button] && !_prevMouseState[button];
        }

        public bool MouseReleased(MouseButton button)
        {
            return !_curMouseState[button] && _prevMouseState[button];
        }

        internal void UpdateState()
        {
            _prevKeyboardState = _curKeyboardState;

            _curKeyboardState = PicoPlatform.GetKeyboardState();

            _prevMouseState = _curMouseState;

            _curMouseState = PicoPlatform.GetMouseState();

            foreach (var gamepad in _gamepads)
            {
                gamepad.Value.Update(PicoPlatform.GetGamepadState(gamepad.Key));
            }
        }
    }
}
