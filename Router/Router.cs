namespace Router;

using Raylib_cs;
using Stateless;

public class Router {
    static Router? Instance;

    public Routes Route;
    public StateMachine<Routes, Trigger> Machine {
        get;
        set {
            if (field != null) return;
            field = value;
        }
    }

    private Router() {
        Route = Routes.MainMenu;
        Machine = new(Routes.MainMenu);

        // The provided callback function will be called whenever the state has
        // been changed (the transition from state A to state B).
        Machine.OnTransitioned(transition => { });

        // Run when the transition is completed.
        Machine.OnTransitionCompleted(transition => {
            Route = transition.Destination;
            Raylib.CloseWindow();

            switch (transition.Destination) {
                case Routes.MainMenu:
                    MainMenu.Main([]);
                    break;

                case Routes.SelectMenu:
                    SelectMenu.Main([]);
                    break;

                case Routes.Game:
                    GameProgram.Main();
                    break;
            }

            Environment.Exit(0);
        });

        Machine.Configure(Routes.MainMenu)
            .Permit(Trigger.SelectBlobs, Routes.SelectMenu);

        Machine.Configure(Routes.SelectMenu)
            .Permit(Trigger.StartGame, Routes.Game);

        Machine.Configure(Routes.Game)
            .Permit(Trigger.ReturnToMenu, Routes.MainMenu);
    }

    public static Router Get() {
        Instance ??= new Router();
        return Instance;
    }
}
