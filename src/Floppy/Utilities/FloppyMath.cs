using Microsoft.Xna.Framework;
using System;

namespace Floppy.Utilities {
    public static class FloppyMath {
        public static Vector2 VectorFromAngle(float angle) {
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }
    }
}
