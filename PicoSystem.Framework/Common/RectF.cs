using System;
using System.Runtime.InteropServices;

namespace PicoSystem.Framework.Common
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RectF : IEquatable<RectF>
    {
        public static readonly RectF Empty = new RectF(Vector2.Zero, Vector2.Zero);

        public float Left;

        public float Top;

        public float Right;

        public float Bottom;


        public bool IsEmpty => Width == 0 && Height == 0;

        public bool IsIdentity => Left == 0 && Top == 0 && Width == 1 && Height == 1;

        /// <summary>
        ///     Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        public float Width
        {
            get { return Right - Left; }
            set { Right = Left + value; }
        }

        /// <summary>
        ///     Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        public float Height
        {
            get { return Bottom - Top; }
            set { Bottom = Top + value; }
        }

        /// <summary>
        ///     Gets or sets the size of rectangle.
        /// </summary>
        /// <value>The size of rectangle.</value>
        public Vector2 Size
        {
            get { return new Vector2(Width, Height); }
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
        public float HalfWidth
        {
            get { return Width / 2; }
            set { Width = value * 2; }
        }

        /// <summary>
        ///     Gets or sets the half of height .
        /// </summary>
        /// <value>The half of height.</value>
        public float HalfHeight
        {
            get { return Height / 2; }
            set { Height = value * 2; }
        }

        /// <summary>
        ///     Gets or sets the half of size of rectangle.
        /// </summary>
        /// <value>The half of size of rectangle.</value>
        public Vector2 HalfSize
        {
            get { return new Vector2(HalfWidth, HalfHeight); }
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
        public float CenterX
        {
            get { return (Left + Right) / 2; }
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
        public float CenterY
        {
            get { return (Top + Bottom) / 2; }
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
        public Vector2 Center
        {
            get { return new Vector2(CenterX, CenterY); }
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
        public Vector2 TopLeft
        {
            get { return new Vector2(Left, Top); }
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
        public Vector2 TopRight
        {
            get { return new Vector2(Right, Top); }
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
        public Vector2 BottomRight
        {
            get { return new Vector2(Right, Bottom); }
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
        public Vector2 BottomLeft
        {
            get { return new Vector2(Left, Bottom); }
            set
            {
                Left = value.X;
                Bottom = value.Y;
            }
        }

        public Vector2 BottomCenter => new Vector2(CenterX, Bottom);

        public Vector2 LeftCenter => new Vector2(Left, CenterY);

        public Vector2 RightCenter => new Vector2(Right, CenterY);

        public Vector2 TopCenter => new Vector2(CenterX, Top);

        /// <summary>
        ///     Gets a value indicating whether this instance is normalized (Left less or equal Right and Top less or equal
        ///     Bottom).
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is normalized; otherwise, <c>false</c>.
        /// </value>
        public bool IsNormalized => Left <= Right && Top <= Bottom;

        public RectF(float left, float top, float right, float bottom)
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
        }

        public RectF(Vector2 leftTop, Vector2 rightBottom)
        {
            this.Left = leftTop.X;
            this.Top = leftTop.Y;
            this.Right = rightBottom.X;
            this.Bottom = rightBottom.Y;
        }

        public RectF(float left, float top, SizeF size)
        {
            this.Left = left;
            this.Top = top;
            this.Right = Left + size.Width;
            this.Bottom = Top + size.Height;
        }

        public RectF(Vector2 leftTop, SizeF size)
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
        public static RectF FromBox(float x, float y, float width, float height)
        {
            return new RectF(x, y, x + width, y + height);
        }

        /// <summary>
        ///     Creates rectangle from top left point and size
        /// </summary>
        /// <param name="leftTop">The left top point.</param>
        /// <param name="size">The size.</param>
        /// <returns>Initialized rectangle</returns>
        public static RectF FromBox(Vector2 leftTop, SizeF size)
        {
            return new RectF(leftTop, size);
        }

        /// <summary>
        ///     Creates rectangle from central point and size
        /// </summary>
        /// <param name="centerX">The center X.</param>
        /// <param name="centerY">The center Y.</param>
        /// <param name="width">The width of rectangle.</param>
        /// <param name="height">The height of rectangle.</param>
        /// <returns>Initialized rectangle</returns>
        public static RectF FromPoint(float centerX, float centerY, float width, float height)
        {
            float halfX = width / 2, halfY = height / 2;
            return new RectF(centerX - halfX, centerY - halfY, centerX + halfX, centerY + halfY);
        }


        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///     true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(RectF other)
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
            return other is RectF && Equals((RectF)other);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return (int)(Left + Top + Right + Bottom);
        }

        /// <summary>
        ///     Compares two instances of <see cref="RectF" />.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns><c>true</c> if values of type <see cref="RectF" /> are equal; otherwise, <c>false</c>.</returns>
        public static bool Equals(ref RectF value1, ref RectF value2)
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


        public RectF Add(RectF value)
        {
            Add(ref this, ref value, out value);
            return value;
        }

        public static RectF Add(RectF value1, RectF value2)
        {
            Add(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Add(ref RectF value1, ref RectF value2, out RectF result)
        {
            result.Left = value1.Left + value2.Left;
            result.Top = value1.Top + value2.Top;
            result.Right = value1.Right + value2.Right;
            result.Bottom = value1.Bottom + value2.Bottom;
        }

        public RectF Add(Vector2 value)
        {
            RectF result;
            Add(ref this, ref value, out result);
            return result;
        }

        public static RectF Add(RectF value1, Vector2 value2)
        {
            Add(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Add(ref RectF value1, ref Vector2 value2, out RectF result)
        {
            result.Left = value1.Left + value2.X;
            result.Top = value1.Top + value2.Y;
            result.Right = value1.Right + value2.X;
            result.Bottom = value1.Bottom + value2.Y;
        }

        public RectF Subtract(RectF value)
        {
            Subtract(ref this, ref value, out value);
            return value;
        }

        public static RectF Subtract(RectF value1, RectF value2)
        {
            Subtract(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Subtract(ref RectF value1, ref RectF value2, out RectF result)
        {
            result.Left = value1.Left - value2.Left;
            result.Top = value1.Top - value2.Top;
            result.Right = value1.Right - value2.Right;
            result.Bottom = value1.Bottom - value2.Bottom;
        }

        public RectF Subtract(Vector2 value)
        {
            RectF result;
            Subtract(ref this, ref value, out result);
            return result;
        }

        public static RectF Subtract(RectF value1, Vector2 value2)
        {
            Subtract(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Subtract(ref RectF value1, ref Vector2 value2, out RectF result)
        {
            result.Left = value1.Left - value2.X;
            result.Top = value1.Top - value2.Y;
            result.Right = value1.Right - value2.X;
            result.Bottom = value1.Bottom - value2.Y;
        }

        public RectF Multiply(RectF value)
        {
            Multiply(ref this, ref value, out value);
            return value;
        }

        public static RectF Multiply(RectF value1, RectF value2)
        {
            Multiply(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Multiply(ref RectF value1, ref RectF value2, out RectF result)
        {
            result.Left = value1.Left * value2.Left;
            result.Top = value1.Top * value2.Top;
            result.Right = value1.Right * value2.Right;
            result.Bottom = value1.Bottom * value2.Bottom;
        }

        public RectF Multiply(Vector2 value)
        {
            RectF result;
            Multiply(ref this, ref value, out result);
            return result;
        }

        public static RectF Multiply(RectF value1, Vector2 value2)
        {
            Multiply(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Multiply(ref RectF value1, ref Vector2 value2, out RectF result)
        {
            result.Left = value1.Left * value2.X;
            result.Top = value1.Top * value2.Y;
            result.Right = value1.Right * value2.X;
            result.Bottom = value1.Bottom * value2.Y;
        }

        public static void Multiply(ref RectF value1, float value2, out RectF result)
        {
            result.Left = value1.Left * value2;
            result.Top = value1.Top * value2;
            result.Right = value1.Right * value2;
            result.Bottom = value1.Bottom * value2;
        }

        public RectF Divide(RectF value)
        {
            Divide(ref this, ref value, out value);
            return value;
        }

        public static RectF Divide(RectF value1, RectF value2)
        {
            Divide(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Divide(ref RectF value1, ref RectF value2, out RectF result)
        {
            result.Left = value1.Left / value2.Left;
            result.Top = value1.Top / value2.Top;
            result.Right = value1.Right / value2.Right;
            result.Bottom = value1.Bottom / value2.Bottom;
        }

        public RectF Divide(Vector2 value)
        {
            RectF result;
            Divide(ref this, ref value, out result);
            return result;
        }

        public static RectF Divide(RectF value1, Vector2 value2)
        {
            Divide(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Divide(ref RectF value1, ref Vector2 value2, out RectF result)
        {
            result.Left = value1.Left / value2.X;
            result.Top = value1.Top / value2.Y;
            result.Right = value1.Right / value2.X;
            result.Bottom = value1.Bottom / value2.Y;
        }


        public static void Divide(ref RectF value1, float value2, out RectF result)
        {
            result.Left = value1.Left / value2;
            result.Top = value1.Top / value2;
            result.Right = value1.Right / value2;
            result.Bottom = value1.Bottom / value2;
        }

        public RectF Inflate(RectF value)
        {
            Inflate(ref this, ref value, out value);
            return value;
        }

        public static RectF Inflate(RectF value1, RectF value2)
        {
            Inflate(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Inflate(ref RectF value1, ref RectF value2, out RectF result)
        {
            result.Left = value1.Left - value2.Left;
            result.Top = value1.Top - value2.Top;
            result.Right = value1.Right + value2.Right;
            result.Bottom = value1.Bottom + value2.Bottom;
        }

        public RectF Inflate(float inflateX, float inflateY)
        {
            return Inflate(new Vector2(inflateX, inflateY));
        }

        public RectF Inflate(Vector2 value)
        {
            RectF result;
            Inflate(ref this, ref value, out result);
            return result;
        }

        public static RectF Inflate(RectF value1, Vector2 value2)
        {
            Inflate(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Inflate(ref RectF value1, ref Vector2 value2, out RectF result)
        {
            result.Left = value1.Left - value2.X;
            result.Top = value1.Top - value2.Y;
            result.Right = value1.Right + value2.X;
            result.Bottom = value1.Bottom + value2.Y;
        }

        public RectF Deflate(RectF value)
        {
            Deflate(ref this, ref value, out value);
            return value;
        }

        public static RectF Deflate(RectF value1, RectF value2)
        {
            Deflate(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Deflate(ref RectF value1, ref RectF value2, out RectF result)
        {
            result.Left = value1.Left + value2.Left;
            result.Top = value1.Top + value2.Top;
            result.Right = value1.Right - value2.Right;
            result.Bottom = value1.Bottom - value2.Bottom;
        }

        public RectF Deflate(float deflateX, float deflateY)
        {
            return Deflate(new Vector2(deflateX, deflateY));
        }

        public RectF Deflate(Vector2 value)
        {
            RectF result;
            Deflate(ref this, ref value, out result);
            return result;
        }

        public static RectF Deflate(RectF value1, Vector2 value2)
        {
            Deflate(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Deflate(ref RectF value1, ref Vector2 value2, out RectF result)
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
        public RectF FlipHorizontal()
        {
            return new RectF(Right, Top, Left, Bottom);
        }

        /// <summary>
        ///     Flips rectangle vertically.
        /// </summary>
        /// <returns>Flipped rectangle</returns>
        public RectF FlipVertical()
        {
            return new RectF(Left, Bottom, Right, Top);
        }

        /// <summary>
        ///     Flips rectangle horizontally.
        /// </summary>
        /// <param name="source">The source rect.</param>
        /// <param name="result">The flipped rect.</param>
        public static void FlipHorizontal(ref RectF source, out RectF result)
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
        public static void FlipVertical(ref RectF source, out RectF result)
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
        public RectF Normalize()
        {
            return new RectF(
                Math.Min(Left, Right),
                Math.Min(Top, Bottom),
                Math.Max(Left, Right),
                Math.Max(Top, Bottom)
                );
        }

        public bool Contains(float pointX, float pointY)
        {
            return Contains(new Vector2(pointX, pointY));
        }

        public bool Contains(Vector2 point)
        {
            return this.Left <= point.X && this.Top <= point.Y && this.Right > point.X && this.Bottom > point.Y;
        }

        public bool Contains(RectF rect)
        {
            return
                this.Left <= rect.Left &&
                this.Top <= rect.Top &&
                this.Right >= rect.Right &&
                this.Bottom >= rect.Bottom;
        }

        public bool Intersects(RectF rect)
        {
            return this.Left <= rect.Right &&
                   this.Top <= rect.Bottom &&
                   this.Right >= rect.Left &&
                   this.Bottom >= rect.Top;
        }

        public static bool operator ==(RectF value1, RectF value2)
        {
            return Equals(ref value1, ref value2);
        }

        public static bool operator !=(RectF value1, RectF value2)
        {
            return !Equals(ref value1, ref value2);
        }

        public static RectF operator +(RectF value1, RectF value2)
        {
            Add(ref value1, ref value2, out value1);
            return value1;
        }

        public static RectF operator +(RectF value1, Vector2 value2)
        {
            Add(ref value1, ref value2, out value1);
            return value1;
        }

        public static RectF operator +(RectF value1, float value2)
        {
            value1.Left += value2;
            value1.Top += value2;
            value1.Right += value2;
            value1.Bottom += value2;
            return value1;
        }

        public static RectF operator -(RectF value1, RectF value2)
        {
            Subtract(ref value1, ref value2, out value1);
            return value1;
        }

        public static RectF operator -(RectF value1, Vector2 value2)
        {
            Subtract(ref value1, ref value2, out value1);
            return value1;
        }

        public static RectF operator -(RectF value1, float value2)
        {
            value1.Left -= value2;
            value1.Top -= value2;
            value1.Right -= value2;
            value1.Bottom -= value2;
            return value1;
        }

        public static RectF operator *(RectF value1, RectF value2)
        {
            Multiply(ref value1, ref value2, out value1);
            return value1;
        }

        public static RectF operator *(RectF value1, Vector2 value2)
        {
            Multiply(ref value1, ref value2, out value1);
            return value1;
        }

        public static RectF operator *(RectF value1, float value2)
        {
            Multiply(ref value1, value2, out value1);
            return value1;
        }

        public static RectF operator *(Vector2 value1, RectF value2)
        {
            Multiply(ref value2, ref value1, out value2);
            return value2;
        }

        public static RectF operator *(float value1, RectF value2)
        {
            Multiply(ref value2, value1, out value2);
            return value2;
        }

        public static RectF operator /(RectF value1, RectF value2)
        {
            Divide(ref value1, ref value2, out value1);
            return value1;
        }

        public static RectF operator /(RectF value1, Vector2 value2)
        {
            Divide(ref value1, ref value2, out value1);
            return value1;
        }

        public static RectF operator /(RectF value1, float value2)
        {
            Divide(ref value1, value2, out value1);
            return value1;
        }
    }
}
