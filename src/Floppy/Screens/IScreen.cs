using Microsoft.Xna.Framework;

namespace Floppy.Screens {
    public interface IScreen {
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }

    public interface IScreen<T> : IScreen {
        void Initialize(T args);
    }
}
