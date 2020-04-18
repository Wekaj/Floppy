using Floppy.Utilities;
using Microsoft.Xna.Framework;

namespace Floppy.Physics {
    public class Body {
        public Body(int id) {
            ID = id;
        }

        public int ID { get; }

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float MaxSpeed { get; set; } = float.MaxValue;
        public float Friction { get; set; }
        public Vector2 Gravity { get; set; }

        public float Mass { get; set; } = 1f;
        public Vector2 Force { get; set; }
        public Vector2 Impulse { get; set; }

        public RectangleF Bounds { get; set; }
        public Vector2 Contact { get; set; }

        public float BounceFactor { get; set; }
    }
}
