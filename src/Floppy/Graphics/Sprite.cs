using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Floppy.Graphics {
    public class Sprite {
        public Sprite(Texture2D texture) {
            Texture = texture;
            SourceRectangle = texture.Bounds;
        }

        public Texture2D Texture { get; set; }
        public Rectangle SourceRectangle { get; set; }
        public Color Color { get; set; } = Color.White;
        public float Rotation { get; set; } = 0f;
        public Vector2 Origin { get; set; } = Vector2.Zero;
        public Vector2 Scale { get; set; } = Vector2.One;
        public SpriteEffects Effects { get; set; } = SpriteEffects.None;
        public float LayerDepth { get; set; } = 0f;

        public void Draw(SpriteBatch spriteBatch, Vector2 position) {
            spriteBatch.Draw(Texture, position, SourceRectangle, Color, Rotation, Origin, Scale, Effects, LayerDepth);
        }
    }
}
