using Microsoft.Xna.Framework.Graphics;

namespace Floppy.Graphics {
    public interface IRenderTargetStack {
        void Push(RenderTarget2D renderTarget);
        RenderTarget2D Pop();
    }
}
