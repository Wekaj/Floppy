using Microsoft.Xna.Framework;
using System;

namespace Floppy.Extensions {
    /// <summary>
    /// Contains extension methods to help with <see cref="Point"/> operations.
    /// </summary>
    public static class PointExtensions {
        /// <summary>
        /// Restricts a point's x-component and y-component to be within specific ranges.
        /// </summary>
        /// <param name="point">The point to clamp.</param>
        /// <param name="minX">The inclusive minimum for the x-component.</param>
        /// <param name="minY">The inclusive minimum for the y-component.</param>
        /// <param name="maxX">The inclusive maximum for the x-component.</param>
        /// <param name="maxY">The inclusive maximum for the y-component.</param>
        /// <returns>The point with clamped components.</returns>
        public static Point Clamp(this Point point, int minX, int minY, int maxX, int maxY) {
            if (minX > maxX) {
                throw new ArgumentException($"The minimum X value ({minX}) cannot be greater than the maximum X value ({maxX}).");
            }
            if (minY > maxY) {
                throw new ArgumentException($"The minimum Y value ({minY}) cannot be greater than the maximum Y value ({maxY}).");
            }

            int x = MathHelper.Clamp(point.X, minX, maxX);
            int y = MathHelper.Clamp(point.Y, minY, maxY);

            return new Point(x, y);
        }

        /// <summary>
        /// Restricts a point's x-component and y-component to be within specific ranges.
        /// </summary>
        /// <param name="point">The point to clamp.</param>
        /// <param name="topLeft">A point containing the inclusive minimums for the x-component and y-component.</param>
        /// <param name="bottomRight">A point containing the inclusive maximums for the x-component and y-component.</param>
        /// <returns>The point with clamped components.</returns>
        public static Point Clamp(this Point point, Point topLeft, Point bottomRight) {
            return Clamp(point, topLeft.X, topLeft.Y, bottomRight.X, bottomRight.Y);
        }

        /// <summary>
        /// Gets the Manhattan distance between two points. This is the sum of the individual distances between
        /// the x-components and the y-components.
        /// </summary>
        /// <param name="point1">The first point.</param>
        /// <param name="point2">The second point.</param>
        /// <returns>The Manhatten distance between the two points.</returns>
        public static int ManhattanDistance(this Point point1, Point point2) {
            int xDiff = Math.Abs(point1.X - point2.X);
            int yDiff = Math.Abs(point1.Y - point2.Y);

            return xDiff + yDiff;
        }
    }
}
