using System.Numerics;
using Raylib_cs;

class GameProgram {
    public static void Main() {
        const int screenWidth = 800;
        const int screenHeight = 600;

        Raylib.InitWindow(screenWidth, screenHeight, "Auto Battlerslop");
        Raylib.SetTargetFPS(60);

        float radius = 20;

        // The "Arena" of the two balls
        Rectangle box = new Rectangle(300, 250, 300, 300);

        Vector2 ballP1 = new Vector2(350, 200);
        Vector2 ballP2 = new Vector2(450, 200);

        Vector2 ballVelocityP1 = new Vector2(180, 150);
        Vector2 ballVelocityP2 = new Vector2(-180, -151);

        while (!Raylib.WindowShouldClose()) {
            float deltaTime = Raylib.GetFrameTime();

            ballP1 += ballVelocityP1 * deltaTime;
            ballP2 += ballVelocityP2 * deltaTime;

            //Player 1 ball collisions
            if (ballP1.X - radius < box.X) {
                ballP1.X = box.X + radius;
                ballVelocityP1.X = MathF.Abs(ballVelocityP1.X);
            }

            if (ballP1.X + radius > box.X + box.Width) {
                ballP1.X = box.X + box.Width - radius;
                ballVelocityP1.X = -MathF.Abs(ballVelocityP1.X);
            }

            if (ballP1.Y - radius < box.Y) {
                ballP1.Y = box.Y + radius;
                ballVelocityP1.Y = MathF.Abs(ballVelocityP1.Y);
            }

            if (ballP1.Y + radius > box.Y + box.Height) {
                ballP1.Y = box.Y + box.Height - radius;
                ballVelocityP1.Y = -MathF.Abs(ballVelocityP1.Y);
            }

            //Player 2 ball collisions
            if (ballP2.X - radius < box.X) {
                ballP2.X = box.X + radius;
                ballVelocityP2.X = MathF.Abs(ballVelocityP2.X);
            }

            if (ballP2.X + radius > box.X + box.Width) {
                ballP2.X = box.X + box.Width - radius;
                ballVelocityP2.X = -MathF.Abs(ballVelocityP2.X);
            }

            if (ballP2.Y - radius < box.Y) {
                ballP2.Y = box.Y + radius;
                ballVelocityP2.Y = MathF.Abs(ballVelocityP2.Y);
            }

            if (ballP2.Y + radius > box.Y + box.Height) {
                ballP2.Y = box.Y + box.Height - radius;
                ballVelocityP2.Y = -MathF.Abs(ballVelocityP2.Y);
            }

            // ball collision detection
            Vector2 delta = ballP2 - ballP1;
            float distanceSquared = delta.LengthSquared();
            float minDistance = radius * 2;

            if (distanceSquared < minDistance * minDistance && distanceSquared > 0) {
                float distance = MathF.Sqrt(distanceSquared);
                Vector2 normal = delta / distance;
                float overlap = minDistance - distance;
                ballP1 -= normal * (overlap / 2);
                ballP2 += normal * (overlap / 2);

                Vector2 relativeVelocity = ballVelocityP2 - ballVelocityP1;
                float speedAlongNormal = Vector2.Dot(relativeVelocity, normal);

                if (speedAlongNormal < 0) {
                    ballVelocityP1 += normal * speedAlongNormal;
                    ballVelocityP2 -= normal * speedAlongNormal;
                }

            }
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            Raylib.DrawRectangleLinesEx(box, 2, Color.White);

            Raylib.DrawCircleV(ballP1, radius, Color.Red);
            Raylib.DrawCircleV(ballP2, radius, Color.Blue);

            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }
}
