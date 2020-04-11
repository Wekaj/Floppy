using Floppy.Extensions;
using Floppy.Input;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Xunit;

namespace Floppy.Tests.Extensions {
    public class MouseStateExtensionsTests {
        public static IEnumerable<object[]> GetButtonStateCases { get; } = new[] {
            new object[] { CreateMouseState(leftButton: ButtonState.Released), MouseButtons.Left, ButtonState.Released },
            new object[] { CreateMouseState(leftButton: ButtonState.Pressed), MouseButtons.Left, ButtonState.Pressed },
            new object[] { CreateMouseState(middleButton: ButtonState.Released), MouseButtons.Middle, ButtonState.Released },
            new object[] { CreateMouseState(middleButton: ButtonState.Pressed), MouseButtons.Middle, ButtonState.Pressed },
            new object[] { CreateMouseState(rightButton: ButtonState.Released), MouseButtons.Right, ButtonState.Released },
            new object[] { CreateMouseState(rightButton: ButtonState.Pressed), MouseButtons.Right, ButtonState.Pressed },
            new object[] { CreateMouseState(xButton1: ButtonState.Released), MouseButtons.X1, ButtonState.Released },
            new object[] { CreateMouseState(xButton1: ButtonState.Pressed), MouseButtons.X1, ButtonState.Pressed },
            new object[] { CreateMouseState(xButton2: ButtonState.Released), MouseButtons.X2, ButtonState.Released },
            new object[] { CreateMouseState(xButton2: ButtonState.Pressed), MouseButtons.X2, ButtonState.Pressed },
        };

        [Theory]
        [MemberData(nameof(GetButtonStateCases))]
        public void GetButtonState_MouseStateAndButton_CorrectState(MouseState mouseState, MouseButtons button, ButtonState expectedButtonState) {
            ButtonState buttonState = MouseStateExtensions.GetButtonState(mouseState, button);

            Assert.Equal(expectedButtonState, buttonState);
        }

        [Theory]
        [MemberData(nameof(GetButtonStateCases))]
        public void IsButtonDown_MouseStateAndButton_CorrectOutput(MouseState mouseState, MouseButtons button, ButtonState expectedButtonState) {
            bool expectedOutput = expectedButtonState == ButtonState.Pressed;

            bool output = MouseStateExtensions.IsButtonDown(mouseState, button);

            Assert.Equal(expectedOutput, output);
        }

        [Theory]
        [MemberData(nameof(GetButtonStateCases))]
        public void IsButtonUp_MouseStateAndButton_CorrectOutput(MouseState mouseState, MouseButtons button, ButtonState expectedButtonState) {
            bool expectedOutput = expectedButtonState == ButtonState.Released;

            bool output = MouseStateExtensions.IsButtonUp(mouseState, button);

            Assert.Equal(expectedOutput, output);
        }

        private static MouseState CreateMouseState(
            ButtonState leftButton = ButtonState.Released,
            ButtonState middleButton = ButtonState.Released,
            ButtonState rightButton = ButtonState.Released,
            ButtonState xButton1 = ButtonState.Released,
            ButtonState xButton2 = ButtonState.Released) {

            return new MouseState(0, 0, 0, leftButton, middleButton, rightButton, xButton1, xButton2);
        }
    }
}
