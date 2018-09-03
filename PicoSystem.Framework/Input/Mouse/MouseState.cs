namespace PicoSystem.Framework.Input.Mouse
{
    internal struct MouseState
    {
        private MouseButton _buttonState;

        internal MouseState(int x, int y, int scroll) : this()
        {

            _buttonState = 0x0;

            X = x;
            Y = y;
            ScrollWheelValue = scroll;

        }

        public bool this[MouseButton button]
        {
            get { return (_buttonState &  button) == button; }
            set
            {
                if (value)
                {
                    _buttonState |= button;
                }
                else
                {
                    _buttonState &= ~button;
                }
            }
        }

        public int X { get; internal set; }

        public int Y { get; internal set; }

        public int ScrollWheelValue { get; internal set; }
    }
}
