using Raylib_cs;
using System.Numerics;

class Game
{
    Player player;
    TileMap tileMap;
    GameCamera gameCamera;
    int currentLevel = 1;

    public Game()
    {
        Raylib.InitWindow(1280, 720, "Platformer");
        Raylib.SetTargetFPS(60);
        tileMap = new TileMap();
        player = new Player("Aswin");
        gameCamera = new GameCamera(1280f, 720f, 1280f, 400f);
        tileMap.LoadMap(currentLevel);
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

        if (tileMap.IsLevelComplete(player))
        {
            NextLevel();
        }

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

    void NextLevel()
    {
        currentLevel ++;

        tileMap.LoadMap(currentLevel);

        player.pos = new Vector2(96, 96);
        player.velocity = Vector2.Zero;
    }
}