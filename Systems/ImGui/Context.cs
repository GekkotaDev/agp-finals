namespace Game.Systems.ImGui;

using System.Numerics;

using Raylib_cs;
using rlImGui_cs;
using ImGuiNET;


/// <summary>
/// ImGui context helper class
/// </summary>
/// 
/// <remarks>
///   <para>
///     This class is nothing more than a utility to indicate the context in
///     which one is allowed to safely draw ImGUI widgets.
///   </para>
/// 
///   <para>
///     rlImGui_cs provides convenience helpers for using ImGUI through the
///     ImGuiNET library, which in turn, is a wrapper for the C++ library Dear
///     ImGui -- short for immediate GUI -- which is a library for creating
///     Graphical User Interfaces within immediate mode graphics. An unsafe
///     call may result in the game crashing.
///   </para>
/// 
///   <para>
///     For nicer syntax, this takes advantage of the <c>IDisposable</c> class's
///     syntax sugar with the <c>using</c> keyword.
///   </para>
/// </remarks>
public class Context : IDisposable {
    public Context(Action action) {
        rlImGui.Begin();
        action?.Invoke();
        // ImGui.SetNextWindowPos(Vector2.Zero);
        // ImGui.SetNextWindowSize(new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight()));
    }

    public void Begin(string windowName) {
        ImGuiWindowFlags flags = ImGuiWindowFlags.NoTitleBar |
                                     ImGuiWindowFlags.NoResize |
                                     ImGuiWindowFlags.NoMove |
                                     ImGuiWindowFlags.NoBackground;

        ImGui.Begin(windowName, flags);
    }

    public void Dispose() {
        ImGui.End();
        rlImGui.End();
    }
}
