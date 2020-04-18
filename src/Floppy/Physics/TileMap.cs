namespace Floppy.Physics {
    public class TileMap {
        private readonly Tile[,] _tiles;
        
        public TileMap(int width, int height, float tileSize) {
            _tiles = new Tile[width, height];

            Width = width;
            Height = height;
            TileSize = tileSize;

            InitializeTiles();
        }

        public Tile this[int x, int y] => _tiles[x, y];

        public int Width { get; }
        public int Height { get; }
        public float TileSize { get; }

        public float EdgeThreshold { get; set; } = 0.001f;

        public bool IsWithinBounds(int x, int y) {
            return x >= 0 && y >= 0 && x < Width && y < Height;
        }

        public bool IsSolid(int x, int y, bool considerPlatforms = false) {
            return IsWithinBounds(x, y) && _tiles[x, y].CollisionType.IsSolid(considerPlatforms);
        }

        private void InitializeTiles() {
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    _tiles[x, y] = new Tile();
                }
            }
        }
    }
}
