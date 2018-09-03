using System;
using System.Diagnostics;
using PicoSystem.Framework.Common;
using PicoSystem.Framework.Content;
using PicoSystem.Framework.GameToolkit.UI;
using PicoSystem.Framework.Graphics;
using PicoSystem.Framework.Input;
using PicoSystem.Framework.Platform;

namespace PicoSystem.Framework
{
    public class GameTime
    {
        public TimeSpan Total;

        public TimeSpan Delta;
    }

    public sealed class PicoGame : IDisposable
    {
        internal static PicoGame Instance { get; private set; }

        public Size DisplaySize
        {
            get { return _displaySize; }
            set
            {
                if (_fullscreen)
                {
                    _fullscreen = false;
                }

                if (_running)
                {
                    _requestedDisplaySize = new Size(value.Width, value.Height);
                    _displayModeChanged = true;
                }
                else
                {
                    _displaySize = new Size(value.Width, value.Height);
                }

            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _titleChanged = true;
                _title = value;
            }
        }

        public bool Fullscreen
        {
            get { return _fullscreen; }
            set
            {
                if (_fullscreen != value)
                {
                    _fullscreen = value;
                    _displayModeChanged = true;
                }
            }
        }

        private bool _running;
        private readonly PicoInput _input;
        private readonly PicoGfx _gfx;
        private Stopwatch _gameTimer;
        private GameTime _gameTime;
        private string _title;
        private Size _displaySize = new Size(800,600);
        private Size _requestedDisplaySize;
        private bool _fullscreen;
        private bool _displayModeChanged;
        private bool _titleChanged;

        public PicoGame()
        {
            Instance = this;

            PicoPlatform.Initialize();

            _gfx = new PicoGfx();
            _input = new PicoInput();

            PicoPlatform.TerminateRequested += (sender, args) => this.Quit();
            PicoPlatform.DisplayResized += OnDisplayResize;

            PicoPlatform.GamePadAdded += (sender, gamepadDesc) => _input.AddGamePad(gamepadDesc);
            PicoPlatform.GamePadRemoved += (sender, slot) => _input.RemoveGamePad(slot);
        }

        public void Start()
        {
            _running = true;

            if (!Fullscreen)
            {
                PicoPlatform.SetDisplaySize(DisplaySize);
            }
            else
            {
                PicoPlatform.SetDisplayFullscreen(true);
            }

            _gfx.Initialize(PicoPlatform.DisplHandle, DisplaySize.Width, DisplaySize.Height);

            PicoPlatform.ShowDisplay();

            _gameTimer = Stopwatch.StartNew();
            _gameTime = new GameTime() {Delta = TimeSpan.Zero, Total = TimeSpan.Zero};

            float dt;

            while (_running)
            {
                _gameTime.Delta = _gameTimer.Elapsed - _gameTime.Total;
                _gameTime.Total = _gameTimer.Elapsed;

                PicoPlatform.ProcessEvents();

                _input.UpdateState();

                dt = (float) _gameTime.Delta.TotalSeconds;

                Update(dt);

                if (!_displayModeChanged)
                {
                    _gfx.Begin();

                    Draw(_gfx);

                    _gfx.End();
                }
                else 
                {
                    if (_fullscreen)
                    {
                        PicoPlatform.SetDisplayFullscreen(true);
                    }
                    else
                    {
                        PicoPlatform.SetDisplayFullscreen(false);
                        PicoPlatform.SetDisplaySize(_requestedDisplaySize);
                    }

                    _displayModeChanged = false;
                }

                if (_titleChanged)
                {
                    PicoPlatform.SetTitle(_title);
                    _titleChanged = false;
                }
            }
        }

        public void Quit()
        {
            _running = false;
        }

        public void ToggleFullscreen()
        {
            this.Fullscreen = !this.Fullscreen;
        }

        private void Update(float dt) { }

        private void Draw(PicoGfx gfx) { }

        public void Dispose()
        {
            _gfx.Dispose();

            PicoPlatform.Shutdown();
        }

       
        private void OnDisplayResize(object sender, Size size)
        {
            if (_displaySize != size)
            {
                _gfx.OnDisplayResize(size.Width, size.Height);

                _displaySize = size;
            }
        }
    }
}
