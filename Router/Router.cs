namespace Router;

using Raylib_cs;
using Stateless;

public class Router {
    static Router? Instance;

    public Routes route;
    public StateMachine<Routes, Trigger> state {
        get;
        set {
            if (field != null) return;
            field = value;
        }
    }

    private Router() {
        route = Routes.MainMenu;
        state = new(Routes.MainMenu);

        // The provided callback function will be called whenever the state has
        // been changed (the transition from state A to state B).
        state.OnTransitioned(transition => { });

        // Run when the transition is completed.
        state.OnTransitionCompleted(transition => {
            switch (transition.Destination) {
                case Routes.MainMenu:
                    Raylib.CloseWindow();
                    MainMenu.Main([]);
                    Environment.Exit(0);
                    break;

                case Routes.SelectMenu:
                    Raylib.CloseWindow();
                    SelectMenu.Main([]);
                    Environment.Exit(0);
                    break;

                case Routes.Game:
                    Raylib.CloseWindow();
                    GameProgram.Main();
                    Environment.Exit(0);
                    break;
            }
        });

        //! CONFIGURE FOR EVERY STATE.
        state.Configure(Routes.MainMenu)
            .Permit(Trigger.SelectBlobs, Routes.SelectMenu);

        state.Configure(Routes.SelectMenu)
            .Permit(Trigger.StartGame, Routes.Game);

        state.Configure(Routes.Game)
            .Permit(Trigger.ReturnToMenu, Routes.MainMenu);
    }

    public static Router Get() {
        Instance ??= new Router();
        return Instance;
    }
}
