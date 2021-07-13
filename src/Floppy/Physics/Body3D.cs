using Floppy.Utilities;
using Microsoft.Xna.Framework;

namespace Floppy.Physics {
    public class Body3D {
        public Vector3 Position { get; set; }
        public Vector3 Velocity { get; set; }
        public float MaxSpeed { get; set; } = float.MaxValue;
        public Vector3 Friction { get; set; }
        public Vector3 Gravity { get; set; }

        public float Mass { get; set; } = 1f;
        public Vector3 Force { get; set; }
        public Vector3 Impulse { get; set; }

        public Cuboid Bounds { get; set; }
        public Vector3 Contact { get; set; }
        public bool IsSubmerged { get; set; }

        public float BounceFactor { get; set; }
        public float? FloatForce { get; set; }

        public bool IgnoresPlatforms { get; set; } = false;
        public bool IgnoresGrates { get; set; } = false;
    }
}
