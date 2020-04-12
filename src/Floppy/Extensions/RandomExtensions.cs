using Floppy.Utilities;
using Microsoft.Xna.Framework;
using System;

namespace Floppy.Extensions {
    /// <summary>
    /// Contains extension methods to help with <see cref="Random"/> operations.
    /// </summary>
    public static class RandomExtensions {
        /// <summary>
        /// Returns a random unit vector.
        /// </summary>
        /// <param name="random">The source of randomness.</param>
        /// <returns>A random unit vector.</returns>
        public static Vector2 NextUnitVector(this Random random) {
            float angle = random.NextAngle();

            return FloppyMath.VectorFromAngle(angle);
        }

        #region Internal Methods

        /// <summary>
        /// Returns a random floating-point number that is greater than or equal to 0, and less than 1.
        /// </summary>
        /// <param name="random">The source of randomness.</param>
        /// <returns>A random floating-point number.</returns>
        internal static float NextSingle(this Random random) {
            return (float)random.NextDouble();
        }

        /// <summary>
        /// Returns a random floating-point number that is between 0 and some exclusive maximum (or minimum) 
        /// value.
        /// </summary>
        /// <param name="random">The source of randomness.</param>
        /// <param name="maxValue">The maximum (or minimum if negative) value.</param>
        /// <returns>A random floating-point number.</returns>
        internal static float NextSingle(this Random random, float maxValue) {
            return random.NextSingle() * maxValue;
        }

        /// <summary>
        /// Returns a random floating-point number that is greater than or equal to some minimum value and
        /// less than some maximum value.
        /// </summary>
        /// <param name="random">The source of randomness.</param>
        /// <param name="minValue">The inclusive minimum.</param>
        /// <param name="maxValue">The exclusive maximum.</param>
        /// <returns>A random floating-point number.</returns>
        internal static float NextSingle(this Random random, float minValue, float maxValue) {
            return minValue + random.NextSingle(maxValue - minValue);
        }

        /// <summary>
        /// Returns a random angle in radians that is greater than or equal to 0π and less than 2π.
        /// </summary>
        /// <param name="random">The source of randomness.</param>
        /// <returns>A random angle in radians.</returns>
        internal static float NextAngle(this Random random) {
            return random.NextSingle(MathHelper.TwoPi);
        }

        #endregion
    }
}
