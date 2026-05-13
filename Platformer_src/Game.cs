using Raylib_cs;

class Game
{
    Player player;
    TileMap tileMap;

    public Game()
    {
        Raylib.InitWindow(1280, 720, "Platformer");
        Raylib.SetTargetFPS(60);
        player = new Player();
        tileMap = new TileMap();
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
        player.Move(); 
    }

    void Draw()
    {
        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.Black);

        player.Draw();

        tileMap.Draw();

        Raylib.EndDrawing();

    }
}