using Floppy.Input;
using Microsoft.Xna.Framework.Input;
using System;

namespace Floppy.Extensions {
    public static class MouseStateExtensions {
        public static ButtonState GetButtonState(this MouseState mouseState, MouseButtons button) {
            switch (button) {
                case MouseButtons.Left: {
                    return mouseState.LeftButton;
                }
                case MouseButtons.Middle: {
                    return mouseState.MiddleButton;
                }
                case MouseButtons.Right: {
                    return mouseState.RightButton;
                }
                case MouseButtons.X1: {
                    return mouseState.XButton1;
                }
                case MouseButtons.X2: {
                    return mouseState.XButton2;
                }
                default: {
                    throw new ArgumentException($"{button} is not a valid mouse button.");
                }
            }
        }

        public static bool IsButtonDown(this MouseState mouseState, MouseButtons button) {
            return mouseState.GetButtonState(button) == ButtonState.Pressed;
        }

        public static bool IsButtonUp(this MouseState mouseState, MouseButtons button) {
            return mouseState.GetButtonState(button) == ButtonState.Released;
        }
    }
}
