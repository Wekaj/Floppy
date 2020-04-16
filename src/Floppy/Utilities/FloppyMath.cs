using Microsoft.Xna.Framework;
using System;

namespace Floppy.Utilities {
    /// <summary>
    /// Contains helpful math operations.
    /// </summary>
    public static class FloppyMath {
        /// <summary>
        /// Create a unit vector for a provided angle in radians, where 0π translates to (1, 0), π/2 translates 
        /// to (0, 1), π translates to (-1, 0), and 3π/2 translates to (0, -1).
        /// </summary>
        /// <param name="angle">The angle in radians.</param>
        /// <returns>The unit vector.</returns>
        public static Vector2 VectorFromAngle(float angle) {
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }
    }
}
