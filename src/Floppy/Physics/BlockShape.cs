using Floppy.Utilities;
using System.Collections.Generic;

namespace Floppy.Physics {
    public enum BlockShape {
        Block,
        Ramp0,
        Ramp1,
        Ramp2,
        Ramp3,
        Ramp4,
        Ramp5,
        Ramp6,
        Ramp7,
        Ramp8,
        Ramp9,
        Ramp10,
        Ramp11,
    }

    public static class BlockShapeExtensions {
        private static readonly Dictionary<BlockShape, Direction3D> _coveredDirections = new() {
            { BlockShape.Block, Direction3D.All },
            { BlockShape.Ramp0, Direction3D.Down | Direction3D.Forwards },
            { BlockShape.Ramp1, Direction3D.Down | Direction3D.Backwards },
            { BlockShape.Ramp2, Direction3D.Down | Direction3D.Right },
            { BlockShape.Ramp3, Direction3D.Down | Direction3D.Left },
            { BlockShape.Ramp4, Direction3D.Down },
            { BlockShape.Ramp5, Direction3D.Down },
            { BlockShape.Ramp6, Direction3D.Down },
            { BlockShape.Ramp7, Direction3D.Down },
            { BlockShape.Ramp8, Direction3D.Down | Direction3D.Backwards | Direction3D.Left },
            { BlockShape.Ramp9, Direction3D.Down | Direction3D.Backwards | Direction3D.Right },
            { BlockShape.Ramp10, Direction3D.Down | Direction3D.Forwards | Direction3D.Left },
            { BlockShape.Ramp11, Direction3D.Down | Direction3D.Forwards | Direction3D.Right },
        };

        public static Direction3D GetCoveredDirections(this BlockShape shape) {
            return _coveredDirections[shape];
        }
    }
}
