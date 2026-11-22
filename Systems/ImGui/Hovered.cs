namespace Game.Systems.ImGui;

using ImGuiNET;

public class Hovered : IDisposable {
    static Dictionary<string, bool> Cache = new();

    public string Key = "";
    public bool Satisfied {
        get => Cache[Key];
    }

    public Hovered(string key) {
        Key = key;
    }

    public void Dispose() {
        Cache[Key] = ImGui.IsItemHovered();
    }
}
