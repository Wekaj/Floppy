using System.Collections.Generic;

namespace Melon.Common.Game {
    public delegate string? Update<TContext>(TContext context, float deltaTime);
    public delegate bool Condition<TContext>(TContext context);
    public delegate void Action<TContext>(TContext context);

    public class State<TContext> {
        private readonly Dictionary<string, Transition> _transitions = new Dictionary<string, Transition>();

        private Update<TContext> _update = (context, deltaTime) => null;
        private Action<TContext> _entry = context => { };
        private Action<TContext> _exit = context => { };

        public State<TContext> AddTransition(string trigger, string state, Condition<TContext>? condition = null, Action<TContext>? action = null) {
            _transitions.Add(trigger, new Transition(condition ?? (context => true), action ?? (context => { }), state));

            return this;
        }

        public State<TContext> SetUpdate(Update<TContext> update) {
            _update = update;

            return this;
        }

        public State<TContext> SetEntry(Action<TContext> entryAction) {
            _entry = entryAction;

            return this;
        }

        public State<TContext> SetExit(Action<TContext> exitAction) {
            _exit = exitAction;

            return this;
        }

        public string? Fire(string trigger, TContext context) {
            if (_transitions.TryGetValue(trigger, out Transition? transition)
                && transition.Condition(context)) {

                transition.Action(context);

                return transition.State;
            }
            else {
                return null;
            }
        }

        public string? Update(TContext context, float deltaTime) {
            return _update(context, deltaTime);
        }

        public void Enter(TContext context) {
            _entry(context);
        }

        public void Exit(TContext context) {
            _exit(context);
        }

        private class Transition {
            public Transition(Condition<TContext> condition, Action<TContext> action, string state) {
                Condition = condition;
                Action = action;
                State = state;
            }

            public Condition<TContext> Condition { get; }
            public Action<TContext> Action { get; }
            public string State { get; }
        }
    }
}
