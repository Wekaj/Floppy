namespace Floppy.Physics {
    public enum TileCollisionType {
        None,
        Solid,
        Platform,
        Grate
    }

    public static class TileCollisionTypeExtensions {
        public static bool IsSolid(this TileCollisionType collisionType, bool considerPlatforms = false, bool ignoreGrates = false) {
            return collisionType == TileCollisionType.Solid
                || collisionType == TileCollisionType.Platform && considerPlatforms
                || collisionType == TileCollisionType.Grate && !ignoreGrates;
        }
    }
}
