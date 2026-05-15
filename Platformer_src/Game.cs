using Raylib_cs;

class Game
{
    Player player;
    TileMap tileMap;
    GameCamera gameCamera;

    public Game()
    {
        Raylib.InitWindow(1280, 720, "Platformer");
        Raylib.SetTargetFPS(60);
        tileMap = new TileMap();
        player = new Player("Aswin");
        gameCamera = new GameCamera(1280f, 720f, 1280f, 400f);
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
        gameCamera.Update(player.pos);
    }

    void Draw()
    {
        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.Black);

        gameCamera.Begin();

        tileMap.BackgroundDraw();

        tileMap.Draw();

        player.Draw();

        gameCamera.End();

        Raylib.EndDrawing();

    }
}