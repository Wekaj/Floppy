using Microsoft.Xna.Framework.Input;

namespace Floppy.Input {
    public class InputState {
        private readonly GamePadState[] _gamePadStates;

        public InputState(MouseState mouseState, KeyboardState keyboardState, GamePadState[] gamePadStates) {
            _gamePadStates = gamePadStates;

            MouseState = mouseState;
            KeyboardState = keyboardState;
        }

        public MouseState MouseState { get; }
        public KeyboardState KeyboardState { get; }

        public static InputState GetCurrentState() {
            MouseState mouseState = Mouse.GetState();
            KeyboardState keyboardState = Keyboard.GetState();

            var gamePadStates = new GamePadState[GamePad.MaximumGamePadCount];
            for (int i = 0; i < gamePadStates.Length; i++) {
                gamePadStates[i] = GamePad.GetState(i);
            }

            return new InputState(mouseState, keyboardState, gamePadStates);
        }

        public GamePadState GetGamePadState(int index) {
            return _gamePadStates[index];
        }
    }
}
