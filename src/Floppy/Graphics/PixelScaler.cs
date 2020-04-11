using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Floppy.Graphics {
    public class PixelScaler {
        private readonly GraphicsDevice _graphicsDevice;
        private readonly SpriteBatch _spriteBatch;

        private RenderTarget2D _renderTarget;

        public PixelScaler(GraphicsDevice graphicsDevice, int scale) {
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(graphicsDevice);

            Scale = scale;

            _renderTarget = CreateRenderTarget();
        }

        public int Scale { get; }

        public void Begin() {
            _graphicsDevice.SetRenderTarget(_renderTarget);
            _graphicsDevice.Clear(Color.Transparent);
        }

        public void End() {
            _graphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _spriteBatch.Draw(_renderTarget, 
                new Rectangle(0, 0, _renderTarget.Width * Scale, _renderTarget.Height * Scale), 
                Color.White);

            _spriteBatch.End();
        }

        public void UpdateSize() {
            _renderTarget.Dispose();

            _renderTarget = CreateRenderTarget();
        }

        private RenderTarget2D CreateRenderTarget() {
            return new RenderTarget2D(_graphicsDevice,
                _graphicsDevice.Viewport.Width / Scale,
                _graphicsDevice.Viewport.Height / Scale,
                false,
                SurfaceFormat.Color,
                DepthFormat.Depth24Stencil8,
                0,
                RenderTargetUsage.PreserveContents); ;
        }
    }
}
