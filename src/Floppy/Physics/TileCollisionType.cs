namespace Floppy.Physics {
    public enum TileCollisionType {
        None,
        Solid,
        Platform
    }

    public static class TileCollisionTypeExtensions {
        public static bool IsSolid(this TileCollisionType collisionType, bool considerPlatforms = false) {
            return collisionType == TileCollisionType.Solid
                || collisionType == TileCollisionType.Platform && considerPlatforms;
        }
    }
}
