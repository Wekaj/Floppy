using Floppy.Utilities.Extensions;
using System.Collections.Generic;

namespace Floppy.Input {
    public class InputBindings {
        private readonly Dictionary<string, IBinding[]> _bindings = new Dictionary<string, IBinding[]>();

        private readonly HashSet<string> _previouslyPressedBindings = new HashSet<string>();
        private readonly HashSet<string> _currentlyPressedBindings = new HashSet<string>();

        public void Set(string id, params IBinding[] bindings) {
            _bindings.Remove(id);
            _bindings.Add(id, bindings);
        }

        public void Update(InputState inputState) {
            _previouslyPressedBindings.Clear();
            _previouslyPressedBindings.AddRange(_currentlyPressedBindings);

            _currentlyPressedBindings.Clear();
            foreach (KeyValuePair<string, IBinding[]> entry in _bindings) {
                for (int i = 0; i < entry.Value.Length; i++) {
                    if (entry.Value[i].IsPressed(inputState)) {
                        _currentlyPressedBindings.Add(entry.Key);
                        break;
                    }
                }
            }
        }

        public void Update() {
            Update(InputState.GetCurrentState());
        }

        public bool IsPressed(string id) {
            return _currentlyPressedBindings.Contains(id);
        }

        public bool WasPressed(string id) {
            return _previouslyPressedBindings.Contains(id);
        }

        public bool JustPressed(string id) {
            return IsPressed(id) && !WasPressed(id);
        }

        public bool JustReleased(string id) {
            return !IsPressed(id) && WasPressed(id);
        }
    }
}
