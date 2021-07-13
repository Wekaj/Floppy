using Microsoft.Xna.Framework;
using System;

namespace Floppy.Utilities {
    [Flags]
    public enum Direction3D {
        None = 0,

        Right = 1,
        Up = 2,
        Forwards = 4,
        Left = 8,
        Down = 16,
        Backwards = 32,

        All = Right | Up | Forwards | Left | Down | Backwards,
    }

    public static class Direction3DExtensions {
        public static Vector3 ToVector(this Direction3D direction) {
            Vector3 result = Vector3.Zero;

            if (direction.HasFlag(Direction3D.Right)) {
                result.X++;
            }
            if (direction.HasFlag(Direction3D.Up)) {
                result.Y++;
            }
            if (direction.HasFlag(Direction3D.Forwards)) {
                result.Z++;
            }
            if (direction.HasFlag(Direction3D.Left)) {
                result.X--;
            }
            if (direction.HasFlag(Direction3D.Down)) {
                result.Y--;
            }
            if (direction.HasFlag(Direction3D.Backwards)) {
                result.Z--;
            }

            return result;
        }
    }
}
