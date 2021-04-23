using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Floppy.Graphics {
    public class RenderTargetStack : IRenderTargetStack {
        private readonly GraphicsDevice _graphicsDevice;

        private readonly Stack<RenderTarget2D> _renderTargets = new();

        public RenderTargetStack(GraphicsDevice graphicsDevice) {
            _graphicsDevice = graphicsDevice;
        }

        public void Push(RenderTarget2D renderTarget) {
            _renderTargets.Push(renderTarget);

            _graphicsDevice.SetRenderTarget(renderTarget);
        }

        public RenderTarget2D Pop() {
            RenderTarget2D popped = _renderTargets.Pop();

            if (_renderTargets.TryPeek(out RenderTarget2D? renderTarget)) {
                _graphicsDevice.SetRenderTarget(renderTarget);
            }
            else {
                _graphicsDevice.SetRenderTarget(null);
            }

            return popped;
        }
    }
}
