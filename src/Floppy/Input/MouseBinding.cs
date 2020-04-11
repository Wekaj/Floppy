using Floppy.Utilities.Extensions;

namespace Floppy.Input {
    public class MouseBinding : IBinding {
        public MouseBinding(MouseButtons button) {
            Button = button;
        }

        public MouseButtons Button { get; }

        public bool IsPressed(InputState inputState) {
            return inputState.MouseState.IsButtonDown(Button);
        }
    }
}
