using System;
using System.Collections.Generic;
using PicoSystem.Framework.Common;
using PicoSystem.Framework.Content;
using PicoSystem.Framework.Input.Gamepad;
using PicoSystem.Framework.Platform.SDL2;

namespace PicoSystem.Framework.Platform
{
    internal class GamePadDevice
    {
        public IntPtr Device;
        public IntPtr HapticDevice;
        public int HapticType;

        public GamepadState State;

        public GamePadDevice()
        {
            State = new GamepadState()
            {
                Thumbsticks = new GamepadThumbsticks(),
                Triggers = new GamePadTriggers()
            };
        }
    }

    internal static partial class PicoPlatform
    {
        public static EventHandler<Tuple<int, string>> GamePadAdded;
        public static EventHandler<int> GamePadRemoved;

        private static readonly Dictionary<int, GamePadDevice> _gamepads = new Dictionary<int, GamePadDevice>();

        private static SDL.SDL_HapticEffect _hapticEffectLeftRight = new SDL.SDL_HapticEffect
        {
            type = SDL.SDL_HAPTIC_LEFTRIGHT,
            leftright = new SDL.SDL_HapticLeftRight
            {
                type = SDL.SDL_HAPTIC_LEFTRIGHT,
                length = SDL.SDL_HAPTIC_INFINITY,
                large_magnitude = ushort.MaxValue,
                small_magnitude = ushort.MaxValue
            }
        };

        public static GamepadDeadZoneMode GamepadDeadzoneMode { get; set; } = GamepadDeadZoneMode.IndependentAxis;

        public static void InitializeGamepadModule()
        {
            // Get GamePad Database

            List<string> gamepadDbFileLines = AssetLoader.LoadEmbeddedTextFile("gamecontrollerdb.txt");

            foreach (var line in gamepadDbFileLines)
            {
                if (!line.StartsWith("#"))
                {
                    SDL.SDL_GameControllerAddMapping(line);
                }
            }

            // Check all controller slots for attached controllers and add them

            for (int i = 0; i < GetMaxNumberOfGamepads(); i++)
            {
                if (SDL.SDL_IsGameController(i) == SDL.SDL_bool.SDL_TRUE)
                {
                    AddGamepadDevice(i);
                }
            }

        }

        public static void AddGamepadDevice(int deviceId)
        {
            var gamepad = new GamePadDevice()
            {
                Device = SDL.SDL_GameControllerOpen(deviceId),
                HapticDevice = SDL.SDL_HapticOpen(deviceId)
            };

            if (gamepad.Device == IntPtr.Zero)
            {
                return;
            }

            var id = 1;

            while (_gamepads.ContainsKey(id))
            {
                id ++;
            }

            _gamepads.Add(id, gamepad);

            if (gamepad.HapticDevice == IntPtr.Zero)
            {
                return;
            }

            try
            {
                if (SDL.SDL_HapticEffectSupported(gamepad.HapticDevice, ref _hapticEffectLeftRight) == 1)
                {
                    SDL.SDL_HapticNewEffect(gamepad.HapticDevice, ref _hapticEffectLeftRight);
                    gamepad.HapticType = 1;

                }
                else if (SDL.SDL_HapticRumbleSupported(gamepad.HapticDevice) == 1)
                {
                    SDL.SDL_HapticRumbleInit(gamepad.HapticDevice);
                    gamepad.HapticType = 2;
                }
                else
                {
                    SDL.SDL_HapticClose(gamepad.HapticDevice);
                }

                string gamepadName = SDL.SDL_GameControllerName(gamepad.Device);

                GamePadAdded?.Invoke(null, new Tuple<int, string>(id, gamepadName));
            }
            catch
            {
                SDL.SDL_HapticClose(gamepad.HapticDevice);
                gamepad.HapticDevice = IntPtr.Zero;
                SDL.SDL_ClearError();
            }
        }

        public static void RemoveGamepadDevice(int instanceId)
        {
            foreach (var entry in _gamepads)
            {
                if (SDL.SDL_JoystickInstanceID(SDL.SDL_GameControllerGetJoystick(entry.Value.Device)) == instanceId)
                {
                    _gamepads.Remove(entry.Key);
                    DisposeGamepadDevice(entry.Value);

                    GamePadRemoved?.Invoke(null, entry.Key);

                    break;

                }
            }
        }

        private static void DisposeGamepadDevice(GamePadDevice device)
        {
            if (device.HapticType > 0)
            {
                SDL.SDL_HapticClose(device.HapticDevice);
            }

            SDL.SDL_GameControllerClose(device.Device);
        }

        public static void CloseGamepadsDevices()
        {
            foreach (var gamePadDevice in _gamepads)
            {
                DisposeGamepadDevice(gamePadDevice.Value);
            }

            _gamepads.Clear();
        }

        public static int GetMaxNumberOfGamepads()
        {
            return 2;
        }

        public static GamepadCapabilities GetGamepadCapabilities(int index)
        {
            if (!_gamepads.ContainsKey(index))
            {
                return new GamepadCapabilities();
            }

            if (SDL.SDL_GameControllerName(_gamepads[index].Device) == "Unknown Gamepad")
            {
                return new GamepadCapabilities()
                {
                    IsConnected = true
                };

            }

            return new GamepadCapabilities()
            {
                IsConnected = true,
                HasAButton = true,
                HasBButton = true,
                HasXButton = true,
                HasYButton = true,
                HasBackButton = true,
                HasStartButton = true,
                HasDPadDownButton = true,
                HasDPadLeftButton = true,
                HasDPadRightButton = true,
                HasDPadUpButton = true,
                HasLeftShoulderButton = true,
                HasRightShoulderButton = true,
                HasLeftStickButton = true,
                HasRightStickButton = true,
                HasLeftTrigger = true,
                HasRightTrigger = true,
                HasLeftXThumbStick = true,
                HasLeftYThumbStick = true,
                HasRightXThumbStick = true,
                HasRightYThumbStick = true,
                HasLeftVibrationMotor = true,
                HasRightVibrationMotor = true,
                HasBigButton = true
            };
        }

        private static float NormalizeAxisValue(int axis)
        {
            if (axis < 0)
            {
                return axis/32768f;
            }

            return axis/32767f;
        }

        public static GamepadState GetGamepadState(int index)
        {
            if (!_gamepads.ContainsKey(index))
            {
                return GamepadState.Default;
            }

            var gamepad = _gamepads[index];

            var device = gamepad.Device;

            var thumbsticksState = new GamepadThumbsticks(

                    new Vector2(
                NormalizeAxisValue(SDL.SDL_GameControllerGetAxis(device,
                    SDL.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_LEFTX)),
                NormalizeAxisValue(SDL.SDL_GameControllerGetAxis(device,
                    SDL.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_LEFTY))
                ),

                    new Vector2(
                NormalizeAxisValue(SDL.SDL_GameControllerGetAxis(device,
                    SDL.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_RIGHTX)),
                NormalizeAxisValue(SDL.SDL_GameControllerGetAxis(device,
                    SDL.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_RIGHTY))
                ),

                GamepadDeadzoneMode
            );

            var triggersState = new GamePadTriggers(
                NormalizeAxisValue(SDL.SDL_GameControllerGetAxis(device, SDL.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_TRIGGERLEFT)),
                NormalizeAxisValue(SDL.SDL_GameControllerGetAxis(device, SDL.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_TRIGGERRIGHT))
            );
            
            var buttonsState = gamepad.State.ButtonsState =
                ((SDL.SDL_GameControllerGetButton(device, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_A) == 1)
                    ? GamepadButton.A
                    : 0) |
                ((SDL.SDL_GameControllerGetButton(device, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_B) == 1)
                    ? GamepadButton.B
                    : 0) |
                ((SDL.SDL_GameControllerGetButton(device, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_BACK) == 1)
                    ? GamepadButton.Back
                    : 0) |
                ((SDL.SDL_GameControllerGetButton(device, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_GUIDE) == 1)
                    ? GamepadButton.BigButton
                    : 0) |
                ((SDL.SDL_GameControllerGetButton(device,
                    SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_LEFTSHOULDER) == 1)
                    ? GamepadButton.LeftShoulder
                    : 0) |
                ((SDL.SDL_GameControllerGetButton(device,
                    SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_RIGHTSHOULDER) == 1)
                    ? GamepadButton.RightShoulder
                    : 0) |
                ((SDL.SDL_GameControllerGetButton(device, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_LEFTSTICK) ==
                  1)
                    ? GamepadButton.LeftStick
                    : 0) |
                ((SDL.SDL_GameControllerGetButton(device, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_RIGHTSTICK) ==
                  1)
                    ? GamepadButton.RightStick
                    : 0) |
                ((SDL.SDL_GameControllerGetButton(device, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_START) == 1)
                    ? GamepadButton.Start
                    : 0) |
                ((SDL.SDL_GameControllerGetButton(device, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_X) == 1)
                    ? GamepadButton.X
                    : 0) |
                ((SDL.SDL_GameControllerGetButton(device, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_Y) == 1)
                    ? GamepadButton.Y
                    : 0) |
                ((SDL.SDL_GameControllerGetButton(device, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_UP) == 1)
                    ? GamepadButton.DPadUp
                    : 0) |
                ((SDL.SDL_GameControllerGetButton(device, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_DOWN) == 1)
                    ? GamepadButton.DPadDown
                    : 0) |
                ((SDL.SDL_GameControllerGetButton(device, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_LEFT) == 1)
                    ? GamepadButton.DPadLeft
                    : 0) |
                ((SDL.SDL_GameControllerGetButton(device, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_RIGHT) == 1)
                    ? GamepadButton.DPadRight
                    : 0) |
                ((triggersState.Left > 0f) ? GamepadButton.LeftTrigger : 0) |
                ((triggersState.Right > 0f) ? GamepadButton.RightTrigger : 0);



            return new GamepadState(thumbsticksState, triggersState, buttonsState); 

        }

        public static bool SetGamePadVibration(int index, float leftMotor, float rightMotor)
        {
            if (!_gamepads.ContainsKey(index))
            {
                return false;
            }

            var gamepad = _gamepads[index];

            if (gamepad.HapticType == 0)
            {
                return false;
            }

            if (leftMotor <= 0.0f && rightMotor <= 0.0f)
            {
                SDL.SDL_HapticStopAll(gamepad.HapticDevice);
            }
            else if (gamepad.HapticType == 1)
            {
                _hapticEffectLeftRight.leftright.large_magnitude = (ushort)(65535f * leftMotor);
                _hapticEffectLeftRight.leftright.small_magnitude = (ushort)(65535f * rightMotor);

                SDL.SDL_HapticUpdateEffect(gamepad.HapticDevice, 0, ref _hapticEffectLeftRight);
                SDL.SDL_HapticRunEffect(gamepad.HapticDevice, 0, 1);
            }
            else if (gamepad.HapticType == 2)
            {
                SDL.SDL_HapticRumblePlay(gamepad.HapticDevice, Math.Max(leftMotor, rightMotor), SDL.SDL_HAPTIC_INFINITY);
            }

            return true;
        }
    }
}
