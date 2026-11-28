using System.Numerics;
using Raylib_cs;

class GameProgram
{
    static readonly Random collisionRandom = new();

    static void Main()
    {
        const int screenWidth = 800;
        const int screenHeight = 600;

        Raylib.InitWindow(screenWidth, screenHeight, "Auto Battler");
        Raylib.SetTargetFPS(60);

        const float radius = 20;

        // The "Arena" of the two balls
        Rectangle box = new Rectangle(
            (screenWidth - 300) / 2,
            (screenHeight - 300) / 2,
            300,
            300);

        Vector2 player1InitialVelocity = new Vector2(180, 150);
        blomeba player1 = new blomeba(
            new Vector2(box.X + 50, box.Y + 50),
            player1InitialVelocity,
            radius,
            Color.Red,
            100,
            1);
        Vector2 player2InitialVelocity = new Vector2(-180, -151);
        blobble player2 = new blobble(
            new Vector2(box.X + 150, box.Y + 50),
            player2InitialVelocity,
            radius,
            Color.Blue,
            100,
            2);
        List<blomeba> balls = new()
        {
            player1,
            player2
        };
        const float qCooldownDuration = 15f;
        float qCooldownRemaining = 0f;
        float player2QCooldownRemaining = 0f;
        const float wCooldownDuration = 30f;
        float wCooldownRemaining = 0f;
        bool isPaused = false;

        while (!Raylib.WindowShouldClose())
        {
            if (Raylib.IsKeyPressed(KeyboardKey.Space))
            {
                isPaused = !isPaused;
            }

            float deltaTime = isPaused ? 0f : Raylib.GetFrameTime();
            qCooldownRemaining = MathF.Max(0f, qCooldownRemaining - deltaTime);
            player2QCooldownRemaining = MathF.Max(0f, player2QCooldownRemaining - deltaTime);
            wCooldownRemaining = MathF.Max(0f, wCooldownRemaining - deltaTime);

            if (!isPaused
                && balls.Contains(player1)
                && player1.HitPoints > 0
                && Raylib.IsKeyPressed(KeyboardKey.Q)
                && qCooldownRemaining <= 0f)
            {
                player1.TakeDamage(25);
                blomeba[] alliedMinis = balls
                    .Where(ball => ball != player1 && ball.TeamId == player1.TeamId)
                    .ToArray();

                foreach (blomeba mini in alliedMinis)
                {
                    Vector2 direction = mini.Velocity.LengthSquared() > 0
                        ? Vector2.Normalize(mini.Velocity)
                        : Vector2.UnitX;
                    balls.Add(new blomeba(
                        mini.Position + direction * (mini.Radius * 2 + 2),
                        mini.Velocity,
                        mini.Radius,
                        mini.Color,
                        5,
                        teamId: mini.TeamId));
                }

                qCooldownRemaining = qCooldownDuration;
            }

            if (!isPaused && Raylib.IsKeyPressed(KeyboardKey.P) && player2QCooldownRemaining <= 0f)
            {
                const int cloneCount = 5;
                float spawnedRadius = player2.OriginalRadius * 0.6f;
                Vector2 baseVelocity = player2.Velocity;

                for (int i = 0; i < cloneCount; i++)
                {
                    float angle = i * MathF.Tau / cloneCount;
                    Vector2 velocity = Rotate(baseVelocity, angle);
                    Vector2 direction = Vector2.Normalize(velocity);
                    balls.Add(new blomeba(
                        player2.Position + direction * (player2.Radius + spawnedRadius + 2),
                        velocity,
                        spawnedRadius,
                        player2.Color,
                        1,
                        teamId: player2.TeamId));
                }

                player2QCooldownRemaining = qCooldownDuration;
            }

            if (!isPaused && Raylib.IsKeyPressed(KeyboardKey.W) && wCooldownRemaining <= 0f)
            {
                float spawnedRadius = player1.Radius * 0.6f;
                Vector2 baseVelocity = player1.Velocity;

                for (int i = 0; i < 3; i++)
                {
                    float angle = i * MathF.Tau / 3;
                    Vector2 velocity = Rotate(baseVelocity, angle);
                    Vector2 direction = Vector2.Normalize(velocity);
                    balls.Add(new blomeba(
                        player1.Position + direction * (player1.Radius + spawnedRadius + 2),
                        velocity,
                        spawnedRadius,
                        player1.Color,
                        teamId: player1.TeamId));
                }

                wCooldownRemaining = wCooldownDuration;
            }

            if (!isPaused && Raylib.IsKeyPressed(KeyboardKey.O))
            {
                player2.ActivateSizeBoost();
            }

            if (!isPaused)
            {
                foreach (blomeba ball in balls)
                {
                    ball.Update(deltaTime, box);
                }

                for (int i = 0; i < balls.Count; i++)
                {
                    for (int j = i + 1; j < balls.Count; j++)
                    {
                        ResolveCollision(balls[i], balls[j]);
                    }
                }

                balls.RemoveAll(ball => ball.HitPoints <= 0);

                player1.Velocity = WithSpeed(player1.Velocity, player1InitialVelocity.Length());
                player2.Velocity = WithSpeed(player2.Velocity, player2InitialVelocity.Length());
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            Raylib.DrawRectangleLinesEx(box, 2, Color.White);
            string qStatus = qCooldownRemaining <= 0f
                ? "Q: Ready"
                : $"Q: {MathF.Ceiling(qCooldownRemaining)}s";
            Raylib.DrawText(qStatus, 20, 20, 20, Color.White);
            string eStatus = wCooldownRemaining <= 0f
                ? "W: Ready"
                : $"W: {MathF.Ceiling(wCooldownRemaining)}s";
            Raylib.DrawText(eStatus, 20, 45, 20, Color.White);
            string player2QStatus = player2QCooldownRemaining <= 0f
                ? "Player 2 P: Ready"
                : $"Player 2 P: {MathF.Ceiling(player2QCooldownRemaining)}s";
            Raylib.DrawText(player2QStatus, 20, 70, 20, Color.White);
            string player2OStatus = player2.IsSizeBoosted
                ? $"Player 2 O: {MathF.Ceiling(player2.SizeBoostRemaining)}s"
                : "Player 2 O: Ready";
            Raylib.DrawText(player2OStatus, 20, 95, 20, Color.White);
            Raylib.DrawText(
                isPaused ? "PAUSED - SPACE TO RESUME" : "SPACE: PAUSE",
                20,
                120,
                20,
                Color.White);

            foreach (blomeba ball in balls)
            {
                ball.Draw();
            }

            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }

    static void ResolveCollision(blomeba first, blomeba second)
    {
        Vector2 delta = second.Position - first.Position;
        float minDistance = first.Radius + second.Radius;
        float distanceSquared = delta.LengthSquared();

        if (distanceSquared >= minDistance * minDistance)
        {
            return;
        }

        Vector2 normal;
        float distance;
        if (distanceSquared > 0)
        {
            distance = MathF.Sqrt(distanceSquared);
            normal = delta / distance;
        }
        else
        {
            normal = Vector2.UnitX;
            distance = 0;
        }

        float firstMass = first.Radius * first.Radius;
        float secondMass = second.Radius * second.Radius;
        float inverseMassSum = 1 / firstMass + 1 / secondMass;
        float overlap = minDistance - distance;

        first.Position -= normal * (overlap * (1 / firstMass) / inverseMassSum);
        second.Position += normal * (overlap * (1 / secondMass) / inverseMassSum);

        Vector2 relativeVelocity = second.Velocity - first.Velocity;
        float speedAlongNormal = Vector2.Dot(relativeVelocity, normal);
        if (speedAlongNormal >= 0)
        {
            return;
        }

        if (first.TeamId != second.TeamId)
        {
            int collisionDamage = collisionRandom.Next(3, 6);
            first.TakeDamage(collisionDamage);
            second.TakeDamage(collisionDamage);
        }

        float impulse = -2 * speedAlongNormal / inverseMassSum;
        first.Velocity -= impulse * (1 / firstMass) * normal;
        second.Velocity += impulse * (1 / secondMass) * normal;
    }

    static Vector2 WithSpeed(Vector2 velocity, float speed)
    {
        return velocity.LengthSquared() > 0
            ? Vector2.Normalize(velocity) * speed
            : new Vector2(speed, 0);
    }

    static Vector2 Rotate(Vector2 vector, float angle)
    {
        float cosine = MathF.Cos(angle);
        float sine = MathF.Sin(angle);
        return new Vector2(
            vector.X * cosine - vector.Y * sine,
            vector.X * sine + vector.Y * cosine);
    }
}
