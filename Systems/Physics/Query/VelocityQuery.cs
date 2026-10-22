namespace Game.Systems.Physics.Query;

public static class VelocityQuery {
    public static Data.Position NextPosition(Data.Position position, Data.Speed speed) {
        return new Data.Position {
            X = position.X + speed.X,
            Y = position.Y + speed.Y,
        };
    }
}
