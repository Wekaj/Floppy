using Microsoft.Xna.Framework;
using System;

namespace Floppy.Utilities {
    [Serializable]
    public struct RectangleF : IEquatable<RectangleF> {
        public RectangleF(float x, float y, float width, float height) {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public RectangleF(Vector2 position, Vector2 size) {
            X = position.X;
            Y = position.Y;
            Width = size.X;
            Height = size.Y;
        }

        public static RectangleF Empty { get; } = new RectangleF(0f, 0f, 0f, 0f);
        public static RectangleF One { get; } = new RectangleF(0f, 0f, 1f, 1f);

        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public Vector2 Position {
            get => new Vector2(X, Y);
            set {
                X = value.X;
                Y = value.Y;
            }
        }

        public Vector2 Size { 
            get => new Vector2(Width, Height);
            set {
                Width = value.X;
                Height = value.Y;
            }
        }

        public Vector2 Center {
            get => new Vector2(X + Width / 2f, Y + Height / 2f);
            set {
                X = value.X - Width / 2f;
                Y = value.Y - Height / 2f;
            }
        }

        public float Left => X;
        public float Top => Y;
        public float Right => X + Width;
        public float Bottom => Y + Height;

        public bool Contains(Vector2 vector) {
            return vector.X >= X && vector.X <= X + Width 
                && vector.Y >= Y && vector.Y <= Y + Height;
        }

        public bool Intersects(RectangleF other) {
            return X < other.X + other.Width && X + Width > other.X
                && Y < other.Y + other.Height && Y + Height > other.Y;
        }

        public RectangleF Offset(Vector2 amount) {
            return new RectangleF(X + amount.X, Y + amount.Y, Width, Height);
        }

        public Rectangle Multiply(Rectangle rectangle) {
            return new Rectangle((int)(X * rectangle.X), (int)(Y * rectangle.Y),
                (int)(Width * rectangle.Width), (int)(Height * rectangle.Height));
        }

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

        public RectangleF Shrink(float amount) {
            return new RectangleF(X + amount, Y + amount,
                Width - amount * 2f, Height - amount * 2f);
        }

        public static RectangleF FromCenter(Vector2 center, float width, float height) {
            return new RectangleF(center.X - width / 2f, center.Y - height / 2f, width, height);
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
