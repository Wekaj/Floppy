using System;
using System.Collections.Generic;

namespace Floppy.Screens {
    public class ScreenManager {
        private readonly Dictionary<Type, Func<IScreen>> _screenBuilders = new Dictionary<Type, Func<IScreen>>();

        public IScreen? CurrentScreen { get; private set; }

        public void AddScreenType<TScreen>(Func<TScreen> screenBuilder)
            where TScreen : class, IScreen {

            _screenBuilders.Add(typeof(TScreen), screenBuilder);
        }

        public void AddScreenType<TScreen, TArgs>(Func<TScreen> screenBuilder)
            where TScreen : class, IScreen<TArgs> {

            _screenBuilders.Add(typeof(TScreen), screenBuilder);
        }

        public void TransitionTo<TScreen>()
            where TScreen : class, IScreen {

            CurrentScreen = CreateScreen<TScreen>();
        }

        public void TransitionTo<TScreen, TArgs>(TArgs args)
            where TScreen : class, IScreen<TArgs> {

            TScreen screen = CreateScreen<TScreen>();

            screen.Initialize(args);

            CurrentScreen = screen;
        }

        private TScreen CreateScreen<TScreen>() 
            where TScreen : class, IScreen {

            return (TScreen)_screenBuilders[typeof(TScreen)].Invoke();
        }
    }
}
