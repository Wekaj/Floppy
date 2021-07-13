namespace Floppy.Physics {
    public interface ITerrain2D {
        float TileSize { get; }
        float EdgeThreshold { get; }

        bool IsSolid(int x, int y);
    }
}
