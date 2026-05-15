using Raylib_cs;

class Game
{
    Player player;
    TileMap tileMap;

    public Game()
    {
        Raylib.InitWindow(1280, 720, "Platformer");
        Raylib.SetTargetFPS(60);
        tileMap = new TileMap();
        player = new Player("Aswin");
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
        player.Update(player, tileMap); 
    }

    void Draw()
    {
        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.Black);

        tileMap.BackgroundDraw();

        tileMap.Draw();

        player.Draw();

        Raylib.EndDrawing();

    }
}