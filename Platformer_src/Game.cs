using Raylib_cs;

class Game
{
    Player player;

    public Game()
    {
        Raylib.InitWindow(1280, 720, "Platformer");
        Raylib.SetTargetFPS(60);
        player = new Player();
    }

    public void Run()
    {
        while (!Raylib.WindowShouldClose())
        {
            Update();
            Draw();
        }

        Raylib.CloseWindow();
    }

    void Update()
    {
        
    }

    void Draw()
    {
        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.Black);

        player.Draw();

        Raylib.EndDrawing();

    }
}