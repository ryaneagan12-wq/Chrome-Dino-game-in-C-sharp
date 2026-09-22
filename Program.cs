using System;
using Raylib_cs;

namespace Chrome_Dino
{
    class ChromeDino
    {
        static void Main(string[] args)
        {
            Raylib.InitWindow(1920, 1100, "Chrome dino game");
            Raylib.SetTargetFPS(60);
            Texture2D Background_img = Raylib.LoadTexture("C:\\Users\\ryan\\OneDrive\\Documents\\C#\\Chrome Dino Game\\Asset Files\\Backgroundfloor.png");
            Texture2D dino_img = Raylib.LoadTexture("C:\\Users\\ryan\\OneDrive\\Documents\\C#\\Chrome Dino Game\\Asset Files\\Dino.png");
            Texture2D dinoduck_img = Raylib.LoadTexture("C:\\Users\\ryan\\OneDrive\\Documents\\C#\\Chrome Dino Game\\Asset Files\\DinoDuck.png");
            Texture2D dinodeath_img = Raylib.LoadTexture("C:\\Users\\ryan\\OneDrive\\Documents\\C#\\Chrome Dino Game\\Asset Files\\DinoDeath.png");
            Texture2D cactus_img = Raylib.LoadTexture("C:\\Users\\ryan\\OneDrive\\Documents\\C#\\Chrome Dino Game\\Asset Files\\cactus.png");
            Texture2D ptero_img = Raylib.LoadTexture("C:\\Users\\ryan\\OneDrive\\Documents\\C#\\Chrome Dino Game\\Asset Files\\ptero.png");
            Texture2D gameover_img = Raylib.LoadTexture("C:\\Users\\ryan\\OneDrive\\Documents\\C#\\Chrome Dino Game\\Asset Files\\Gameover.png");


            int player_width = 67;
            int player_height = 72;
            int player_x = 0;
            int player_y = 1000;

            int player2_height = 47;

            int playerdeath_x = 0;
            int playerdeath_y = 1000;


            int enemy_width = 26;
            int enemy_height = 50;
            int enemy_x = 850;
            int enemy_y = 1000;
            decimal enemy_speed = 10.0m;


            int enemy2_width = 76;
            int enemy2_x = 1920;
            int enemy2_y = 850;
            decimal enemy2_speed = 10.0m;

            int gameover_width = 257;
            int gameover_height = 72;
            int gameover_x = 840;
            int gameover_y = 550;
            Rectangle gameover_rect = new Rectangle(gameover_x, gameover_y, gameover_width, gameover_height);

            Color Grey = new Color(32, 32, 36);
            Color Nardo = new Color(162, 164, 168);

            int dino_gravity = 0;
            bool game_over = false;
            bool duck = false;
            double start_ticks = Raylib.GetTime();
            double current_score = 0;



            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Grey);

                if (!game_over)
                {
                    if ((Raylib.IsKeyDown(KeyboardKey.Up) || Raylib.IsKeyDown(KeyboardKey.Space)) && player_y >= 1000)
                    {
                        dino_gravity = -20;
                    }
                    duck = Raylib.IsKeyDown(KeyboardKey.Down) && player_y >= 1000;
                }
                Raylib.DrawTexture(Background_img, 0, 900, Color.White);
                Raylib.DrawTexture(cactus_img, enemy_x, enemy_y, Color.White);
                Raylib.DrawTexture(ptero_img, enemy2_x, enemy2_y, Color.White);
                if (!game_over)
                {
                    enemy_x -= (int)enemy_speed;
                    if (enemy_x < -enemy_width)
                    {
                        enemy_x = Raylib.GetRandomValue(1920, 1920 + 300);
                    }
                    enemy_speed += 0.0001m;
                    current_score = (Raylib.GetTime() - start_ticks) / 10;

                    if (current_score >= 450)
                    {
                        enemy2_x -= (int)enemy2_speed;
                        if (enemy2_x < enemy2_width)
                        {
                            enemy2_x = Raylib.GetRandomValue(1920, 1920 + 300);
                            enemy2_y = Raylib.GetRandomValue(600, 750);
                        }
                        enemy2_speed += 0.0001m;
                    }
                }
                Raylib.DrawTexture(cactus_img, enemy_x, enemy_y, Color.White);
                Raylib.DrawTexture(ptero_img, enemy2_x, enemy2_y, Color.White);
                if (player_x < enemy_x + enemy_width && player_x + player_width > enemy_x && player_y < enemy_y + enemy_height && player_y + player_height > enemy_y)
                {
                    Raylib.DrawTexture(dinodeath_img, playerdeath_x, playerdeath_y, Color.White);
                    game_over = true;
                }
                dino_gravity += 1;
                player_y += dino_gravity;
                if (player_y >= 1000)
                {
                    player_y = 1000;
                    dino_gravity = 0;
                }
                if (duck)
                {
                    int duck_y = player_y + (player_height - player2_height);
                    Raylib.DrawTexture(dinoduck_img, player_x, duck_y, Color.White);
                }
                else
                {
                    Raylib.DrawTexture(dino_img, player_x, player_y, Color.White);
                }
                if (!game_over)
                {
                    Raylib.DrawText($"{current_score:F0}", 1880, 0, 30, Nardo);
                    Raylib.DrawText($"HI: {current_score:F0}", 1780, 0, 30, Nardo);
                }
                else
                {
                    Raylib.DrawText($"{current_score:F0}", 1880, 0, 30, Nardo);
                    Raylib.DrawText($"HI: {current_score:F0} ", 1780, 0, 30, Nardo);
                    Raylib.DrawTexture(dinodeath_img, playerdeath_x, playerdeath_y, Color.White);
                    Raylib.DrawTexture(gameover_img, gameover_x, gameover_y, Color.White);
                }

                if (Raylib.IsMouseButtonPressed(0))
                {
                    if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), gameover_rect))
                    {
                        player_x = 0;
                        player_y = 1000;
                        playerdeath_x = 0;
                        playerdeath_y = 1000;
                        enemy_x = 850;
                        enemy_y = 1000;
                        enemy2_x = 1920;
                        enemy2_y = 850;
                        start_ticks = Raylib.GetTime();
                        current_score = 0;
                        game_over = false;
                    }
                }

                Raylib.EndDrawing();
            }
        }
    }
}

