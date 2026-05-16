using System.Numerics;
using Raylib_cs;
using System.Text.Json;

public class Layer
{
    public string name { get; set; } = "";
    public int[] data { get; set; } = [];
}

public class Map
{
    public int width { get; set; } 
    public int height { get; set; }

    public List<Layer> layers { get; set; } = [];
}


class TileMap
{
    public int[,] Tiles = new int[0, 0];
    public int[,] collisionTiles = new int[0, 0];
    public int[,] backgroundTiles = new int[0, 0];
    Texture2D tileSet;
    Texture2D door;
    public int TILE_SIZE = 16;

    public void LoadMap(int level)
    {
        int LEVEL = level;

        string json = File.ReadAllText("../Assets/Levels/level"+ LEVEL +".tmj");

        tileSet = Raylib.LoadTexture("../Assets/world_tileset.png");

        Map map = JsonSerializer.Deserialize<Map>(json) ?? throw new Exception("Failed to Deserialize json");

        Tiles = new int[map.height, map.width] ?? throw new Exception("Fail");

        int[] rawData = map.layers[1].data;

        for (int y = 0; y < map.height; y++)
        {
            for (int x = 0; x < map.width; x++)
            {
                Tiles[y, x] =
                    rawData[y * map.width + x];
            }
        }

        backgroundTiles = new int[map.height, map.width];

        int[] rawBackground = map.layers[0].data;

        for (int y = 0; y < map.height; y++)
        {
            for (int x = 0; x < map.width; x++)
            {
                backgroundTiles[y, x] = rawBackground[y * map.width + x];
            }
        }

        collisionTiles = new int[map.height, map.width];

        int[] rawCollision =
            map.layers[2].data;

        for (int y = 0; y < map.height; y++)
        {
            for (int x = 0; x < map.width; x++)
            {
                collisionTiles[y, x] =
                    rawCollision[y * map.width + x];
            }
        }
    }

    public bool IsLevelComplete(Player player)
    {
        int playerTileX = (int)(player.pos.X / TILE_SIZE);
        int playerTileY = (int)(player.pos.Y / TILE_SIZE);

        return collisionTiles[playerTileY, playerTileX] == 2;
    } 

    public void Draw()
    {
        int columns = tileSet.Width / TILE_SIZE;

        for (int y = 0; y < Tiles.GetLength(0); y++)
        {
            for (int x = 0; x < Tiles.GetLength(1); x++)
            {
                int tileId = Tiles[y, x];

                // 0 means empty
                if (tileId == 0)
                    continue;
                
                // Tiled starts counting from 1
                tileId--;

                int tileX =
                    (tileId % columns) * TILE_SIZE;

                int tileY =
                    (tileId / columns) * TILE_SIZE;

                Rectangle source = new Rectangle(
                    tileX,
                    tileY,
                    TILE_SIZE,
                    TILE_SIZE
                );

                Vector2 position = new Vector2(
                    x * TILE_SIZE,
                    y * TILE_SIZE
                );

                Raylib.DrawTextureRec(
                    tileSet,
                    source,
                    position,
                    Color.White
                );
            }
        }
    }

    public void BackgroundDraw()
    {
        int columns = tileSet.Width / TILE_SIZE;

        for (int y = 0; y < backgroundTiles.GetLength(0); y++)
        {
            for (int x = 0; x < backgroundTiles.GetLength(1); x++)
            {
                int tileId = backgroundTiles[y, x];

                // 0 means empty
                if (tileId == 0)
                    continue;

                // Tiled starts counting from 1
                tileId--;

                int tileX =
                    (tileId % columns) * TILE_SIZE;

                int tileY =
                    (tileId / columns) * TILE_SIZE;

                Rectangle source = new Rectangle(
                    tileX,
                    tileY,
                    TILE_SIZE,
                    TILE_SIZE
                );

                Vector2 position = new Vector2(
                    x * TILE_SIZE,
                    y * TILE_SIZE
                );

                Raylib.DrawTextureRec(
                    tileSet,
                    source,
                    position,
                    Color.White
                );
            }
        }
    }
}