using System.Numerics;
using Raylib_cs;
using Stateless;
using rlImGui_cs;
using ImGuiNET;

//! I've had the willpower to actually deal with ts now.
//!
//! My reaction
//! https://youtu.be/vQJIjyzVqeM
//! https://youtu.be/C0PgRC2BF6c
//! https://youtu.be/t6i2BzTkTVQ?t=30
//!
//! Valve programmers are relatable
//! https://www.youtube.com/watch?v=k238XpMMn38
class MainMenu {
    public static void Main(string[] args) {
        //! no.
        //!
        //! I get the spirit of what you're doing here but the execution is all
        //! wrong. I've mentioned using a state machine to manage screens and 
        //! their respective transitions but I didn't mention using it as a
        //! local state management utility.
        //!
        //! Yes, you can use them as a local state management solution but here
        //! is not appropriate; on top of that, stringly typing it defeats the
        //! point of a state machine if you're using it more as a state reducer
        //! a la React.js with the useReducer() hook.
        //!
        //! The fact that it's a string means the compiler can't help you out
        //! if you've made a typo. Compilers are good; let the compiler do work
        //! you would've manually done anyways and so you can spend time on
        //! other things rather than allocating more time to runtime debugging.
        //!
        //! Use an enum to effectively use a state machine library, enums should
        //! be enough to define the state and associated data. If that isn't
        //! enough, you could make a class hierarchy.
        //!
        //! So I've moved it to the global scope under the Router namespace
        //! where it can actually do it's job at hinting what the current screen
        //! should be rendered along with any side effects it should be have.
        //!
        //! This is a good enough system to manage screens without the complexities
        //! of a web development style router which ain't needed here.

        //! p.s: don't uncomment this.
        var router = Router.Router.Get();
        

        // stateMachine.Configure("Menu")
        //     .Permit("ClickPlay", "Launching")
        //     .Permit("ClickQuit", "Exiting");

        // stateMachine.Configure("Launching")
        //     .OnEntry(() => {
        //         Raylib.CloseWindow();
        //         GameProgram.Main();
        //         Environment.Exit(0);
        //     });

        Raylib.InitWindow(800, 600, "Blob Brawl");
        Raylib.SetTargetFPS(60);
        rlImGui.Setup(true);

        while (!Raylib.WindowShouldClose() && Router.Router.Get().Route == Router.Routes.MainMenu) {

            //! Mate did you really just go ahead and did things from damn
            //! scratch again instead of enlisting the help of a 3rd party
            //! library that does it for you?
            //!
            //! Did I not fucking told you about rlImGui_cs??
            //!
            //! I'm going to have to throw everything out and replace it
            //! with rlImGui.ImageButton. It's not that difficult to
            //! create placeholder buttons in Figma ffs or at the very
            //! least use ImGui.Button().
            //!
            //! I'm doing that later. I don't have the mental bandwidth
            //! to deal with this bs right now.
            //! vvvvvvvvvvvvvvvv

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            rlImGui.Begin();

            //! You use Gemini right? Here's something the "never will be
            //! sentient" Large LANGUAGE MODEL that is Gemini returned all by
            //! just querying
            //!
            //! "rlImGui-cs without window"
            //!
            //! And quit treating it like a damn person, those tech CEOs and
            //! Thiel types keep saying that just so you could give them
            //! attention and money.

            // Create a full-screen ImGui window with no chrome

            ImGuiWindowFlags flags = ImGuiWindowFlags.NoTitleBar |
                                     ImGuiWindowFlags.NoResize |
                                     ImGuiWindowFlags.NoMove |
                                     ImGuiWindowFlags.NoBackground;

            ImGui.SetNextWindowPos(Vector2.Zero);
            ImGui.SetNextWindowSize(new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight()));
 

            ImGui.Begin("Root", flags);

            //! Isn't this much more precise and shorter?

            // Put your ImGui widgets here
            if (ImGui.Button("Debug")) {
                Console.WriteLine("I'm clicked!"); ;
            }
            ImGui.End();


            var center = ImGui.GetMainViewport().GetCenter();
            ImGui.SetNextWindowPos(center, ImGuiCond.Always, new Vector2(0.5f, 0.5f));
            ImGui.Begin("AlwaysCenteredButtons", flags);

            if (ImGui.Button("PLAY")) {
                router.Machine.Fire(Router.Trigger.SelectBlobs);
            }

            if (ImGui.Button("QUIT")) {
                Environment.Exit(0);
            }

            ImGui.End();
            rlImGui.End();
            Raylib.EndDrawing();
        }

        rlImGui.Shutdown();
        Raylib.CloseWindow();
    }
}