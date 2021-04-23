using Floppy.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Floppy.Graphics {
    public class PrimitiveDrawer {
        private readonly SpriteBatch _spriteBatch;

        private readonly Texture2D _pixelTexture;

        public PrimitiveDrawer(SpriteBatch spriteBatch, ContentManager content) {
            _spriteBatch = spriteBatch;

            _pixelTexture = content.Load<Texture2D>("Textures/Pixel");
        }

        public void DrawRectangle(Rectangle rectangle, Color color, float thickness = 1f) {
            var topLeft = new Vector2(rectangle.Left, rectangle.Top);
            var topRight = new Vector2(rectangle.Right, rectangle.Top);
            var bottomLeft = new Vector2(rectangle.Left, rectangle.Bottom);
            var bottomRight = new Vector2(rectangle.Right, rectangle.Bottom);

            DrawLine(topLeft, topRight, color, thickness);
            DrawLine(topRight, bottomRight, color, thickness);
            DrawLine(bottomRight, bottomLeft, color, thickness);
            DrawLine(bottomLeft, topLeft, color, thickness);
        }

        public void DrawCircle(Vector2 center, float radius, Color color, float thickness = 1f) {
            const int points = 20;
            const float arcRadians = MathHelper.TwoPi / points;

            float radians = 0f;
            for (int i = 0; i <= points; i++) {
                Vector2 p1 = center + FloppyMath.VectorFromAngle(radians) * radius;
                Vector2 p2 = center + FloppyMath.VectorFromAngle(radians + arcRadians) * radius;

                DrawLine(p1, p2, color, thickness);

                radians += arcRadians;
            }
        }

        public void DrawLine(Vector2 point1, Vector2 point2, Color color, float thickness = 1f) {
            float distance = Vector2.Distance(point1, point2);
            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);

            DrawLine(point1, distance, angle, color, thickness);
        }

        public void DrawLine(Vector2 point, float length, float angle, Color color, float thickness = 1f) {
            var origin = new Vector2(0f, 0.5f);
            var scale = new Vector2(length, thickness);

            _spriteBatch.Draw(_pixelTexture, point, null, color, angle, origin, scale, SpriteEffects.None, 0);
        }
    }
}
