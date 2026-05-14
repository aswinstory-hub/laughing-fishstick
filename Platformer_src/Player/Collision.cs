using System;
using Raylib_cs;

class Collision
{
    public Collision()
    {
        
    }

    public void CheckYCollision(Player player, TileMap tileMap)
    {
        Rectangle playerRect = new Rectangle((int)player.pos.X, (int)player.pos.Y, (float)player.SIZE, (float)player.SIZE);

        int TILE_SIZE = tileMap.TILE_SIZE;

        int left   = (int)(playerRect.X / tileMap.TILE_SIZE);
        int right  = (int)((playerRect.X + player.SIZE - 1) / tileMap.TILE_SIZE);

        int top    = (int)(playerRect.Y / tileMap.TILE_SIZE);
        int bottom = (int)((playerRect.Y + player.SIZE - 1) / tileMap.TILE_SIZE);

        player.isGrounded = false;

        for (int y = top; y <= bottom; y++)
        {
            for (int x = left; x <= right; x++)
            {
                if (tileMap.collisionTiles[y, x] == 1)
                {
                    Rectangle tileRect = new Rectangle(
                        x * TILE_SIZE,
                        y * TILE_SIZE,
                        TILE_SIZE,
                        TILE_SIZE
                    );

                    if (Raylib.CheckCollisionRecs(playerRect, tileRect))
                    {
                        // falling down
                        if (player.velocity.Y > 0)
                        {
                            player.pos.Y = tileRect.Y - playerRect.Height;

                            player.velocity.Y = 0;

                            player.isGrounded = true;
                        }

                        // hitting ceiling
                        else if (player.velocity.Y < 0)
                        {
                            player.pos.Y = tileRect.Y;

                            player.velocity.Y = 0;
                        }
                    }
                }
            }
        }
    }
}