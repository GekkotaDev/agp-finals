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
using Raylib_cs;

public class SelectMenu {
    public static void Main(string[] arguments) {
        const int screenWidth = 800;
        const int screenHeight = 600;

        Raylib.InitWindow(screenWidth, screenHeight, "Blob Brawl");
        Raylib.SetTargetFPS(60);
    }
}
