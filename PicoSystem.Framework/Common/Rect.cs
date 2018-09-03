using System;
using System.Runtime.InteropServices;

namespace PicoSystem.Framework.Common
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Rect : IEquatable<Rect>
    {
        public static readonly Rect Empty = new Rect(Point.Empty, Point.Empty);

        public int Left;

        public int Top;

        public int Right;

        public int Bottom;


        public bool IsEmpty => Width == 0 && Height == 0;

        /// <summary>
        ///     Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        public int Width
        {
            get { return Right - Left; }
            set { Right = Left + value; }
        }

        /// <summary>
        ///     Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        public int Height
        {
            get { return Bottom - Top; }
            set { Bottom = Top + value; }
        }

        /// <summary>
        ///     Gets or sets the size of rectangle.
        /// </summary>
        /// <value>The size of rectangle.</value>
        public Point Size
        {
            get { return new Point(Width, Height); }
            set
            {
                Width = value.X;
                Height = value.Y;
            }
        }

        /// <summary>
        ///     Gets or sets the width half.
        /// </summary>
        /// <value>The half of width .</value>
        public int HalfWidth
        {
            get { return Width/2; }
            set { Width = value*2; }
        }

        /// <summary>
        ///     Gets or sets the half of height .
        /// </summary>
        /// <value>The half of height.</value>
        public int HalfHeight
        {
            get { return Height/2; }
            set { Height = value*2; }
        }

        /// <summary>
        ///     Gets or sets the half of size of rectangle.
        /// </summary>
        /// <value>The half of size of rectangle.</value>
        public Point HalfSize
        {
            get { return new Point(HalfWidth, HalfHeight); }
            set
            {
                HalfWidth = value.X;
                HalfHeight = value.Y;
            }
        }

        /// <summary>
        ///     Gets or sets the center X.
        /// </summary>
        /// <value>The center X.</value>
        public int CenterX
        {
            get { return (Left + Right)/2; }
            set
            {
                var delta = value - CenterX;
                Left += delta;
                Right += delta;
            }
        }

        /// <summary>
        ///     Gets or sets the center Y.
        /// </summary>
        /// <value>The center Y.</value>
        public int CenterY
        {
            get { return (Top + Bottom)/2; }
            set
            {
                var delta = value - CenterY;
                Top += delta;
                Bottom += delta;
            }
        }

        /// <summary>
        ///     Gets or sets the center of rectangle.
        /// </summary>
        /// <value>The center of rectangle.</value>
        public Point Center
        {
            get { return new Point(CenterX, CenterY); }
            set
            {
                CenterX = value.X;
                CenterY = value.Y;
            }
        }

        /// <summary>
        ///     Gets or sets the left top point.
        /// </summary>
        /// <value>The left top point.</value>
        public Point TopLeft
        {
            get { return new Point(Left, Top); }
            set
            {
                Left = value.X;
                Top = value.Y;
            }
        }

        /// <summary>
        ///     Gets or sets the right top  point.
        /// </summary>
        /// <value>The right top point.</value>
        public Point TopRight
        {
            get { return new Point(Right, Top); }
            set
            {
                Right = value.X;
                Top = value.Y;
            }
        }

        /// <summary>
        ///     Gets or sets the right bottom point.
        /// </summary>
        /// <value>The right bottom point.</value>
        public Point BottomRight
        {
            get { return new Point(Right, Bottom); }
            set
            {
                Right = value.X;
                Bottom = value.Y;
            }
        }

        /// <summary>
        ///     Gets or sets the left bottom point.
        /// </summary>
        /// <value>The left bottom point.</value>
        public Point BottomLeft
        {
            get { return new Point(Left, Bottom); }
            set
            {
                Left = value.X;
                Bottom = value.Y;
            }
        }

        public Point BottomCenter => new Point(CenterX, Bottom);

        public Point LeftCenter => new Point(Left, CenterY);

        public Point RightCenter => new Point(Right, CenterY);

        public Point TopCenter => new Point(CenterX, Top);

        /// <summary>
        ///     Gets a value indicating whether this instance is normalized (Left less or equal Right and Top less or equal
        ///     Bottom).
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is normalized; otherwise, <c>false</c>.
        /// </value>
        public bool IsNormalized => Left <= Right && Top <= Bottom;

        public Rect(int left, int top, int right, int bottom)
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
        }

        public Rect(Point leftTop, Point rightBottom)
        {
            this.Left = leftTop.X;
            this.Top = leftTop.Y;
            this.Right = rightBottom.X;
            this.Bottom = rightBottom.Y;
        }

        public Rect(int left, int top, Size size)
        {
            this.Left = left;
            this.Top = top;
            this.Right = Left + size.Width;
            this.Bottom = Top + size.Height;
        }

        public Rect(Point leftTop, Size size)
        {
            this.Left = leftTop.X;
            this.Top = leftTop.Y;
            this.Right = Left + size.Width;
            this.Bottom = Top + size.Height;
        }

        /// <summary>
        ///     Creates rectangle from top left point and size
        /// </summary>
        /// <param name="x">The x of left top corner.</param>
        /// <param name="y">The y of left top corner.</param>
        /// <param name="width">The width of rectangle.</param>
        /// <param name="height">The height of rectangle.</param>
        /// <returns>Initialized rectangle</returns>
        public static Rect FromBox(int x, int y, int width, int height)
        {
            return new Rect(x, y, x + width, y + height);
        }

        /// <summary>
        ///     Creates rectangle from top left point and size
        /// </summary>
        /// <param name="leftTop">The left top point.</param>
        /// <param name="size">The size.</param>
        /// <returns>Initialized rectangle</returns>
        public static Rect FromBox(Point leftTop, Size size)
        {
            return new Rect(leftTop, leftTop + size);
        }

        /// <summary>
        ///     Creates rectangle from central point and size
        /// </summary>
        /// <param name="centerX">The center X.</param>
        /// <param name="centerY">The center Y.</param>
        /// <param name="width">The width of rectangle.</param>
        /// <param name="height">The height of rectangle.</param>
        /// <returns>Initialized rectangle</returns>
        public static Rect FromPoint(int centerX, int centerY, int width, int height)
        {
            int halfX = width/2, halfY = height/2;
            return new Rect(centerX - halfX, centerY - halfY, centerX + halfX, centerY + halfY);
        }


        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///     true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(Rect other)
        {
            return Left == other.Left && Top == other.Top && Right == other.Right && Bottom == other.Bottom;
        }

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object other)
        {
            return other is Rect && Equals((Rect) other);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return Left + Top + Right + Bottom;
        }

        /// <summary>
        ///     Compares two instances of <see cref="Rect" />.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns><c>true</c> if values of type <see cref="Rect" /> are equal; otherwise, <c>false</c>.</returns>
        public static bool Equals(ref Rect value1, ref Rect value2)
        {
            return value1.Left == value2.Left && value1.Top == value2.Top && value1.Right == value2.Right &&
                   value1.Bottom == value2.Bottom;
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this rect.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.String" /> that represents this rect.
        /// </returns>
        public override string ToString()
        {
            return $"{{{Left},{Top},{Right},{Bottom}}}";
        }


        public Rect Add(Rect value)
        {
            Add(ref this, ref value, out value);
            return value;
        }

        public static Rect Add(Rect value1, Rect value2)
        {
            Add(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Add(ref Rect value1, ref Rect value2, out Rect result)
        {
            result.Left = value1.Left + value2.Left;
            result.Top = value1.Top + value2.Top;
            result.Right = value1.Right + value2.Right;
            result.Bottom = value1.Bottom + value2.Bottom;
        }

        public Rect Add(Point value)
        {
            Rect result;
            Add(ref this, ref value, out result);
            return result;
        }

        public static Rect Add(Rect value1, Point value2)
        {
            Add(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Add(ref Rect value1, ref Point value2, out Rect result)
        {
            result.Left = value1.Left + value2.X;
            result.Top = value1.Top + value2.Y;
            result.Right = value1.Right + value2.X;
            result.Bottom = value1.Bottom + value2.Y;
        }

        public Rect Subtract(Rect value)
        {
            Subtract(ref this, ref value, out value);
            return value;
        }

        public static Rect Subtract(Rect value1, Rect value2)
        {
            Subtract(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Subtract(ref Rect value1, ref Rect value2, out Rect result)
        {
            result.Left = value1.Left - value2.Left;
            result.Top = value1.Top - value2.Top;
            result.Right = value1.Right - value2.Right;
            result.Bottom = value1.Bottom - value2.Bottom;
        }

        public Rect Subtract(Point value)
        {
            Rect result;
            Subtract(ref this, ref value, out result);
            return result;
        }

        public static Rect Subtract(Rect value1, Point value2)
        {
            Subtract(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Subtract(ref Rect value1, ref Point value2, out Rect result)
        {
            result.Left = value1.Left - value2.X;
            result.Top = value1.Top - value2.Y;
            result.Right = value1.Right - value2.X;
            result.Bottom = value1.Bottom - value2.Y;
        }

        public Rect Multiply(Rect value)
        {
            Multiply(ref this, ref value, out value);
            return value;
        }

        public static Rect Multiply(Rect value1, Rect value2)
        {
            Multiply(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Multiply(ref Rect value1, ref Rect value2, out Rect result)
        {
            result.Left = value1.Left*value2.Left;
            result.Top = value1.Top*value2.Top;
            result.Right = value1.Right*value2.Right;
            result.Bottom = value1.Bottom*value2.Bottom;
        }

        public Rect Multiply(Point value)
        {
            Rect result;
            Multiply(ref this, ref value, out result);
            return result;
        }

        public static Rect Multiply(Rect value1, Point value2)
        {
            Multiply(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Multiply(ref Rect value1, ref Point value2, out Rect result)
        {
            result.Left = value1.Left*value2.X;
            result.Top = value1.Top*value2.Y;
            result.Right = value1.Right*value2.X;
            result.Bottom = value1.Bottom*value2.Y;
        }

        public static void Multiply(ref Rect value1, int value2, out Rect result)
        {
            result.Left = value1.Left*value2;
            result.Top = value1.Top*value2;
            result.Right = value1.Right*value2;
            result.Bottom = value1.Bottom*value2;
        }

        public Rect Divide(Rect value)
        {
            Divide(ref this, ref value, out value);
            return value;
        }

        public static Rect Divide(Rect value1, Rect value2)
        {
            Divide(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Divide(ref Rect value1, ref Rect value2, out Rect result)
        {
            result.Left = value1.Left/value2.Left;
            result.Top = value1.Top/value2.Top;
            result.Right = value1.Right/value2.Right;
            result.Bottom = value1.Bottom/value2.Bottom;
        }

        public Rect Divide(Point value)
        {
            Rect result;
            Divide(ref this, ref value, out result);
            return result;
        }

        public static Rect Divide(Rect value1, Point value2)
        {
            Divide(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Divide(ref Rect value1, ref Point value2, out Rect result)
        {
            result.Left = value1.Left/value2.X;
            result.Top = value1.Top/value2.Y;
            result.Right = value1.Right/value2.X;
            result.Bottom = value1.Bottom/value2.Y;
        }


        public static void Divide(ref Rect value1, int value2, out Rect result)
        {
            result.Left = value1.Left/value2;
            result.Top = value1.Top/value2;
            result.Right = value1.Right/value2;
            result.Bottom = value1.Bottom/value2;
        }

        public Rect Inflate(Rect value)
        {
            Inflate(ref this, ref value, out value);
            return value;
        }

        public static Rect Inflate(Rect value1, Rect value2)
        {
            Inflate(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Inflate(ref Rect value1, ref Rect value2, out Rect result)
        {
            result.Left = value1.Left - value2.Left;
            result.Top = value1.Top - value2.Top;
            result.Right = value1.Right + value2.Right;
            result.Bottom = value1.Bottom + value2.Bottom;
        }

        public Rect Inflate(int inflateX, int inflateY)
        {
            return Inflate(new Point(inflateX, inflateY));
        }

        public Rect Inflate(Point value)
        {
            Rect result;
            Inflate(ref this, ref value, out result);
            return result;
        }

        public static Rect Inflate(Rect value1, Point value2)
        {
            Inflate(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Inflate(ref Rect value1, ref Point value2, out Rect result)
        {
            result.Left = value1.Left - value2.X;
            result.Top = value1.Top - value2.Y;
            result.Right = value1.Right + value2.X;
            result.Bottom = value1.Bottom + value2.Y;
        }

        public Rect Deflate(Rect value)
        {
            Deflate(ref this, ref value, out value);
            return value;
        }

        public static Rect Deflate(Rect value1, Rect value2)
        {
            Deflate(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Deflate(ref Rect value1, ref Rect value2, out Rect result)
        {
            result.Left = value1.Left + value2.Left;
            result.Top = value1.Top + value2.Top;
            result.Right = value1.Right - value2.Right;
            result.Bottom = value1.Bottom - value2.Bottom;
        }

        public Rect Deflate(int deflateX, int deflateY)
        {
            return Deflate(new Point(deflateX, deflateY));
        }

        public Rect Deflate(Point value)
        {
            Rect result;
            Deflate(ref this, ref value, out result);
            return result;
        }

        public static Rect Deflate(Rect value1, Point value2)
        {
            Deflate(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Deflate(ref Rect value1, ref Point value2, out Rect result)
        {
            result.Left = value1.Left + value2.X;
            result.Top = value1.Top + value2.Y;
            result.Right = value1.Right - value2.X;
            result.Bottom = value1.Bottom - value2.Y;
        }

        /// <summary>
        ///     Flips rectangle horizontally.
        /// </summary>
        /// <returns>Flipped rectangle</returns>
        public Rect FlipHorizontal()
        {
            return new Rect(Right, Top, Left, Bottom);
        }

        /// <summary>
        ///     Flips rectangle vertically.
        /// </summary>
        /// <returns>Flipped rectangle</returns>
        public Rect FlipVertical()
        {
            return new Rect(Left, Bottom, Right, Top);
        }

        /// <summary>
        ///     Flips rectangle horizontally.
        /// </summary>
        /// <param name="source">The source rect.</param>
        /// <param name="result">The flipped rect.</param>
        public static void FlipHorizontal(ref Rect source, out Rect result)
        {
            result.Left = source.Right;
            result.Top = source.Top;
            result.Right = source.Left;
            result.Bottom = source.Bottom;
        }

        /// <summary>
        ///     Flips rectangle vertically.
        /// </summary>
        /// <param name="source">The source rect.</param>
        /// <param name="result">The flipped rect.</param>
        public static void FlipVertical(ref Rect source, out Rect result)
        {
            result.Left = source.Left;
            result.Top = source.Bottom;
            result.Right = source.Right;
            result.Bottom = source.Top;
        }

        /// <summary>
        ///     Normalizes rectangle. Ensures top left coordinate is top left point
        /// </summary>
        /// <returns>Normalized copy of rectangle</returns>
        public Rect Normalize()
        {
            return new Rect(
                Math.Min(Left, Right),
                Math.Min(Top, Bottom),
                Math.Max(Left, Right),
                Math.Max(Top, Bottom)
                );
        }

        public bool Contains(int pointX, int pointY)
        {
            return this.Left <= pointX && this.Top <= pointY && this.Right > pointX && this.Bottom > pointY;
        }

        public bool Contains(Point point)
        {
            return this.Contains(point.X, point.Y);
        }

        public bool Contains(Rect rect)
        {
            return
                this.Left <= rect.Left &&
                this.Top <= rect.Top &&
                this.Right >= rect.Right &&
                this.Bottom >= rect.Bottom;
        }

        public bool Intersects(Rect rect)
        {
            return this.Left <= rect.Right &&
                   this.Top <= rect.Bottom &&
                   this.Right >= rect.Left &&
                   this.Bottom >= rect.Top;
        }

        public static bool operator ==(Rect value1, Rect value2)
        {
            return Equals(ref value1, ref value2);
        }

        public static bool operator !=(Rect value1, Rect value2)
        {
            return !Equals(ref value1, ref value2);
        }

        public static Rect operator +(Rect value1, Rect value2)
        {
            Add(ref value1, ref value2, out value1);
            return value1;
        }

        public static Rect operator +(Rect value1, Point value2)
        {
            Add(ref value1, ref value2, out value1);
            return value1;
        }

        public static Rect operator +(Rect value1, int value2)
        {
            value1.Left += value2;
            value1.Top += value2;
            value1.Right += value2;
            value1.Bottom += value2;
            return value1;
        }

        public static Rect operator -(Rect value1, Rect value2)
        {
            Subtract(ref value1, ref value2, out value1);
            return value1;
        }

        public static Rect operator -(Rect value1, Point value2)
        {
            Subtract(ref value1, ref value2, out value1);
            return value1;
        }

        public static Rect operator -(Rect value1, int value2)
        {
            value1.Left -= value2;
            value1.Top -= value2;
            value1.Right -= value2;
            value1.Bottom -= value2;
            return value1;
        }

        public static Rect operator *(Rect value1, Rect value2)
        {
            Multiply(ref value1, ref value2, out value1);
            return value1;
        }

        public static Rect operator *(Rect value1, Point value2)
        {
            Multiply(ref value1, ref value2, out value1);
            return value1;
        }

        public static Rect operator *(Rect value1, int value2)
        {
            Multiply(ref value1, value2, out value1);
            return value1;
        }

        public static Rect operator *(Point value1, Rect value2)
        {
            Multiply(ref value2, ref value1, out value2);
            return value2;
        }

        public static Rect operator *(int value1, Rect value2)
        {
            Multiply(ref value2, value1, out value2);
            return value2;
        }

        public static Rect operator /(Rect value1, Rect value2)
        {
            Divide(ref value1, ref value2, out value1);
            return value1;
        }

        public static Rect operator /(Rect value1, Point value2)
        {
            Divide(ref value1, ref value2, out value1);
            return value1;
        }

        public static Rect operator /(Rect value1, int value2)
        {
            Divide(ref value1, value2, out value1);
            return value1;
        }
    }
}