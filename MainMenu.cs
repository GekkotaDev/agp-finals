using System.Numerics;
using Raylib_cs;
using Stateless;

class MainMenu {
    public static void Main(string[] args) {
        var stateMachine = new StateMachine<string, string>("Menu");

        stateMachine.Configure("Menu")
            .Permit("ClickPlay", "Launching")
            .Permit("ClickQuit", "Exiting");

        stateMachine.Configure("Launching")
            .OnEntry(() => {
                Raylib.CloseWindow();
                GameProgram.Main();
                Environment.Exit(0);
            });

        Raylib.InitWindow(800, 600, "Blob Brawl");
        Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose() && stateMachine.State != "Exiting") {
            float screenWidth = Raylib.GetScreenWidth();
            float screenHeight = Raylib.GetScreenHeight();
            float buttonWidth = 200f;
            float buttonHeight = 50f;
            float spacing = 20f;
            float totalHeight = (buttonHeight * 2) + spacing;
            float startY = (screenHeight - totalHeight) / 2f;

            Rectangle playButton = new Rectangle((screenWidth - buttonWidth) / 2f, startY, buttonWidth, buttonHeight);
            Rectangle quitButton = new Rectangle((screenWidth - buttonWidth) / 2f, startY + buttonHeight + spacing, buttonWidth, buttonHeight);
            Vector2 mousePosition = Raylib.GetMousePosition();


            bool isMouseOverPlay = Raylib.CheckCollisionPointRec(mousePosition, playButton);
            bool isMouseOverQuit = Raylib.CheckCollisionPointRec(mousePosition, quitButton);

            if (stateMachine.State == "Menu") {
                if (isMouseOverPlay && Raylib.IsMouseButtonPressed(MouseButton.Left)) {
                    stateMachine.Fire("ClickPlay");
                } else if (isMouseOverQuit && Raylib.IsMouseButtonPressed(MouseButton.Left)) {
                    stateMachine.Fire("ClickQuit");
                }
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);


            Color playColor = isMouseOverPlay ? Color.DarkBlue : Color.Blue;
            Raylib.DrawRectangleRec(playButton, playColor);
            Raylib.DrawRectangleLinesEx(playButton, 2, Color.White);
            Raylib.DrawText("PLAY", (int)playButton.X + 75, (int)playButton.Y + 15, 20, Color.White);


            Color quitColor = isMouseOverQuit ? Color.Maroon : Color.Red;
            Raylib.DrawRectangleRec(quitButton, quitColor);
            Raylib.DrawRectangleLinesEx(quitButton, 2, Color.White);
            Raylib.DrawText("QUIT", (int)quitButton.X + 75, (int)quitButton.Y + 15, 20, Color.White);

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}