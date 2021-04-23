using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Floppy.Graphics {
    public class FixedFrameAnimation : IAnimation {
        private readonly SingleTextureAnimation _innerAnimation;
        private readonly int _frameWidth;
        private readonly int _frameHeight;

        public FixedFrameAnimation(Texture2D texture, int frameWidth, int frameHeight) {
            _innerAnimation = new SingleTextureAnimation(texture);
            _frameWidth = frameWidth;
            _frameHeight = frameHeight;
        }

        public bool IsLooping {
            get => _innerAnimation.IsLooping;
            set => _innerAnimation.IsLooping = value;
        }

        public Vector2 Origin {
            get => _innerAnimation.Origin;
            set => _innerAnimation.Origin = value;
        }

        public SpriteEffects Effects {
            get => _innerAnimation.Effects;
            set => _innerAnimation.Effects = value;
        }

        public FixedFrameAnimation AddFrame(int x, int y, float duration) {
            _innerAnimation.AddFrame(new Rectangle(x * _frameWidth, y * _frameHeight, _frameWidth, _frameHeight), duration);

            return this;
        }

        public void Apply(Sprite sprite, float time) {
            _innerAnimation.Apply(sprite, time);
        }
    }
}
