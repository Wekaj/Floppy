﻿using Microsoft.Xna.Framework.Input;

namespace Floppy.Input {
    public class GamePadBinding : IBinding {
        public GamePadBinding(int gamePadIndex, Buttons button) {
            GamePadIndex = gamePadIndex;
            Button = button;
        }

        public int GamePadIndex { get; }
        public Buttons Button { get; }

        public bool IsPressed(InputState inputState) {
            return inputState.GetGamePadState(GamePadIndex).IsButtonDown(Button);
        }
    }
}
