using Floppy.Input;
using Microsoft.Xna.Framework.Input;
using System;

namespace Floppy.Extensions {
    /// <summary>
    /// Contains extension methods to help with <see cref="MouseState"/> operations.
    /// </summary>
    public static class MouseStateExtensions {
        /// <summary>
        /// Gets the state of a mouse button using the <see cref="MouseButtons"/> enum.
        /// </summary>
        /// <param name="mouseState">The mouse state to check within.</param>
        /// <param name="button">The individual mouse button being checked.</param>
        /// <returns>The state of the mouse button.</returns>
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

        /// <summary>
        /// Checks if a mouse button is pressed.
        /// </summary>
        /// <param name="mouseState">The mouse state to check within.</param>
        /// <param name="button">The individual mouse button being checked.</param>
        /// <returns>If the mouse button is pressed.</returns>
        public static bool IsButtonDown(this MouseState mouseState, MouseButtons button) {
            return mouseState.GetButtonState(button) == ButtonState.Pressed;
        }

        /// <summary>
        /// Checks if a mouse button is released.
        /// </summary>
        /// <param name="mouseState">The mouse state to check within.</param>
        /// <param name="button">The individual mouse button being checked.</param>
        /// <returns>If the mouse button is released.</returns>
        public static bool IsButtonUp(this MouseState mouseState, MouseButtons button) {
            return mouseState.GetButtonState(button) == ButtonState.Released;
        }
    }
}
