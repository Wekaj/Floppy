using Microsoft.Xna.Framework;
using System;

namespace Floppy.Extensions {
    /// <summary>
    /// Contains extension methods to help with <see cref="Vector2"/> operations.
    /// </summary>
    public static class Vector2Extensions {
        /// <summary>
        /// Gets a vector's angle in radians, where (1, 0) has an angle of 0π, (0, 1) has an angle of π/2, (-1, 0)
        /// has an angle of π, and (0, -1) has an angle of 3π/2. The resulting angle will always be greater than 
        /// or equal to zero and less than 2π.
        /// </summary>
        /// <param name="vector">The vector to get the angle from.</param>
        /// <returns>The vector's angle in radians.</returns>
        public static float GetAngle(this Vector2 vector) {
            if (!float.IsFinite(vector.X) || !float.IsFinite(vector.Y)) {
                return float.NaN;
            }
            if (vector.X == 0f && vector.Y == 0f) {
                return float.NaN;
            }

            return ((float)Math.Atan2(vector.Y, vector.X) + MathHelper.TwoPi) % MathHelper.TwoPi;
        }

        /// <summary>
        /// Get a copy of a vector with a different x-component.
        /// </summary>
        /// <param name="vector">The vector being copied.</param>
        /// <param name="x">The x-component of the resulting vector.</param>
        /// <returns>A copy of the original vector with a different x-component.</returns>
        public static Vector2 SetX(this Vector2 vector, float x) {
            return new Vector2(x, vector.Y);
        }

        /// <summary>
        /// Get a copy of a vector with a different y-component.
        /// </summary>
        /// <param name="vector">The vector being copied.</param>
        /// <param name="x">The y-component of the resulting vector.</param>
        /// <returns>A copy of the original vector with a different y-component.</returns>
        public static Vector2 SetY(this Vector2 vector, float y) {
            return new Vector2(vector.X, y);
        }
    }
}