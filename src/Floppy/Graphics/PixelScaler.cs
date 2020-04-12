using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Floppy.Graphics {
    public class PixelScaler {
        private readonly GraphicsDevice _graphicsDevice;
        private readonly IRenderTargetStack _renderTargetStack;
        private readonly SpriteBatch _spriteBatch;

        private RenderTarget2D _renderTarget;
        private Rectangle _destinationRectangle;

        public PixelScaler(GraphicsDevice graphicsDevice, IRenderTargetStack renderTargetStack) {
            _graphicsDevice = graphicsDevice;
            _renderTargetStack = renderTargetStack;
            _spriteBatch = new SpriteBatch(graphicsDevice);

            _renderTarget = CreateRenderTarget();
            _destinationRectangle = CreateDestinationRectangle();
        }

        public int Scale { get; set; } = 1;

        public void Begin() {
            if (RenderTargetIsOutdated()) {
                UpdateRenderTarget();
            }

            _renderTargetStack.Push(_renderTarget);

            _graphicsDevice.Clear(Color.Transparent);
        }

        public void End() {
            _renderTargetStack.Pop();

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(_renderTarget, _destinationRectangle, Color.White);
            _spriteBatch.End();
        }

        private bool RenderTargetIsOutdated() {
            return _renderTarget.Width != _graphicsDevice.Viewport.Width / Scale 
                || _renderTarget.Height != _graphicsDevice.Viewport.Height / Scale;
        }

        private void UpdateRenderTarget() {
            _renderTarget.Dispose();

            _renderTarget = CreateRenderTarget();
            _destinationRectangle = CreateDestinationRectangle();
        }

        private RenderTarget2D CreateRenderTarget() {
            return new RenderTarget2D(_graphicsDevice,
                _graphicsDevice.Viewport.Width / Scale,
                _graphicsDevice.Viewport.Height / Scale,
                false,
                SurfaceFormat.Color,
                DepthFormat.Depth24Stencil8,
                0,
                RenderTargetUsage.PreserveContents);
        }

        private Rectangle CreateDestinationRectangle() {
            return new Rectangle(0, 0, _renderTarget.Width * Scale, _renderTarget.Height * Scale);
        }
    }
}
