using System.Runtime.CompilerServices;
using Raylib_cs;

class TileMap
{

    int TILE_SIZE = 64;
    public int[,] level = 
    {
        {0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0},
        {1, 1, 1, 1, 1, 1, 1, 1}};
    
    public void Draw()
    {
        for (int y = 0; y < level.GetLength(0); y++)
        {
            for (int x = 0; x < level.GetLength(1); x++)
            {
                if (level[y, x] == 1)
                {
                    Raylib.DrawRectangle(x * TILE_SIZE, y * TILE_SIZE, TILE_SIZE, TILE_SIZE, Color.DarkBlue);
                }
            }
        }
    }
}