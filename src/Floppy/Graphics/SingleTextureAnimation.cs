using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Floppy.Graphics {
    public class SingleTextureAnimation : IAnimation {
        private readonly Texture2D _texture;

        private float _totalDuration;

        private readonly List<Frame> _frames = new List<Frame>();
        
        public SingleTextureAnimation(Texture2D texture) {
            _texture = texture;
        }

        public bool IsLooping { get; set; } = false;
        public Vector2 Origin { get; set; } = Vector2.Zero;
        public SpriteEffects Effects { get; set; } = SpriteEffects.None;

        public SingleTextureAnimation AddFrame(Rectangle sourceRectangle, float duration) {
            _frames.Add(new Frame(sourceRectangle, duration));

            _totalDuration += duration;

            return this;
        }

        public void Apply(Sprite sprite, float time) {
            sprite.Texture = _texture;
            sprite.Origin = Origin;
            sprite.Effects = Effects;

            if (IsLooping) {
                time %= _totalDuration;
            }

            for (int i = 0; i < _frames.Count; i++) {
                if (time <= _frames[i].Duration || i == _frames.Count - 1) {
                    sprite.SourceRectangle = _frames[i].SourceRectangle;
                    break;
                }
                else {
                    time -= _frames[i].Duration;
                }
            }
        }

        private class Frame {
            public Frame(Rectangle sourceRectangle, float duration) {
                SourceRectangle = sourceRectangle;
                Duration = duration;
            }

            public Rectangle SourceRectangle { get; }
            public float Duration { get; }
        }
    }
}
