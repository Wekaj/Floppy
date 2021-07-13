using Microsoft.Xna.Framework;
using System;

namespace Floppy.Utilities {
    /// <summary>
    /// Describes an axis-aligned cuboid with float components.
    /// </summary>
    [Serializable]
    public struct Cuboid : IEquatable<Cuboid> {
        /// <summary>
        /// Creates a new cuboid.
        /// </summary>
        /// <param name="x">The x-coordinate of the side of the cuboid.</param>
        /// <param name="y">The y-coordinate of the side of the cuboid.</param>
        /// <param name="z">The z-coordinate of the side of the cuboid.</param>
        /// <param name="width">The cuboid's width.</param>
        /// <param name="height">The cuboid's height.</param>
        /// <param name="depth">The cuboid's depth.</param>
        public Cuboid(float x, float y, float z, float width, float height, float depth) {
            X = x;
            Y = y;
            Z = z;
            Width = width;
            Height = height;
            Depth = depth;
        }

        /// <summary>
        /// Creates a new cuboid from a position and size.
        /// </summary>
        /// <param name="position">The corner of the cuboid.</param>
        /// <param name="size">The cuboid's size.</param>
        public Cuboid(Vector3 position, Vector3 size) {
            X = position.X;
            Y = position.Y;
            Z = position.Z;
            Width = size.X;
            Height = size.Y;
            Depth = size.Z;
        }

        /// <summary>
        /// A cuboid at (0, 0, 0) with zero width and zero height.
        /// </summary>
        public static Cuboid Empty { get; } = new Cuboid(0f, 0f, 0f, 0f, 0f, 0f);

        /// <summary>
        /// A cuboid at (0, 0, 0) with a width and height of one.
        /// </summary>
        public static Cuboid One { get; } = new Cuboid(0f, 0f, 0f, 1f, 1f, 1f);

        /// <summary>
        /// The x-coordinate of the cuboid.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// The y-coordinate of the cuboid.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// The z-coordinate of the cuboid.
        /// </summary>
        public float Z { get; set; }

        /// <summary>
        /// The width of the cuboid.
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// The height of the cuboid.
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        /// The depth of the cuboid.
        /// </summary>
        public float Depth { get; set; }

        /// <summary>
        /// The x-coordinate and y-coordinate of the cuboid in a vector.
        /// </summary>
        public Vector3 Position {
            get => new Vector3(X, Y, Z);
            set {
                X = value.X;
                Y = value.Y;
                Z = value.Z;
            }
        }

        /// <summary>
        /// The width and height of the cuboid in a vector.
        /// </summary>
        public Vector3 Size {
            get => new Vector3(Width, Height, Depth);
            set {
                Width = value.X;
                Height = value.Y;
                Depth = value.Z;
            }
        }

        /// <summary>
        /// The center of the cuboid.
        /// </summary>
        public Vector3 Center {
            get => new Vector3(X + Width / 2f, Y + Height / 2f, Z + Depth / 2f);
            set {
                X = value.X - Width / 2f;
                Y = value.Y - Height / 2f;
                Z = value.Z - Depth / 2f;
            }
        }

        /// <summary>
        /// The x-coordinate of the left side of the cuboid.
        /// </summary>
        public float Left => X;

        /// <summary>
        /// The y-coordinate of the bottom side of the cuboid.
        /// </summary>
        public float Bottom => Y;

        /// <summary>
        /// The z-coordinate of the front side of the cuboid.
        /// </summary>
        public float Front => Z;

        /// <summary>
        /// The x-coordinate of the right side of the cuboid.
        /// </summary>
        public float Right => X + Width;

        /// <summary>
        /// The y-coordinate of the top side of the cuboid.
        /// </summary>
        public float Top => Y + Height;

        /// <summary>
        /// The z-coordinate of the back side of the cuboid.
        /// </summary>
        public float Back => Z + Depth;

        /// <summary>
        /// Creates a cuboid from the center rather than the top-left corner.
        /// </summary>
        /// <param name="center">The center of the cuboid.</param>
        /// <param name="width">The cuboid's width.</param>
        /// <param name="height">The cuboid's height.</param>
        /// <param name="depth">The cuboid's depth.</param>
        /// <returns>The new cuboid.</returns>
        public static Cuboid FromCenter(Vector3 center, float width, float height, float depth) {
            return new Cuboid(center.X - width / 2f, center.Y - height / 2f, center.Z - depth / 2f, width, height, depth);
        }

        /// <summary>
        /// Checks if a point is contained within the cuboid.
        /// </summary>
        /// <param name="vector">The point to check.</param>
        /// <returns>If the point is contained within the cuboid.</returns>
        public bool Contains(Vector3 vector) {
            return vector.X >= X && vector.X <= X + Width
                && vector.Y >= Y && vector.Y <= Y + Height
                && vector.Z >= Z && vector.Z <= Z + Depth;
        }

        /// <summary>
        /// Checks if the cuboid intersects another cuboid.
        /// </summary>
        /// <param name="other">The other cuboid to check.</param>
        /// <returns>If the cuboids intersect.</returns>
        public bool Intersects(Cuboid other) {
            return X < other.X + other.Width && X + Width > other.X
                && Y < other.Y + other.Height && Y + Height > other.Y
                && Z < other.Z + other.Depth && Z + Depth > other.Z;
        }

        /// <summary>
        /// Offsets the cuboid's x-component and y-component.
        /// </summary>
        /// <param name="amount">A vector containing the offsets.</param>
        /// <returns>The cuboid with the offset applied.</returns>
        public Cuboid Offset(Vector3 amount) {
            return new Cuboid(X + amount.X, Y + amount.Y, Z + amount.Z, Width, Height, Depth);
        }

        /// <summary>
        /// Extends the cuboid. Positive vectors will increase the width and height, while negative vectors
        /// will still increase the width and height but also decrease the x-coordinate and y-coordinate.
        /// </summary>
        /// <param name="amount">The vector containing the extension values.</param>
        /// <returns>The extended cuboid.</returns>
        public Cuboid Extend(Vector3 amount) {
            Cuboid result = this;

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

            if (amount.Z < 0f) {
                result.Z += amount.Z;
                result.Depth -= amount.Z;
            }
            else {
                result.Depth += amount.Z;
            }

            return result;
        }

        /// <summary>
        /// Shrinks the cuboid on all sides by a fixed amount.
        /// </summary>
        /// <param name="amount">The amount to shrink the cuboid by.</param>
        /// <returns>The shrunked cuboid.</returns>
        public Cuboid Shrink(float amount) {
            return new Cuboid(X + amount, Y + amount, Z + amount,
                Width - amount * 2f, Height - amount * 2f, Depth - amount * 2f);
        }

        public override bool Equals(object? obj) {
            return obj is Cuboid cuboid && Equals(cuboid);
        }

        public bool Equals(Cuboid other) {
            return X == other.X
                && Y == other.Y
                && Z == other.Z
                && Width == other.Width
                && Height == other.Height
                && Depth == other.Depth;
        }

        public override int GetHashCode() {
            return HashCode.Combine(X, Y, Z, Width, Height, Depth);
        }

        public static bool operator ==(Cuboid f1, Cuboid f2) {
            return f1.Equals(f2);
        }

        public static bool operator !=(Cuboid f1, Cuboid f2) {
            return !(f1 == f2);
        }
    }
}
