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
    public int[,] Tiles;
    public int[,] collisionTiles;
    Texture2D tileSet;
    public int TILE_SIZE = 16;

    public TileMap()
    {
        string json = File.ReadAllText("../Assets/Levels/level1.tmj");

        tileSet = Raylib.LoadTexture("../Assets/world_tileset.png");

        Map map = JsonSerializer.Deserialize<Map>(json) ?? throw new Exception("Failed to Deserialize json");

        Tiles = new int[map.height, map.width];

        int[] rawData = map.layers[0].data;

        for (int y = 0; y < map.height; y++)
        {
            for (int x = 0; x < map.width; x++)
            {
                Tiles[y, x] =
                    rawData[y * map.width + x];
            }
        }

        collisionTiles = new int[map.height, map.width];

        int[] rawCollision =
            map.layers[1].data;

        for (int y = 0; y < map.height; y++)
        {
            for (int x = 0; x < map.width; x++)
            {
                collisionTiles[y, x] =
                    rawCollision[y * map.width + x];
            }
        }
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
}