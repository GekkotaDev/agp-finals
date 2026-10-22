namespace Game.Systems.Physics.Data.Volume;

public class Circle(int radius) {
    public float Width { get => radius * 2; }
    public float Height { get => radius * 2; }
}
