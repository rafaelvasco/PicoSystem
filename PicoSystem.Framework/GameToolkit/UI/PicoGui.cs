using System;
using System.Collections.Generic;
using PicoSystem.Framework.Common;
using PicoSystem.Framework.Content;
using PicoSystem.Framework.Graphics;
using PicoSystem.Framework.Input;
using PicoSystem.Framework.Input.Mouse;

namespace PicoSystem.Framework.GameToolkit.UI
{
    internal class UIMouseState
    {
        public int MouseX;
        public int MouseY;
        public int LastMouseX;
        public int LastMouseY;
        public bool MouseLeftDown;
        public bool MouseRightDown;
        public bool MouseMiddleDown;

        public void UpdatePosition(int x, int y)
        {
            LastMouseX = MouseX;
            LastMouseY = MouseY;

            MouseX = x;
            MouseY = y;
        }

        public bool Moved => MouseX != LastMouseX || MouseY != LastMouseY;
    }

    public enum Orientation
    {
        Horizontal,
        Vertical
    }

    /* ============================================================== */

    public abstract class Widget
    {

        public int X
        {
            get { return _x; }
            set
            {
                _x = value;
                Invalidate();
            }
        }

        public int Y
        {
            get { return _y; }
            set
            {
                _y = value;
                Invalidate();
            }
        }

        public int W
        {
            get { return _w; }
            set
            {
                _w = value;
                Invalidate();
            }
        }

        public int H
        {
            get { return _h; }
            set
            {
                _h = value;
                Invalidate();
            }
        }

        public Container Parent { get; }

        public int GlobalX => Parent?.GlobalX + X ?? X;
        public int GlobalY => Parent?.GlobalY + Y ?? Y;

        public bool Hovered { get; protected set; }
        public bool Active { get; protected set; }
        public bool IsContainer { get; internal set; }

        public Rect BoundingRect => Rect.FromBox(GlobalX, GlobalY, W,H);

        protected readonly PicoGui Gui;

        private int _x;
        private int _y;
        private int _w;
        private int _h;

        protected Widget(PicoGui gui)
        {
            Gui = gui;
            Parent = null;
        }

        protected Widget(PicoGui gui, Container parent)
        {
            Gui = gui;
            Parent = parent;
        }

        public virtual bool ContainsPoint(int px, int py)
        {
            if (
                px < GlobalX ||
                py < GlobalY ||
                px > GlobalX + W ||
                py > GlobalY + H)
            {
                return false;
            }

            return true;
        }

        protected void Invalidate()
        {
            Gui.Invalidate();
        }

        internal abstract void Update(UIMouseState mouseState);
        internal abstract void Draw(PicoGfx gfx, PicoGuiTheme theme);
    }

    public class Container : Widget
    {
        protected List<Widget> _children;
        protected Dictionary<string, int> _widgetMap;
        protected bool _layoutInvalidated = true;

        public int Padding { get; set; } = 10;

        private void AddWidget(string id, Widget widget)
        {
            _children.Add(widget);
            _widgetMap.Add(id, _children.Count - 1);

            _layoutInvalidated = true;
        }

        public Button AddButton(string id, string label)
        {
            var button = new Button(Gui, this) {Label = label};


            AddWidget(id, button);

            return button;
        }

        public CheckBox AddCheckbox(string id)
        {
            var checkbox = new CheckBox(Gui, this);

            AddWidget(id, checkbox);

            return checkbox;
        }

        public Panel AddPanel(string id)
        {
            var panel = new Panel(Gui, this)
            {
                IsContainer = true
            };

            AddWidget(id, panel);

            return panel;
        }

        public Slider AddSlider(string id, int minValue, int maxValue, int step, Orientation orientation = Orientation.Horizontal)
        {
            var slider = new Slider(
                Gui,
                this,
                value: minValue,
                minValue: minValue,
                maxValue: maxValue,
                step: step, orientation:
                orientation);

            AddWidget(id, slider);

            return slider;
        }

        public Container AddContainer(string id)
        {
            var container = new Container(Gui, this) { IsContainer = true };

            AddWidget(id, container);

            return container;
        }

        public HorizontalContainer AddHorizontalContainer(string id)
        {
            var container = new HorizontalContainer(Gui, this) { IsContainer = true };

            AddWidget(id, container);

            return container;
        }

        public VerticalContainer AddVerticalContainer(string id)
        {
            var container = new VerticalContainer(Gui, this) { IsContainer = true };

            AddWidget(id, container);

            return container;
        }

        protected virtual void DoLayout() { }

        internal override void Update(UIMouseState mouseState)
        {
            if (_layoutInvalidated)
            {
                DoLayout();
                _layoutInvalidated = false;
            }
            
            foreach (var widget in _children)
            {
                if ( this.BoundingRect.Intersects(widget.BoundingRect))
                {
                    widget.Update(mouseState);
                }
            }
        }

        internal override void Draw(PicoGfx gfx, PicoGuiTheme theme)
        {
            var x = (this.GlobalX);
            var y = (this.GlobalY);
            var w = (this.W);
            var h = (this.H);

            gfx.BeginClip(x, y, w, h);

            foreach (var widget in _children)
            {
                widget.Draw(gfx, theme);
            }

            gfx.EndClip();
        }

        internal Container(PicoGui gui, int width, int height) : base(gui)
        {
            W = width;
            H = height;
            _children =
                new List<Widget>();
            _widgetMap = new Dictionary<string, int>();
        } 

        internal Container(PicoGui gui, Container parent) : base(gui, parent)
        {
            W = parent.W ;
            H = parent.H;
            _children = 
                new List<Widget>();
            _widgetMap = new Dictionary<string, int>();
        }
    }

    public class VerticalContainer : Container
    {
        public int ItemSpacing { get; set; } = 10;
        public bool StretchItems { get; set; } = true;

        internal VerticalContainer(PicoGui gui, Container parent) : base(gui, parent)
        {
        }

        protected override void DoLayout()
        {

            int itemHeight = 0;

            if (StretchItems)
            {
                itemHeight = (this.H - 2 * Padding - (_children.Count - 1) * ItemSpacing) / _children.Count;
            }

            for (int i = 0; i < _children.Count; i++)
            {
                var widget = _children[i];

                if (!StretchItems)
                {
                    itemHeight = widget.H;
                }

                widget.Y = this.Y + (i * itemHeight) + (i * ItemSpacing) + Padding;
                widget.X = this.X + this.Padding;
                
                widget.H = itemHeight;

                if (StretchItems || widget.IsContainer)
                {
                    widget.W = this.W - 2 * Padding;
                }
            }
        }
    }

    public class HorizontalContainer : Container
    {
        public int ItemSpacing { get; set; } = 10;
        public bool StretchItems { get; set; } = true;

        internal HorizontalContainer(PicoGui gui, Container parent) : base(gui, parent)
        {
        }

        protected override void DoLayout()
        {
            int itemWidth = 0;

            if (StretchItems)
            {
                itemWidth = (this.W - 2 * Padding - (_children.Count - 1) * ItemSpacing) / _children.Count;
            }

            for (int i = 0; i < _children.Count; i++)
            {
                var widget = _children[i];

                if (!StretchItems)
                {
                    itemWidth = widget.W;
                }

                widget.X = this.X + (i * itemWidth) + (i * ItemSpacing) + Padding;
                widget.Y = this.Y + this.Padding;

                widget.W = itemWidth;

                if (StretchItems || widget.IsContainer)
                {
                    widget.H = this.H - 2 * Padding;
                }
            }
            
        }
    }

    public class Button : Widget
    {
        public static Size DefaultSize => new Size(100, 30);

        public event EventHandler OnClicked;

        public string Label { get; set; } = "Click Me";

        internal override void Update(UIMouseState mouseState)
        {
            if (this.ContainsPoint(mouseState.MouseX, mouseState.MouseY))
            {
                this.Hovered = true;

                if (mouseState.MouseLeftDown && !this.Active)
                {
                    this.Active = true;
                    Invalidate();
                }
                else if (!mouseState.MouseLeftDown && this.Active)
                {
                    this.Active = false;
                    OnClicked?.Invoke(this, EventArgs.Empty);
                    Invalidate();
                }
            }
            else
            {
                if (this.Hovered)
                {
                    if (this.Active)
                    {
                        this.Active = false;
                        Invalidate();
                    }
                    
                    this.Hovered = false;

                    
                }
            }
        }

        internal override void Draw(PicoGfx gfx, PicoGuiTheme theme)
        {
            theme.DrawButton(gfx, this);
        }

        internal Button(PicoGui gui, Container parent) : base(gui, parent)
        {
            W = DefaultSize.Width;
            H = DefaultSize.Height;
        }
    }

    public class CheckBox : Widget
    {
        public static Size DefaultSize => new Size(20, 20);

        public event EventHandler OnChecked;
        public event EventHandler OnUnChecked;

        public bool Checked
        {
            get { return _checked; }
            set
            {
                _checked = value;
                Invalidate();
            }
        }

        private bool _checked;

        internal CheckBox(PicoGui gui, Container parent) : base(gui, parent)
        {
            W = DefaultSize.Width;
            H = DefaultSize.Height;
        }

        internal override void Update(UIMouseState mouseState)
        {
            if (this.ContainsPoint(mouseState.MouseX, mouseState.MouseY))
            {
                this.Hovered = true;

                if (!_checked)
                {
                    if (mouseState.MouseLeftDown && !this.Active)
                    {
                        this.Active = true;
                        Invalidate();
                    }
                    else if (!mouseState.MouseLeftDown && this.Active)
                    {
                        this._checked = true;
                        OnChecked?.Invoke(this, EventArgs.Empty);
                        this.Active = false;
                        Invalidate();
                    }
                }
                else
                {
                    if (mouseState.MouseLeftDown && !this.Active)
                    {
                        this.Active = true;
                        Invalidate();
                    }
                    else if (!mouseState.MouseLeftDown && this.Active)
                    {
                        this._checked = false;
                        OnUnChecked?.Invoke(this, EventArgs.Empty);
                        this.Active = false;
                        Invalidate();
                    }
                }

            }
            else
            {
                if (Active)
                {
                    this._checked = !this._checked;
                    this.Active = false;
                    Invalidate();
                }
            }
        }

        internal override void Draw(PicoGfx gfx, PicoGuiTheme theme)
        {
            theme.DrawCheckBox(gfx, this);
        }
    }
        
    public class Panel : Container
    {
        public static Size DefaultSize => new Size(200, 200);

        internal override void Draw(PicoGfx gfx, PicoGuiTheme theme)
        {
            theme.DrawPanel(gfx, this);

            base.Draw(gfx, theme);
            
        }

        internal Panel(PicoGui gui, Container parent) : base(gui, parent)
        {
            W = DefaultSize.Width;
            H = DefaultSize.Height;
        }
    }

    public class Slider : Widget
    {
        public static Size DefaultSize => new Size(200, 30);

        public int Value
        {
            get { return _value; }
            set
            {
                _value = Calculate.Clamp(value, _minValue, _maxValue);
                Invalidate();
            }
        }

        public int MinValue
        {
            get { return _minValue; }
            set
            {
                _minValue = value;

                if (_minValue > _maxValue)
                {
                    _minValue = _maxValue;
                }

                if (_value < _minValue)
                {
                    _value = _minValue;
                }

                Invalidate();
            }
        }

        public int MaxValue
        {
            get { return _maxValue; }
            set
            {
                _maxValue = value;

                if (_maxValue < _minValue)
                {
                    _maxValue = _minValue;
                }

                if (_value > _maxValue)
                {
                    _value = _maxValue;
                }

                Invalidate();
            }
        }

        public int Step
        {
            get { return _step; }
            set
            {
                _step = value;

                if (_step <= 1)
                {
                    _step = 1;
                }

                if (_step > _maxValue - _minValue)
                {
                    _step = _maxValue - _minValue;
                }

                Invalidate();
            }
        }

        public Orientation Orientation
        {
            get { return _orientation; }
            set
            {
                _orientation = value;

                if (_orientation == Orientation.Vertical)
                {
                    int temp = W;
                    W = H;
                    H = temp;
                }

                Invalidate();
            }
        }

        public event EventHandler<int> OnValueChanged; 

        private int _value;
        private int _minValue;
        private int _maxValue;
        private int _step;
        private Orientation _orientation;
        private bool _sliding;

        internal override void Update(UIMouseState mouseState)
        {
            if (this.ContainsPoint(mouseState.MouseX, mouseState.MouseY))
            {
                if (mouseState.MouseLeftDown)
                {
                    if (!_sliding)
                    {
                        _sliding = true;
                        UpdateIndicator(mouseState.MouseX, mouseState.MouseY);
                    }
                }
            }

            if (!mouseState.MouseLeftDown && _sliding)
            {
                _sliding = false;
            }

            if (_sliding && mouseState.Moved)
            {
                UpdateIndicator(mouseState.MouseX, mouseState.MouseY);

            }

        }

        private void UpdateIndicator(int x, int y)
        {
            // Indicator area is offset by 2 pixels of origin , so offset x and y position by minus 2
            var factor = 
                _orientation == Orientation.Horizontal ? 
                Calculate.Clamp((float) ((x-2) - GlobalX) / W, 0.0f, 1.0f) : 
                Calculate.Clamp((float)((y-2) - GlobalY) / H, 0.0f, 1.0f);

            _value = (int)(((_maxValue - _minValue) * factor + _minValue) / _step) * _step;
            OnValueChanged?.Invoke(this, _value);

            Invalidate();

        }

        internal override void Draw(PicoGfx gfx, PicoGuiTheme theme)
        {
            theme.DrawSlider(gfx, this);
        }

        internal Slider(PicoGui gui, Container parent, int value, int minValue, int maxValue, int step, Orientation orientation) : base(gui, parent)
        {
            W = DefaultSize.Width;
            H = DefaultSize.Height;

            this._minValue = minValue;
            this._maxValue = maxValue;

            if (this._minValue > this._maxValue)
            {
                var temp = this._maxValue;
                this._maxValue = this._minValue;
                this._minValue = temp;
            }

            this._value = Calculate.Clamp(value, this._minValue, this._maxValue);
            this._step = Calculate.Clamp(step, 1, _maxValue);
            this._orientation = orientation;

            if (_orientation == Orientation.Vertical)
            {
                int temp = W;
                W = H;
                H = temp;
            }
        }
    }

    /* ============================================================== */

    public abstract class PicoGuiTheme
    {
        public abstract void DrawButton(PicoGfx gfx, Button button);
        public abstract void DrawPanel(PicoGfx gfx, Panel panel);
        public abstract void DrawSlider(PicoGfx gfx, Slider slider);
        public abstract void DrawCheckBox(PicoGfx gfx, CheckBox checkbox);
    }

    public class DefaultTheme : PicoGuiTheme
    {
        private Font _font;

        public Color FrameOuterBorderColor = new Color(17, 27, 73);

        public Color FrameInnerBorderColor = new Color(109, 136, 255);

        public Color FrameFillColor = new Color(75, 94, 175);

        public Color ButtonFillColor = new Color(75, 94, 175);

        public Color ButtonActiveFillColor = new Color(55, 74, 155);

        public Color SliderFillColor = new Color(55, 74, 155);

        public Color CheckboxFillColor = new Color(41, 173, 255);

        private void DrawFrame(PicoGfx gfx, int x, int y, int w, int h, ref Color outerBorderColor, ref Color innerBorderColor, ref Color fillColor, bool drawShadow)
        {
            gfx.SetColor(ref outerBorderColor);
            gfx.DrawRect(x, y, w, h);

            gfx.SetColor(ref innerBorderColor);
            gfx.DrawRect(x+1 , y+1 , w-2 , h-2 );

            gfx.SetColor(ref fillColor);
            gfx.FillRect(x + 2, y + 2, w - 4, h - 4);
            if (drawShadow)
            {
                gfx.SetColor(ref outerBorderColor);
                gfx.DrawHLine(x, x+w-1, y+h);
            }
        }

        public override void DrawButton(PicoGfx gfx, Button button)
        {
            var x = (button.GlobalX);
            var y = (button.GlobalY);
            var w = button.W;
            var h = button.H;

            var textSize = new Size(

                button.Label.Length * 8,
                8
            );

            var labelPosX = (x) + (w / 2 - textSize.Width / 2);
            var labelPosY = (y+2) + (h / 2 - textSize.Height / 2);

            if (!button.Active)
            {
                DrawFrame(
                    gfx, 
                    x, y, 
                    w, h, 
                    ref FrameOuterBorderColor, 
                    ref FrameInnerBorderColor, 
                    ref ButtonFillColor, 
                    drawShadow: true);

                
                gfx.DrawText(labelPosX, labelPosY, _font, button.Label);
            }
            else
            {
                DrawFrame(
                    gfx,
                    x, y+1,
                    w, h,
                    ref FrameOuterBorderColor,
                    ref FrameInnerBorderColor,
                    ref ButtonActiveFillColor,
                    drawShadow: false);

                gfx.DrawText(labelPosX, labelPosY+1, _font, button.Label);
            }
            
        }

        public override void DrawPanel(PicoGfx gfx, Panel panel)
        {
            var x = panel.GlobalX;
            var y = panel.GlobalY;
            var w = panel.W;
            var h = panel.H;

            DrawFrame(
                gfx,
                x, y,
                w, h,
                ref FrameOuterBorderColor,
                ref FrameInnerBorderColor,
                ref FrameFillColor,
                drawShadow: true);

        }

        public override void DrawSlider(PicoGfx gfx, Slider slider)
        {
            int x = slider.GlobalX;
            int y = slider.GlobalY;
            int w = slider.W;
            int h = slider.H;

            //var textSize = canvas.MeasureText(slider.Value.ToString(), _font);

            //var labelPosX = (x) + (w / 2 - textSize.Width / 2);
            //var labelPosY = (y + 2) + (h / 2 - textSize.Height / 2);

            float valueFactor = (float)(slider.Value) / (slider.MaxValue - slider.MinValue);

            if (slider.Orientation == Orientation.Horizontal)
            {
                DrawFrame(
                    gfx,
                    x, y,
                    w, h,
                    ref FrameOuterBorderColor,
                    ref FrameInnerBorderColor,
                    ref FrameFillColor,
                    drawShadow: true);


                int indicatorSize = (int)(valueFactor * w);

                indicatorSize = Calculate.Clamp(indicatorSize, 0, w - 4);

                gfx.SetColor(ref SliderFillColor);
                gfx.FillRect(x+2,y+2, indicatorSize, h-4);

                //canvas.DrawText($"{slider.Value}", _font, labelPosX, labelPosY, 2);

            }
            else
            {
                DrawFrame(
                    gfx,
                    x, y,
                    w, h,
                    ref FrameOuterBorderColor,
                    ref FrameInnerBorderColor,
                    ref FrameFillColor,
                    drawShadow: true);

                int indicatorSize = (int)(((float)(slider.Value) / (slider.MaxValue - slider.MinValue)) * h);

                indicatorSize = Calculate.Clamp(indicatorSize, 0, h - 4);

                gfx.SetColor(ref SliderFillColor);
                gfx.FillRect(x+2, y+2, w - 3, indicatorSize);
            }
        }

        public override void DrawCheckBox(PicoGfx gfx, CheckBox checkbox)
        {
            var x = checkbox.GlobalX;
            var y = checkbox.GlobalY;
            var w = checkbox.W;
            var h = checkbox.H;

            if (!checkbox.Active)
            {

                Color fillColor = !checkbox.Checked ? FrameFillColor : CheckboxFillColor;

                DrawFrame(
                    gfx,
                    x, y,
                    w, h,
                    ref FrameOuterBorderColor,
                    ref FrameInnerBorderColor,
                    ref fillColor,
                    drawShadow: true);

            }
            else
            {
                DrawFrame(
                    gfx,
                    x, y+1,
                    w, h,
                    ref FrameOuterBorderColor,
                    ref FrameInnerBorderColor,
                    ref CheckboxFillColor,
                    drawShadow: false);
            }

        }
    }

    /* ============================================================== */

    public class PicoGui : Resource
    {
        public static PicoGui Instance { get; internal set; }

        public Container Root => _root;

        private readonly UIMouseState _uiMouseState;
        private readonly PicoGuiTheme _theme;
        private readonly Container _root;
        
        private bool _invalidated = true;

        private int _zoom;

        private readonly PicoSurface _guiSurface;

        internal PicoGui(PicoGfx gfx, int width, int height)
        {
            _guiSurface = new PicoSurface(gfx, width, height, PicoSurface.AccessType.Target);

            _uiMouseState = new UIMouseState();

            _theme = new DefaultTheme();

            _root = new Container(this, width, height);

            PicoGui.Instance = this;
        }

        public void Resize(int width, int height)
        {
            _root.W = width;
            _root.H = height;

            _invalidated = true;
        }

        public void Update(PicoInput input)
        {
            _uiMouseState.UpdatePosition(input.MouseX, input.MouseY);

            var leftDown = input.MouseDown(MouseButton.Left);
            var middleDown = input.MouseDown(MouseButton.Middle);
            var rightDown = input.MouseDown(MouseButton.Right);

            _uiMouseState.MouseLeftDown = leftDown;
            _uiMouseState.MouseMiddleDown = middleDown;
            _uiMouseState.MouseRightDown = rightDown;

            _root.Update(_uiMouseState);

        }

        public void Render(PicoGfx gfx)
        {
            if (_invalidated)
            {
                gfx.BeginSurface(_guiSurface);

                _root.Draw(gfx, _theme);

                gfx.EndSurface();

                _invalidated = false;
            }
            
            gfx.DrawSurface(_guiSurface, 0, 0);
            
        }

        public void Invalidate()
        {
            _invalidated = true;
        } 

        public override void Dispose()
        {
        }
    }
}
