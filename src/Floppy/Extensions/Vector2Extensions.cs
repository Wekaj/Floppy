using Microsoft.Xna.Framework;
using System;

namespace Floppy.Extensions {
    public static class Vector2Extensions {
        public static float GetAngle(this Vector2 vector) {
            return ((float)Math.Atan2(vector.Y, vector.X) + MathHelper.TwoPi) % MathHelper.TwoPi;
        }

        public static Vector2 SetX(this Vector2 vector, float x) {
            return new Vector2(x, vector.Y);
        }

        public static Vector2 SetY(this Vector2 vector, float y) {
            return new Vector2(vector.X, y);
        }
    }
}