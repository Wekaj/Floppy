using Microsoft.Xna.Framework;
using System;

namespace Floppy.Extensions {
    public static class PointExtensions {
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

        public static Point Clamp(this Point point, Point topLeft, Point bottomRight) {
            return Clamp(point, topLeft.X, topLeft.Y, bottomRight.X, bottomRight.Y);
        }

        public static int ManhattenDistance(this Point point1, Point point2) {
            int xDiff = Math.Abs(point1.X - point2.X);
            int yDiff = Math.Abs(point1.Y - point2.Y);

            return xDiff + yDiff;
        }
    }
}
