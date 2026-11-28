using System.Numerics;
using Raylib_cs;

// Kung naiirita kayo sa error na yan paste this: #pragma warning restore CS8981 (ignores lower-case characters)

class blomeba {
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public float Radius { get; protected set; }
    public Color Color { get; }
    public int HitPoints { get; private set; }
    public int TeamId { get; }

    public blomeba(Vector2 position, Vector2 velocity, float radius, Color color, int hitPoints = 5, int teamId = 0) {
        Position = position;
        Velocity = velocity;
        Radius = radius;
        Color = color;
        HitPoints = hitPoints;
        TeamId = teamId;
    }

    public void TakeDamage(int damage) {
        HitPoints = Math.Max(0, HitPoints - damage);
    }

    public void DoubleHealth() {
        HitPoints *= 2;
    }

    public void HalveHealth() {
        HitPoints /= 2;
    }

    public virtual void Update(float deltaTime, Rectangle box) {
        Position += Velocity * deltaTime;

        if (Position.X - Radius < box.X) {
            Position = new Vector2(box.X + Radius, Position.Y);
            Velocity = new Vector2(MathF.Abs(Velocity.X), Velocity.Y);
        }

        if (Position.X + Radius > box.X + box.Width) {
            Position = new Vector2(box.X + box.Width - Radius, Position.Y);
            Velocity = new Vector2(-MathF.Abs(Velocity.X), Velocity.Y);
        }

        if (Position.Y - Radius < box.Y) {
            Position = new Vector2(Position.X, box.Y + Radius);
            Velocity = new Vector2(Velocity.X, MathF.Abs(Velocity.Y));
        }

        if (Position.Y + Radius > box.Y + box.Height) {
            Position = new Vector2(Position.X, box.Y + box.Height - Radius);
            Velocity = new Vector2(Velocity.X, -MathF.Abs(Velocity.Y));
        }
    }

    public void Draw() {
        Raylib.DrawCircleV(Position, Radius, Color);
        string hitPointsText = HitPoints.ToString();
        int fontSize = Math.Max(10, (int)Radius * 4 / 5);
        int textWidth = Raylib.MeasureText(hitPointsText, fontSize);
        Raylib.DrawText(
            hitPointsText,
            (int)(Position.X - textWidth / 2f),
            (int)(Position.Y - fontSize / 2f),
            fontSize,
            Color.Black);
    }
}

#pragma warning disable CS8981
class blobble : blomeba {
    const float SizeDuration = 7f;
    readonly float originalRadius;
    float sizeRemaining;

    public blobble(Vector2 position, Vector2 velocity, float radius, Color color, int hitPoints = 5, int teamId = 0)
        : base(position, velocity, radius, color, hitPoints, teamId) {
        originalRadius = radius;
    }

    public float OriginalRadius => originalRadius;
    public bool IsSizeBoosted => sizeRemaining > 0f;
    public float SizeBoostRemaining => sizeRemaining;

    public void ActivateSizeBoost() {
        if (IsSizeBoosted) {
            return;
        }

        Radius = originalRadius * 2f;
        DoubleHealth();
        sizeRemaining = SizeDuration;
    }

    public override void Update(float deltaTime, Rectangle box) {
        base.Update(deltaTime, box);

        if (sizeRemaining <= 0f) {
            return;
        }

        sizeRemaining = MathF.Max(0f, sizeRemaining - deltaTime);
        if (sizeRemaining == 0f) {
            Radius = originalRadius;
            HalveHealth();
        }
    }
}
#pragma warning restore CS8981
