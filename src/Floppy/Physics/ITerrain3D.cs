using Microsoft.Xna.Framework;

namespace Floppy.Physics {
    public interface ITerrain3D {
        Vector3 TileSize { get; }
        float EdgeThreshold { get; }

        bool IsSolid(int x, int y, int z);
        BlockShape GetBlockShape(int x, int y, int z);
    }
}
