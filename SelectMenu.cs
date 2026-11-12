//* If this needs cleaning up in the future, a checklist of stuff
//*
//* - [ ] Use namespaces to avoid polluting the default global namespace. This
//*       would be nice to have in Godot ngl.
//*
//* - [ ] Remove all references to "slop" particularly those front and center.
//*
//* - [ ] Structure in-game screens around a global state machine.
//*
//*     - [ ] Refactor strings to use enums instead; stringly typed data is as
//*           good as dynamically typed, that puts more effort onto programmers
//*           that can be reduced by the compiler.
//*
//* - [ ] Rename managers to something that actually tells what a class does.
//*       https://blog.codinghorror.com/i-shall-call-it-somethingmanager/
using System.Numerics;
using Raylib_cs;
using Stateless;
using rlImGui_cs;
using ImGuiNET;



public class SelectMenu {
    public static void Main(string[] arguments) {
        var router = Router.Router.Get();
        const int screenWidth = 800;
        const int screenHeight = 600;
        
        
        
        Raylib.InitWindow(screenWidth, screenHeight, "Blob Brawl");
        Raylib.SetTargetFPS(60);
        rlImGui.Setup(true);
        Texture2D myImage = Raylib.LoadTexture(@"D:\School\4th Year\Advanced Game\AGP Clone\agp-finals\assets\sprites\slime.png");
        
        bool shouldStartGame = false;

         while (!Raylib.WindowShouldClose() && Router.Router.Get().Route == Router.Routes.SelectMenu) {

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            rlImGui.Begin();
            
            
            var viewport = ImGui.GetMainViewport();

            Vector2 topCenter = new Vector2(viewport.WorkPos.X + (viewport.WorkSize.X * 0.5f),viewport.WorkPos.Y + 30.0f);
            ImGui.SetNextWindowPos(topCenter, ImGuiCond.Always, new Vector2(0.5f, 0.0f));
            ImGui.Begin("Header", ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.NoSavedSettings);
            ImGui.Text("Character Select");
            ImGui.End();


            Vector2 cardPos = new Vector2(viewport.WorkPos.X + (viewport.WorkSize.X * 0.5f),viewport.WorkPos.Y + 100.0f);
            Vector2 imageBoxSize = new Vector2(180, 260);
            ImGui.SetNextWindowPos(cardPos, ImGuiCond.Always, new Vector2(0.5f, 0.0f));
            ImGui.Begin("CharacterCard", ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.NoSavedSettings);
            if (ImGui.ImageButton("Character 1", (IntPtr)myImage.Id, imageBoxSize)) 
            {
                Console.WriteLine("testing");
                shouldStartGame = true; 
            }

            ImGui.End();

            rlImGui.End();
            Raylib.EndDrawing();

            if (shouldStartGame) {
                rlImGui.Shutdown();
                Raylib.UnloadTexture(myImage);
                Raylib.CloseWindow();

                router.Machine.Fire(Router.Trigger.StartGame);
                GameProgram.Main();
                break;
            }
        }

        rlImGui.Shutdown();
        Raylib.UnloadTexture(myImage);
        Raylib.CloseWindow();
    }
}
