using Microsoft.Xna.Framework;
using System;

namespace Floppy.Utilities {
    /// <summary>
    /// Describes an axis-aligned rectangle with float components.
    /// </summary>
    [Serializable]
    public struct RectangleF : IEquatable<RectangleF> {
        /// <summary>
        /// Creates a new rectangle.
        /// </summary>
        /// <param name="x">The x-coordinate of the left side of the rectangle.</param>
        /// <param name="y">The y-coordinate of the top side of the rectangle.</param>
        /// <param name="width">The rectangle's width.</param>
        /// <param name="height">The rectangle's height.</param>
        public RectangleF(float x, float y, float width, float height) {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Creates a new rectangle from a position and size.
        /// </summary>
        /// <param name="position">The top-left corner of the rectangle.</param>
        /// <param name="size">The rectangle's size.</param>
        public RectangleF(Vector2 position, Vector2 size) {
            X = position.X;
            Y = position.Y;
            Width = size.X;
            Height = size.Y;
        }

        /// <summary>
        /// A rectangle at (0, 0) with zero width and zero height.
        /// </summary>
        public static RectangleF Empty { get; } = new RectangleF(0f, 0f, 0f, 0f);

        /// <summary>
        /// A rectangle at (0, 0) with a width and height of one.
        /// </summary>
        public static RectangleF One { get; } = new RectangleF(0f, 0f, 1f, 1f);

        /// <summary>
        /// The x-coordinate of the rectangle.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// The y-coordinate of the rectangle.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// The width of the rectangle.
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// The height of the rectangle.
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        /// The x-coordinate and y-coordinate of the rectangle in a vector.
        /// </summary>
        public Vector2 Position {
            get => new Vector2(X, Y);
            set {
                X = value.X;
                Y = value.Y;
            }
        }

        /// <summary>
        /// The width and height of the rectangle in a vector.
        /// </summary>
        public Vector2 Size { 
            get => new Vector2(Width, Height);
            set {
                Width = value.X;
                Height = value.Y;
            }
        }

        /// <summary>
        /// The center of the rectangle.
        /// </summary>
        public Vector2 Center {
            get => new Vector2(X + Width / 2f, Y + Height / 2f);
            set {
                X = value.X - Width / 2f;
                Y = value.Y - Height / 2f;
            }
        }

        /// <summary>
        /// The x-coordinate of the left side of the rectangle.
        /// </summary>
        public float Left => X;

        /// <summary>
        /// The y-coordinate of the top side of the rectangle.
        /// </summary>
        public float Top => Y;

        /// <summary>
        /// The x-coordinate of the right side of the rectangle.
        /// </summary>
        public float Right => X + Width;

        /// <summary>
        /// The y-coordinate of the bottom side of the rectangle.
        /// </summary>
        public float Bottom => Y + Height;

        /// <summary>
        /// Creates a rectangle from the center rather than the top-left corner.
        /// </summary>
        /// <param name="center">The center of the rectangle.</param>
        /// <param name="width">The rectangle's width.</param>
        /// <param name="height">The rectangle's height.</param>
        /// <returns>The new rectangle.</returns>
        public static RectangleF FromCenter(Vector2 center, float width, float height) {
            return new RectangleF(center.X - width / 2f, center.Y - height / 2f, width, height);
        }

        /// <summary>
        /// Checks if a point is contained within the rectangle.
        /// </summary>
        /// <param name="vector">The point to check.</param>
        /// <returns>If the point is contained within the rectangle.</returns>
        public bool Contains(Vector2 vector) {
            return vector.X >= X && vector.X <= X + Width 
                && vector.Y >= Y && vector.Y <= Y + Height;
        }

        /// <summary>
        /// Checks if the rectangle intersects another rectangle.
        /// </summary>
        /// <param name="other">The other rectangle to check.</param>
        /// <returns>If the rectangles intersect.</returns>
        public bool Intersects(RectangleF other) {
            return X < other.X + other.Width && X + Width > other.X
                && Y < other.Y + other.Height && Y + Height > other.Y;
        }

        /// <summary>
        /// Offsets the rectangle's x-component and y-component.
        /// </summary>
        /// <param name="amount">A vector containing the offsets.</param>
        /// <returns>The rectangle with the offset applied.</returns>
        public RectangleF Offset(Vector2 amount) {
            return new RectangleF(X + amount.X, Y + amount.Y, Width, Height);
        }

        /// <summary>
        /// Multiplies the components of the rectangle with another rectangle's. Can be used to get a sub-rectangle
        /// from a parent rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle to multiply components with.</param>
        /// <returns>The resulting rectangle.</returns>
        public Rectangle Multiply(Rectangle rectangle) {
            return new Rectangle((int)(X * rectangle.X), (int)(Y * rectangle.Y),
                (int)(Width * rectangle.Width), (int)(Height * rectangle.Height));
        }

        /// <summary>
        /// Extends the rectangle. Positive vectors will increase the width and height, while negative vectors
        /// will still increase the width and height but also decrease the x-coordinate and y-coordinate.
        /// </summary>
        /// <param name="amount">The vector containing the extension values.</param>
        /// <returns>The extended rectangle.</returns>
        public RectangleF Extend(Vector2 amount) {
            RectangleF result = this;

            if (amount.X < 0f) {
                result.X += amount.X;
                result.Width -= amount.X;
            }
            else {
                result.Width += amount.X;
            }

            if (amount.Y < 0f) {
                result.Y += amount.Y;
                result.Height -= amount.Y;
            }
            else {
                result.Height += amount.Y;
            }

            return result;
        }

        /// <summary>
        /// Shrinks the rectangle on all sides by a fixed amount.
        /// </summary>
        /// <param name="amount">The amount to shrink the rectangle by.</param>
        /// <returns>The shrunked rectangle.</returns>
        public RectangleF Shrink(float amount) {
            return new RectangleF(X + amount, Y + amount,
                Width - amount * 2f, Height - amount * 2f);
        }

        public override bool Equals(object? obj) {
            return obj is RectangleF && Equals((RectangleF)obj);
        }

        public bool Equals(RectangleF other) {
            return X == other.X 
                && Y == other.Y 
                && Width == other.Width 
                && Height == other.Height;
        }

        public override int GetHashCode() {
            return HashCode.Combine(X, Y, Width, Height);
        }

        public static bool operator ==(RectangleF f1, RectangleF f2) {
            return f1.Equals(f2);
        }

        public static bool operator !=(RectangleF f1, RectangleF f2) {
            return !(f1 == f2);
        }
    }
}
