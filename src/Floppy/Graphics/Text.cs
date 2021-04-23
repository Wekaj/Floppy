using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Floppy.Graphics {
    public class Text {
        public SpriteFont? Font { get; set; }
        public string? Content { get; set; }
        public Color Color { get; set; } = Color.White;
        public float Rotation { get; set; } = 0f;
        public Vector2 Origin { get; set; } = Vector2.Zero;
        public Vector2 Scale { get; set; } = Vector2.One;
        public SpriteEffects Effects { get; set; } = SpriteEffects.None;
        public float LayerDepth { get; set; } = 0f;

        public Vector2 OriginNormalized {
            get => Origin / GetTextSize();
            set => Origin = value * GetTextSize();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position) {
            if (Font is not null && Content is not null) {
                spriteBatch.DrawString(Font, Content, position, Color, Rotation, Origin, Scale, Effects, LayerDepth);
            }
        }

        private Vector2 GetTextSize() {
            return Font?.MeasureString(Content ?? "") ?? Vector2.One;
        }
    }
}
