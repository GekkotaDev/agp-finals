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

        ImGuiWindowFlags imGuiFlags = ImGuiWindowFlags.NoTitleBar |
                             ImGuiWindowFlags.NoResize |
                             ImGuiWindowFlags.NoMove |
                             ImGuiWindowFlags.NoBackground;

        while (!Raylib.WindowShouldClose() && Router.Router.Get().Route == Router.Routes.MainMenu) {
            float screenWidth = Raylib.GetScreenWidth();
            float screenHeight = Raylib.GetScreenHeight();
            float buttonWidth = 200f;
            float buttonHeight = 50f;
            float spacing = 20f;
            float totalHeight = (buttonHeight * 2) + spacing;
            float startY = (screenHeight - totalHeight) / 2f;

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
            //! 
            //! https://youtu.be/hUxoiHfAFBo?t=112
            //! vvvvvvvvvvvvvvvv
            Rectangle playButton = new Rectangle((screenWidth - buttonWidth) / 2f, startY, buttonWidth, buttonHeight);
            Rectangle quitButton = new Rectangle((screenWidth - buttonWidth) / 2f, startY + buttonHeight + spacing, buttonWidth, buttonHeight);
            Vector2 mousePosition = Raylib.GetMousePosition();


            bool isMouseOverPlay = Raylib.CheckCollisionPointRec(mousePosition, playButton);
            bool isMouseOverQuit = Raylib.CheckCollisionPointRec(mousePosition, quitButton);

            if (router.Route == ) {
                if (isMouseOverPlay && Raylib.IsMouseButtonPressed(MouseButton.Left)) {
                    router.Machine.Fire("ClickPlay");
                } else if (isMouseOverQuit && Raylib.IsMouseButtonPressed(MouseButton.Left)) {
                    router.Machine.Fire("ClickQuit");
                }
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            rlImGui.Begin();

            //! For someone who uses Gemini, you sure can't even put it to good
            //! use. I've queried the Gemini large language model to have a
            //! functioning rlImGui

            // Create a full-screen ImGui window with no chrome
            ImGui.SetNextWindowPos(Vector2.Zero);
            ImGui.SetNextWindowSize(new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight()));
            ImGuiWindowFlags flags = ImGuiWindowFlags.NoTitleBar |
                                     ImGuiWindowFlags.NoResize |
                                     ImGuiWindowFlags.NoMove |
                                     ImGuiWindowFlags.NoBackground;

            ImGui.Begin("HiddenRoot", flags);

            // Put your ImGui widgets here
            if (ImGui.Button("Test")) {
                Console.WriteLine("I'm clicked!"); ;
            }

            ImGui.End();

            Color playColor = isMouseOverPlay ? Color.DarkBlue : Color.Blue;
            Raylib.DrawRectangleRec(playButton, playColor);
            Raylib.DrawRectangleLinesEx(playButton, 2, Color.White);
            Raylib.DrawText("PLAY", (int)playButton.X + 75, (int)playButton.Y + 15, 20, Color.White);


            Color quitColor = isMouseOverQuit ? Color.Maroon : Color.Red;
            Raylib.DrawRectangleRec(quitButton, quitColor);
            Raylib.DrawRectangleLinesEx(quitButton, 2, Color.White);
            Raylib.DrawText("QUIT", (int)quitButton.X + 75, (int)quitButton.Y + 15, 20, Color.White);

            rlImGui.End();
            Raylib.EndDrawing();
        }

        rlImGui.Shutdown();
        Raylib.CloseWindow();
    }
}